using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Account
{
    [TLObject(-1131605573)]
    public class TLRequestGetPasswordSettings : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -1131605573;
            }
        }

        public byte[] CurrentPasswordHash { get; set; }
        public Account.TLPasswordSettings Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.CurrentPasswordHash = BytesUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            BytesUtil.Serialize(this.CurrentPasswordHash, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            this.Response = (Account.TLPasswordSettings)ObjectUtils.DeserializeObject(br);

        }
    }
}
