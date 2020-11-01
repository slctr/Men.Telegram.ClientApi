using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(1728035348)]
    public class TLDialog : TLObject
    {
        public override int Constructor
        {
            get
            {
                return 1728035348;
            }
        }

        public int Flags { get; set; }
        public bool Pinned { get; set; }
        public TLAbsPeer Peer { get; set; }
        public int TopMessage { get; set; }
        public int ReadInboxMaxId { get; set; }
        public int ReadOutboxMaxId { get; set; }
        public int UnreadCount { get; set; }
        public TLAbsPeerNotifySettings NotifySettings { get; set; }
        public int? Pts { get; set; }
        public TLAbsDraftMessage Draft { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.Pinned ? (this.Flags | 4) : (this.Flags & ~4);
            this.Flags = this.Pts != null ? (this.Flags | 1) : (this.Flags & ~1);
            this.Flags = this.Draft != null ? (this.Flags | 2) : (this.Flags & ~2);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            this.Pinned = (this.Flags & 4) != 0;
            this.Peer = (TLAbsPeer)ObjectUtils.DeserializeObject(br);
            this.TopMessage = br.ReadInt32();
            this.ReadInboxMaxId = br.ReadInt32();
            this.ReadOutboxMaxId = br.ReadInt32();
            this.UnreadCount = br.ReadInt32();
            this.NotifySettings = (TLAbsPeerNotifySettings)ObjectUtils.DeserializeObject(br);
            if ((this.Flags & 1) != 0)
            {
                this.Pts = br.ReadInt32();
            }
            else
            {
                this.Pts = null;
            }

            if ((this.Flags & 2) != 0)
            {
                this.Draft = (TLAbsDraftMessage)ObjectUtils.DeserializeObject(br);
            }
            else
            {
                this.Draft = null;
            }
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);

            ObjectUtils.SerializeObject(this.Peer, bw);
            bw.Write(this.TopMessage);
            bw.Write(this.ReadInboxMaxId);
            bw.Write(this.ReadOutboxMaxId);
            bw.Write(this.UnreadCount);
            ObjectUtils.SerializeObject(this.NotifySettings, bw);
            if ((this.Flags & 1) != 0)
            {
                bw.Write(this.Pts.Value);
            }

            if ((this.Flags & 2) != 0)
            {
                ObjectUtils.SerializeObject(this.Draft, bw);
            }
        }
    }
}
