using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1910892683)]
    public class TLNearestDc : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -1910892683;
            }
        }

        public string Country { get; set; }
        public int ThisDc { get; set; }
        public int NearestDc { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Country = StringUtil.Deserialize(br);
            this.ThisDc = br.ReadInt32();
            this.NearestDc = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            StringUtil.Serialize(this.Country, bw);
            bw.Write(this.ThisDc);
            bw.Write(this.NearestDc);

        }
    }
}
