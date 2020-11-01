using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-415938591)]
    public class TLUpdateBotCallbackQuery : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return -415938591;
            }
        }

        public int Flags { get; set; }
        public long QueryId { get; set; }
        public int UserId { get; set; }
        public TLAbsPeer Peer { get; set; }
        public int MsgId { get; set; }
        public long ChatInstance { get; set; }
        public byte[] Data { get; set; }
        public string GameShortName { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.Data != null ? (this.Flags | 1) : (this.Flags & ~1);
            this.Flags = this.GameShortName != null ? (this.Flags | 2) : (this.Flags & ~2);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            this.QueryId = br.ReadInt64();
            this.UserId = br.ReadInt32();
            this.Peer = (TLAbsPeer)ObjectUtils.DeserializeObject(br);
            this.MsgId = br.ReadInt32();
            this.ChatInstance = br.ReadInt64();
            if ((this.Flags & 1) != 0)
                this.Data = BytesUtil.Deserialize(br);
            else
                this.Data = null;

            if ((this.Flags & 2) != 0)
                this.GameShortName = StringUtil.Deserialize(br);
            else
                this.GameShortName = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);
            bw.Write(this.QueryId);
            bw.Write(this.UserId);
            ObjectUtils.SerializeObject(this.Peer, bw);
            bw.Write(this.MsgId);
            bw.Write(this.ChatInstance);
            if ((this.Flags & 1) != 0)
                BytesUtil.Serialize(this.Data, bw);
            if ((this.Flags & 2) != 0)
                StringUtil.Serialize(this.GameShortName, bw);

        }
    }
}
