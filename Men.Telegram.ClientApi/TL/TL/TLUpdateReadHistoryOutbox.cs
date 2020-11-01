using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(791617983)]
    public class TLUpdateReadHistoryOutbox : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return 791617983;
            }
        }

        public TLAbsPeer Peer { get; set; }
        public int MaxId { get; set; }
        public int Pts { get; set; }
        public int PtsCount { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Peer = (TLAbsPeer)ObjectUtils.DeserializeObject(br);
            this.MaxId = br.ReadInt32();
            this.Pts = br.ReadInt32();
            this.PtsCount = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            ObjectUtils.SerializeObject(this.Peer, bw);
            bw.Write(this.MaxId);
            bw.Write(this.Pts);
            bw.Write(this.PtsCount);

        }
    }
}
