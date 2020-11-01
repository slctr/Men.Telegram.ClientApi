using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-805141448)]
    public class TLImportedContact : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -805141448;
            }
        }

        public int UserId { get; set; }
        public long ClientId { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.UserId = br.ReadInt32();
            this.ClientId = br.ReadInt64();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            bw.Write(this.UserId);
            bw.Write(this.ClientId);

        }
    }
}
