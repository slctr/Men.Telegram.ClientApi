using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Men.Telegram.ClientApi.Core.Sessions;
using Men.Telegram.ClientApi.Models;
using TeleSharp.TL;
using TeleSharp.TL.Account;
using TeleSharp.TL.Auth;
using TeleSharp.TL.Contacts;
using TeleSharp.TL.Help;
using TeleSharp.TL.Messages;
using TeleSharp.TL.Upload;
using TLSharp.Core;
using TLSharp.Core.Auth;
using TLSharp.Core.Exceptions;
using TLSharp.Core.MTProto.Crypto;
using TLSharp.Core.Network;
using TLSharp.Core.Network.Exceptions;
using TLSharp.Core.Utils;
using TLAuthorization = TeleSharp.TL.Auth.TLAuthorization;

namespace Men.Telegram.ClientApi
{
    public class TelegramClient : IDisposable
    {
        public TelegramAuthModel AuthModel { get; protected set; }

        public TelegramSettings Settings { get; protected set; }


        protected readonly TelegramSession _session;


        private MtProtoSender sender;
        private TcpTransport transport;
        private string apiHash = string.Empty;
        private int apiId = 0;
        //private Session session;
        private List<TLDcOption> dcOptions;
        private TcpClientConnectionHandler handler;
        private DataCenterIPVersion dcIpVersion;

        //public Session Session
        //{
        //    get { return this.session; }
        //}


        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="authModel">Telegram authenthication model</param>
        /// <param name="settings">Telegram settings for client</param>
        public TelegramClient(
            TelegramSettings settings = null)
        {
            settings ??= new TelegramSettings();
            settings.SessionStore ??= new FileSessionStore();
            settings.SessionId ??= "session";

            //settings.Handler ??= null;
            //settings.DcIpVersion = DataCenterIPVersion.Default;

            //this.apiHash = apiHash;
            //this.apiId = apiId;
            //this.handler = handler;
            //this.dcIpVersion = dcIpVersion;

            this._session = TelegramSession.TryLoadOrCreateNew(settings.SessionStore, settings.SessionId);
            this.transport = new TcpTransport(
                this._session.DataCenter.Address,
                this._session.DataCenter.Port,
                this.handler);
        }


        /// <summary>
        /// Authenticate to Telegram
        /// </summary>
        /// <param name="reconnect"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task AuthenticateAsync(
            TelegramAuthModel authModel,
            bool reconnect = false,
            CancellationToken token = default)
        {
            if (authModel == null
                || authModel.ApiId <= 0
                || string.IsNullOrWhiteSpace(authModel.ApiHash))
            {
                throw new ArgumentException($"Argument {nameof(authModel)} can't be null or contains empty properties. See https://core.telegram.org/api/obtaining_api_id");
            }

            token.ThrowIfCancellationRequested();

            if (this._session.AuthKey == null || reconnect)
            {
                Step3_Response result = await Authenticator.DoAuthentication(this.transport, token);
               
                this._session.AuthKey = result.AuthKey;
                this._session.TimeOffset = result.TimeOffset;
            }

            this.sender = new MtProtoSender(this.transport, this._session);

            TLRequestGetConfig config = new TLRequestGetConfig();
            TLRequestInitConnection request = new TLRequestInitConnection()
            {
                ApiId = apiId,
                AppVersion = "1.0.0",
                DeviceModel = "PC",
                LangCode = "en",
                Query = config,
                SystemVersion = "Win 10.0"
            };
            TLRequestInvokeWithLayer invokewithLayer = new TLRequestInvokeWithLayer()
            {
                Layer = 66,
                Query = request
            };
            await this.sender.Send(invokewithLayer, token);
            await this.sender.Receive(invokewithLayer, token);

            this.dcOptions = ((TLConfig)invokewithLayer.Response).DcOptions.ToList();
        }

        private async Task ReconnectToDcAsync(int dcId, CancellationToken token = default)
        {
            token.ThrowIfCancellationRequested();

            if (this.dcOptions == null || !this.dcOptions.Any())
            {
                throw new InvalidOperationException($"Can't reconnect. Establish initial connection first.");
            }

            TLExportedAuthorization exported = null;
            if (this._session.TLUser != null)
            {
                TLRequestExportAuthorization exportAuthorization = new TLRequestExportAuthorization() { DcId = dcId };
                exported = await this.SendRequestAsync<TLExportedAuthorization>(exportAuthorization, token).ConfigureAwait(false);
            }

            IEnumerable<TLDcOption> dcs;
            if (this.dcIpVersion == DataCenterIPVersion.OnlyIPv6)
            {
                dcs = this.dcOptions.Where(d => d.Id == dcId && d.Ipv6); // selects only ipv6 addresses 	
            }
            else if (this.dcIpVersion == DataCenterIPVersion.OnlyIPv4)
            {
                dcs = this.dcOptions.Where(d => d.Id == dcId && !d.Ipv6); // selects only ipv4 addresses
            }
            else
            {
                dcs = this.dcOptions.Where(d => d.Id == dcId); // any
            }

            dcs = dcs.Where(d => !d.MediaOnly);

            TLDcOption dc;
            if (this.dcIpVersion != DataCenterIPVersion.Default)
            {
                if (!dcs.Any())
                {
                    throw new Exception($"Telegram server didn't provide us with any IPAddress that matches your preferences. If you chose OnlyIPvX, try switch to PreferIPvX instead.");
                }

                dcs = dcs.OrderBy(d => d.Ipv6);
                dc = this.dcIpVersion == DataCenterIPVersion.PreferIPv4 ? dcs.First() : dcs.Last(); // ipv4 addresses are at the beginning of the list because it was ordered
            }
            else
            {
                dc = dcs.First();
            }

            DataCenter dataCenter = new DataCenter(dcId, dc.IpAddress, dc.Port);

            this.transport = new TcpTransport(dc.IpAddress, dc.Port, this.handler);
            this._session.DataCenter = dataCenter;

            //await this.AuthenticateAsync(true, token).ConfigureAwait(false);

            if (this._session.TLUser != null)
            {
                TLRequestImportAuthorization importAuthorization = new TLRequestImportAuthorization() { Id = exported.Id, Bytes = exported.Bytes };
                TLAuthorization imported = await this.SendRequestAsync<TLAuthorization>(importAuthorization, token).ConfigureAwait(false);
                this.OnUserAuthenticated((TLUser)imported.User);
            }
        }

        private async Task RequestWithDcMigration(TLMethod request, CancellationToken token = default)
        {
            if (this.sender == null)
            {
                throw new InvalidOperationException("Not connected!");
            }

            bool completed = false;
            while (!completed)
            {
                try
                {
                    await this.sender.Send(request, token).ConfigureAwait(false);
                    await this.sender.Receive(request, token).ConfigureAwait(false);
                    completed = true;
                }
                catch (DataCenterMigrationException e)
                {
                    if (this._session.DataCenter.DataCenterId.HasValue &&
                        this._session.DataCenter.DataCenterId.Value == e.DC)
                    {
                        throw new Exception($"Telegram server replied requesting a migration to DataCenter {e.DC} when this connection was already using this DataCenter", e);
                    }

                    await this.ReconnectToDcAsync(e.DC, token).ConfigureAwait(false);
                    // prepare the request for another try
                    request.ConfirmReceived = false;
                }
            }
        }

        public bool IsUserAuthorized()
        {
            return this._session.TLUser != null;
        }

        public async Task<bool> IsPhoneRegisteredAsync(string phoneNumber, CancellationToken token = default)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                throw new ArgumentNullException(nameof(phoneNumber));
            }

            TLRequestCheckPhone authCheckPhoneRequest = new TLRequestCheckPhone() { PhoneNumber = phoneNumber };

            await this.RequestWithDcMigration(authCheckPhoneRequest, token).ConfigureAwait(false);

            return authCheckPhoneRequest.Response.PhoneRegistered;
        }

        public async Task<string> SendCodeRequestAsync(string phoneNumber, CancellationToken token = default)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                throw new ArgumentNullException(nameof(phoneNumber));
            }

            TLRequestSendCode request = new TLRequestSendCode() { PhoneNumber = phoneNumber, ApiId = apiId, ApiHash = apiHash };

            await this.RequestWithDcMigration(request, token).ConfigureAwait(false);

            return request.Response.PhoneCodeHash;
        }

        public async Task<TLUser> MakeAuthAsync(string phoneNumber, string phoneCodeHash, string code, CancellationToken token = default)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                throw new ArgumentNullException(nameof(phoneNumber));
            }

            if (string.IsNullOrWhiteSpace(phoneCodeHash))
            {
                throw new ArgumentNullException(nameof(phoneCodeHash));
            }

            if (string.IsNullOrWhiteSpace(code))
            {
                throw new ArgumentNullException(nameof(code));
            }

            TLRequestSignIn request = new TLRequestSignIn() { PhoneNumber = phoneNumber, PhoneCodeHash = phoneCodeHash, PhoneCode = code };

            await this.RequestWithDcMigration(request, token).ConfigureAwait(false);

            this.OnUserAuthenticated((TLUser)request.Response.User);

            return (TLUser)request.Response.User;
        }

        public async Task<TLPassword> GetPasswordSetting(CancellationToken token = default)
        {
            TLRequestGetPassword request = new TLRequestGetPassword();

            await this.RequestWithDcMigration(request, token).ConfigureAwait(false);

            return (TLPassword)request.Response;
        }

        public async Task<TLUser> MakeAuthWithPasswordAsync(TLPassword password, string password_str, CancellationToken token = default)
        {
            token.ThrowIfCancellationRequested();

            byte[] password_Bytes = Encoding.UTF8.GetBytes(password_str);
            IEnumerable<byte> rv = password.CurrentSalt.Concat(password_Bytes).Concat(password.CurrentSalt);

            SHA256Managed hashstring = new SHA256Managed();
            byte[] password_hash = hashstring.ComputeHash(rv.ToArray());

            TLRequestCheckPassword request = new TLRequestCheckPassword() { PasswordHash = password_hash };

            await this.RequestWithDcMigration(request, token).ConfigureAwait(false);

            this.OnUserAuthenticated((TLUser)request.Response.User);

            return (TLUser)request.Response.User;
        }

        public async Task<TLUser> SignUpAsync(string phoneNumber, string phoneCodeHash, string code, string firstName, string lastName, CancellationToken token = default)
        {
            TLRequestSignUp request = new TLRequestSignUp() { PhoneNumber = phoneNumber, PhoneCode = code, PhoneCodeHash = phoneCodeHash, FirstName = firstName, LastName = lastName };

            await this.RequestWithDcMigration(request, token).ConfigureAwait(false);

            this.OnUserAuthenticated((TLUser)request.Response.User);

            return (TLUser)request.Response.User;
        }

        public async Task<T> SendRequestAsync<T>(TLMethod methodToExecute, CancellationToken token = default)
        {
            await this.RequestWithDcMigration(methodToExecute, token).ConfigureAwait(false);

            object result = methodToExecute.GetType().GetProperty("Response").GetValue(methodToExecute);

            return (T)result;
        }

        internal async Task<T> SendAuthenticatedRequestAsync<T>(TLMethod methodToExecute, CancellationToken token = default)
        {
            if (!this.IsUserAuthorized())
            {
                throw new InvalidOperationException("Authorize user first!");
            }

            return await this.SendRequestAsync<T>(methodToExecute, token)
                .ConfigureAwait(false);
        }

        public async Task<TLUser> UpdateUsernameAsync(string username, CancellationToken token = default)
        {
            TLRequestUpdateUsername req = new TLRequestUpdateUsername { Username = username };

            return await this.SendAuthenticatedRequestAsync<TLUser>(req, token)
                .ConfigureAwait(false);
        }

        public async Task<bool> CheckUsernameAsync(string username, CancellationToken token = default)
        {
            TLRequestCheckUsername req = new TLRequestCheckUsername { Username = username };

            return await this.SendAuthenticatedRequestAsync<bool>(req, token)
                .ConfigureAwait(false);
        }

        public async Task<TLImportedContacts> ImportContactsAsync(IReadOnlyList<TLInputPhoneContact> contacts, CancellationToken token = default)
        {
            TLRequestImportContacts req = new TLRequestImportContacts { Contacts = new TLVector<TLInputPhoneContact>(contacts) };

            return await this.SendAuthenticatedRequestAsync<TLImportedContacts>(req, token)
                .ConfigureAwait(false);
        }

        public async Task<bool> DeleteContactsAsync(IReadOnlyList<TLAbsInputUser> users, CancellationToken token = default)
        {
            TLRequestDeleteContacts req = new TLRequestDeleteContacts { Id = new TLVector<TLAbsInputUser>(users) };

            return await this.SendAuthenticatedRequestAsync<bool>(req, token)
                .ConfigureAwait(false);
        }

        public async Task<TLLink> DeleteContactAsync(TLAbsInputUser user, CancellationToken token = default)
        {
            TLRequestDeleteContact req = new TLRequestDeleteContact { Id = user };

            return await this.SendAuthenticatedRequestAsync<TLLink>(req, token)
                .ConfigureAwait(false);
        }

        public async Task<TLContacts> GetContactsAsync(CancellationToken token = default)
        {
            TLRequestGetContacts req = new TLRequestGetContacts() { Hash = "" };

            return await this.SendAuthenticatedRequestAsync<TLContacts>(req, token)
                .ConfigureAwait(false);
        }

        public async Task<TLAbsUpdates> SendMessageAsync(TLAbsInputPeer peer, string message, CancellationToken token = default)
        {
            return await this.SendAuthenticatedRequestAsync<TLAbsUpdates>(
                    new TLRequestSendMessage()
                    {
                        Peer = peer,
                        Message = message,
                        RandomId = Helpers.GenerateRandomLong()
                    }, token)
                .ConfigureAwait(false);
        }

        public async Task<bool> SendTypingAsync(TLAbsInputPeer peer, CancellationToken token = default)
        {
            TLRequestSetTyping req = new TLRequestSetTyping()
            {
                Action = new TLSendMessageTypingAction(),
                Peer = peer
            };
            return await this.SendAuthenticatedRequestAsync<bool>(req, token)
                .ConfigureAwait(false);
        }

        public async Task<TLAbsDialogs> GetUserDialogsAsync(int offsetDate = 0, int offsetId = 0, TLAbsInputPeer offsetPeer = null, int limit = 100, CancellationToken token = default)
        {
            if (offsetPeer == null)
            {
                offsetPeer = new TLInputPeerSelf();
            }

            TLRequestGetDialogs req = new TLRequestGetDialogs()
            {
                OffsetDate = offsetDate,
                OffsetId = offsetId,
                OffsetPeer = offsetPeer,
                Limit = limit
            };
            return await this.SendAuthenticatedRequestAsync<TLAbsDialogs>(req, token)
                .ConfigureAwait(false);
        }

        public async Task<TLAbsUpdates> SendUploadedPhoto(TLAbsInputPeer peer, TLAbsInputFile file, string caption, CancellationToken token = default)
        {
            return await this.SendAuthenticatedRequestAsync<TLAbsUpdates>(new TLRequestSendMedia()
            {
                RandomId = Helpers.GenerateRandomLong(),
                Background = false,
                ClearDraft = false,
                Media = new TLInputMediaUploadedPhoto() { File = file, Caption = caption },
                Peer = peer
            }, token)
                .ConfigureAwait(false);
        }

        public async Task<TLAbsUpdates> SendUploadedDocument(
            TLAbsInputPeer peer, TLAbsInputFile file, string caption, string mimeType, TLVector<TLAbsDocumentAttribute> attributes, CancellationToken token = default)
        {
            return await this.SendAuthenticatedRequestAsync<TLAbsUpdates>(new TLRequestSendMedia()
            {
                RandomId = Helpers.GenerateRandomLong(),
                Background = false,
                ClearDraft = false,
                Media = new TLInputMediaUploadedDocument()
                {
                    File = file,
                    Caption = caption,
                    MimeType = mimeType,
                    Attributes = attributes
                },
                Peer = peer
            }, token)
                .ConfigureAwait(false);
        }

        public async Task<TLFile> GetFile(TLAbsInputFileLocation location, int filePartSize, int offset = 0, CancellationToken token = default)
        {
            TLFile result = await this.SendAuthenticatedRequestAsync<TLFile>(new TLRequestGetFile
            {
                Location = location,
                Limit = filePartSize,
                Offset = offset
            }, token)
                .ConfigureAwait(false);
            return result;
        }

        public async Task SendPingAsync(CancellationToken token = default)
        {
            await this.sender.SendPingAsync(token)
                .ConfigureAwait(false);
        }

        public async Task<TLAbsMessages> GetHistoryAsync(TLAbsInputPeer peer, int offsetId = 0, int offsetDate = 0, int addOffset = 0, int limit = 100, int maxId = 0, int minId = 0, CancellationToken token = default)
        {
            TLRequestGetHistory req = new TLRequestGetHistory()
            {
                Peer = peer,
                OffsetId = offsetId,
                OffsetDate = offsetDate,
                AddOffset = addOffset,
                Limit = limit,
                MaxId = maxId,
                MinId = minId
            };
            return await this.SendAuthenticatedRequestAsync<TLAbsMessages>(req, token)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Serch user or chat. API: contacts.search#11f812d8 q:string limit:int = contacts.Found;
        /// </summary>
        /// <param name="q">User or chat name</param>
        /// <param name="limit">Max result count</param>
        /// <returns></returns>
        public async Task<TLFound> SearchUserAsync(string q, int limit = 10, CancellationToken token = default)
        {
            TeleSharp.TL.Contacts.TLRequestSearch r = new TeleSharp.TL.Contacts.TLRequestSearch
            {
                Q = q,
                Limit = limit
            };

            return await this.SendAuthenticatedRequestAsync<TLFound>(r, token)
                .ConfigureAwait(false);
        }

        private void OnUserAuthenticated(TLUser TLUser)
        {
            this._session.TLUser = TLUser;
            this._session.SessionExpires = int.MaxValue;

            this._session.Save();
        }

        public bool IsConnected
        {
            get
            {
                if (this.transport == null)
                {
                    return false;
                }

                return this.transport.IsConnected;
            }
        }

        public void Dispose()
        {
            if (this.transport != null)
            {
                this.transport.Dispose();
                this.transport = null;
            }
        }
    }
}
