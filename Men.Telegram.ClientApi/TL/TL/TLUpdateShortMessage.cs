using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1857044719)]
    public class TLUpdateShortMessage : TLAbsUpdates
    {
        public override int Constructor
        {
            get
            {
                return -1857044719;
            }
        }

        public int Flags { get; set; }
        public bool Out { get; set; }
        public bool Mentioned { get; set; }
        public bool MediaUnread { get; set; }
        public bool Silent { get; set; }
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Message { get; set; }
        public int Pts { get; set; }
        public int PtsCount { get; set; }
        public int Date { get; set; }
        public TLMessageFwdHeader FwdFrom { get; set; }
        public int? ViaBotId { get; set; }
        public int? ReplyToMsgId { get; set; }
        public TLVector<TLAbsMessageEntity> Entities { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.Out ? (this.Flags | 2) : (this.Flags & ~2);
            this.Flags = this.Mentioned ? (this.Flags | 16) : (this.Flags & ~16);
            this.Flags = this.MediaUnread ? (this.Flags | 32) : (this.Flags & ~32);
            this.Flags = this.Silent ? (this.Flags | 8192) : (this.Flags & ~8192);
            this.Flags = this.FwdFrom != null ? (this.Flags | 4) : (this.Flags & ~4);
            this.Flags = this.ViaBotId != null ? (this.Flags | 2048) : (this.Flags & ~2048);
            this.Flags = this.ReplyToMsgId != null ? (this.Flags | 8) : (this.Flags & ~8);
            this.Flags = this.Entities != null ? (this.Flags | 128) : (this.Flags & ~128);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            this.Out = (this.Flags & 2) != 0;
            this.Mentioned = (this.Flags & 16) != 0;
            this.MediaUnread = (this.Flags & 32) != 0;
            this.Silent = (this.Flags & 8192) != 0;
            this.Id = br.ReadInt32();
            this.UserId = br.ReadInt32();
            this.Message = StringUtil.Deserialize(br);
            this.Pts = br.ReadInt32();
            this.PtsCount = br.ReadInt32();
            this.Date = br.ReadInt32();
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

            if ((this.Flags & 128) != 0)
                this.Entities = (TLVector<TLAbsMessageEntity>)ObjectUtils.DeserializeVector<TLAbsMessageEntity>(br);
            else
                this.Entities = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);




            bw.Write(this.Id);
            bw.Write(this.UserId);
            StringUtil.Serialize(this.Message, bw);
            bw.Write(this.Pts);
            bw.Write(this.PtsCount);
            bw.Write(this.Date);
            if ((this.Flags & 4) != 0)
                ObjectUtils.SerializeObject(this.FwdFrom, bw);
            if ((this.Flags & 2048) != 0)
                bw.Write(this.ViaBotId.Value);
            if ((this.Flags & 8) != 0)
                bw.Write(this.ReplyToMsgId.Value);
            if ((this.Flags & 128) != 0)
                ObjectUtils.SerializeObject(this.Entities, bw);

        }
    }
}
