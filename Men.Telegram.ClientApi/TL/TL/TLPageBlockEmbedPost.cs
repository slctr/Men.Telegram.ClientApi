using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(690781161)]
    public class TLPageBlockEmbedPost : TLAbsPageBlock
    {
        public override int Constructor
        {
            get
            {
                return 690781161;
            }
        }

        public string Url { get; set; }
        public long WebpageId { get; set; }
        public long AuthorPhotoId { get; set; }
        public string Author { get; set; }
        public int Date { get; set; }
        public TLVector<TLAbsPageBlock> Blocks { get; set; }
        public TLAbsRichText Caption { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Url = StringUtil.Deserialize(br);
            this.WebpageId = br.ReadInt64();
            this.AuthorPhotoId = br.ReadInt64();
            this.Author = StringUtil.Deserialize(br);
            this.Date = br.ReadInt32();
            this.Blocks = (TLVector<TLAbsPageBlock>)ObjectUtils.DeserializeVector<TLAbsPageBlock>(br);
            this.Caption = (TLAbsRichText)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            StringUtil.Serialize(this.Url, bw);
            bw.Write(this.WebpageId);
            bw.Write(this.AuthorPhotoId);
            StringUtil.Serialize(this.Author, bw);
            bw.Write(this.Date);
            ObjectUtils.SerializeObject(this.Blocks, bw);
            ObjectUtils.SerializeObject(this.Caption, bw);

        }
    }
}
