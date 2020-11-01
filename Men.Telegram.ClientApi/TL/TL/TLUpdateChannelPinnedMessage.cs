using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1738988427)]
    public class TLUpdateChannelPinnedMessage : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return -1738988427;
            }
        }

        public int ChannelId { get; set; }
        public int Id { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.ChannelId = br.ReadInt32();
            this.Id = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            bw.Write(this.ChannelId);
            bw.Write(this.Id);

        }
    }
}
