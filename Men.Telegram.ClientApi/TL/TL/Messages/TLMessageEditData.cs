using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(649453030)]
    public class TLMessageEditData : TLObject
    {
        public override int Constructor
        {
            get
            {
                return 649453030;
            }
        }

        public int Flags { get; set; }
        public bool Caption { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.Caption ? (this.Flags | 1) : (this.Flags & ~1);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            this.Caption = (this.Flags & 1) != 0;

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);


        }
    }
}
