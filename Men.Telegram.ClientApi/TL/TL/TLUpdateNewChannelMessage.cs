using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(1656358105)]
    public class TLUpdateNewChannelMessage : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return 1656358105;
            }
        }

        public TLAbsMessage Message { get; set; }
        public int Pts { get; set; }
        public int PtsCount { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Message = (TLAbsMessage)ObjectUtils.DeserializeObject(br);
            this.Pts = br.ReadInt32();
            this.PtsCount = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            ObjectUtils.SerializeObject(this.Message, bw);
            bw.Write(this.Pts);
            bw.Write(this.PtsCount);

        }
    }
}
