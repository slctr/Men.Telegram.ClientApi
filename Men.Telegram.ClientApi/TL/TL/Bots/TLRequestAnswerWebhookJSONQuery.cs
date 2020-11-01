using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Bots
{
    [TLObject(-434028723)]
    public class TLRequestAnswerWebhookJSONQuery : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -434028723;
            }
        }

        public long QueryId { get; set; }
        public TLDataJSON Data { get; set; }
        public bool Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.QueryId = br.ReadInt64();
            this.Data = (TLDataJSON)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            bw.Write(this.QueryId);
            ObjectUtils.SerializeObject(this.Data, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            this.Response = BoolUtil.Deserialize(br);

        }
    }
}
