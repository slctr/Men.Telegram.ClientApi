using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(852135825)]
    public class TLRequestGetWebPage : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 852135825;
            }
        }

        public string Url { get; set; }
        public int Hash { get; set; }
        public TLAbsWebPage Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Url = StringUtil.Deserialize(br);
            this.Hash = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            StringUtil.Serialize(this.Url, bw);
            bw.Write(this.Hash);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            this.Response = (TLAbsWebPage)ObjectUtils.DeserializeObject(br);

        }
    }
}
