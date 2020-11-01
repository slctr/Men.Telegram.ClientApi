using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Auth
{
    [TLObject(174260510)]
    public class TLRequestCheckPassword : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 174260510;
            }
        }

        public byte[] PasswordHash { get; set; }
        public Auth.TLAuthorization Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.PasswordHash = BytesUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            BytesUtil.Serialize(this.PasswordHash, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            this.Response = (Auth.TLAuthorization)ObjectUtils.DeserializeObject(br);

        }
    }
}
