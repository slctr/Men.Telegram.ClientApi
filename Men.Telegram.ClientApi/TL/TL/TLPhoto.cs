using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1836524247)]
    public class TLPhoto : TLAbsPhoto
    {
        public override int Constructor
        {
            get
            {
                return -1836524247;
            }
        }

        public int Flags { get; set; }
        public bool HasStickers { get; set; }
        public long Id { get; set; }
        public long AccessHash { get; set; }
        public int Date { get; set; }
        public TLVector<TLAbsPhotoSize> Sizes { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.HasStickers ? (this.Flags | 1) : (this.Flags & ~1);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            this.HasStickers = (this.Flags & 1) != 0;
            this.Id = br.ReadInt64();
            this.AccessHash = br.ReadInt64();
            this.Date = br.ReadInt32();
            this.Sizes = (TLVector<TLAbsPhotoSize>)ObjectUtils.DeserializeVector<TLAbsPhotoSize>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);

            bw.Write(this.Id);
            bw.Write(this.AccessHash);
            bw.Write(this.Date);
            ObjectUtils.SerializeObject(this.Sizes, bw);

        }
    }
}
