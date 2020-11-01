using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(1035731989)]
    public class TLRequestAcceptEncryption : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 1035731989;
            }
        }

        public TLInputEncryptedChat Peer { get; set; }
        public byte[] GB { get; set; }
        public long KeyFingerprint { get; set; }
        public TLAbsEncryptedChat Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Peer = (TLInputEncryptedChat)ObjectUtils.DeserializeObject(br);
            this.GB = BytesUtil.Deserialize(br);
            this.KeyFingerprint = br.ReadInt64();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            ObjectUtils.SerializeObject(this.Peer, bw);
            BytesUtil.Serialize(this.GB, bw);
            bw.Write(this.KeyFingerprint);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            this.Response = (TLAbsEncryptedChat)ObjectUtils.DeserializeObject(br);

        }
    }
}
