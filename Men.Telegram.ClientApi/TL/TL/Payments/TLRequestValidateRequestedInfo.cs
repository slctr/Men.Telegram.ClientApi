using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Payments
{
    [TLObject(1997180532)]
    public class TLRequestValidateRequestedInfo : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 1997180532;
            }
        }

        public int Flags { get; set; }
        public bool Save { get; set; }
        public int MsgId { get; set; }
        public TLPaymentRequestedInfo Info { get; set; }
        public Payments.TLValidatedRequestedInfo Response { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.Save ? (this.Flags | 1) : (this.Flags & ~1);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            this.Save = (this.Flags & 1) != 0;
            this.MsgId = br.ReadInt32();
            this.Info = (TLPaymentRequestedInfo)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);

            bw.Write(this.MsgId);
            ObjectUtils.SerializeObject(this.Info, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            this.Response = (Payments.TLValidatedRequestedInfo)ObjectUtils.DeserializeObject(br);

        }
    }
}
