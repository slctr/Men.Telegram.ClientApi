using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Channels
{
    [TLObject(-1502421484)]
    public class TLRequestKickFromChannel : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -1502421484;
            }
        }

        public TLAbsInputChannel Channel { get; set; }
        public TLAbsInputUser UserId { get; set; }
        public bool Kicked { get; set; }
        public TLAbsUpdates Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Channel = (TLAbsInputChannel)ObjectUtils.DeserializeObject(br);
            this.UserId = (TLAbsInputUser)ObjectUtils.DeserializeObject(br);
            this.Kicked = BoolUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            ObjectUtils.SerializeObject(this.Channel, bw);
            ObjectUtils.SerializeObject(this.UserId, bw);
            BoolUtil.Serialize(this.Kicked, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            this.Response = (TLAbsUpdates)ObjectUtils.DeserializeObject(br);

        }
    }
}
