using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(-1451792525)]
    public class TLRequestSendEncrypted : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -1451792525;
            }
        }

        public TLInputEncryptedChat Peer { get; set; }
        public long RandomId { get; set; }
        public byte[] Data { get; set; }
        public Messages.TLAbsSentEncryptedMessage Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Peer = (TLInputEncryptedChat)ObjectUtils.DeserializeObject(br);
            this.RandomId = br.ReadInt64();
            this.Data = BytesUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            ObjectUtils.SerializeObject(this.Peer, bw);
            bw.Write(this.RandomId);
            BytesUtil.Serialize(this.Data, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            this.Response = (Messages.TLAbsSentEncryptedMessage)ObjectUtils.DeserializeObject(br);

        }
    }
}
