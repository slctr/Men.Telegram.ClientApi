using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-352032773)]
    public class TLUpdateChannelTooLong : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return -352032773;
            }
        }

        public int Flags { get; set; }
        public int ChannelId { get; set; }
        public int? Pts { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.Pts != null ? (this.Flags | 1) : (this.Flags & ~1);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            this.ChannelId = br.ReadInt32();
            if ((this.Flags & 1) != 0)
            {
                this.Pts = br.ReadInt32();
            }
            else
            {
                this.Pts = null;
            }
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);
            bw.Write(this.ChannelId);
            if ((this.Flags & 1) != 0)
            {
                bw.Write(this.Pts.Value);
            }
        }
    }
}
