using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(-1240849242)]
    public class TLStickerSet : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -1240849242;
            }
        }

        public TL.TLStickerSet Set { get; set; }
        public TLVector<TLStickerPack> Packs { get; set; }
        public TLVector<TLAbsDocument> Documents { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Set = (TL.TLStickerSet)ObjectUtils.DeserializeObject(br);
            this.Packs = (TLVector<TLStickerPack>)ObjectUtils.DeserializeVector<TLStickerPack>(br);
            this.Documents = (TLVector<TLAbsDocument>)ObjectUtils.DeserializeVector<TLAbsDocument>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            ObjectUtils.SerializeObject(this.Set, bw);
            ObjectUtils.SerializeObject(this.Packs, bw);
            ObjectUtils.SerializeObject(this.Documents, bw);

        }
    }
}
