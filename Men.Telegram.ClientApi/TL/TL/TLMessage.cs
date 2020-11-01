using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1063525281)]
    public class TLMessage : TLAbsMessage
    {
        public override int Constructor
        {
            get
            {
                return -1063525281;
            }
        }

        public int Flags { get; set; }
        public bool Out { get; set; }
        public bool Mentioned { get; set; }
        public bool MediaUnread { get; set; }
        public bool Silent { get; set; }
        public bool Post { get; set; }
        public int Id { get; set; }
        public int? FromId { get; set; }
        public TLAbsPeer ToId { get; set; }
        public TLMessageFwdHeader FwdFrom { get; set; }
        public int? ViaBotId { get; set; }
        public int? ReplyToMsgId { get; set; }
        public int Date { get; set; }
        public string Message { get; set; }
        public TLAbsMessageMedia Media { get; set; }
        public TLAbsReplyMarkup ReplyMarkup { get; set; }
        public TLVector<TLAbsMessageEntity> Entities { get; set; }
        public int? Views { get; set; }
        public int? EditDate { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.Out ? (this.Flags | 2) : (this.Flags & ~2);
            this.Flags = this.Mentioned ? (this.Flags | 16) : (this.Flags & ~16);
            this.Flags = this.MediaUnread ? (this.Flags | 32) : (this.Flags & ~32);
            this.Flags = this.Silent ? (this.Flags | 8192) : (this.Flags & ~8192);
            this.Flags = this.Post ? (this.Flags | 16384) : (this.Flags & ~16384);
            this.Flags = this.FromId != null ? (this.Flags | 256) : (this.Flags & ~256);
            this.Flags = this.FwdFrom != null ? (this.Flags | 4) : (this.Flags & ~4);
            this.Flags = this.ViaBotId != null ? (this.Flags | 2048) : (this.Flags & ~2048);
            this.Flags = this.ReplyToMsgId != null ? (this.Flags | 8) : (this.Flags & ~8);
            this.Flags = this.Media != null ? (this.Flags | 512) : (this.Flags & ~512);
            this.Flags = this.ReplyMarkup != null ? (this.Flags | 64) : (this.Flags & ~64);
            this.Flags = this.Entities != null ? (this.Flags | 128) : (this.Flags & ~128);
            this.Flags = this.Views != null ? (this.Flags | 1024) : (this.Flags & ~1024);
            this.Flags = this.EditDate != null ? (this.Flags | 32768) : (this.Flags & ~32768);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            this.Out = (this.Flags & 2) != 0;
            this.Mentioned = (this.Flags & 16) != 0;
            this.MediaUnread = (this.Flags & 32) != 0;
            this.Silent = (this.Flags & 8192) != 0;
            this.Post = (this.Flags & 16384) != 0;
            this.Id = br.ReadInt32();
            if ((this.Flags & 256) != 0)
                this.FromId = br.ReadInt32();
            else
                this.FromId = null;

            this.ToId = (TLAbsPeer)ObjectUtils.DeserializeObject(br);
            if ((this.Flags & 4) != 0)
                this.FwdFrom = (TLMessageFwdHeader)ObjectUtils.DeserializeObject(br);
            else
                this.FwdFrom = null;

            if ((this.Flags & 2048) != 0)
                this.ViaBotId = br.ReadInt32();
            else
                this.ViaBotId = null;

            if ((this.Flags & 8) != 0)
                this.ReplyToMsgId = br.ReadInt32();
            else
                this.ReplyToMsgId = null;

            this.Date = br.ReadInt32();
            this.Message = StringUtil.Deserialize(br);
            if ((this.Flags & 512) != 0)
                this.Media = (TLAbsMessageMedia)ObjectUtils.DeserializeObject(br);
            else
                this.Media = null;

            if ((this.Flags & 64) != 0)
                this.ReplyMarkup = (TLAbsReplyMarkup)ObjectUtils.DeserializeObject(br);
            else
                this.ReplyMarkup = null;

            if ((this.Flags & 128) != 0)
                this.Entities = (TLVector<TLAbsMessageEntity>)ObjectUtils.DeserializeVector<TLAbsMessageEntity>(br);
            else
                this.Entities = null;

            if ((this.Flags & 1024) != 0)
                this.Views = br.ReadInt32();
            else
                this.Views = null;

            if ((this.Flags & 32768) != 0)
                this.EditDate = br.ReadInt32();
            else
                this.EditDate = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);





            bw.Write(this.Id);
            if ((this.Flags & 256) != 0)
                bw.Write(this.FromId.Value);
            ObjectUtils.SerializeObject(this.ToId, bw);
            if ((this.Flags & 4) != 0)
                ObjectUtils.SerializeObject(this.FwdFrom, bw);
            if ((this.Flags & 2048) != 0)
                bw.Write(this.ViaBotId.Value);
            if ((this.Flags & 8) != 0)
                bw.Write(this.ReplyToMsgId.Value);
            bw.Write(this.Date);
            StringUtil.Serialize(this.Message, bw);
            if ((this.Flags & 512) != 0)
                ObjectUtils.SerializeObject(this.Media, bw);
            if ((this.Flags & 64) != 0)
                ObjectUtils.SerializeObject(this.ReplyMarkup, bw);
            if ((this.Flags & 128) != 0)
                ObjectUtils.SerializeObject(this.Entities, bw);
            if ((this.Flags & 1024) != 0)
                bw.Write(this.Views.Value);
            if ((this.Flags & 32768) != 0)
                bw.Write(this.EditDate.Value);

        }
    }
}
