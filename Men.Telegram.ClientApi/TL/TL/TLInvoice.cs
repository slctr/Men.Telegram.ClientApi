using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1022713000)]
    public class TLInvoice : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -1022713000;
            }
        }

        public int Flags { get; set; }
        public bool Test { get; set; }
        public bool NameRequested { get; set; }
        public bool PhoneRequested { get; set; }
        public bool EmailRequested { get; set; }
        public bool ShippingAddressRequested { get; set; }
        public bool Flexible { get; set; }
        public string Currency { get; set; }
        public TLVector<TLLabeledPrice> Prices { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.Test ? (this.Flags | 1) : (this.Flags & ~1);
            this.Flags = this.NameRequested ? (this.Flags | 2) : (this.Flags & ~2);
            this.Flags = this.PhoneRequested ? (this.Flags | 4) : (this.Flags & ~4);
            this.Flags = this.EmailRequested ? (this.Flags | 8) : (this.Flags & ~8);
            this.Flags = this.ShippingAddressRequested ? (this.Flags | 16) : (this.Flags & ~16);
            this.Flags = this.Flexible ? (this.Flags | 32) : (this.Flags & ~32);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            this.Test = (this.Flags & 1) != 0;
            this.NameRequested = (this.Flags & 2) != 0;
            this.PhoneRequested = (this.Flags & 4) != 0;
            this.EmailRequested = (this.Flags & 8) != 0;
            this.ShippingAddressRequested = (this.Flags & 16) != 0;
            this.Flexible = (this.Flags & 32) != 0;
            this.Currency = StringUtil.Deserialize(br);
            this.Prices = (TLVector<TLLabeledPrice>)ObjectUtils.DeserializeVector<TLLabeledPrice>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);






            StringUtil.Serialize(this.Currency, bw);
            ObjectUtils.SerializeObject(this.Prices, bw);

        }
    }
}
