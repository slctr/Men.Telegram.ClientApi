using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1868808300)]
    public class TLPaymentRequestedInfo : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -1868808300;
            }
        }

        public int Flags { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public TLPostAddress ShippingAddress { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.Name != null ? (this.Flags | 1) : (this.Flags & ~1);
            this.Flags = this.Phone != null ? (this.Flags | 2) : (this.Flags & ~2);
            this.Flags = this.Email != null ? (this.Flags | 4) : (this.Flags & ~4);
            this.Flags = this.ShippingAddress != null ? (this.Flags | 8) : (this.Flags & ~8);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            if ((this.Flags & 1) != 0)
            {
                this.Name = StringUtil.Deserialize(br);
            }
            else
            {
                this.Name = null;
            }

            if ((this.Flags & 2) != 0)
            {
                this.Phone = StringUtil.Deserialize(br);
            }
            else
            {
                this.Phone = null;
            }

            if ((this.Flags & 4) != 0)
            {
                this.Email = StringUtil.Deserialize(br);
            }
            else
            {
                this.Email = null;
            }

            if ((this.Flags & 8) != 0)
            {
                this.ShippingAddress = (TLPostAddress)ObjectUtils.DeserializeObject(br);
            }
            else
            {
                this.ShippingAddress = null;
            }
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);
            if ((this.Flags & 1) != 0)
            {
                StringUtil.Serialize(this.Name, bw);
            }

            if ((this.Flags & 2) != 0)
            {
                StringUtil.Serialize(this.Phone, bw);
            }

            if ((this.Flags & 4) != 0)
            {
                StringUtil.Serialize(this.Email, bw);
            }

            if ((this.Flags & 8) != 0)
            {
                ObjectUtils.SerializeObject(this.ShippingAddress, bw);
            }
        }
    }
}
