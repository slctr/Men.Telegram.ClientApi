using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-364179876)]
    public class TLUpdateChatParticipantAdd : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return -364179876;
            }
        }

        public int ChatId { get; set; }
        public int UserId { get; set; }
        public int InviterId { get; set; }
        public int Date { get; set; }
        public int Version { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.ChatId = br.ReadInt32();
            this.UserId = br.ReadInt32();
            this.InviterId = br.ReadInt32();
            this.Date = br.ReadInt32();
            this.Version = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            bw.Write(this.ChatId);
            bw.Write(this.UserId);
            bw.Write(this.InviterId);
            bw.Write(this.Date);
            bw.Write(this.Version);

        }
    }
}
