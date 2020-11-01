using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Payments
{
    [TLObject(730364339)]
    public class TLRequestSendPaymentForm : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 730364339;
            }
        }

        public int Flags { get; set; }
        public int MsgId { get; set; }
        public string RequestedInfoId { get; set; }
        public string ShippingOptionId { get; set; }
        public TLAbsInputPaymentCredentials Credentials { get; set; }
        public Payments.TLAbsPaymentResult Response { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.RequestedInfoId != null ? (this.Flags | 1) : (this.Flags & ~1);
            this.Flags = this.ShippingOptionId != null ? (this.Flags | 2) : (this.Flags & ~2);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            this.MsgId = br.ReadInt32();
            if ((this.Flags & 1) != 0)
                this.RequestedInfoId = StringUtil.Deserialize(br);
            else
                this.RequestedInfoId = null;

            if ((this.Flags & 2) != 0)
                this.ShippingOptionId = StringUtil.Deserialize(br);
            else
                this.ShippingOptionId = null;

            this.Credentials = (TLAbsInputPaymentCredentials)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);
            bw.Write(this.MsgId);
            if ((this.Flags & 1) != 0)
                StringUtil.Serialize(this.RequestedInfoId, bw);
            if ((this.Flags & 2) != 0)
                StringUtil.Serialize(this.ShippingOptionId, bw);
            ObjectUtils.SerializeObject(this.Credentials, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            this.Response = (Payments.TLAbsPaymentResult)ObjectUtils.DeserializeObject(br);

        }
    }
}
