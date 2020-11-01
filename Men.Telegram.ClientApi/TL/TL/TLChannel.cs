using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1588737454)]
    public class TLChannel : TLAbsChat
    {
        public override int Constructor
        {
            get
            {
                return -1588737454;
            }
        }

        public int Flags { get; set; }
        public bool Creator { get; set; }
        public bool Kicked { get; set; }
        public bool Left { get; set; }
        public bool Editor { get; set; }
        public bool Moderator { get; set; }
        public bool Broadcast { get; set; }
        public bool Verified { get; set; }
        public bool Megagroup { get; set; }
        public bool Restricted { get; set; }
        public bool Democracy { get; set; }
        public bool Signatures { get; set; }
        public bool Min { get; set; }
        public int Id { get; set; }
        public long? AccessHash { get; set; }
        public string Title { get; set; }
        public string Username { get; set; }
        public TLAbsChatPhoto Photo { get; set; }
        public int Date { get; set; }
        public int Version { get; set; }
        public string RestrictionReason { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.Creator ? (this.Flags | 1) : (this.Flags & ~1);
            this.Flags = this.Kicked ? (this.Flags | 2) : (this.Flags & ~2);
            this.Flags = this.Left ? (this.Flags | 4) : (this.Flags & ~4);
            this.Flags = this.Editor ? (this.Flags | 8) : (this.Flags & ~8);
            this.Flags = this.Moderator ? (this.Flags | 16) : (this.Flags & ~16);
            this.Flags = this.Broadcast ? (this.Flags | 32) : (this.Flags & ~32);
            this.Flags = this.Verified ? (this.Flags | 128) : (this.Flags & ~128);
            this.Flags = this.Megagroup ? (this.Flags | 256) : (this.Flags & ~256);
            this.Flags = this.Restricted ? (this.Flags | 512) : (this.Flags & ~512);
            this.Flags = this.Democracy ? (this.Flags | 1024) : (this.Flags & ~1024);
            this.Flags = this.Signatures ? (this.Flags | 2048) : (this.Flags & ~2048);
            this.Flags = this.Min ? (this.Flags | 4096) : (this.Flags & ~4096);
            this.Flags = this.AccessHash != null ? (this.Flags | 8192) : (this.Flags & ~8192);
            this.Flags = this.Username != null ? (this.Flags | 64) : (this.Flags & ~64);
            this.Flags = this.RestrictionReason != null ? (this.Flags | 512) : (this.Flags & ~512);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            this.Creator = (this.Flags & 1) != 0;
            this.Kicked = (this.Flags & 2) != 0;
            this.Left = (this.Flags & 4) != 0;
            this.Editor = (this.Flags & 8) != 0;
            this.Moderator = (this.Flags & 16) != 0;
            this.Broadcast = (this.Flags & 32) != 0;
            this.Verified = (this.Flags & 128) != 0;
            this.Megagroup = (this.Flags & 256) != 0;
            this.Restricted = (this.Flags & 512) != 0;
            this.Democracy = (this.Flags & 1024) != 0;
            this.Signatures = (this.Flags & 2048) != 0;
            this.Min = (this.Flags & 4096) != 0;
            this.Id = br.ReadInt32();
            if ((this.Flags & 8192) != 0)
                this.AccessHash = br.ReadInt64();
            else
                this.AccessHash = null;

            this.Title = StringUtil.Deserialize(br);
            if ((this.Flags & 64) != 0)
                this.Username = StringUtil.Deserialize(br);
            else
                this.Username = null;

            this.Photo = (TLAbsChatPhoto)ObjectUtils.DeserializeObject(br);
            this.Date = br.ReadInt32();
            this.Version = br.ReadInt32();
            if ((this.Flags & 512) != 0)
                this.RestrictionReason = StringUtil.Deserialize(br);
            else
                this.RestrictionReason = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);












            bw.Write(this.Id);
            if ((this.Flags & 8192) != 0)
                bw.Write(this.AccessHash.Value);
            StringUtil.Serialize(this.Title, bw);
            if ((this.Flags & 64) != 0)
                StringUtil.Serialize(this.Username, bw);
            ObjectUtils.SerializeObject(this.Photo, bw);
            bw.Write(this.Date);
            bw.Write(this.Version);
            if ((this.Flags & 512) != 0)
                StringUtil.Serialize(this.RestrictionReason, bw);

        }
    }
}
