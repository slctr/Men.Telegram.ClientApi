using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Upload
{
    [TLObject(157948117)]
    public class TLFile : TLAbsFile
    {
        public override int Constructor
        {
            get
            {
                return 157948117;
            }
        }

        public Storage.TLAbsFileType Type { get; set; }
        public int Mtime { get; set; }
        public byte[] Bytes { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Type = (Storage.TLAbsFileType)ObjectUtils.DeserializeObject(br);
            this.Mtime = br.ReadInt32();
            this.Bytes = BytesUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            ObjectUtils.SerializeObject(this.Type, bw);
            bw.Write(this.Mtime);
            BytesUtil.Serialize(this.Bytes, bw);

        }
    }
}
