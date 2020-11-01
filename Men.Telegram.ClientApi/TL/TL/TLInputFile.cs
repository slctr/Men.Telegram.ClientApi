using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-181407105)]
    public class TLInputFile : TLAbsInputFile
    {
        public override int Constructor
        {
            get
            {
                return -181407105;
            }
        }

        public long Id { get; set; }
        public int Parts { get; set; }
        public string Name { get; set; }
        public string Md5Checksum { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Id = br.ReadInt64();
            this.Parts = br.ReadInt32();
            this.Name = StringUtil.Deserialize(br);
            this.Md5Checksum = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            bw.Write(this.Id);
            bw.Write(this.Parts);
            StringUtil.Serialize(this.Name, bw);
            StringUtil.Serialize(this.Md5Checksum, bw);

        }
    }
}
