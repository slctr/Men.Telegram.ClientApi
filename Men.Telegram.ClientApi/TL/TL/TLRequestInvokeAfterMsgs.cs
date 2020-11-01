using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(1036301552)]
    public class TLRequestInvokeAfterMsgs : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 1036301552;
            }
        }

        public TLVector<long> MsgIds { get; set; }
        public TLObject Query { get; set; }
        public TLObject Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.MsgIds = (TLVector<long>)ObjectUtils.DeserializeVector<long>(br);
            this.Query = (TLObject)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            ObjectUtils.SerializeObject(this.MsgIds, bw);
            ObjectUtils.SerializeObject(this.Query, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            this.Response = (TLObject)ObjectUtils.DeserializeObject(br);

        }
    }
}
