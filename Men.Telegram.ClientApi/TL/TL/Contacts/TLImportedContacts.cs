using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Contacts
{
    [TLObject(-1387117803)]
    public class TLImportedContacts : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -1387117803;
            }
        }

        public TLVector<TLImportedContact> Imported { get; set; }
        public TLVector<long> RetryContacts { get; set; }
        public TLVector<TLAbsUser> Users { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Imported = (TLVector<TLImportedContact>)ObjectUtils.DeserializeVector<TLImportedContact>(br);
            this.RetryContacts = (TLVector<long>)ObjectUtils.DeserializeVector<long>(br);
            this.Users = (TLVector<TLAbsUser>)ObjectUtils.DeserializeVector<TLAbsUser>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            ObjectUtils.SerializeObject(this.Imported, bw);
            ObjectUtils.SerializeObject(this.RetryContacts, bw);
            ObjectUtils.SerializeObject(this.Users, bw);

        }
    }
}
