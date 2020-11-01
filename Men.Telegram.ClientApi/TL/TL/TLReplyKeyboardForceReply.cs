using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-200242528)]
    public class TLReplyKeyboardForceReply : TLAbsReplyMarkup
    {
        public override int Constructor
        {
            get
            {
                return -200242528;
            }
        }

        public int Flags { get; set; }
        public bool SingleUse { get; set; }
        public bool Selective { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.SingleUse ? (this.Flags | 2) : (this.Flags & ~2);
            this.Flags = this.Selective ? (this.Flags | 4) : (this.Flags & ~4);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            this.SingleUse = (this.Flags & 2) != 0;
            this.Selective = (this.Flags & 4) != 0;

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);



        }
    }
}
