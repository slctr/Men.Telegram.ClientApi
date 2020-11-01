using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(913498268)]
    public class TLRequestGetPeerSettings : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 913498268;
            }
        }

        public TLAbsInputPeer Peer { get; set; }
        public TLPeerSettings Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Peer = (TLAbsInputPeer)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            ObjectUtils.SerializeObject(this.Peer, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            this.Response = (TLPeerSettings)ObjectUtils.DeserializeObject(br);

        }
    }
}
