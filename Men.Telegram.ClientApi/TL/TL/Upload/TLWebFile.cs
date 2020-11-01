using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Upload
{
    [TLObject(568808380)]
    public class TLWebFile : TLObject
    {
        public override int Constructor
        {
            get
            {
                return 568808380;
            }
        }

        public int Size { get; set; }
        public string MimeType { get; set; }
        public Storage.TLAbsFileType FileType { get; set; }
        public int Mtime { get; set; }
        public byte[] Bytes { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Size = br.ReadInt32();
            this.MimeType = StringUtil.Deserialize(br);
            this.FileType = (Storage.TLAbsFileType)ObjectUtils.DeserializeObject(br);
            this.Mtime = br.ReadInt32();
            this.Bytes = BytesUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            bw.Write(this.Size);
            StringUtil.Serialize(this.MimeType, bw);
            ObjectUtils.SerializeObject(this.FileType, bw);
            bw.Write(this.Mtime);
            BytesUtil.Serialize(this.Bytes, bw);

        }
    }
}
