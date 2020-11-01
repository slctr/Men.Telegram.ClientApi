using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-2132731265)]
    public class TLMessageActionPhoneCall : TLAbsMessageAction
    {
        public override int Constructor
        {
            get
            {
                return -2132731265;
            }
        }

        public int Flags { get; set; }
        public long CallId { get; set; }
        public TLAbsPhoneCallDiscardReason Reason { get; set; }
        public int? Duration { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.Reason != null ? (this.Flags | 1) : (this.Flags & ~1);
            this.Flags = this.Duration != null ? (this.Flags | 2) : (this.Flags & ~2);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            this.CallId = br.ReadInt64();
            if ((this.Flags & 1) != 0)
            {
                this.Reason = (TLAbsPhoneCallDiscardReason)ObjectUtils.DeserializeObject(br);
            }
            else
            {
                this.Reason = null;
            }

            if ((this.Flags & 2) != 0)
            {
                this.Duration = br.ReadInt32();
            }
            else
            {
                this.Duration = null;
            }
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);
            bw.Write(this.CallId);
            if ((this.Flags & 1) != 0)
            {
                ObjectUtils.SerializeObject(this.Reason, bw);
            }

            if ((this.Flags & 2) != 0)
            {
                bw.Write(this.Duration.Value);
            }
        }
    }
}
