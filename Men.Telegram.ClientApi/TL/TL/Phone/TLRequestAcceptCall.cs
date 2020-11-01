using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Phone
{
    [TLObject(1003664544)]
    public class TLRequestAcceptCall : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 1003664544;
            }
        }

        public TLInputPhoneCall Peer { get; set; }
        public byte[] GB { get; set; }
        public TLPhoneCallProtocol Protocol { get; set; }
        public Phone.TLPhoneCall Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Peer = (TLInputPhoneCall)ObjectUtils.DeserializeObject(br);
            this.GB = BytesUtil.Deserialize(br);
            this.Protocol = (TLPhoneCallProtocol)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            ObjectUtils.SerializeObject(this.Peer, bw);
            BytesUtil.Serialize(this.GB, bw);
            ObjectUtils.SerializeObject(this.Protocol, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            this.Response = (Phone.TLPhoneCall)ObjectUtils.DeserializeObject(br);

        }
    }
}
