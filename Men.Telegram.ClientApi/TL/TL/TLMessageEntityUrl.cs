using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(1859134776)]
    public class TLMessageEntityUrl : TLAbsMessageEntity
    {
        public override int Constructor
        {
            get
            {
                return 1859134776;
            }
        }

        public int Offset { get; set; }
        public int Length { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Offset = br.ReadInt32();
            this.Length = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            bw.Write(this.Offset);
            bw.Write(this.Length);

        }
    }
}
