using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(-1269012015)]
    public class TLAffectedHistory : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -1269012015;
            }
        }

        public int Pts { get; set; }
        public int PtsCount { get; set; }
        public int Offset { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Pts = br.ReadInt32();
            this.PtsCount = br.ReadInt32();
            this.Offset = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            bw.Write(this.Pts);
            bw.Write(this.PtsCount);
            bw.Write(this.Offset);

        }
    }
}
