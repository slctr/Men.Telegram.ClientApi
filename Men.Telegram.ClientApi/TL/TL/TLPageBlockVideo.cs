using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-640214938)]
    public class TLPageBlockVideo : TLAbsPageBlock
    {
        public override int Constructor
        {
            get
            {
                return -640214938;
            }
        }

        public int Flags { get; set; }
        public bool Autoplay { get; set; }
        public bool Loop { get; set; }
        public long VideoId { get; set; }
        public TLAbsRichText Caption { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.Autoplay ? (this.Flags | 1) : (this.Flags & ~1);
            this.Flags = this.Loop ? (this.Flags | 2) : (this.Flags & ~2);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            this.Autoplay = (this.Flags & 1) != 0;
            this.Loop = (this.Flags & 2) != 0;
            this.VideoId = br.ReadInt64();
            this.Caption = (TLAbsRichText)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);


            bw.Write(this.VideoId);
            ObjectUtils.SerializeObject(this.Caption, bw);

        }
    }
}
