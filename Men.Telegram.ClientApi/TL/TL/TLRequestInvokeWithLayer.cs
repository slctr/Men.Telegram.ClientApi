using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-627372787)]
    public class TLRequestInvokeWithLayer : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -627372787;
            }
        }

        public int Layer { get; set; }
        public TLObject Query { get; set; }
        public TLObject Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Layer = br.ReadInt32();
            this.Query = (TLObject)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            bw.Write(this.Layer);
            ObjectUtils.SerializeObject(this.Query, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            this.Response = (TLObject)ObjectUtils.DeserializeObject(br);

        }
    }
}
