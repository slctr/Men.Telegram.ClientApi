using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(250621158)]
    public class TLDocumentAttributeVideo : TLAbsDocumentAttribute
    {
        public override int Constructor
        {
            get
            {
                return 250621158;
            }
        }

        public int Flags { get; set; }
        public bool RoundMessage { get; set; }
        public int Duration { get; set; }
        public int W { get; set; }
        public int H { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.RoundMessage ? (this.Flags | 1) : (this.Flags & ~1);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            this.RoundMessage = (this.Flags & 1) != 0;
            this.Duration = br.ReadInt32();
            this.W = br.ReadInt32();
            this.H = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);

            bw.Write(this.Duration);
            bw.Write(this.W);
            bw.Write(this.H);

        }
    }
}
