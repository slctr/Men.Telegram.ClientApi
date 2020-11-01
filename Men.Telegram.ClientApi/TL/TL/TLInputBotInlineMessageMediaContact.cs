using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(766443943)]
    public class TLInputBotInlineMessageMediaContact : TLAbsInputBotInlineMessage
    {
        public override int Constructor
        {
            get
            {
                return 766443943;
            }
        }

        public int Flags { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public TLAbsReplyMarkup ReplyMarkup { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.ReplyMarkup != null ? (this.Flags | 4) : (this.Flags & ~4);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            this.PhoneNumber = StringUtil.Deserialize(br);
            this.FirstName = StringUtil.Deserialize(br);
            this.LastName = StringUtil.Deserialize(br);
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
            StringUtil.Serialize(this.PhoneNumber, bw);
            StringUtil.Serialize(this.FirstName, bw);
            StringUtil.Serialize(this.LastName, bw);
            if ((this.Flags & 4) != 0)
            {
                ObjectUtils.SerializeObject(this.ReplyMarkup, bw);
            }
        }
    }
}
