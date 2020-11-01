using Men.Telegram.ClientApi.Core.Sessions;

namespace TLSharp.Core
{
    public interface ISessionStore
    {
        void Save(TelegramSession session);
        TelegramSession Load(string sessionUserId);
    }
}
