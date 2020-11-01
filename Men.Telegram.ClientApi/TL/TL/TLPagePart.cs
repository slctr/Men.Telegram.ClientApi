using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1913754556)]
    public class TLPagePart : TLAbsPage
    {
        public override int Constructor
        {
            get
            {
                return -1913754556;
            }
        }

        public TLVector<TLAbsPageBlock> Blocks { get; set; }
        public TLVector<TLAbsPhoto> Photos { get; set; }
        public TLVector<TLAbsDocument> Videos { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Blocks = (TLVector<TLAbsPageBlock>)ObjectUtils.DeserializeVector<TLAbsPageBlock>(br);
            this.Photos = (TLVector<TLAbsPhoto>)ObjectUtils.DeserializeVector<TLAbsPhoto>(br);
            this.Videos = (TLVector<TLAbsDocument>)ObjectUtils.DeserializeVector<TLAbsDocument>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            ObjectUtils.SerializeObject(this.Blocks, bw);
            ObjectUtils.SerializeObject(this.Photos, bw);
            ObjectUtils.SerializeObject(this.Videos, bw);

        }
    }
}
