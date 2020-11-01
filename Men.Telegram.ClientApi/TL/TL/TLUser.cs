using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(773059779)]
    public class TLUser : TLAbsUser
    {
        public override int Constructor
        {
            get
            {
                return 773059779;
            }
        }

        public int Flags { get; set; }
        public bool Self { get; set; }
        public bool Contact { get; set; }
        public bool MutualContact { get; set; }
        public bool Deleted { get; set; }
        public bool Bot { get; set; }
        public bool BotChatHistory { get; set; }
        public bool BotNochats { get; set; }
        public bool Verified { get; set; }
        public bool Restricted { get; set; }
        public bool Min { get; set; }
        public bool BotInlineGeo { get; set; }
        public int Id { get; set; }
        public long? AccessHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Phone { get; set; }
        public TLAbsUserProfilePhoto Photo { get; set; }
        public TLAbsUserStatus Status { get; set; }
        public int? BotInfoVersion { get; set; }
        public string RestrictionReason { get; set; }
        public string BotInlinePlaceholder { get; set; }
        public string LangCode { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.Self ? (this.Flags | 1024) : (this.Flags & ~1024);
            this.Flags = this.Contact ? (this.Flags | 2048) : (this.Flags & ~2048);
            this.Flags = this.MutualContact ? (this.Flags | 4096) : (this.Flags & ~4096);
            this.Flags = this.Deleted ? (this.Flags | 8192) : (this.Flags & ~8192);
            this.Flags = this.Bot ? (this.Flags | 16384) : (this.Flags & ~16384);
            this.Flags = this.BotChatHistory ? (this.Flags | 32768) : (this.Flags & ~32768);
            this.Flags = this.BotNochats ? (this.Flags | 65536) : (this.Flags & ~65536);
            this.Flags = this.Verified ? (this.Flags | 131072) : (this.Flags & ~131072);
            this.Flags = this.Restricted ? (this.Flags | 262144) : (this.Flags & ~262144);
            this.Flags = this.Min ? (this.Flags | 1048576) : (this.Flags & ~1048576);
            this.Flags = this.BotInlineGeo ? (this.Flags | 2097152) : (this.Flags & ~2097152);
            this.Flags = this.AccessHash != null ? (this.Flags | 1) : (this.Flags & ~1);
            this.Flags = this.FirstName != null ? (this.Flags | 2) : (this.Flags & ~2);
            this.Flags = this.LastName != null ? (this.Flags | 4) : (this.Flags & ~4);
            this.Flags = this.Username != null ? (this.Flags | 8) : (this.Flags & ~8);
            this.Flags = this.Phone != null ? (this.Flags | 16) : (this.Flags & ~16);
            this.Flags = this.Photo != null ? (this.Flags | 32) : (this.Flags & ~32);
            this.Flags = this.Status != null ? (this.Flags | 64) : (this.Flags & ~64);
            this.Flags = this.BotInfoVersion != null ? (this.Flags | 16384) : (this.Flags & ~16384);
            this.Flags = this.RestrictionReason != null ? (this.Flags | 262144) : (this.Flags & ~262144);
            this.Flags = this.BotInlinePlaceholder != null ? (this.Flags | 524288) : (this.Flags & ~524288);
            this.Flags = this.LangCode != null ? (this.Flags | 4194304) : (this.Flags & ~4194304);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            this.Self = (this.Flags & 1024) != 0;
            this.Contact = (this.Flags & 2048) != 0;
            this.MutualContact = (this.Flags & 4096) != 0;
            this.Deleted = (this.Flags & 8192) != 0;
            this.Bot = (this.Flags & 16384) != 0;
            this.BotChatHistory = (this.Flags & 32768) != 0;
            this.BotNochats = (this.Flags & 65536) != 0;
            this.Verified = (this.Flags & 131072) != 0;
            this.Restricted = (this.Flags & 262144) != 0;
            this.Min = (this.Flags & 1048576) != 0;
            this.BotInlineGeo = (this.Flags & 2097152) != 0;
            this.Id = br.ReadInt32();
            if ((this.Flags & 1) != 0)
                this.AccessHash = br.ReadInt64();
            else
                this.AccessHash = null;

            if ((this.Flags & 2) != 0)
                this.FirstName = StringUtil.Deserialize(br);
            else
                this.FirstName = null;

            if ((this.Flags & 4) != 0)
                this.LastName = StringUtil.Deserialize(br);
            else
                this.LastName = null;

            if ((this.Flags & 8) != 0)
                this.Username = StringUtil.Deserialize(br);
            else
                this.Username = null;

            if ((this.Flags & 16) != 0)
                this.Phone = StringUtil.Deserialize(br);
            else
                this.Phone = null;

            if ((this.Flags & 32) != 0)
                this.Photo = (TLAbsUserProfilePhoto)ObjectUtils.DeserializeObject(br);
            else
                this.Photo = null;

            if ((this.Flags & 64) != 0)
                this.Status = (TLAbsUserStatus)ObjectUtils.DeserializeObject(br);
            else
                this.Status = null;

            if ((this.Flags & 16384) != 0)
                this.BotInfoVersion = br.ReadInt32();
            else
                this.BotInfoVersion = null;

            if ((this.Flags & 262144) != 0)
                this.RestrictionReason = StringUtil.Deserialize(br);
            else
                this.RestrictionReason = null;

            if ((this.Flags & 524288) != 0)
                this.BotInlinePlaceholder = StringUtil.Deserialize(br);
            else
                this.BotInlinePlaceholder = null;

            if ((this.Flags & 4194304) != 0)
                this.LangCode = StringUtil.Deserialize(br);
            else
                this.LangCode = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);











            bw.Write(this.Id);
            if ((this.Flags & 1) != 0)
                bw.Write(this.AccessHash.Value);
            if ((this.Flags & 2) != 0)
                StringUtil.Serialize(this.FirstName, bw);
            if ((this.Flags & 4) != 0)
                StringUtil.Serialize(this.LastName, bw);
            if ((this.Flags & 8) != 0)
                StringUtil.Serialize(this.Username, bw);
            if ((this.Flags & 16) != 0)
                StringUtil.Serialize(this.Phone, bw);
            if ((this.Flags & 32) != 0)
                ObjectUtils.SerializeObject(this.Photo, bw);
            if ((this.Flags & 64) != 0)
                ObjectUtils.SerializeObject(this.Status, bw);
            if ((this.Flags & 16384) != 0)
                bw.Write(this.BotInfoVersion.Value);
            if ((this.Flags & 262144) != 0)
                StringUtil.Serialize(this.RestrictionReason, bw);
            if ((this.Flags & 524288) != 0)
                StringUtil.Serialize(this.BotInlinePlaceholder, bw);
            if ((this.Flags & 4194304) != 0)
                StringUtil.Serialize(this.LangCode, bw);

        }
    }
}
