using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(-1318189314)]
    public class TLRequestSendInlineBotResult : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -1318189314;
            }
        }

        public int Flags { get; set; }
        public bool Silent { get; set; }
        public bool Background { get; set; }
        public bool ClearDraft { get; set; }
        public TLAbsInputPeer Peer { get; set; }
        public int? ReplyToMsgId { get; set; }
        public long RandomId { get; set; }
        public long QueryId { get; set; }
        public string Id { get; set; }
        public TLAbsUpdates Response { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.Silent ? (this.Flags | 32) : (this.Flags & ~32);
            this.Flags = this.Background ? (this.Flags | 64) : (this.Flags & ~64);
            this.Flags = this.ClearDraft ? (this.Flags | 128) : (this.Flags & ~128);
            this.Flags = this.ReplyToMsgId != null ? (this.Flags | 1) : (this.Flags & ~1);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            this.Silent = (this.Flags & 32) != 0;
            this.Background = (this.Flags & 64) != 0;
            this.ClearDraft = (this.Flags & 128) != 0;
            this.Peer = (TLAbsInputPeer)ObjectUtils.DeserializeObject(br);
            if ((this.Flags & 1) != 0)
                this.ReplyToMsgId = br.ReadInt32();
            else
                this.ReplyToMsgId = null;

            this.RandomId = br.ReadInt64();
            this.QueryId = br.ReadInt64();
            this.Id = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);



            ObjectUtils.SerializeObject(this.Peer, bw);
            if ((this.Flags & 1) != 0)
                bw.Write(this.ReplyToMsgId.Value);
            bw.Write(this.RandomId);
            bw.Write(this.QueryId);
            StringUtil.Serialize(this.Id, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            this.Response = (TLAbsUpdates)ObjectUtils.DeserializeObject(br);

        }
    }
}
