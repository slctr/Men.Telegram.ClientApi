using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1995686519)]
    public class TLInputBotInlineMessageID : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -1995686519;
            }
        }

        public int DcId { get; set; }
        public long Id { get; set; }
        public long AccessHash { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.DcId = br.ReadInt32();
            this.Id = br.ReadInt64();
            this.AccessHash = br.ReadInt64();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            bw.Write(this.DcId);
            bw.Write(this.Id);
            bw.Write(this.AccessHash);

        }
    }
}
