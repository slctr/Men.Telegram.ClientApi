using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(889353612)]
    public class TLReplyKeyboardMarkup : TLAbsReplyMarkup
    {
        public override int Constructor
        {
            get
            {
                return 889353612;
            }
        }

        public int Flags { get; set; }
        public bool Resize { get; set; }
        public bool SingleUse { get; set; }
        public bool Selective { get; set; }
        public TLVector<TLKeyboardButtonRow> Rows { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.Resize ? (this.Flags | 1) : (this.Flags & ~1);
            this.Flags = this.SingleUse ? (this.Flags | 2) : (this.Flags & ~2);
            this.Flags = this.Selective ? (this.Flags | 4) : (this.Flags & ~4);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            this.Resize = (this.Flags & 1) != 0;
            this.SingleUse = (this.Flags & 2) != 0;
            this.Selective = (this.Flags & 4) != 0;
            this.Rows = (TLVector<TLKeyboardButtonRow>)ObjectUtils.DeserializeVector<TLKeyboardButtonRow>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);



            ObjectUtils.SerializeObject(this.Rows, bw);

        }
    }
}
