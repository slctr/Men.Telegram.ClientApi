using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Contacts
{
    [TLObject(986597452)]
    public class TLLink : TLObject
    {
        public override int Constructor
        {
            get
            {
                return 986597452;
            }
        }

        public TLAbsContactLink MyLink { get; set; }
        public TLAbsContactLink ForeignLink { get; set; }
        public TLAbsUser User { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.MyLink = (TLAbsContactLink)ObjectUtils.DeserializeObject(br);
            this.ForeignLink = (TLAbsContactLink)ObjectUtils.DeserializeObject(br);
            this.User = (TLAbsUser)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            ObjectUtils.SerializeObject(this.MyLink, bw);
            ObjectUtils.SerializeObject(this.ForeignLink, bw);
            ObjectUtils.SerializeObject(this.User, bw);

        }
    }
}
