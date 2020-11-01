using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-2134272152)]
    public class TLInputMessagesFilterPhoneCalls : TLAbsMessagesFilter
    {
        public override int Constructor
        {
            get
            {
                return -2134272152;
            }
        }

        public int Flags { get; set; }
        public bool Missed { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.Missed ? (this.Flags | 1) : (this.Flags & ~1);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            this.Missed = (this.Flags & 1) != 0;

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);


        }
    }
}
