using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Account
{
    [TLObject(1669245048)]
    public class TLRequestRegisterDevice : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 1669245048;
            }
        }

        public int TokenType { get; set; }
        public string Token { get; set; }
        public bool Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.TokenType = br.ReadInt32();
            this.Token = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            bw.Write(this.TokenType);
            StringUtil.Serialize(this.Token, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            this.Response = BoolUtil.Deserialize(br);

        }
    }
}
