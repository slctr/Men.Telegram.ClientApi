using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1369215196)]
    public class TLDisabledFeature : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -1369215196;
            }
        }

        public string Feature { get; set; }
        public string Description { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Feature = StringUtil.Deserialize(br);
            this.Description = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            StringUtil.Serialize(this.Feature, bw);
            StringUtil.Serialize(this.Description, bw);

        }
    }
}
