using Men.Telegram.ClientApi.Core.Sessions;

namespace TLSharp.Core
{
    public class FakeSessionStore : ISessionStore
    {
        public void Save(TelegramSession session)
        {

        }

        public TelegramSession Load(string sessionUserId)
        {
            return null;
        }
    }
}
