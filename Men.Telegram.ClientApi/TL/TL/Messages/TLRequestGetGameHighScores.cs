using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(-400399203)]
    public class TLRequestGetGameHighScores : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -400399203;
            }
        }

        public TLAbsInputPeer Peer { get; set; }
        public int Id { get; set; }
        public TLAbsInputUser UserId { get; set; }
        public Messages.TLHighScores Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Peer = (TLAbsInputPeer)ObjectUtils.DeserializeObject(br);
            this.Id = br.ReadInt32();
            this.UserId = (TLAbsInputUser)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            ObjectUtils.SerializeObject(this.Peer, bw);
            bw.Write(this.Id);
            ObjectUtils.SerializeObject(this.UserId, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            this.Response = (Messages.TLHighScores)ObjectUtils.DeserializeObject(br);

        }
    }
}
