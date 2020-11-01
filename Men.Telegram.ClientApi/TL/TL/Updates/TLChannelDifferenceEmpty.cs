using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Updates
{
    [TLObject(1041346555)]
    public class TLChannelDifferenceEmpty : TLAbsChannelDifference
    {
        public override int Constructor
        {
            get
            {
                return 1041346555;
            }
        }

        public int Flags { get; set; }
        public bool Final { get; set; }
        public int Pts { get; set; }
        public int? Timeout { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.Final ? (this.Flags | 1) : (this.Flags & ~1);
            this.Flags = this.Timeout != null ? (this.Flags | 2) : (this.Flags & ~2);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            this.Final = (this.Flags & 1) != 0;
            this.Pts = br.ReadInt32();
            if ((this.Flags & 2) != 0)
                this.Timeout = br.ReadInt32();
            else
                this.Timeout = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);

            bw.Write(this.Pts);
            if ((this.Flags & 2) != 0)
                bw.Write(this.Timeout.Value);

        }
    }
}
