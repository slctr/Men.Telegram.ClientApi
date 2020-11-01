using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(1125058340)]
    public class TLInputDocumentFileLocation : TLAbsInputFileLocation
    {
        public override int Constructor
        {
            get
            {
                return 1125058340;
            }
        }

        public long Id { get; set; }
        public long AccessHash { get; set; }
        public int Version { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Id = br.ReadInt64();
            this.AccessHash = br.ReadInt64();
            this.Version = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            bw.Write(this.Id);
            bw.Write(this.AccessHash);
            bw.Write(this.Version);

        }
    }
}
