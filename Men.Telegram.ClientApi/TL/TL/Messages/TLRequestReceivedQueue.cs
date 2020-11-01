using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(1436924774)]
    public class TLRequestReceivedQueue : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 1436924774;
            }
        }

        public int MaxQts { get; set; }
        public TLVector<long> Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.MaxQts = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            bw.Write(this.MaxQts);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            this.Response = (TLVector<long>)ObjectUtils.DeserializeVector<long>(br);

        }
    }
}
