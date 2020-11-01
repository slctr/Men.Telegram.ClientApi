using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Payments
{
    [TLObject(1342771681)]
    public class TLPaymentReceipt : TLObject
    {
        public override int Constructor
        {
            get
            {
                return 1342771681;
            }
        }

        public int Flags { get; set; }
        public int Date { get; set; }
        public int BotId { get; set; }
        public TLInvoice Invoice { get; set; }
        public int ProviderId { get; set; }
        public TLPaymentRequestedInfo Info { get; set; }
        public TLShippingOption Shipping { get; set; }
        public string Currency { get; set; }
        public long TotalAmount { get; set; }
        public string CredentialsTitle { get; set; }
        public TLVector<TLAbsUser> Users { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.Info != null ? (this.Flags | 1) : (this.Flags & ~1);
            this.Flags = this.Shipping != null ? (this.Flags | 2) : (this.Flags & ~2);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            this.Date = br.ReadInt32();
            this.BotId = br.ReadInt32();
            this.Invoice = (TLInvoice)ObjectUtils.DeserializeObject(br);
            this.ProviderId = br.ReadInt32();
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
                this.Shipping = (TLShippingOption)ObjectUtils.DeserializeObject(br);
            }
            else
            {
                this.Shipping = null;
            }

            this.Currency = StringUtil.Deserialize(br);
            this.TotalAmount = br.ReadInt64();
            this.CredentialsTitle = StringUtil.Deserialize(br);
            this.Users = (TLVector<TLAbsUser>)ObjectUtils.DeserializeVector<TLAbsUser>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);
            bw.Write(this.Date);
            bw.Write(this.BotId);
            ObjectUtils.SerializeObject(this.Invoice, bw);
            bw.Write(this.ProviderId);
            if ((this.Flags & 1) != 0)
            {
                ObjectUtils.SerializeObject(this.Info, bw);
            }

            if ((this.Flags & 2) != 0)
            {
                ObjectUtils.SerializeObject(this.Shipping, bw);
            }

            StringUtil.Serialize(this.Currency, bw);
            bw.Write(this.TotalAmount);
            StringUtil.Serialize(this.CredentialsTitle, bw);
            ObjectUtils.SerializeObject(this.Users, bw);

        }
    }
}
