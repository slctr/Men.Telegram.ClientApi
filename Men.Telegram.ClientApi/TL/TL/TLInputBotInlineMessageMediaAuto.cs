using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(691006739)]
    public class TLInputBotInlineMessageMediaAuto : TLAbsInputBotInlineMessage
    {
        public override int Constructor
        {
            get
            {
                return 691006739;
            }
        }

        public int Flags { get; set; }
        public string Caption { get; set; }
        public TLAbsReplyMarkup ReplyMarkup { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.ReplyMarkup != null ? (this.Flags | 4) : (this.Flags & ~4);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            this.Caption = StringUtil.Deserialize(br);
            if ((this.Flags & 4) != 0)
            {
                this.ReplyMarkup = (TLAbsReplyMarkup)ObjectUtils.DeserializeObject(br);
            }
            else
            {
                this.ReplyMarkup = null;
            }
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);
            StringUtil.Serialize(this.Caption, bw);
            if ((this.Flags & 4) != 0)
            {
                ObjectUtils.SerializeObject(this.ReplyMarkup, bw);
            }
        }
    }
}
