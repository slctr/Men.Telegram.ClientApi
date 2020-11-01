using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-886477832)]
    public class TLLabeledPrice : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -886477832;
            }
        }

        public string Label { get; set; }
        public long Amount { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Label = StringUtil.Deserialize(br);
            this.Amount = br.ReadInt64();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            StringUtil.Serialize(this.Label, bw);
            bw.Write(this.Amount);

        }
    }
}
