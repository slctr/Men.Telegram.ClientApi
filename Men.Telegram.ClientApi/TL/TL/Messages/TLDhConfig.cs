using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(740433629)]
    public class TLDhConfig : TLAbsDhConfig
    {
        public override int Constructor
        {
            get
            {
                return 740433629;
            }
        }

        public int G { get; set; }
        public byte[] P { get; set; }
        public int Version { get; set; }
        public byte[] Random { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.G = br.ReadInt32();
            this.P = BytesUtil.Deserialize(br);
            this.Version = br.ReadInt32();
            this.Random = BytesUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            bw.Write(this.G);
            BytesUtil.Serialize(this.P, bw);
            bw.Write(this.Version);
            BytesUtil.Serialize(this.Random, bw);

        }
    }
}
