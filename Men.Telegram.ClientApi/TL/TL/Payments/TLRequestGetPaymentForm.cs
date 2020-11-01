using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Payments
{
    [TLObject(-1712285883)]
    public class TLRequestGetPaymentForm : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -1712285883;
            }
        }

        public int MsgId { get; set; }
        public Payments.TLPaymentForm Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.MsgId = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            bw.Write(this.MsgId);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            this.Response = (Payments.TLPaymentForm)ObjectUtils.DeserializeObject(br);

        }
    }
}