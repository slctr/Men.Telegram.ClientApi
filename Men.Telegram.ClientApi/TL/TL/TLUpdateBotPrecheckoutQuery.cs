using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(1563376297)]
    public class TLUpdateBotPrecheckoutQuery : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return 1563376297;
            }
        }

        public int Flags { get; set; }
        public long QueryId { get; set; }
        public int UserId { get; set; }
        public byte[] Payload { get; set; }
        public TLPaymentRequestedInfo Info { get; set; }
        public string ShippingOptionId { get; set; }
        public string Currency { get; set; }
        public long TotalAmount { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.Info != null ? (this.Flags | 1) : (this.Flags & ~1);
            this.Flags = this.ShippingOptionId != null ? (this.Flags | 2) : (this.Flags & ~2);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            this.QueryId = br.ReadInt64();
            this.UserId = br.ReadInt32();
            this.Payload = BytesUtil.Deserialize(br);
            if ((this.Flags & 1) != 0)
            {
                this.Info = (TLPaymentRequestedInfo)ObjectUtils.DeserializeObject(br);
            }
            else
            {
                this.Info = null;
            }

            if ((this.Flags & 2) != 0)
            {
                this.ShippingOptionId = StringUtil.Deserialize(br);
            }
            else
            {
                this.ShippingOptionId = null;
            }

            this.Currency = StringUtil.Deserialize(br);
            this.TotalAmount = br.ReadInt64();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);
            bw.Write(this.QueryId);
            bw.Write(this.UserId);
            BytesUtil.Serialize(this.Payload, bw);
            if ((this.Flags & 1) != 0)
            {
                ObjectUtils.SerializeObject(this.Info, bw);
            }

            if ((this.Flags & 2) != 0)
            {
                StringUtil.Serialize(this.ShippingOptionId, bw);
            }

            StringUtil.Serialize(this.Currency, bw);
            bw.Write(this.TotalAmount);

        }
    }
}
