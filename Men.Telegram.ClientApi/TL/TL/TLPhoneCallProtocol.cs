using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1564789301)]
    public class TLPhoneCallProtocol : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -1564789301;
            }
        }

        public int Flags { get; set; }
        public bool UdpP2p { get; set; }
        public bool UdpReflector { get; set; }
        public int MinLayer { get; set; }
        public int MaxLayer { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.UdpP2p ? (this.Flags | 1) : (this.Flags & ~1);
            this.Flags = this.UdpReflector ? (this.Flags | 2) : (this.Flags & ~2);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            this.UdpP2p = (this.Flags & 1) != 0;
            this.UdpReflector = (this.Flags & 2) != 0;
            this.MinLayer = br.ReadInt32();
            this.MaxLayer = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);


            bw.Write(this.MinLayer);
            bw.Write(this.MaxLayer);

        }
    }
}
