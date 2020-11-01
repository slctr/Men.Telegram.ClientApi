using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-74070332)]
    public class TLInputPhoto : TLAbsInputPhoto
    {
        public override int Constructor
        {
            get
            {
                return -74070332;
            }
        }

        public long Id { get; set; }
        public long AccessHash { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Id = br.ReadInt64();
            this.AccessHash = br.ReadInt64();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            bw.Write(this.Id);
            bw.Write(this.AccessHash);

        }
    }
}
