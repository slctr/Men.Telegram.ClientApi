using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Upload
{
    [TLObject(779755552)]
    public class TLRequestReuploadCdnFile : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 779755552;
            }
        }

        public byte[] FileToken { get; set; }
        public byte[] RequestToken { get; set; }
        public bool Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.FileToken = BytesUtil.Deserialize(br);
            this.RequestToken = BytesUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            BytesUtil.Serialize(this.FileToken, bw);
            BytesUtil.Serialize(this.RequestToken, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            this.Response = BoolUtil.Deserialize(br);

        }
    }
}
