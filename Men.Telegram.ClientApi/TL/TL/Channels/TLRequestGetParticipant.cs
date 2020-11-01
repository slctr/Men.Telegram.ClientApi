using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Channels
{
    [TLObject(1416484774)]
    public class TLRequestGetParticipant : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 1416484774;
            }
        }

        public TLAbsInputChannel Channel { get; set; }
        public TLAbsInputUser UserId { get; set; }
        public Channels.TLChannelParticipant Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Channel = (TLAbsInputChannel)ObjectUtils.DeserializeObject(br);
            this.UserId = (TLAbsInputUser)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            ObjectUtils.SerializeObject(this.Channel, bw);
            ObjectUtils.SerializeObject(this.UserId, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            this.Response = (Channels.TLChannelParticipant)ObjectUtils.DeserializeObject(br);

        }
    }
}
