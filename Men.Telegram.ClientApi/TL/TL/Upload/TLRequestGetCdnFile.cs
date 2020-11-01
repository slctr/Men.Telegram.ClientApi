using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Upload
{
    [TLObject(536919235)]
    public class TLRequestGetCdnFile : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 536919235;
            }
        }

        public byte[] FileToken { get; set; }
        public int Offset { get; set; }
        public int Limit { get; set; }
        public Upload.TLAbsCdnFile Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.FileToken = BytesUtil.Deserialize(br);
            this.Offset = br.ReadInt32();
            this.Limit = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            BytesUtil.Serialize(this.FileToken, bw);
            bw.Write(this.Offset);
            bw.Write(this.Limit);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            this.Response = (Upload.TLAbsCdnFile)ObjectUtils.DeserializeObject(br);

        }
    }
}
