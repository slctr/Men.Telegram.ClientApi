using System;
using System.IO;
using TeleSharp.TL;
using TLSharp.Core;
using TLSharp.Core.MTProto;
using TLSharp.Core.MTProto.Crypto;

namespace Men.Telegram.ClientApi.Core.Sessions
{
    public class TelegramSession : ISession
    {
        protected const string c_DefaultConnectionAddress = "149.154.175.100";//"149.154.167.50";
        protected const int c_DefaultConnectionPort = 443;

        internal object Lock = new object();

        public string SessionUserId { get; set; }
        public AuthKey AuthKey { get; set; }
        public ulong Id { get; set; }
        public int Sequence { get; set; }
        public ulong Salt { get; set; }
        public int TimeOffset { get; set; }
        public long LastMessageId { get; set; }
        public int SessionExpires { get; set; }
        public TLUser TLUser { get; set; }

        private ISessionStore store;
        internal DataCenter DataCenter { get; set; }


        public TelegramSession(ISessionStore store)
        {
            this.store = store;
        }


        public byte[] ToBytes()
        {
            using (MemoryStream stream = new MemoryStream())
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                writer.Write(this.Id);
                writer.Write(this.Sequence);
                writer.Write(this.Salt);
                writer.Write(this.LastMessageId);
                writer.Write(this.TimeOffset);
                Serializers.String.Write(writer, this.DataCenter.Address);
                writer.Write(this.DataCenter.Port);

                if (this.TLUser != null)
                {
                    writer.Write(1);
                    writer.Write(this.SessionExpires);
                    ObjectUtils.SerializeObject(this.TLUser, writer);
                }
                else
                {
                    writer.Write(0);
                }

                Serializers.Bytes.Write(writer, this.AuthKey.Data);

                return stream.ToArray();
            }
        }

        public static TelegramSession FromBytes(byte[] buffer, ISessionStore store, string sessionUserId)
        {
            using (MemoryStream stream = new MemoryStream(buffer))
            using (BinaryReader reader = new BinaryReader(stream))
            {
                ulong id = reader.ReadUInt64();
                int sequence = reader.ReadInt32();
                ulong salt = reader.ReadUInt64();
                long lastMessageId = reader.ReadInt64();
                int timeOffset = reader.ReadInt32();
                string serverAddress = Serializers.String.Read(reader);
                int port = reader.ReadInt32();

                bool isAuthExsist = reader.ReadInt32() == 1;
                int sessionExpires = 0;
                TLUser TLUser = null;
                if (isAuthExsist)
                {
                    sessionExpires = reader.ReadInt32();
                    TLUser = (TLUser)ObjectUtils.DeserializeObject(reader);
                }

                byte[] authData = Serializers.Bytes.Read(reader);
                DataCenter defaultDataCenter = new DataCenter(serverAddress, port);

                return new TelegramSession(store)
                {
                    AuthKey = new AuthKey(authData),
                    Id = id,
                    Salt = salt,
                    Sequence = sequence,
                    LastMessageId = lastMessageId,
                    TimeOffset = timeOffset,
                    SessionExpires = sessionExpires,
                    TLUser = TLUser,
                    SessionUserId = sessionUserId,
                    DataCenter = defaultDataCenter,
                };
            }
        }

        public void Save()
        {
            this.store.Save(this);
        }

        public static TelegramSession TryLoadOrCreateNew(ISessionStore store, string sessionUserId)
        {
            DataCenter defaultDataCenter = new DataCenter(
                c_DefaultConnectionAddress,
                c_DefaultConnectionPort
            );

            return store.Load(sessionUserId) ?? new TelegramSession(store)
            {
                Id = GenerateRandomUlong(),
                SessionUserId = sessionUserId,
                DataCenter = defaultDataCenter,
            };
        }

        private static ulong GenerateRandomUlong()
        {
            Random random = new Random();
            ulong rand = (ulong)random.Next() << 32 | (ulong)random.Next();
            return rand;
        }

        public long GetNewMessageId()
        {
            long time = Convert.ToInt64((DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds);
            long newMessageId = time / 1000 + this.TimeOffset << 32 |
                                time % 1000 << 22 |
                                new Random().Next(524288) << 2; // 2^19
                                                                // [ unix timestamp : 32 bit] [ milliseconds : 10 bit ] [ buffer space : 1 bit ] [ random : 19 bit ] [ msg_id type : 2 bit ] = [ msg_id : 64 bit ]

            if (this.LastMessageId >= newMessageId)
            {
                newMessageId = this.LastMessageId + 4;
            }

            this.LastMessageId = newMessageId;
            return newMessageId;
        }
    }
}
