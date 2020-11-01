using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Help
{
    [TLObject(398898678)]
    public class TLSupport : TLObject
    {
        public override int Constructor
        {
            get
            {
                return 398898678;
            }
        }

        public string PhoneNumber { get; set; }
        public TLAbsUser User { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.PhoneNumber = StringUtil.Deserialize(br);
            this.User = (TLAbsUser)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            StringUtil.Serialize(this.PhoneNumber, bw);
            ObjectUtils.SerializeObject(this.User, bw);

        }
    }
}
