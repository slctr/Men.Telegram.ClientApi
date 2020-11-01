using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(-436833542)]
    public class TLRequestSetBotShippingResults : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -436833542;
            }
        }

        public int Flags { get; set; }
        public long QueryId { get; set; }
        public string Error { get; set; }
        public TLVector<TLShippingOption> ShippingOptions { get; set; }
        public bool Response { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.Error != null ? (this.Flags | 1) : (this.Flags & ~1);
            this.Flags = this.ShippingOptions != null ? (this.Flags | 2) : (this.Flags & ~2);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            this.QueryId = br.ReadInt64();
            if ((this.Flags & 1) != 0)
            {
                this.Error = StringUtil.Deserialize(br);
            }
            else
            {
                this.Error = null;
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
            bw.Write(this.QueryId);
            if ((this.Flags & 1) != 0)
            {
                StringUtil.Serialize(this.Error, bw);
            }

            if ((this.Flags & 2) != 0)
            {
                ObjectUtils.SerializeObject(this.ShippingOptions, bw);
            }
        }
        public override void DeserializeResponse(BinaryReader br)
        {
            this.Response = BoolUtil.Deserialize(br);

        }
    }
}
