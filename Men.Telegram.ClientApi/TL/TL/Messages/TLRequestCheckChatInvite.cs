using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(1051570619)]
    public class TLRequestCheckChatInvite : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 1051570619;
            }
        }

        public string Hash { get; set; }
        public TLAbsChatInvite Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Hash = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            StringUtil.Serialize(this.Hash, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            this.Response = (TLAbsChatInvite)ObjectUtils.DeserializeObject(br);

        }
    }
}
