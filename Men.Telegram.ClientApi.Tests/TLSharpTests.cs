using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Men.Telegram.ClientApi;
using Men.Telegram.ClientApi.Models;
using TeleSharp.TL;
using TeleSharp.TL.Messages;
using TLSharp.Core.Exceptions;
using TLSharp.Core.Network.Exceptions;
using TLSharp.Core.Utils;

namespace TLSharp.Tests
{
    public class TLSharpTests
    {
        private string NumberToSendMessage { get; set; }

        private string NumberToAuthenticate { get; set; }

        private string CodeToAuthenticate { get; set; }

        private string PasswordToAuthenticate { get; set; }

        private string NotRegisteredNumberToSignUp { get; set; }

        private string UserNameToSendMessage { get; set; }

        private string NumberToGetUserFull { get; set; }

        private string NumberToAddToChat { get; set; }

        private string ApiHash { get; set; }

        private int ApiId { get; set; }

        class Assert
        {
            static internal void IsNotNull(object obj)
            {
                IsNotNullHanlder(obj);
            }

            static internal void IsTrue(bool cond)
            {
                IsTrueHandler(cond);
            }
        }

        internal static Action<object> IsNotNullHanlder;
        internal static Action<bool> IsTrueHandler;

        protected void Init(Action<object> notNullHandler, Action<bool> trueHandler)
        {
            IsNotNullHanlder = notNullHandler;
            IsTrueHandler = trueHandler;

            // Setup your API settings and phone numbers in app.config
            this.GatherTestConfiguration();
        }

        private TelegramClient NewClient()
        {
            try
            {
                TelegramClient telegramClient = new TelegramClient();
                return telegramClient;
            }
            catch (MissingApiConfigurationException ex)
            {
                throw new Exception($"Please add your API settings to the `app.config` file. (More info: {MissingApiConfigurationException.InfoUrl})",
                                    ex);
            }
        }

        private void GatherTestConfiguration()
        {
            string appConfigMsgWarning = "{0} not configured in app.config! Some tests may fail.";

            this.ApiHash = ConfigurationManager.AppSettings[nameof(this.ApiHash)];
            if (string.IsNullOrEmpty(this.ApiHash))
            {
                Debug.WriteLine(appConfigMsgWarning, nameof(this.ApiHash));
            }

            string apiId = ConfigurationManager.AppSettings[nameof(this.ApiId)];
            if (string.IsNullOrEmpty(apiId))
            {
                Debug.WriteLine(appConfigMsgWarning, nameof(this.ApiId));
            }
            else
            {
                this.ApiId = int.Parse(apiId);
            }

            this.NumberToAuthenticate = ConfigurationManager.AppSettings[nameof(this.NumberToAuthenticate)];
            if (string.IsNullOrEmpty(this.NumberToAuthenticate))
            {
                Debug.WriteLine(appConfigMsgWarning, nameof(this.NumberToAuthenticate));
            }

            this.CodeToAuthenticate = ConfigurationManager.AppSettings[nameof(this.CodeToAuthenticate)];
            if (string.IsNullOrEmpty(this.CodeToAuthenticate))
            {
                Debug.WriteLine(appConfigMsgWarning, nameof(this.CodeToAuthenticate));
            }

            this.PasswordToAuthenticate = ConfigurationManager.AppSettings[nameof(this.PasswordToAuthenticate)];
            if (string.IsNullOrEmpty(this.PasswordToAuthenticate))
            {
                Debug.WriteLine(appConfigMsgWarning, nameof(this.PasswordToAuthenticate));
            }

            this.NotRegisteredNumberToSignUp = ConfigurationManager.AppSettings[nameof(this.NotRegisteredNumberToSignUp)];
            if (string.IsNullOrEmpty(this.NotRegisteredNumberToSignUp))
            {
                Debug.WriteLine(appConfigMsgWarning, nameof(this.NotRegisteredNumberToSignUp));
            }

            this.UserNameToSendMessage = ConfigurationManager.AppSettings[nameof(this.UserNameToSendMessage)];
            if (string.IsNullOrEmpty(this.UserNameToSendMessage))
            {
                Debug.WriteLine(appConfigMsgWarning, nameof(this.UserNameToSendMessage));
            }

            this.NumberToGetUserFull = ConfigurationManager.AppSettings[nameof(this.NumberToGetUserFull)];
            if (string.IsNullOrEmpty(this.NumberToGetUserFull))
            {
                Debug.WriteLine(appConfigMsgWarning, nameof(this.NumberToGetUserFull));
            }

            this.NumberToAddToChat = ConfigurationManager.AppSettings[nameof(this.NumberToAddToChat)];
            if (string.IsNullOrEmpty(this.NumberToAddToChat))
            {
                Debug.WriteLine(appConfigMsgWarning, nameof(this.NumberToAddToChat));
            }
        }

        public virtual async Task AuthUser()
        {
            TelegramClient client = this.NewClient();

            TelegramAuthModel authModel = new TelegramAuthModel()
            {
                ApiId = this.ApiId,
                ApiHash = this.ApiHash
            };
            await client.AuthenticateAsync(authModel);

            string hash = await client.SendCodeRequestAsync(this.NumberToAuthenticate);
            string code = this.CodeToAuthenticate; // you can change code in debugger too

            if (String.IsNullOrWhiteSpace(code))
            {
                throw new Exception("CodeToAuthenticate is empty in the app.config file, fill it with the code you just got now by SMS/Telegram");
            }

            TLUser user = null;
            try
            {
                user = await client.MakeAuthAsync(this.NumberToAuthenticate, hash, code);
            }
            catch (CloudPasswordNeededException ex)
            {
                TeleSharp.TL.Account.TLPassword passwordSetting = await client.GetPasswordSetting();
                string password = this.PasswordToAuthenticate;

                user = await client.MakeAuthWithPasswordAsync(passwordSetting, password);
            }
            catch (InvalidPhoneCodeException ex)
            {
                throw new Exception("CodeToAuthenticate is wrong in the app.config file, fill it with the code you just got now by SMS/Telegram",
                                    ex);
            }
            Assert.IsNotNull(user);
            Assert.IsTrue(client.IsUserAuthorized());
        }

        public virtual async Task SendMessageTest()
        {
            this.NumberToSendMessage = ConfigurationManager.AppSettings[nameof(this.NumberToSendMessage)];
            if (string.IsNullOrWhiteSpace(this.NumberToSendMessage))
            {
                throw new Exception($"Please fill the '{nameof(this.NumberToSendMessage)}' setting in app.config file first");
            }

            // this is because the contacts in the address come without the "+" prefix
            string normalizedNumber = this.NumberToSendMessage.StartsWith("+") ?
                this.NumberToSendMessage.Substring(1, this.NumberToSendMessage.Length - 1) :
                this.NumberToSendMessage;

            TelegramClient client = this.NewClient();

            TelegramAuthModel authModel = new TelegramAuthModel()
            {
                ApiId = this.ApiId,
                ApiHash = this.ApiHash
            };
            await client.AuthenticateAsync(authModel);

            TeleSharp.TL.Contacts.TLContacts result = await client.GetContactsAsync();

            TLUser user = result.Users
                .OfType<TLUser>()
                .FirstOrDefault(x => x.Phone == normalizedNumber);

            if (user == null)
            {
                throw new System.Exception("Number was not found in Contacts List of user: " + this.NumberToSendMessage);
            }

            await client.SendTypingAsync(new TLInputPeerUser() { UserId = user.Id });
            Thread.Sleep(3000);
            await client.SendMessageAsync(new TLInputPeerUser() { UserId = user.Id }, "TEST");
        }

        public virtual async Task SendMessageToChannelTest()
        {
            TelegramClient client = this.NewClient();

            TelegramAuthModel authModel = new TelegramAuthModel()
            {
                ApiId = this.ApiId,
                ApiHash = this.ApiHash
            };
            await client.AuthenticateAsync(authModel);

            TLDialogs dialogs = (TLDialogs)await client.GetUserDialogsAsync();
            TLChannel chat = dialogs.Chats
                .OfType<TLChannel>()
                .FirstOrDefault(c => c.Title == "TestGroup");

            await client.SendMessageAsync(new TLInputPeerChannel() { ChannelId = chat.Id, AccessHash = chat.AccessHash.Value }, "TEST MSG");
        }

        public virtual async Task SendPhotoToContactTest()
        {
            TelegramClient client = this.NewClient();

            TelegramAuthModel authModel = new TelegramAuthModel()
            {
                ApiId = this.ApiId,
                ApiHash = this.ApiHash
            };
            await client.AuthenticateAsync(authModel);

            TeleSharp.TL.Contacts.TLContacts result = await client.GetContactsAsync();

            TLUser user = result.Users
                .OfType<TLUser>()
                .FirstOrDefault(x => x.Phone == this.NumberToSendMessage);

            TLInputFile fileResult = (TLInputFile)await client.UploadFile("cat.jpg", new StreamReader("data/cat.jpg"));
            await client.SendUploadedPhoto(new TLInputPeerUser() { UserId = user.Id }, fileResult, "kitty");
        }

        public virtual async Task SendBigFileToContactTest()
        {
            TelegramClient client = this.NewClient();

            TelegramAuthModel authModel = new TelegramAuthModel()
            {
                ApiId = this.ApiId,
                ApiHash = this.ApiHash
            };
            await client.AuthenticateAsync(authModel);

            TeleSharp.TL.Contacts.TLContacts result = await client.GetContactsAsync();

            TLUser user = result.Users
                .OfType<TLUser>()
                .FirstOrDefault(x => x.Phone == this.NumberToSendMessage);

            TLInputFileBig fileResult = (TLInputFileBig)await client.UploadFile("some.zip", new StreamReader("<some big file path>"));

            await client.SendUploadedDocument(
                new TLInputPeerUser() { UserId = user.Id },
                fileResult,
                "some zips",
                "application/zip",
                new TLVector<TLAbsDocumentAttribute>());
        }

        public virtual async Task DownloadFileFromContactTest()
        {
            TelegramClient client = this.NewClient();

            TelegramAuthModel authModel = new TelegramAuthModel()
            {
                ApiId = this.ApiId,
                ApiHash = this.ApiHash
            };
            await client.AuthenticateAsync(authModel);

            TeleSharp.TL.Contacts.TLContacts result = await client.GetContactsAsync();

            TLUser user = result.Users
                .OfType<TLUser>()
                .FirstOrDefault(x => x.Phone == this.NumberToSendMessage);

            TLInputPeerUser inputPeer = new TLInputPeerUser() { UserId = user.Id };
            TLMessagesSlice res = await client.SendRequestAsync<TLMessagesSlice>(new TLRequestGetHistory() { Peer = inputPeer });
            TLDocument document = res.Messages
                .OfType<TLMessage>()
                .Where(m => m.Media != null)
                .Select(m => m.Media)
                .OfType<TLMessageMediaDocument>()
                .Select(md => md.Document)
                .OfType<TLDocument>()
                .First();

            TeleSharp.TL.Upload.TLFile resFile = await client.GetFile(
                new TLInputDocumentFileLocation()
                {
                    AccessHash = document.AccessHash,
                    Id = document.Id,
                    Version = document.Version
                },
                document.Size);

            Assert.IsTrue(resFile.Bytes.Length > 0);
        }

        public virtual async Task DownloadFileFromWrongLocationTest()
        {
            TelegramClient client = this.NewClient();

            TelegramAuthModel authModel = new TelegramAuthModel()
            {
                ApiId = this.ApiId,
                ApiHash = this.ApiHash
            };
            await client.AuthenticateAsync(authModel);

            TeleSharp.TL.Contacts.TLContacts result = await client.GetContactsAsync();

            TLUser user = result.Users
                .OfType<TLUser>()
                .FirstOrDefault(x => x.Id == 5880094);

            TLUserProfilePhoto photo = ((TLUserProfilePhoto)user.Photo);
            TLFileLocation photoLocation = (TLFileLocation)photo.PhotoBig;

            TeleSharp.TL.Upload.TLFile resFile = await client.GetFile(new TLInputFileLocation()
            {
                LocalId = photoLocation.LocalId,
                Secret = photoLocation.Secret,
                VolumeId = photoLocation.VolumeId
            }, 1024);

            TLAbsDialogs res = await client.GetUserDialogsAsync();

            Assert.IsTrue(resFile.Bytes.Length > 0);
        }

        public virtual async Task SignUpNewUser()
        {
            TelegramClient client = this.NewClient();
            TelegramAuthModel authModel = new TelegramAuthModel()
            {
                ApiId = this.ApiId,
                ApiHash = this.ApiHash
            };
            await client.AuthenticateAsync(authModel);

            string hash = await client.SendCodeRequestAsync(this.NotRegisteredNumberToSignUp);
            string code = "";

            TLUser registeredUser = await client.SignUpAsync(this.NotRegisteredNumberToSignUp, hash, code, "TLSharp", "User");
            Assert.IsNotNull(registeredUser);
            Assert.IsTrue(client.IsUserAuthorized());

            TLUser loggedInUser = await client.MakeAuthAsync(this.NotRegisteredNumberToSignUp, hash, code);
            Assert.IsNotNull(loggedInUser);
        }

        public virtual async Task CheckPhones()
        {
            TelegramClient client = this.NewClient();
            TelegramAuthModel authModel = new TelegramAuthModel()
            {
                ApiId = this.ApiId,
                ApiHash = this.ApiHash
            };
            await client.AuthenticateAsync(authModel);

            bool result = await client.IsPhoneRegisteredAsync(this.NumberToAuthenticate);
            Assert.IsTrue(result);
        }

        public virtual async Task FloodExceptionShouldNotCauseCannotReadPackageLengthError()
        {
            for (int i = 0; i < 50; i++)
            {
                try
                {
                    await this.CheckPhones();
                }
                catch (FloodException floodException)
                {
                    Console.WriteLine($"FLOODEXCEPTION: {floodException}");
                    Thread.Sleep(floodException.TimeToWait);
                }
            }
        }

        public virtual async Task SendMessageByUserNameTest()
        {
            this.UserNameToSendMessage = ConfigurationManager.AppSettings[nameof(this.UserNameToSendMessage)];
            if (string.IsNullOrWhiteSpace(this.UserNameToSendMessage))
            {
                throw new Exception($"Please fill the '{nameof(this.UserNameToSendMessage)}' setting in app.config file first");
            }

            TelegramClient client = this.NewClient();

            TelegramAuthModel authModel = new TelegramAuthModel()
            {
                ApiId = this.ApiId,
                ApiHash = this.ApiHash
            };
            await client.AuthenticateAsync(authModel);

            TeleSharp.TL.Contacts.TLFound result = await client.SearchUserAsync(this.UserNameToSendMessage);

            TLUser user = result.Users
                .Where(x => x.GetType() == typeof(TLUser))
                .OfType<TLUser>()
                .FirstOrDefault(x => x.Username == this.UserNameToSendMessage.TrimStart('@'));

            if (user == null)
            {
                TeleSharp.TL.Contacts.TLContacts contacts = await client.GetContactsAsync();

                user = contacts.Users
                    .Where(x => x.GetType() == typeof(TLUser))
                    .OfType<TLUser>()
                    .FirstOrDefault(x => x.Username == this.UserNameToSendMessage.TrimStart('@'));
            }

            if (user == null)
            {
                throw new System.Exception("Username was not found: " + this.UserNameToSendMessage);
            }

            await client.SendTypingAsync(new TLInputPeerUser() { UserId = user.Id });
            Thread.Sleep(3000);
            await client.SendMessageAsync(new TLInputPeerUser() { UserId = user.Id }, "TEST");
        }
    }
}
