using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(-1725551049)]
    public class TLChannelMessages : TLAbsMessages
    {
        public override int Constructor
        {
            get
            {
                return -1725551049;
            }
        }

        public int Flags { get; set; }
        public int Pts { get; set; }
        public int Count { get; set; }
        public TLVector<TLAbsMessage> Messages { get; set; }
        public TLVector<TLAbsChat> Chats { get; set; }
        public TLVector<TLAbsUser> Users { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            this.Pts = br.ReadInt32();
            this.Count = br.ReadInt32();
            this.Messages = (TLVector<TLAbsMessage>)ObjectUtils.DeserializeVector<TLAbsMessage>(br);
            this.Chats = (TLVector<TLAbsChat>)ObjectUtils.DeserializeVector<TLAbsChat>(br);
            this.Users = (TLVector<TLAbsUser>)ObjectUtils.DeserializeVector<TLAbsUser>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);
            bw.Write(this.Pts);
            bw.Write(this.Count);
            ObjectUtils.SerializeObject(this.Messages, bw);
            ObjectUtils.SerializeObject(this.Chats, bw);
            ObjectUtils.SerializeObject(this.Users, bw);

        }
    }
}
