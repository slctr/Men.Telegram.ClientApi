using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(1406570614)]
    public class TLFileLocation : TLAbsFileLocation
    {
        public override int Constructor
        {
            get
            {
                return 1406570614;
            }
        }

        public int DcId { get; set; }
        public long VolumeId { get; set; }
        public int LocalId { get; set; }
        public long Secret { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.DcId = br.ReadInt32();
            this.VolumeId = br.ReadInt64();
            this.LocalId = br.ReadInt32();
            this.Secret = br.ReadInt64();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            bw.Write(this.DcId);
            bw.Write(this.VolumeId);
            bw.Write(this.LocalId);
            bw.Write(this.Secret);

        }
    }
}
