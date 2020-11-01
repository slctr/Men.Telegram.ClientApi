using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Auth
{
    [TLObject(-841733627)]
    public class TLRequestBindTempAuthKey : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -841733627;
            }
        }

        public long PermAuthKeyId { get; set; }
        public long Nonce { get; set; }
        public int ExpiresAt { get; set; }
        public byte[] EncryptedMessage { get; set; }
        public bool Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.PermAuthKeyId = br.ReadInt64();
            this.Nonce = br.ReadInt64();
            this.ExpiresAt = br.ReadInt32();
            this.EncryptedMessage = BytesUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            bw.Write(this.PermAuthKeyId);
            bw.Write(this.Nonce);
            bw.Write(this.ExpiresAt);
            BytesUtil.Serialize(this.EncryptedMessage, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            this.Response = BoolUtil.Deserialize(br);

        }
    }
}
