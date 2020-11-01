using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(922273905)]
    public class TLDocumentEmpty : TLAbsDocument
    {
        public override int Constructor
        {
            get
            {
                return 922273905;
            }
        }

        public long Id { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Id = br.ReadInt64();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            bw.Write(this.Id);

        }
    }
}
