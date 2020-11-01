using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(864953444)]
    public class TLRequestGetDocumentByHash : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 864953444;
            }
        }

        public byte[] Sha256 { get; set; }
        public int Size { get; set; }
        public string MimeType { get; set; }
        public TLAbsDocument Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Sha256 = BytesUtil.Deserialize(br);
            this.Size = br.ReadInt32();
            this.MimeType = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            BytesUtil.Serialize(this.Sha256, bw);
            bw.Write(this.Size);
            StringUtil.Serialize(this.MimeType, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            this.Response = (TLAbsDocument)ObjectUtils.DeserializeObject(br);

        }
    }
}
