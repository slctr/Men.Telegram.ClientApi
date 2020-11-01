using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Contacts
{
    [TLObject(-1878523231)]
    public class TLBlockedSlice : TLAbsBlocked
    {
        public override int Constructor
        {
            get
            {
                return -1878523231;
            }
        }

        public int Count { get; set; }
        public TLVector<TLContactBlocked> Blocked { get; set; }
        public TLVector<TLAbsUser> Users { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Count = br.ReadInt32();
            this.Blocked = (TLVector<TLContactBlocked>)ObjectUtils.DeserializeVector<TLContactBlocked>(br);
            this.Users = (TLVector<TLAbsUser>)ObjectUtils.DeserializeVector<TLAbsUser>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            bw.Write(this.Count);
            ObjectUtils.SerializeObject(this.Blocked, bw);
            ObjectUtils.SerializeObject(this.Users, bw);

        }
    }
}
