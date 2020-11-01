using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Contacts
{
    [TLObject(1871416498)]
    public class TLContacts : TLAbsContacts
    {
        public override int Constructor
        {
            get
            {
                return 1871416498;
            }
        }

        public TLVector<TLContact> Contacts { get; set; }
        public TLVector<TLAbsUser> Users { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Contacts = (TLVector<TLContact>)ObjectUtils.DeserializeVector<TLContact>(br);
            this.Users = (TLVector<TLAbsUser>)ObjectUtils.DeserializeVector<TLAbsUser>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            ObjectUtils.SerializeObject(this.Contacts, bw);
            ObjectUtils.SerializeObject(this.Users, bw);

        }
    }
}
