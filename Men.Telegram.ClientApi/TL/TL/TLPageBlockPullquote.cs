using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(1329878739)]
    public class TLPageBlockPullquote : TLAbsPageBlock
    {
        public override int Constructor
        {
            get
            {
                return 1329878739;
            }
        }

        public TLAbsRichText Text { get; set; }
        public TLAbsRichText Caption { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Text = (TLAbsRichText)ObjectUtils.DeserializeObject(br);
            this.Caption = (TLAbsRichText)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            ObjectUtils.SerializeObject(this.Text, bw);
            ObjectUtils.SerializeObject(this.Caption, bw);

        }
    }
}
