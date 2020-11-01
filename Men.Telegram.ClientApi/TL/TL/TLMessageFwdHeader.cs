using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-947462709)]
    public class TLMessageFwdHeader : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -947462709;
            }
        }

        public int Flags { get; set; }
        public int? FromId { get; set; }
        public int Date { get; set; }
        public int? ChannelId { get; set; }
        public int? ChannelPost { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.FromId != null ? (this.Flags | 1) : (this.Flags & ~1);
            this.Flags = this.ChannelId != null ? (this.Flags | 2) : (this.Flags & ~2);
            this.Flags = this.ChannelPost != null ? (this.Flags | 4) : (this.Flags & ~4);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            if ((this.Flags & 1) != 0)
                this.FromId = br.ReadInt32();
            else
                this.FromId = null;

            this.Date = br.ReadInt32();
            if ((this.Flags & 2) != 0)
                this.ChannelId = br.ReadInt32();
            else
                this.ChannelId = null;

            if ((this.Flags & 4) != 0)
                this.ChannelPost = br.ReadInt32();
            else
                this.ChannelPost = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);
            if ((this.Flags & 1) != 0)
                bw.Write(this.FromId.Value);
            bw.Write(this.Date);
            if ((this.Flags & 2) != 0)
                bw.Write(this.ChannelId.Value);
            if ((this.Flags & 4) != 0)
                bw.Write(this.ChannelPost.Value);

        }
    }
}
