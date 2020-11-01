using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1009430225)]
    public class TLChannelFull : TLAbsChatFull
    {
        public override int Constructor
        {
            get
            {
                return -1009430225;
            }
        }

        public int Flags { get; set; }
        public bool CanViewParticipants { get; set; }
        public bool CanSetUsername { get; set; }
        public int Id { get; set; }
        public string About { get; set; }
        public int? ParticipantsCount { get; set; }
        public int? AdminsCount { get; set; }
        public int? KickedCount { get; set; }
        public int ReadInboxMaxId { get; set; }
        public int ReadOutboxMaxId { get; set; }
        public int UnreadCount { get; set; }
        public TLAbsPhoto ChatPhoto { get; set; }
        public TLAbsPeerNotifySettings NotifySettings { get; set; }
        public TLAbsExportedChatInvite ExportedInvite { get; set; }
        public TLVector<TLBotInfo> BotInfo { get; set; }
        public int? MigratedFromChatId { get; set; }
        public int? MigratedFromMaxId { get; set; }
        public int? PinnedMsgId { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.CanViewParticipants ? (this.Flags | 8) : (this.Flags & ~8);
            this.Flags = this.CanSetUsername ? (this.Flags | 64) : (this.Flags & ~64);
            this.Flags = this.ParticipantsCount != null ? (this.Flags | 1) : (this.Flags & ~1);
            this.Flags = this.AdminsCount != null ? (this.Flags | 2) : (this.Flags & ~2);
            this.Flags = this.KickedCount != null ? (this.Flags | 4) : (this.Flags & ~4);
            this.Flags = this.MigratedFromChatId != null ? (this.Flags | 16) : (this.Flags & ~16);
            this.Flags = this.MigratedFromMaxId != null ? (this.Flags | 16) : (this.Flags & ~16);
            this.Flags = this.PinnedMsgId != null ? (this.Flags | 32) : (this.Flags & ~32);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            this.CanViewParticipants = (this.Flags & 8) != 0;
            this.CanSetUsername = (this.Flags & 64) != 0;
            this.Id = br.ReadInt32();
            this.About = StringUtil.Deserialize(br);
            if ((this.Flags & 1) != 0)
                this.ParticipantsCount = br.ReadInt32();
            else
                this.ParticipantsCount = null;

            if ((this.Flags & 2) != 0)
                this.AdminsCount = br.ReadInt32();
            else
                this.AdminsCount = null;

            if ((this.Flags & 4) != 0)
                this.KickedCount = br.ReadInt32();
            else
                this.KickedCount = null;

            this.ReadInboxMaxId = br.ReadInt32();
            this.ReadOutboxMaxId = br.ReadInt32();
            this.UnreadCount = br.ReadInt32();
            this.ChatPhoto = (TLAbsPhoto)ObjectUtils.DeserializeObject(br);
            this.NotifySettings = (TLAbsPeerNotifySettings)ObjectUtils.DeserializeObject(br);
            this.ExportedInvite = (TLAbsExportedChatInvite)ObjectUtils.DeserializeObject(br);
            this.BotInfo = (TLVector<TLBotInfo>)ObjectUtils.DeserializeVector<TLBotInfo>(br);
            if ((this.Flags & 16) != 0)
                this.MigratedFromChatId = br.ReadInt32();
            else
                this.MigratedFromChatId = null;

            if ((this.Flags & 16) != 0)
                this.MigratedFromMaxId = br.ReadInt32();
            else
                this.MigratedFromMaxId = null;

            if ((this.Flags & 32) != 0)
                this.PinnedMsgId = br.ReadInt32();
            else
                this.PinnedMsgId = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);


            bw.Write(this.Id);
            StringUtil.Serialize(this.About, bw);
            if ((this.Flags & 1) != 0)
                bw.Write(this.ParticipantsCount.Value);
            if ((this.Flags & 2) != 0)
                bw.Write(this.AdminsCount.Value);
            if ((this.Flags & 4) != 0)
                bw.Write(this.KickedCount.Value);
            bw.Write(this.ReadInboxMaxId);
            bw.Write(this.ReadOutboxMaxId);
            bw.Write(this.UnreadCount);
            ObjectUtils.SerializeObject(this.ChatPhoto, bw);
            ObjectUtils.SerializeObject(this.NotifySettings, bw);
            ObjectUtils.SerializeObject(this.ExportedInvite, bw);
            ObjectUtils.SerializeObject(this.BotInfo, bw);
            if ((this.Flags & 16) != 0)
                bw.Write(this.MigratedFromChatId.Value);
            if ((this.Flags & 16) != 0)
                bw.Write(this.MigratedFromMaxId.Value);
            if ((this.Flags & 32) != 0)
                bw.Write(this.PinnedMsgId.Value);

        }
    }
}
