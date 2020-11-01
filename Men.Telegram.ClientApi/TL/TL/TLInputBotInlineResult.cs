using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(750510426)]
    public class TLInputBotInlineResult : TLAbsInputBotInlineResult
    {
        public override int Constructor
        {
            get
            {
                return 750510426;
            }
        }

        public int Flags { get; set; }
        public string Id { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string ThumbUrl { get; set; }
        public string ContentUrl { get; set; }
        public string ContentType { get; set; }
        public int? W { get; set; }
        public int? H { get; set; }
        public int? Duration { get; set; }
        public TLAbsInputBotInlineMessage SendMessage { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.Title != null ? (this.Flags | 2) : (this.Flags & ~2);
            this.Flags = this.Description != null ? (this.Flags | 4) : (this.Flags & ~4);
            this.Flags = this.Url != null ? (this.Flags | 8) : (this.Flags & ~8);
            this.Flags = this.ThumbUrl != null ? (this.Flags | 16) : (this.Flags & ~16);
            this.Flags = this.ContentUrl != null ? (this.Flags | 32) : (this.Flags & ~32);
            this.Flags = this.ContentType != null ? (this.Flags | 32) : (this.Flags & ~32);
            this.Flags = this.W != null ? (this.Flags | 64) : (this.Flags & ~64);
            this.Flags = this.H != null ? (this.Flags | 64) : (this.Flags & ~64);
            this.Flags = this.Duration != null ? (this.Flags | 128) : (this.Flags & ~128);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            this.Id = StringUtil.Deserialize(br);
            this.Type = StringUtil.Deserialize(br);
            if ((this.Flags & 2) != 0)
            {
                this.Title = StringUtil.Deserialize(br);
            }
            else
            {
                this.Title = null;
            }

            if ((this.Flags & 4) != 0)
            {
                this.Description = StringUtil.Deserialize(br);
            }
            else
            {
                this.Description = null;
            }

            if ((this.Flags & 8) != 0)
            {
                this.Url = StringUtil.Deserialize(br);
            }
            else
            {
                this.Url = null;
            }

            if ((this.Flags & 16) != 0)
            {
                this.ThumbUrl = StringUtil.Deserialize(br);
            }
            else
            {
                this.ThumbUrl = null;
            }

            if ((this.Flags & 32) != 0)
            {
                this.ContentUrl = StringUtil.Deserialize(br);
            }
            else
            {
                this.ContentUrl = null;
            }

            if ((this.Flags & 32) != 0)
            {
                this.ContentType = StringUtil.Deserialize(br);
            }
            else
            {
                this.ContentType = null;
            }

            if ((this.Flags & 64) != 0)
            {
                this.W = br.ReadInt32();
            }
            else
            {
                this.W = null;
            }

            if ((this.Flags & 64) != 0)
            {
                this.H = br.ReadInt32();
            }
            else
            {
                this.H = null;
            }

            if ((this.Flags & 128) != 0)
            {
                this.Duration = br.ReadInt32();
            }
            else
            {
                this.Duration = null;
            }

            this.SendMessage = (TLAbsInputBotInlineMessage)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);
            StringUtil.Serialize(this.Id, bw);
            StringUtil.Serialize(this.Type, bw);
            if ((this.Flags & 2) != 0)
            {
                StringUtil.Serialize(this.Title, bw);
            }

            if ((this.Flags & 4) != 0)
            {
                StringUtil.Serialize(this.Description, bw);
            }

            if ((this.Flags & 8) != 0)
            {
                StringUtil.Serialize(this.Url, bw);
            }

            if ((this.Flags & 16) != 0)
            {
                StringUtil.Serialize(this.ThumbUrl, bw);
            }

            if ((this.Flags & 32) != 0)
            {
                StringUtil.Serialize(this.ContentUrl, bw);
            }

            if ((this.Flags & 32) != 0)
            {
                StringUtil.Serialize(this.ContentType, bw);
            }

            if ((this.Flags & 64) != 0)
            {
                bw.Write(this.W.Value);
            }

            if ((this.Flags & 64) != 0)
            {
                bw.Write(this.H.Value);
            }

            if ((this.Flags & 128) != 0)
            {
                bw.Write(this.Duration.Value);
            }

            ObjectUtils.SerializeObject(this.SendMessage, bw);

        }
    }
}
