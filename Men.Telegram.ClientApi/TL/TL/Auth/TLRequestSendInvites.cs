using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Auth
{
    [TLObject(1998331287)]
    public class TLRequestSendInvites : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 1998331287;
            }
        }

        public TLVector<string> PhoneNumbers { get; set; }
        public string Message { get; set; }
        public bool Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.PhoneNumbers = (TLVector<string>)ObjectUtils.DeserializeVector<string>(br);
            this.Message = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            ObjectUtils.SerializeObject(this.PhoneNumbers, bw);
            StringUtil.Serialize(this.Message, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            this.Response = BoolUtil.Deserialize(br);

        }
    }
}
