using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(-91733382)]
    public class TLRequestSendMessage : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -91733382;
            }
        }

        public int Flags { get; set; }
        public bool NoWebpage { get; set; }
        public bool Silent { get; set; }
        public bool Background { get; set; }
        public bool ClearDraft { get; set; }
        public TLAbsInputPeer Peer { get; set; }
        public int? ReplyToMsgId { get; set; }
        public string Message { get; set; }
        public long RandomId { get; set; }
        public TLAbsReplyMarkup ReplyMarkup { get; set; }
        public TLVector<TLAbsMessageEntity> Entities { get; set; }
        public TLAbsUpdates Response { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.NoWebpage ? (this.Flags | 2) : (this.Flags & ~2);
            this.Flags = this.Silent ? (this.Flags | 32) : (this.Flags & ~32);
            this.Flags = this.Background ? (this.Flags | 64) : (this.Flags & ~64);
            this.Flags = this.ClearDraft ? (this.Flags | 128) : (this.Flags & ~128);
            this.Flags = this.ReplyToMsgId != null ? (this.Flags | 1) : (this.Flags & ~1);
            this.Flags = this.ReplyMarkup != null ? (this.Flags | 4) : (this.Flags & ~4);
            this.Flags = this.Entities != null ? (this.Flags | 8) : (this.Flags & ~8);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            this.NoWebpage = (this.Flags & 2) != 0;
            this.Silent = (this.Flags & 32) != 0;
            this.Background = (this.Flags & 64) != 0;
            this.ClearDraft = (this.Flags & 128) != 0;
            this.Peer = (TLAbsInputPeer)ObjectUtils.DeserializeObject(br);
            if ((this.Flags & 1) != 0)
                this.ReplyToMsgId = br.ReadInt32();
            else
                this.ReplyToMsgId = null;

            this.Message = StringUtil.Deserialize(br);
            this.RandomId = br.ReadInt64();
            if ((this.Flags & 4) != 0)
                this.ReplyMarkup = (TLAbsReplyMarkup)ObjectUtils.DeserializeObject(br);
            else
                this.ReplyMarkup = null;

            if ((this.Flags & 8) != 0)
                this.Entities = (TLVector<TLAbsMessageEntity>)ObjectUtils.DeserializeVector<TLAbsMessageEntity>(br);
            else
                this.Entities = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);




            ObjectUtils.SerializeObject(this.Peer, bw);
            if ((this.Flags & 1) != 0)
                bw.Write(this.ReplyToMsgId.Value);
            StringUtil.Serialize(this.Message, bw);
            bw.Write(this.RandomId);
            if ((this.Flags & 4) != 0)
                ObjectUtils.SerializeObject(this.ReplyMarkup, bw);
            if ((this.Flags & 8) != 0)
                ObjectUtils.SerializeObject(this.Entities, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            this.Response = (TLAbsUpdates)ObjectUtils.DeserializeObject(br);

        }
    }
}
