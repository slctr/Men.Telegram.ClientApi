using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1739392570)]
    public class TLDocumentAttributeAudio : TLAbsDocumentAttribute
    {
        public override int Constructor
        {
            get
            {
                return -1739392570;
            }
        }

        public int Flags { get; set; }
        public bool Voice { get; set; }
        public int Duration { get; set; }
        public string Title { get; set; }
        public string Performer { get; set; }
        public byte[] Waveform { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.Voice ? (this.Flags | 1024) : (this.Flags & ~1024);
            this.Flags = this.Title != null ? (this.Flags | 1) : (this.Flags & ~1);
            this.Flags = this.Performer != null ? (this.Flags | 2) : (this.Flags & ~2);
            this.Flags = this.Waveform != null ? (this.Flags | 4) : (this.Flags & ~4);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            this.Voice = (this.Flags & 1024) != 0;
            this.Duration = br.ReadInt32();
            if ((this.Flags & 1) != 0)
                this.Title = StringUtil.Deserialize(br);
            else
                this.Title = null;

            if ((this.Flags & 2) != 0)
                this.Performer = StringUtil.Deserialize(br);
            else
                this.Performer = null;

            if ((this.Flags & 4) != 0)
                this.Waveform = BytesUtil.Deserialize(br);
            else
                this.Waveform = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);

            bw.Write(this.Duration);
            if ((this.Flags & 1) != 0)
                StringUtil.Serialize(this.Title, bw);
            if ((this.Flags & 2) != 0)
                StringUtil.Serialize(this.Performer, bw);
            if ((this.Flags & 4) != 0)
                BytesUtil.Serialize(this.Waveform, bw);

        }
    }
}
