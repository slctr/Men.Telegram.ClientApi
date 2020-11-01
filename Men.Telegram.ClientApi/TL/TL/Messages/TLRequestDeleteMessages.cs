using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(-443640366)]
    public class TLRequestDeleteMessages : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -443640366;
            }
        }

        public int Flags { get; set; }
        public bool Revoke { get; set; }
        public TLVector<int> Id { get; set; }
        public Messages.TLAffectedMessages Response { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.Revoke ? (this.Flags | 1) : (this.Flags & ~1);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            this.Revoke = (this.Flags & 1) != 0;
            this.Id = (TLVector<int>)ObjectUtils.DeserializeVector<int>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);

            ObjectUtils.SerializeObject(this.Id, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            this.Response = (Messages.TLAffectedMessages)ObjectUtils.DeserializeObject(br);

        }
    }
}
