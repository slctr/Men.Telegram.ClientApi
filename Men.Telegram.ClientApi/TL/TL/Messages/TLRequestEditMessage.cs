using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(-829299510)]
    public class TLRequestEditMessage : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -829299510;
            }
        }

        public int Flags { get; set; }
        public bool NoWebpage { get; set; }
        public TLAbsInputPeer Peer { get; set; }
        public int Id { get; set; }
        public string Message { get; set; }
        public TLAbsReplyMarkup ReplyMarkup { get; set; }
        public TLVector<TLAbsMessageEntity> Entities { get; set; }
        public TLAbsUpdates Response { get; set; }


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
            this.Peer = (TLAbsInputPeer)ObjectUtils.DeserializeObject(br);
            this.Id = br.ReadInt32();
            if ((this.Flags & 2048) != 0)
                this.Message = StringUtil.Deserialize(br);
            else
                this.Message = null;

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
            bw.Write(this.Id);
            if ((this.Flags & 2048) != 0)
                StringUtil.Serialize(this.Message, bw);
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
