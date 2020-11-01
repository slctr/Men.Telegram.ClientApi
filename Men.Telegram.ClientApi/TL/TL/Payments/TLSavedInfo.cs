using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Payments
{
    [TLObject(-74456004)]
    public class TLSavedInfo : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -74456004;
            }
        }

        public int Flags { get; set; }
        public bool HasSavedCredentials { get; set; }
        public TLPaymentRequestedInfo SavedInfo { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.HasSavedCredentials ? (this.Flags | 2) : (this.Flags & ~2);
            this.Flags = this.SavedInfo != null ? (this.Flags | 1) : (this.Flags & ~1);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            this.HasSavedCredentials = (this.Flags & 2) != 0;
            if ((this.Flags & 1) != 0)
            {
                this.SavedInfo = (TLPaymentRequestedInfo)ObjectUtils.DeserializeObject(br);
            }
            else
            {
                this.SavedInfo = null;
            }
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);

            if ((this.Flags & 1) != 0)
            {
                ObjectUtils.SerializeObject(this.SavedInfo, bw);
            }
        }
    }
}
