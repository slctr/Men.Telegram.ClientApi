using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(-302170017)]
    public class TLAllStickers : TLAbsAllStickers
    {
        public override int Constructor
        {
            get
            {
                return -302170017;
            }
        }

        public int Hash { get; set; }
        public TLVector<TLStickerSet> Sets { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Hash = br.ReadInt32();
            this.Sets = (TLVector<TLStickerSet>)ObjectUtils.DeserializeVector<TLStickerSet>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            bw.Write(this.Hash);
            ObjectUtils.SerializeObject(this.Sets, bw);

        }
    }
}
