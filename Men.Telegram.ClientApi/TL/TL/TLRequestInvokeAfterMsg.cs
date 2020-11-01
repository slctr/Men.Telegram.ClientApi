using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-878758099)]
    public class TLRequestInvokeAfterMsg : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -878758099;
            }
        }

        public long MsgId { get; set; }
        public TLObject Query { get; set; }
        public TLObject Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.MsgId = br.ReadInt64();
            this.Query = (TLObject)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            bw.Write(this.MsgId);
            ObjectUtils.SerializeObject(this.Query, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            this.Response = (TLObject)ObjectUtils.DeserializeObject(br);

        }
    }
}
