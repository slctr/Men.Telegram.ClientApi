using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1642487306)]
    public class TLMessageService : TLAbsMessage
    {
        public override int Constructor
        {
            get
            {
                return -1642487306;
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
        public int? ReplyToMsgId { get; set; }
        public int Date { get; set; }
        public TLAbsMessageAction Action { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.Out ? (this.Flags | 2) : (this.Flags & ~2);
            this.Flags = this.Mentioned ? (this.Flags | 16) : (this.Flags & ~16);
            this.Flags = this.MediaUnread ? (this.Flags | 32) : (this.Flags & ~32);
            this.Flags = this.Silent ? (this.Flags | 8192) : (this.Flags & ~8192);
            this.Flags = this.Post ? (this.Flags | 16384) : (this.Flags & ~16384);
            this.Flags = this.FromId != null ? (this.Flags | 256) : (this.Flags & ~256);
            this.Flags = this.ReplyToMsgId != null ? (this.Flags | 8) : (this.Flags & ~8);

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
            {
                this.FromId = br.ReadInt32();
            }
            else
            {
                this.FromId = null;
            }

            this.ToId = (TLAbsPeer)ObjectUtils.DeserializeObject(br);
            if ((this.Flags & 8) != 0)
            {
                this.ReplyToMsgId = br.ReadInt32();
            }
            else
            {
                this.ReplyToMsgId = null;
            }

            this.Date = br.ReadInt32();
            this.Action = (TLAbsMessageAction)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);





            bw.Write(this.Id);
            if ((this.Flags & 256) != 0)
            {
                bw.Write(this.FromId.Value);
            }

            ObjectUtils.SerializeObject(this.ToId, bw);
            if ((this.Flags & 8) != 0)
            {
                bw.Write(this.ReplyToMsgId.Value);
            }

            bw.Write(this.Date);
            ObjectUtils.SerializeObject(this.Action, bw);

        }
    }
}
