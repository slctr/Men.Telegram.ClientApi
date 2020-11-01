using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-613092008)]
    public class TLChatInvite : TLAbsChatInvite
    {
        public override int Constructor
        {
            get
            {
                return -613092008;
            }
        }

        public int Flags { get; set; }
        public bool Channel { get; set; }
        public bool Broadcast { get; set; }
        public bool Public { get; set; }
        public bool Megagroup { get; set; }
        public string Title { get; set; }
        public TLAbsChatPhoto Photo { get; set; }
        public int ParticipantsCount { get; set; }
        public TLVector<TLAbsUser> Participants { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.Channel ? (this.Flags | 1) : (this.Flags & ~1);
            this.Flags = this.Broadcast ? (this.Flags | 2) : (this.Flags & ~2);
            this.Flags = this.Public ? (this.Flags | 4) : (this.Flags & ~4);
            this.Flags = this.Megagroup ? (this.Flags | 8) : (this.Flags & ~8);
            this.Flags = this.Participants != null ? (this.Flags | 16) : (this.Flags & ~16);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            this.Channel = (this.Flags & 1) != 0;
            this.Broadcast = (this.Flags & 2) != 0;
            this.Public = (this.Flags & 4) != 0;
            this.Megagroup = (this.Flags & 8) != 0;
            this.Title = StringUtil.Deserialize(br);
            this.Photo = (TLAbsChatPhoto)ObjectUtils.DeserializeObject(br);
            this.ParticipantsCount = br.ReadInt32();
            if ((this.Flags & 16) != 0)
            {
                this.Participants = (TLVector<TLAbsUser>)ObjectUtils.DeserializeVector<TLAbsUser>(br);
            }
            else
            {
                this.Participants = null;
            }
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);




            StringUtil.Serialize(this.Title, bw);
            ObjectUtils.SerializeObject(this.Photo, bw);
            bw.Write(this.ParticipantsCount);
            if ((this.Flags & 16) != 0)
            {
                ObjectUtils.SerializeObject(this.Participants, bw);
            }
        }
    }
}
