using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(253890367)]
    public class TLUserFull : TLObject
    {
        public override int Constructor
        {
            get
            {
                return 253890367;
            }
        }

        public int Flags { get; set; }
        public bool Blocked { get; set; }
        public bool PhoneCallsAvailable { get; set; }
        public bool PhoneCallsPrivate { get; set; }
        public TLAbsUser User { get; set; }
        public string About { get; set; }
        public Contacts.TLLink Link { get; set; }
        public TLAbsPhoto ProfilePhoto { get; set; }
        public TLAbsPeerNotifySettings NotifySettings { get; set; }
        public TLBotInfo BotInfo { get; set; }
        public int CommonChatsCount { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.Blocked ? (this.Flags | 1) : (this.Flags & ~1);
            this.Flags = this.PhoneCallsAvailable ? (this.Flags | 16) : (this.Flags & ~16);
            this.Flags = this.PhoneCallsPrivate ? (this.Flags | 32) : (this.Flags & ~32);
            this.Flags = this.About != null ? (this.Flags | 2) : (this.Flags & ~2);
            this.Flags = this.ProfilePhoto != null ? (this.Flags | 4) : (this.Flags & ~4);
            this.Flags = this.BotInfo != null ? (this.Flags | 8) : (this.Flags & ~8);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            this.Blocked = (this.Flags & 1) != 0;
            this.PhoneCallsAvailable = (this.Flags & 16) != 0;
            this.PhoneCallsPrivate = (this.Flags & 32) != 0;
            this.User = (TLAbsUser)ObjectUtils.DeserializeObject(br);
            if ((this.Flags & 2) != 0)
            {
                this.About = StringUtil.Deserialize(br);
            }
            else
            {
                this.About = null;
            }

            this.Link = (Contacts.TLLink)ObjectUtils.DeserializeObject(br);
            if ((this.Flags & 4) != 0)
            {
                this.ProfilePhoto = (TLAbsPhoto)ObjectUtils.DeserializeObject(br);
            }
            else
            {
                this.ProfilePhoto = null;
            }

            this.NotifySettings = (TLAbsPeerNotifySettings)ObjectUtils.DeserializeObject(br);
            if ((this.Flags & 8) != 0)
            {
                this.BotInfo = (TLBotInfo)ObjectUtils.DeserializeObject(br);
            }
            else
            {
                this.BotInfo = null;
            }

            this.CommonChatsCount = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);



            ObjectUtils.SerializeObject(this.User, bw);
            if ((this.Flags & 2) != 0)
            {
                StringUtil.Serialize(this.About, bw);
            }

            ObjectUtils.SerializeObject(this.Link, bw);
            if ((this.Flags & 4) != 0)
            {
                ObjectUtils.SerializeObject(this.ProfilePhoto, bw);
            }

            ObjectUtils.SerializeObject(this.NotifySettings, bw);
            if ((this.Flags & 8) != 0)
            {
                ObjectUtils.SerializeObject(this.BotInfo, bw);
            }

            bw.Write(this.CommonChatsCount);

        }
    }
}
