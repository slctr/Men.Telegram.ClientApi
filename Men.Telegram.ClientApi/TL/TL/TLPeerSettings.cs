using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-2122045747)]
    public class TLPeerSettings : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -2122045747;
            }
        }

        public int Flags { get; set; }
        public bool ReportSpam { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.ReportSpam ? (this.Flags | 1) : (this.Flags & ~1);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            this.ReportSpam = (this.Flags & 1) != 0;

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);


        }
    }
}
