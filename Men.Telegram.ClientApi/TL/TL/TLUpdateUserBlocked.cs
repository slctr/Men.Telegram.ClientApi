using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-2131957734)]
    public class TLUpdateUserBlocked : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return -2131957734;
            }
        }

        public int UserId { get; set; }
        public bool Blocked { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.UserId = br.ReadInt32();
            this.Blocked = BoolUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            bw.Write(this.UserId);
            BoolUtil.Serialize(this.Blocked, bw);

        }
    }
}
