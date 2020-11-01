using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(319564933)]
    public class TLRequestEditInlineBotMessage : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 319564933;
            }
        }

        public int Flags { get; set; }
        public bool NoWebpage { get; set; }
        public TLInputBotInlineMessageID Id { get; set; }
        public string Message { get; set; }
        public TLAbsReplyMarkup ReplyMarkup { get; set; }
        public TLVector<TLAbsMessageEntity> Entities { get; set; }
        public bool Response { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.NoWebpage ? (this.Flags | 2) : (this.Flags & ~2);
            this.Flags = this.Message != null ? (this.Flags | 2048) : (this.Flags & ~2048);
            this.Flags = this.ReplyMarkup != null ? (this.Flags | 4) : (this.Flags & ~4);
            this.Flags = this.Entities != null ? (this.Flags | 8) : (this.Flags & ~8);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            this.NoWebpage = (this.Flags & 2) != 0;
            this.Id = (TLInputBotInlineMessageID)ObjectUtils.DeserializeObject(br);
            if ((this.Flags & 2048) != 0)
            {
                this.Message = StringUtil.Deserialize(br);
            }
            else
            {
                this.Message = null;
            }

            if ((this.Flags & 4) != 0)
            {
                this.ReplyMarkup = (TLAbsReplyMarkup)ObjectUtils.DeserializeObject(br);
            }
            else
            {
                this.ReplyMarkup = null;
            }

            if ((this.Flags & 8) != 0)
            {
                this.Entities = (TLVector<TLAbsMessageEntity>)ObjectUtils.DeserializeVector<TLAbsMessageEntity>(br);
            }
            else
            {
                this.Entities = null;
            }
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);

            ObjectUtils.SerializeObject(this.Id, bw);
            if ((this.Flags & 2048) != 0)
            {
                StringUtil.Serialize(this.Message, bw);
            }

            if ((this.Flags & 4) != 0)
            {
                ObjectUtils.SerializeObject(this.ReplyMarkup, bw);
            }

            if ((this.Flags & 8) != 0)
            {
                ObjectUtils.SerializeObject(this.Entities, bw);
            }
        }
        public override void DeserializeResponse(BinaryReader br)
        {
            this.Response = BoolUtil.Deserialize(br);

        }
    }
}
