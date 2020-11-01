using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(872932635)]
    public class TLStickerSetMultiCovered : TLAbsStickerSetCovered
    {
        public override int Constructor
        {
            get
            {
                return 872932635;
            }
        }

        public TLStickerSet Set { get; set; }
        public TLVector<TLAbsDocument> Covers { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Set = (TLStickerSet)ObjectUtils.DeserializeObject(br);
            this.Covers = (TLVector<TLAbsDocument>)ObjectUtils.DeserializeVector<TLAbsDocument>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            ObjectUtils.SerializeObject(this.Set, bw);
            ObjectUtils.SerializeObject(this.Covers, bw);

        }
    }
}
