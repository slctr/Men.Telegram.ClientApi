using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Contacts
{
    [TLObject(446822276)]
    public class TLFound : TLObject
    {
        public override int Constructor
        {
            get
            {
                return 446822276;
            }
        }

        public TLVector<TLAbsPeer> Results { get; set; }
        public TLVector<TLAbsChat> Chats { get; set; }
        public TLVector<TLAbsUser> Users { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Results = (TLVector<TLAbsPeer>)ObjectUtils.DeserializeVector<TLAbsPeer>(br);
            this.Chats = (TLVector<TLAbsChat>)ObjectUtils.DeserializeVector<TLAbsChat>(br);
            this.Users = (TLVector<TLAbsUser>)ObjectUtils.DeserializeVector<TLAbsUser>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            ObjectUtils.SerializeObject(this.Results, bw);
            ObjectUtils.SerializeObject(this.Chats, bw);
            ObjectUtils.SerializeObject(this.Users, bw);

        }
    }
}
