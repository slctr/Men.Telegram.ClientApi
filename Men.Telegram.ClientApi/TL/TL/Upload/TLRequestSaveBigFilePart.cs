using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Upload
{
    [TLObject(-562337987)]
    public class TLRequestSaveBigFilePart : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -562337987;
            }
        }

        public long FileId { get; set; }
        public int FilePart { get; set; }
        public int FileTotalParts { get; set; }
        public byte[] Bytes { get; set; }
        public bool Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.FileId = br.ReadInt64();
            this.FilePart = br.ReadInt32();
            this.FileTotalParts = br.ReadInt32();
            this.Bytes = BytesUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            bw.Write(this.FileId);
            bw.Write(this.FilePart);
            bw.Write(this.FileTotalParts);
            BytesUtil.Serialize(this.Bytes, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            this.Response = BoolUtil.Deserialize(br);

        }
    }
}
