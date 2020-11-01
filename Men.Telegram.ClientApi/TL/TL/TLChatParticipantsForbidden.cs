using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-57668565)]
    public class TLChatParticipantsForbidden : TLAbsChatParticipants
    {
        public override int Constructor
        {
            get
            {
                return -57668565;
            }
        }

        public int Flags { get; set; }
        public int ChatId { get; set; }
        public TLAbsChatParticipant SelfParticipant { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.SelfParticipant != null ? (this.Flags | 1) : (this.Flags & ~1);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            this.ChatId = br.ReadInt32();
            if ((this.Flags & 1) != 0)
                this.SelfParticipant = (TLAbsChatParticipant)ObjectUtils.DeserializeObject(br);
            else
                this.SelfParticipant = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);
            bw.Write(this.ChatId);
            if ((this.Flags & 1) != 0)
                ObjectUtils.SerializeObject(this.SelfParticipant, bw);

        }
    }
}
