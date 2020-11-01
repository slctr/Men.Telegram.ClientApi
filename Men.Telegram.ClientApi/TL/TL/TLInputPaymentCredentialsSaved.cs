using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1056001329)]
    public class TLInputPaymentCredentialsSaved : TLAbsInputPaymentCredentials
    {
        public override int Constructor
        {
            get
            {
                return -1056001329;
            }
        }

        public string Id { get; set; }
        public byte[] TmpPassword { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Id = StringUtil.Deserialize(br);
            this.TmpPassword = BytesUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            StringUtil.Serialize(this.Id, bw);
            BytesUtil.Serialize(this.TmpPassword, bw);

        }
    }
}
