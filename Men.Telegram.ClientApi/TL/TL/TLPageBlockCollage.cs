using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(145955919)]
    public class TLPageBlockCollage : TLAbsPageBlock
    {
        public override int Constructor
        {
            get
            {
                return 145955919;
            }
        }

        public TLVector<TLAbsPageBlock> Items { get; set; }
        public TLAbsRichText Caption { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Items = (TLVector<TLAbsPageBlock>)ObjectUtils.DeserializeVector<TLAbsPageBlock>(br);
            this.Caption = (TLAbsRichText)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            ObjectUtils.SerializeObject(this.Items, bw);
            ObjectUtils.SerializeObject(this.Caption, bw);

        }
    }
}
