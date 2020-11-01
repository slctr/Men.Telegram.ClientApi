using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Phone
{
    [TLObject(-326966976)]
    public class TLPhoneCall : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -326966976;
            }
        }

        public TLAbsPhoneCall PhoneCall { get; set; }
        public TLVector<TLAbsUser> Users { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.PhoneCall = (TLAbsPhoneCall)ObjectUtils.DeserializeObject(br);
            this.Users = (TLVector<TLAbsUser>)ObjectUtils.DeserializeVector<TLAbsUser>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            ObjectUtils.SerializeObject(this.PhoneCall, bw);
            ObjectUtils.SerializeObject(this.Users, bw);

        }
    }
}
