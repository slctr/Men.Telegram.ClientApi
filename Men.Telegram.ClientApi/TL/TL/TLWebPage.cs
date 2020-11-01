using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(1594340540)]
    public class TLWebPage : TLAbsWebPage
    {
        public override int Constructor
        {
            get
            {
                return 1594340540;
            }
        }

        public int Flags { get; set; }
        public long Id { get; set; }
        public string Url { get; set; }
        public string DisplayUrl { get; set; }
        public int Hash { get; set; }
        public string Type { get; set; }
        public string SiteName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TLAbsPhoto Photo { get; set; }
        public string EmbedUrl { get; set; }
        public string EmbedType { get; set; }
        public int? EmbedWidth { get; set; }
        public int? EmbedHeight { get; set; }
        public int? Duration { get; set; }
        public string Author { get; set; }
        public TLAbsDocument Document { get; set; }
        public TLAbsPage CachedPage { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.Type != null ? (this.Flags | 1) : (this.Flags & ~1);
            this.Flags = this.SiteName != null ? (this.Flags | 2) : (this.Flags & ~2);
            this.Flags = this.Title != null ? (this.Flags | 4) : (this.Flags & ~4);
            this.Flags = this.Description != null ? (this.Flags | 8) : (this.Flags & ~8);
            this.Flags = this.Photo != null ? (this.Flags | 16) : (this.Flags & ~16);
            this.Flags = this.EmbedUrl != null ? (this.Flags | 32) : (this.Flags & ~32);
            this.Flags = this.EmbedType != null ? (this.Flags | 32) : (this.Flags & ~32);
            this.Flags = this.EmbedWidth != null ? (this.Flags | 64) : (this.Flags & ~64);
            this.Flags = this.EmbedHeight != null ? (this.Flags | 64) : (this.Flags & ~64);
            this.Flags = this.Duration != null ? (this.Flags | 128) : (this.Flags & ~128);
            this.Flags = this.Author != null ? (this.Flags | 256) : (this.Flags & ~256);
            this.Flags = this.Document != null ? (this.Flags | 512) : (this.Flags & ~512);
            this.Flags = this.CachedPage != null ? (this.Flags | 1024) : (this.Flags & ~1024);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            this.Id = br.ReadInt64();
            this.Url = StringUtil.Deserialize(br);
            this.DisplayUrl = StringUtil.Deserialize(br);
            this.Hash = br.ReadInt32();
            if ((this.Flags & 1) != 0)
            {
                this.Type = StringUtil.Deserialize(br);
            }
            else
            {
                this.Type = null;
            }

            if ((this.Flags & 2) != 0)
            {
                this.SiteName = StringUtil.Deserialize(br);
            }
            else
            {
                this.SiteName = null;
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

            if ((this.Flags & 16) != 0)
            {
                this.Photo = (TLAbsPhoto)ObjectUtils.DeserializeObject(br);
            }
            else
            {
                this.Photo = null;
            }

            if ((this.Flags & 32) != 0)
            {
                this.EmbedUrl = StringUtil.Deserialize(br);
            }
            else
            {
                this.EmbedUrl = null;
            }

            if ((this.Flags & 32) != 0)
            {
                this.EmbedType = StringUtil.Deserialize(br);
            }
            else
            {
                this.EmbedType = null;
            }

            if ((this.Flags & 64) != 0)
            {
                this.EmbedWidth = br.ReadInt32();
            }
            else
            {
                this.EmbedWidth = null;
            }

            if ((this.Flags & 64) != 0)
            {
                this.EmbedHeight = br.ReadInt32();
            }
            else
            {
                this.EmbedHeight = null;
            }

            if ((this.Flags & 128) != 0)
            {
                this.Duration = br.ReadInt32();
            }
            else
            {
                this.Duration = null;
            }

            if ((this.Flags & 256) != 0)
            {
                this.Author = StringUtil.Deserialize(br);
            }
            else
            {
                this.Author = null;
            }

            if ((this.Flags & 512) != 0)
            {
                this.Document = (TLAbsDocument)ObjectUtils.DeserializeObject(br);
            }
            else
            {
                this.Document = null;
            }

            if ((this.Flags & 1024) != 0)
            {
                this.CachedPage = (TLAbsPage)ObjectUtils.DeserializeObject(br);
            }
            else
            {
                this.CachedPage = null;
            }
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);
            bw.Write(this.Id);
            StringUtil.Serialize(this.Url, bw);
            StringUtil.Serialize(this.DisplayUrl, bw);
            bw.Write(this.Hash);
            if ((this.Flags & 1) != 0)
            {
                StringUtil.Serialize(this.Type, bw);
            }

            if ((this.Flags & 2) != 0)
            {
                StringUtil.Serialize(this.SiteName, bw);
            }

            if ((this.Flags & 4) != 0)
            {
                StringUtil.Serialize(this.Title, bw);
            }

            if ((this.Flags & 8) != 0)
            {
                StringUtil.Serialize(this.Description, bw);
            }

            if ((this.Flags & 16) != 0)
            {
                ObjectUtils.SerializeObject(this.Photo, bw);
            }

            if ((this.Flags & 32) != 0)
            {
                StringUtil.Serialize(this.EmbedUrl, bw);
            }

            if ((this.Flags & 32) != 0)
            {
                StringUtil.Serialize(this.EmbedType, bw);
            }

            if ((this.Flags & 64) != 0)
            {
                bw.Write(this.EmbedWidth.Value);
            }

            if ((this.Flags & 64) != 0)
            {
                bw.Write(this.EmbedHeight.Value);
            }

            if ((this.Flags & 128) != 0)
            {
                bw.Write(this.Duration.Value);
            }

            if ((this.Flags & 256) != 0)
            {
                StringUtil.Serialize(this.Author, bw);
            }

            if ((this.Flags & 512) != 0)
            {
                ObjectUtils.SerializeObject(this.Document, bw);
            }

            if ((this.Flags & 1024) != 0)
            {
                ObjectUtils.SerializeObject(this.CachedPage, bw);
            }
        }
    }
}
