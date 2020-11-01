using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Account
{
    [TLObject(2081952796)]
    public class TLPassword : TLAbsPassword
    {
        public override int Constructor
        {
            get
            {
                return 2081952796;
            }
        }

        public byte[] CurrentSalt { get; set; }
        public byte[] NewSalt { get; set; }
        public string Hint { get; set; }
        public bool HasRecovery { get; set; }
        public string EmailUnconfirmedPattern { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.CurrentSalt = BytesUtil.Deserialize(br);
            this.NewSalt = BytesUtil.Deserialize(br);
            this.Hint = StringUtil.Deserialize(br);
            this.HasRecovery = BoolUtil.Deserialize(br);
            this.EmailUnconfirmedPattern = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            BytesUtil.Serialize(this.CurrentSalt, bw);
            BytesUtil.Serialize(this.NewSalt, bw);
            StringUtil.Serialize(this.Hint, bw);
            BoolUtil.Serialize(this.HasRecovery, bw);
            StringUtil.Serialize(this.EmailUnconfirmedPattern, bw);

        }
    }
}
