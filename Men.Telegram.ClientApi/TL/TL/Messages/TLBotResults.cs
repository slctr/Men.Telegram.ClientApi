using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(-858565059)]
    public class TLBotResults : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -858565059;
            }
        }

        public int Flags { get; set; }
        public bool Gallery { get; set; }
        public long QueryId { get; set; }
        public string NextOffset { get; set; }
        public TLInlineBotSwitchPM SwitchPm { get; set; }
        public TLVector<TLAbsBotInlineResult> Results { get; set; }
        public int CacheTime { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.Gallery ? (this.Flags | 1) : (this.Flags & ~1);
            this.Flags = this.NextOffset != null ? (this.Flags | 2) : (this.Flags & ~2);
            this.Flags = this.SwitchPm != null ? (this.Flags | 4) : (this.Flags & ~4);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            this.Gallery = (this.Flags & 1) != 0;
            this.QueryId = br.ReadInt64();
            if ((this.Flags & 2) != 0)
                this.NextOffset = StringUtil.Deserialize(br);
            else
                this.NextOffset = null;

            if ((this.Flags & 4) != 0)
                this.SwitchPm = (TLInlineBotSwitchPM)ObjectUtils.DeserializeObject(br);
            else
                this.SwitchPm = null;

            this.Results = (TLVector<TLAbsBotInlineResult>)ObjectUtils.DeserializeVector<TLAbsBotInlineResult>(br);
            this.CacheTime = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);

            bw.Write(this.QueryId);
            if ((this.Flags & 2) != 0)
                StringUtil.Serialize(this.NextOffset, bw);
            if ((this.Flags & 4) != 0)
                ObjectUtils.SerializeObject(this.SwitchPm, bw);
            ObjectUtils.SerializeObject(this.Results, bw);
            bw.Write(this.CacheTime);

        }
    }
}
