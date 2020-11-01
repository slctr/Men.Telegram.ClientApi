using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Updates
{
    [TLObject(543450958)]
    public class TLChannelDifference : TLAbsChannelDifference
    {
        public override int Constructor
        {
            get
            {
                return 543450958;
            }
        }

        public int Flags { get; set; }
        public bool Final { get; set; }
        public int Pts { get; set; }
        public int? Timeout { get; set; }
        public TLVector<TLAbsMessage> NewMessages { get; set; }
        public TLVector<TLAbsUpdate> OtherUpdates { get; set; }
        public TLVector<TLAbsChat> Chats { get; set; }
        public TLVector<TLAbsUser> Users { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.Final ? (this.Flags | 1) : (this.Flags & ~1);
            this.Flags = this.Timeout != null ? (this.Flags | 2) : (this.Flags & ~2);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            this.Final = (this.Flags & 1) != 0;
            this.Pts = br.ReadInt32();
            if ((this.Flags & 2) != 0)
                this.Timeout = br.ReadInt32();
            else
                this.Timeout = null;

            this.NewMessages = (TLVector<TLAbsMessage>)ObjectUtils.DeserializeVector<TLAbsMessage>(br);
            this.OtherUpdates = (TLVector<TLAbsUpdate>)ObjectUtils.DeserializeVector<TLAbsUpdate>(br);
            this.Chats = (TLVector<TLAbsChat>)ObjectUtils.DeserializeVector<TLAbsChat>(br);
            this.Users = (TLVector<TLAbsUser>)ObjectUtils.DeserializeVector<TLAbsUser>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);

            bw.Write(this.Pts);
            if ((this.Flags & 2) != 0)
                bw.Write(this.Timeout.Value);
            ObjectUtils.SerializeObject(this.NewMessages, bw);
            ObjectUtils.SerializeObject(this.OtherUpdates, bw);
            ObjectUtils.SerializeObject(this.Chats, bw);
            ObjectUtils.SerializeObject(this.Users, bw);

        }
    }
}
