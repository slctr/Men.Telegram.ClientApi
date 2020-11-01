using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1343524562)]
    public class TLInputChannel : TLAbsInputChannel
    {
        public override int Constructor
        {
            get
            {
                return -1343524562;
            }
        }

        public int ChannelId { get; set; }
        public long AccessHash { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.ChannelId = br.ReadInt32();
            this.AccessHash = br.ReadInt64();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            bw.Write(this.ChannelId);
            bw.Write(this.AccessHash);

        }
    }
}
