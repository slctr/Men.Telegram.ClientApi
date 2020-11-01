using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-686710068)]
    public class TLUpdateDialogPinned : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return -686710068;
            }
        }

        public int Flags { get; set; }
        public bool Pinned { get; set; }
        public TLAbsPeer Peer { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.Pinned ? (this.Flags | 1) : (this.Flags & ~1);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            this.Pinned = (this.Flags & 1) != 0;
            this.Peer = (TLAbsPeer)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);

            ObjectUtils.SerializeObject(this.Peer, bw);

        }
    }
}
