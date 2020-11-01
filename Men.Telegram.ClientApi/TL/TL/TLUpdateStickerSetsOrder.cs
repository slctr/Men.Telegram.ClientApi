using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(196268545)]
    public class TLUpdateStickerSetsOrder : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return 196268545;
            }
        }

        public int Flags { get; set; }
        public bool Masks { get; set; }
        public TLVector<long> Order { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.Masks ? (this.Flags | 1) : (this.Flags & ~1);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            this.Masks = (this.Flags & 1) != 0;
            this.Order = (TLVector<long>)ObjectUtils.DeserializeVector<long>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);

            ObjectUtils.SerializeObject(this.Order, bw);

        }
    }
}
