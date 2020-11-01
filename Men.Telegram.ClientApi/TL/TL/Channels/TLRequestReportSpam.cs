using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Channels
{
    [TLObject(-32999408)]
    public class TLRequestReportSpam : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -32999408;
            }
        }

        public TLAbsInputChannel Channel { get; set; }
        public TLAbsInputUser UserId { get; set; }
        public TLVector<int> Id { get; set; }
        public bool Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Channel = (TLAbsInputChannel)ObjectUtils.DeserializeObject(br);
            this.UserId = (TLAbsInputUser)ObjectUtils.DeserializeObject(br);
            this.Id = (TLVector<int>)ObjectUtils.DeserializeVector<int>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            ObjectUtils.SerializeObject(this.Channel, bw);
            ObjectUtils.SerializeObject(this.UserId, bw);
            ObjectUtils.SerializeObject(this.Id, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            this.Response = BoolUtil.Deserialize(br);

        }
    }
}
