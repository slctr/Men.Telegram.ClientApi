using TeleSharp.TL;
using TLSharp.Core;
using TLSharp.Core.MTProto.Crypto;

namespace Men.Telegram.ClientApi.Core.Sessions
{
    public interface ISession
    {
        string SessionUserId { get; set; }
        AuthKey AuthKey { get; set; }
        ulong Id { get; set; }
        int Sequence { get; set; }
        ulong Salt { get; set; }
        int TimeOffset { get; set; }
        long LastMessageId { get; set; }
        int SessionExpires { get; set; }
        TLUser TLUser { get; set; }
        //DataCenter DataCenter { get; set; }

        //Session TryLoadOrCreateNew(ISessionStore store, string sessionUserId);



        //byte[] ToBytes();

        //Session FromBytes(byte[] buffer, ISessionStore store, string sessionUserId);

        //void Save();

        //long GetNewMessageId();
    }
}
