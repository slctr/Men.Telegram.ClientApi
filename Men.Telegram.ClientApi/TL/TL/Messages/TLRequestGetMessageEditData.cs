using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(-39416522)]
    public class TLRequestGetMessageEditData : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -39416522;
            }
        }

        public TLAbsInputPeer Peer { get; set; }
        public int Id { get; set; }
        public Messages.TLMessageEditData Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Peer = (TLAbsInputPeer)ObjectUtils.DeserializeObject(br);
            this.Id = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            ObjectUtils.SerializeObject(this.Peer, bw);
            bw.Write(this.Id);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            this.Response = (Messages.TLMessageEditData)ObjectUtils.DeserializeObject(br);

        }
    }
}
