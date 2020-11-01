using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(238054714)]
    public class TLRequestReadHistory : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 238054714;
            }
        }

        public TLAbsInputPeer Peer { get; set; }
        public int MaxId { get; set; }
        public Messages.TLAffectedMessages Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Peer = (TLAbsInputPeer)ObjectUtils.DeserializeObject(br);
            this.MaxId = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            ObjectUtils.SerializeObject(this.Peer, bw);
            bw.Write(this.MaxId);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            this.Response = (Messages.TLAffectedMessages)ObjectUtils.DeserializeObject(br);

        }
    }
}
