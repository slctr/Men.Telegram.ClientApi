using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(301019932)]
    public class TLUpdateShortSentMessage : TLAbsUpdates
    {
        public override int Constructor
        {
            get
            {
                return 301019932;
            }
        }

        public int Flags { get; set; }
        public bool Out { get; set; }
        public int Id { get; set; }
        public int Pts { get; set; }
        public int PtsCount { get; set; }
        public int Date { get; set; }
        public TLAbsMessageMedia Media { get; set; }
        public TLVector<TLAbsMessageEntity> Entities { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.Out ? (this.Flags | 2) : (this.Flags & ~2);
            this.Flags = this.Media != null ? (this.Flags | 512) : (this.Flags & ~512);
            this.Flags = this.Entities != null ? (this.Flags | 128) : (this.Flags & ~128);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            this.Out = (this.Flags & 2) != 0;
            this.Id = br.ReadInt32();
            this.Pts = br.ReadInt32();
            this.PtsCount = br.ReadInt32();
            this.Date = br.ReadInt32();
            if ((this.Flags & 512) != 0)
                this.Media = (TLAbsMessageMedia)ObjectUtils.DeserializeObject(br);
            else
                this.Media = null;

            if ((this.Flags & 128) != 0)
                this.Entities = (TLVector<TLAbsMessageEntity>)ObjectUtils.DeserializeVector<TLAbsMessageEntity>(br);
            else
                this.Entities = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);

            bw.Write(this.Id);
            bw.Write(this.Pts);
            bw.Write(this.PtsCount);
            bw.Write(this.Date);
            if ((this.Flags & 512) != 0)
                ObjectUtils.SerializeObject(this.Media, bw);
            if ((this.Flags & 128) != 0)
                ObjectUtils.SerializeObject(this.Entities, bw);

        }
    }
}
