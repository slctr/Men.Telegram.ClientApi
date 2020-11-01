using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Auth
{
    [TLObject(1738800940)]
    public class TLRequestImportBotAuthorization : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 1738800940;
            }
        }

        public int Flags { get; set; }
        public int ApiId { get; set; }
        public string ApiHash { get; set; }
        public string BotAuthToken { get; set; }
        public Auth.TLAuthorization Response { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            this.ApiId = br.ReadInt32();
            this.ApiHash = StringUtil.Deserialize(br);
            this.BotAuthToken = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);
            bw.Write(this.ApiId);
            StringUtil.Serialize(this.ApiHash, bw);
            StringUtil.Serialize(this.BotAuthToken, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            this.Response = (Auth.TLAuthorization)ObjectUtils.DeserializeObject(br);

        }
    }
}
