using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(-123893531)]
    public class TLFeaturedStickers : TLAbsFeaturedStickers
    {
        public override int Constructor
        {
            get
            {
                return -123893531;
            }
        }

        public int Hash { get; set; }
        public TLVector<TLAbsStickerSetCovered> Sets { get; set; }
        public TLVector<long> Unread { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Hash = br.ReadInt32();
            this.Sets = (TLVector<TLAbsStickerSetCovered>)ObjectUtils.DeserializeVector<TLAbsStickerSetCovered>(br);
            this.Unread = (TLVector<long>)ObjectUtils.DeserializeVector<long>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            bw.Write(this.Hash);
            ObjectUtils.SerializeObject(this.Sets, bw);
            ObjectUtils.SerializeObject(this.Unread, bw);

        }
    }
}
