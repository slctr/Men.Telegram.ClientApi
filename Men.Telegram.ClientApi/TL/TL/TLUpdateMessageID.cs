using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(1318109142)]
    public class TLUpdateMessageID : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return 1318109142;
            }
        }

        public int Id { get; set; }
        public long RandomId { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Id = br.ReadInt32();
            this.RandomId = br.ReadInt64();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            bw.Write(this.Id);
            bw.Write(this.RandomId);

        }
    }
}
