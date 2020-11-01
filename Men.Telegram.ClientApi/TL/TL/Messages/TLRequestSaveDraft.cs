using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(-1137057461)]
    public class TLRequestSaveDraft : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -1137057461;
            }
        }

        public int Flags { get; set; }
        public bool NoWebpage { get; set; }
        public int? ReplyToMsgId { get; set; }
        public TLAbsInputPeer Peer { get; set; }
        public string Message { get; set; }
        public TLVector<TLAbsMessageEntity> Entities { get; set; }
        public bool Response { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.NoWebpage ? (this.Flags | 2) : (this.Flags & ~2);
            this.Flags = this.ReplyToMsgId != null ? (this.Flags | 1) : (this.Flags & ~1);
            this.Flags = this.Entities != null ? (this.Flags | 8) : (this.Flags & ~8);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            this.NoWebpage = (this.Flags & 2) != 0;
            if ((this.Flags & 1) != 0)
                this.ReplyToMsgId = br.ReadInt32();
            else
                this.ReplyToMsgId = null;

            this.Peer = (TLAbsInputPeer)ObjectUtils.DeserializeObject(br);
            this.Message = StringUtil.Deserialize(br);
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

            if ((this.Flags & 1) != 0)
                bw.Write(this.ReplyToMsgId.Value);
            ObjectUtils.SerializeObject(this.Peer, bw);
            StringUtil.Serialize(this.Message, bw);
            if ((this.Flags & 8) != 0)
                ObjectUtils.SerializeObject(this.Entities, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            this.Response = BoolUtil.Deserialize(br);

        }
    }
}
