using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(1036876423)]
    public class TLInputBotInlineMessageText : TLAbsInputBotInlineMessage
    {
        public override int Constructor
        {
            get
            {
                return 1036876423;
            }
        }

        public int Flags { get; set; }
        public bool NoWebpage { get; set; }
        public string Message { get; set; }
        public TLVector<TLAbsMessageEntity> Entities { get; set; }
        public TLAbsReplyMarkup ReplyMarkup { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.NoWebpage ? (this.Flags | 1) : (this.Flags & ~1);
            this.Flags = this.Entities != null ? (this.Flags | 2) : (this.Flags & ~2);
            this.Flags = this.ReplyMarkup != null ? (this.Flags | 4) : (this.Flags & ~4);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            this.NoWebpage = (this.Flags & 1) != 0;
            this.Message = StringUtil.Deserialize(br);
            if ((this.Flags & 2) != 0)
            {
                this.Entities = (TLVector<TLAbsMessageEntity>)ObjectUtils.DeserializeVector<TLAbsMessageEntity>(br);
            }
            else
            {
                this.Entities = null;
            }

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

            StringUtil.Serialize(this.Message, bw);
            if ((this.Flags & 2) != 0)
            {
                ObjectUtils.SerializeObject(this.Entities, bw);
            }

            if ((this.Flags & 4) != 0)
            {
                ObjectUtils.SerializeObject(this.ReplyMarkup, bw);
            }
        }
    }
}
