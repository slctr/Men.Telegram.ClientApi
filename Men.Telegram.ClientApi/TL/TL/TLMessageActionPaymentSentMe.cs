using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1892568281)]
    public class TLMessageActionPaymentSentMe : TLAbsMessageAction
    {
        public override int Constructor
        {
            get
            {
                return -1892568281;
            }
        }

        public int Flags { get; set; }
        public string Currency { get; set; }
        public long TotalAmount { get; set; }
        public byte[] Payload { get; set; }
        public TLPaymentRequestedInfo Info { get; set; }
        public string ShippingOptionId { get; set; }
        public TLPaymentCharge Charge { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.Info != null ? (this.Flags | 1) : (this.Flags & ~1);
            this.Flags = this.ShippingOptionId != null ? (this.Flags | 2) : (this.Flags & ~2);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            this.Currency = StringUtil.Deserialize(br);
            this.TotalAmount = br.ReadInt64();
            this.Payload = BytesUtil.Deserialize(br);
            if ((this.Flags & 1) != 0)
                this.Info = (TLPaymentRequestedInfo)ObjectUtils.DeserializeObject(br);
            else
                this.Info = null;

            if ((this.Flags & 2) != 0)
                this.ShippingOptionId = StringUtil.Deserialize(br);
            else
                this.ShippingOptionId = null;

            this.Charge = (TLPaymentCharge)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);
            StringUtil.Serialize(this.Currency, bw);
            bw.Write(this.TotalAmount);
            BytesUtil.Serialize(this.Payload, bw);
            if ((this.Flags & 1) != 0)
                ObjectUtils.SerializeObject(this.Info, bw);
            if ((this.Flags & 2) != 0)
                StringUtil.Serialize(this.ShippingOptionId, bw);
            ObjectUtils.SerializeObject(this.Charge, bw);

        }
    }
}
