using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Account
{
    [TLObject(-1764049896)]
    public class TLNoPassword : TLAbsPassword
    {
        public override int Constructor
        {
            get
            {
                return -1764049896;
            }
        }

        public byte[] NewSalt { get; set; }
        public string EmailUnconfirmedPattern { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.NewSalt = BytesUtil.Deserialize(br);
            this.EmailUnconfirmedPattern = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            BytesUtil.Serialize(this.NewSalt, bw);
            StringUtil.Serialize(this.EmailUnconfirmedPattern, bw);

        }
    }
}
