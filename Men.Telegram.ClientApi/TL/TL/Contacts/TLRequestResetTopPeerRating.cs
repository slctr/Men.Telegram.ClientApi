using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Contacts
{
    [TLObject(451113900)]
    public class TLRequestResetTopPeerRating : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 451113900;
            }
        }

        public TLAbsTopPeerCategory Category { get; set; }
        public TLAbsInputPeer Peer { get; set; }
        public bool Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Category = (TLAbsTopPeerCategory)ObjectUtils.DeserializeObject(br);
            this.Peer = (TLAbsInputPeer)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            ObjectUtils.SerializeObject(this.Category, bw);
            ObjectUtils.SerializeObject(this.Peer, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            this.Response = BoolUtil.Deserialize(br);

        }
    }
}
