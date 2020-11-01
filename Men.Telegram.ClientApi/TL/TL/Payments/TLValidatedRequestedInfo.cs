using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Payments
{
    [TLObject(-784000893)]
    public class TLValidatedRequestedInfo : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -784000893;
            }
        }

        public int Flags { get; set; }
        public string Id { get; set; }
        public TLVector<TLShippingOption> ShippingOptions { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.Id != null ? (this.Flags | 1) : (this.Flags & ~1);
            this.Flags = this.ShippingOptions != null ? (this.Flags | 2) : (this.Flags & ~2);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            if ((this.Flags & 1) != 0)
            {
                this.Id = StringUtil.Deserialize(br);
            }
            else
            {
                this.Id = null;
            }

            if ((this.Flags & 2) != 0)
            {
                this.ShippingOptions = (TLVector<TLShippingOption>)ObjectUtils.DeserializeVector<TLShippingOption>(br);
            }
            else
            {
                this.ShippingOptions = null;
            }
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);
            if ((this.Flags & 1) != 0)
            {
                StringUtil.Serialize(this.Id, bw);
            }

            if ((this.Flags & 2) != 0)
            {
                ObjectUtils.SerializeObject(this.ShippingOptions, bw);
            }
        }
    }
}
