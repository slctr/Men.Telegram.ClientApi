using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(1527873830)]
    public class TLRequestReadFeaturedStickers : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 1527873830;
            }
        }

        public TLVector<long> Id { get; set; }
        public bool Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Id = (TLVector<long>)ObjectUtils.DeserializeVector<long>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            ObjectUtils.SerializeObject(this.Id, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            this.Response = BoolUtil.Deserialize(br);

        }
    }
}
