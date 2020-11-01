using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(1855224129)]
    public class TLUpdateChatAdmins : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return 1855224129;
            }
        }

        public int ChatId { get; set; }
        public bool Enabled { get; set; }
        public int Version { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.ChatId = br.ReadInt32();
            this.Enabled = BoolUtil.Deserialize(br);
            this.Version = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            bw.Write(this.ChatId);
            BoolUtil.Serialize(this.Enabled, bw);
            bw.Write(this.Version);

        }
    }
}
