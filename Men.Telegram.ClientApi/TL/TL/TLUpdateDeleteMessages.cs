using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1576161051)]
    public class TLUpdateDeleteMessages : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return -1576161051;
            }
        }

        public TLVector<int> Messages { get; set; }
        public int Pts { get; set; }
        public int PtsCount { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Messages = (TLVector<int>)ObjectUtils.DeserializeVector<int>(br);
            this.Pts = br.ReadInt32();
            this.PtsCount = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            ObjectUtils.SerializeObject(this.Messages, bw);
            bw.Write(this.Pts);
            bw.Write(this.PtsCount);

        }
    }
}
