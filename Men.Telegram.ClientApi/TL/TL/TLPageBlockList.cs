using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(978896884)]
    public class TLPageBlockList : TLAbsPageBlock
    {
        public override int Constructor
        {
            get
            {
                return 978896884;
            }
        }

        public bool Ordered { get; set; }
        public TLVector<TLAbsRichText> Items { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Ordered = BoolUtil.Deserialize(br);
            this.Items = (TLVector<TLAbsRichText>)ObjectUtils.DeserializeVector<TLAbsRichText>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            BoolUtil.Serialize(this.Ordered, bw);
            ObjectUtils.SerializeObject(this.Items, bw);

        }
    }
}
