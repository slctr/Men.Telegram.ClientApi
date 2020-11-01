using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(1558317424)]
    public class TLRecentStickers : TLAbsRecentStickers
    {
        public override int Constructor
        {
            get
            {
                return 1558317424;
            }
        }

        public int Hash { get; set; }
        public TLVector<TLAbsDocument> Stickers { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Hash = br.ReadInt32();
            this.Stickers = (TLVector<TLAbsDocument>)ObjectUtils.DeserializeVector<TLAbsDocument>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            bw.Write(this.Hash);
            ObjectUtils.SerializeObject(this.Stickers, bw);

        }
    }
}
