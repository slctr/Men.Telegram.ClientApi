using TLSharp.Core;
using TLSharp.Core.Network;

namespace Men.Telegram.ClientApi.Models
{
    public class TelegramSettings
    {
        public ISessionStore SessionStore { get; set; }

        public string SessionId { get; set; }

        public TcpClientConnectionHandler Handler { get; set; }

        public DataCenterIPVersion DcIpVersion { get; set; }
    }
}
