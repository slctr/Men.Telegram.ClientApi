using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(1355435489)]
    public class TLPhoneCallDiscarded : TLAbsPhoneCall
    {
        public override int Constructor
        {
            get
            {
                return 1355435489;
            }
        }

        public int Flags { get; set; }
        public bool NeedRating { get; set; }
        public bool NeedDebug { get; set; }
        public long Id { get; set; }
        public TLAbsPhoneCallDiscardReason Reason { get; set; }
        public int? Duration { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.NeedRating ? (this.Flags | 4) : (this.Flags & ~4);
            this.Flags = this.NeedDebug ? (this.Flags | 8) : (this.Flags & ~8);
            this.Flags = this.Reason != null ? (this.Flags | 1) : (this.Flags & ~1);
            this.Flags = this.Duration != null ? (this.Flags | 2) : (this.Flags & ~2);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            this.NeedRating = (this.Flags & 4) != 0;
            this.NeedDebug = (this.Flags & 8) != 0;
            this.Id = br.ReadInt64();
            if ((this.Flags & 1) != 0)
                this.Reason = (TLAbsPhoneCallDiscardReason)ObjectUtils.DeserializeObject(br);
            else
                this.Reason = null;

            if ((this.Flags & 2) != 0)
                this.Duration = br.ReadInt32();
            else
                this.Duration = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);


            bw.Write(this.Id);
            if ((this.Flags & 1) != 0)
                ObjectUtils.SerializeObject(this.Reason, bw);
            if ((this.Flags & 2) != 0)
                bw.Write(this.Duration.Value);

        }
    }
}
