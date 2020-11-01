using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(-162681021)]
    public class TLRequestRequestEncryption : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -162681021;
            }
        }

        public TLAbsInputUser UserId { get; set; }
        public int RandomId { get; set; }
        public byte[] GA { get; set; }
        public TLAbsEncryptedChat Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.UserId = (TLAbsInputUser)ObjectUtils.DeserializeObject(br);
            this.RandomId = br.ReadInt32();
            this.GA = BytesUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            ObjectUtils.SerializeObject(this.UserId, bw);
            bw.Write(this.RandomId);
            BytesUtil.Serialize(this.GA, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            this.Response = (TLAbsEncryptedChat)ObjectUtils.DeserializeObject(br);

        }
    }
}
