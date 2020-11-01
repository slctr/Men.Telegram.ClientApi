using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Account
{
    [TLObject(-92517498)]
    public class TLRequestUpdatePasswordSettings : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -92517498;
            }
        }

        public byte[] CurrentPasswordHash { get; set; }
        public Account.TLPasswordInputSettings NewSettings { get; set; }
        public bool Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.CurrentPasswordHash = BytesUtil.Deserialize(br);
            this.NewSettings = (Account.TLPasswordInputSettings)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            BytesUtil.Serialize(this.CurrentPasswordHash, bw);
            ObjectUtils.SerializeObject(this.NewSettings, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            this.Response = BoolUtil.Deserialize(br);

        }
    }
}
