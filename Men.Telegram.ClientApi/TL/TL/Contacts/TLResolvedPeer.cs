using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Contacts
{
    [TLObject(2131196633)]
    public class TLResolvedPeer : TLObject
    {
        public override int Constructor
        {
            get
            {
                return 2131196633;
            }
        }

        public TLAbsPeer Peer { get; set; }
        public TLVector<TLAbsChat> Chats { get; set; }
        public TLVector<TLAbsUser> Users { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Peer = (TLAbsPeer)ObjectUtils.DeserializeObject(br);
            this.Chats = (TLVector<TLAbsChat>)ObjectUtils.DeserializeVector<TLAbsChat>(br);
            this.Users = (TLVector<TLAbsUser>)ObjectUtils.DeserializeVector<TLAbsUser>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            ObjectUtils.SerializeObject(this.Peer, bw);
            ObjectUtils.SerializeObject(this.Chats, bw);
            ObjectUtils.SerializeObject(this.Users, bw);

        }
    }
}
