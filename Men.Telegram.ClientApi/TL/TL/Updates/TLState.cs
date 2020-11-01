using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Updates
{
    [TLObject(-1519637954)]
    public class TLState : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -1519637954;
            }
        }

        public int Pts { get; set; }
        public int Qts { get; set; }
        public int Date { get; set; }
        public int Seq { get; set; }
        public int UnreadCount { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Pts = br.ReadInt32();
            this.Qts = br.ReadInt32();
            this.Date = br.ReadInt32();
            this.Seq = br.ReadInt32();
            this.UnreadCount = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            bw.Write(this.Pts);
            bw.Write(this.Qts);
            bw.Write(this.Date);
            bw.Write(this.Seq);
            bw.Write(this.UnreadCount);

        }
    }
}
