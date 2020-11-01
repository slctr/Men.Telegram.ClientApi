using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Payments
{
    [TLObject(1314881805)]
    public class TLPaymentResult : TLAbsPaymentResult
    {
        public override int Constructor
        {
            get
            {
                return 1314881805;
            }
        }

        public TLAbsUpdates Updates { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Updates = (TLAbsUpdates)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            ObjectUtils.SerializeObject(this.Updates, bw);

        }
    }
}
