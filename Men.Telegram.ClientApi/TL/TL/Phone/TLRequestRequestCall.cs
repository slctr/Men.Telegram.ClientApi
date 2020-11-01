using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Phone
{
    [TLObject(1536537556)]
    public class TLRequestRequestCall : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 1536537556;
            }
        }

        public TLAbsInputUser UserId { get; set; }
        public int RandomId { get; set; }
        public byte[] GAHash { get; set; }
        public TLPhoneCallProtocol Protocol { get; set; }
        public Phone.TLPhoneCall Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.UserId = (TLAbsInputUser)ObjectUtils.DeserializeObject(br);
            this.RandomId = br.ReadInt32();
            this.GAHash = BytesUtil.Deserialize(br);
            this.Protocol = (TLPhoneCallProtocol)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            ObjectUtils.SerializeObject(this.UserId, bw);
            bw.Write(this.RandomId);
            BytesUtil.Serialize(this.GAHash, bw);
            ObjectUtils.SerializeObject(this.Protocol, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            this.Response = (Phone.TLPhoneCall)ObjectUtils.DeserializeObject(br);

        }
    }
}
