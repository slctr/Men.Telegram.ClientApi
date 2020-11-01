using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(400266251)]
    public class TLBotInlineMediaResult : TLAbsBotInlineResult
    {
        public override int Constructor
        {
            get
            {
                return 400266251;
            }
        }

        public int Flags { get; set; }
        public string Id { get; set; }
        public string Type { get; set; }
        public TLAbsPhoto Photo { get; set; }
        public TLAbsDocument Document { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TLAbsBotInlineMessage SendMessage { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.Photo != null ? (this.Flags | 1) : (this.Flags & ~1);
            this.Flags = this.Document != null ? (this.Flags | 2) : (this.Flags & ~2);
            this.Flags = this.Title != null ? (this.Flags | 4) : (this.Flags & ~4);
            this.Flags = this.Description != null ? (this.Flags | 8) : (this.Flags & ~8);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            this.Id = StringUtil.Deserialize(br);
            this.Type = StringUtil.Deserialize(br);
            if ((this.Flags & 1) != 0)
            {
                this.Photo = (TLAbsPhoto)ObjectUtils.DeserializeObject(br);
            }
            else
            {
                this.Photo = null;
            }

            if ((this.Flags & 2) != 0)
            {
                this.Document = (TLAbsDocument)ObjectUtils.DeserializeObject(br);
            }
            else
            {
                this.Document = null;
            }

            if ((this.Flags & 4) != 0)
            {
                this.Title = StringUtil.Deserialize(br);
            }
            else
            {
                this.Title = null;
            }

            if ((this.Flags & 8) != 0)
            {
                this.Description = StringUtil.Deserialize(br);
            }
            else
            {
                this.Description = null;
            }

            this.SendMessage = (TLAbsBotInlineMessage)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);
            StringUtil.Serialize(this.Id, bw);
            StringUtil.Serialize(this.Type, bw);
            if ((this.Flags & 1) != 0)
            {
                ObjectUtils.SerializeObject(this.Photo, bw);
            }

            if ((this.Flags & 2) != 0)
            {
                ObjectUtils.SerializeObject(this.Document, bw);
            }

            if ((this.Flags & 4) != 0)
            {
                StringUtil.Serialize(this.Title, bw);
            }

            if ((this.Flags & 8) != 0)
            {
                StringUtil.Serialize(this.Description, bw);
            }

            ObjectUtils.SerializeObject(this.SendMessage, bw);

        }
    }
}
