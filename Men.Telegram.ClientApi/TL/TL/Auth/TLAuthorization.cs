using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Auth
{
    [TLObject(-855308010)]
    public class TLAuthorization : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -855308010;
            }
        }

        public int Flags { get; set; }
        public int? TmpSessions { get; set; }
        public TLAbsUser User { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.TmpSessions != null ? (this.Flags | 1) : (this.Flags & ~1);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            if ((this.Flags & 1) != 0)
            {
                this.TmpSessions = br.ReadInt32();
            }
            else
            {
                this.TmpSessions = null;
            }

            this.User = (TLAbsUser)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);
            if ((this.Flags & 1) != 0)
            {
                bw.Write(this.TmpSessions.Value);
            }

            ObjectUtils.SerializeObject(this.User, bw);

        }
    }
}
