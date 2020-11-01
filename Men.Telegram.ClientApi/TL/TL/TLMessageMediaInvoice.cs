using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-2074799289)]
    public class TLMessageMediaInvoice : TLAbsMessageMedia
    {
        public override int Constructor
        {
            get
            {
                return -2074799289;
            }
        }

        public int Flags { get; set; }
        public bool ShippingAddressRequested { get; set; }
        public bool Test { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TLWebDocument Photo { get; set; }
        public int? ReceiptMsgId { get; set; }
        public string Currency { get; set; }
        public long TotalAmount { get; set; }
        public string StartParam { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.ShippingAddressRequested ? (this.Flags | 2) : (this.Flags & ~2);
            this.Flags = this.Test ? (this.Flags | 8) : (this.Flags & ~8);
            this.Flags = this.Photo != null ? (this.Flags | 1) : (this.Flags & ~1);
            this.Flags = this.ReceiptMsgId != null ? (this.Flags | 4) : (this.Flags & ~4);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            this.ShippingAddressRequested = (this.Flags & 2) != 0;
            this.Test = (this.Flags & 8) != 0;
            this.Title = StringUtil.Deserialize(br);
            this.Description = StringUtil.Deserialize(br);
            if ((this.Flags & 1) != 0)
            {
                this.Photo = (TLWebDocument)ObjectUtils.DeserializeObject(br);
            }
            else
            {
                this.Photo = null;
            }

            if ((this.Flags & 4) != 0)
            {
                this.ReceiptMsgId = br.ReadInt32();
            }
            else
            {
                this.ReceiptMsgId = null;
            }

            this.Currency = StringUtil.Deserialize(br);
            this.TotalAmount = br.ReadInt64();
            this.StartParam = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);


            StringUtil.Serialize(this.Title, bw);
            StringUtil.Serialize(this.Description, bw);
            if ((this.Flags & 1) != 0)
            {
                ObjectUtils.SerializeObject(this.Photo, bw);
            }

            if ((this.Flags & 4) != 0)
            {
                bw.Write(this.ReceiptMsgId.Value);
            }

            StringUtil.Serialize(this.Currency, bw);
            bw.Write(this.TotalAmount);
            StringUtil.Serialize(this.StartParam, bw);

        }
    }
}
