using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(1009288385)]
    public class TLTextUrl : TLAbsRichText
    {
        public override int Constructor
        {
            get
            {
                return 1009288385;
            }
        }

        public TLAbsRichText Text { get; set; }
        public string Url { get; set; }
        public long WebpageId { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Text = (TLAbsRichText)ObjectUtils.DeserializeObject(br);
            this.Url = StringUtil.Deserialize(br);
            this.WebpageId = br.ReadInt64();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            ObjectUtils.SerializeObject(this.Text, bw);
            StringUtil.Serialize(this.Url, bw);
            bw.Write(this.WebpageId);

        }
    }
}
