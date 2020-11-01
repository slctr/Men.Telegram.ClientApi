using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(1851755554)]
    public class TLUpdateChatParticipantDelete : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return 1851755554;
            }
        }

        public int ChatId { get; set; }
        public int UserId { get; set; }
        public int Version { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.ChatId = br.ReadInt32();
            this.UserId = br.ReadInt32();
            this.Version = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            bw.Write(this.ChatId);
            bw.Write(this.UserId);
            bw.Write(this.Version);

        }
    }
}
