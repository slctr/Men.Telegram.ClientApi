using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(90744648)]
    public class TLKeyboardButtonSwitchInline : TLAbsKeyboardButton
    {
        public override int Constructor
        {
            get
            {
                return 90744648;
            }
        }

        public int Flags { get; set; }
        public bool SamePeer { get; set; }
        public string Text { get; set; }
        public string Query { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.SamePeer ? (this.Flags | 1) : (this.Flags & ~1);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            this.SamePeer = (this.Flags & 1) != 0;
            this.Text = StringUtil.Deserialize(br);
            this.Query = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);

            StringUtil.Serialize(this.Text, bw);
            StringUtil.Serialize(this.Query, bw);

        }
    }
}
