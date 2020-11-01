using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(1587647177)]
    public class TLRequestGetRecentStickers : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 1587647177;
            }
        }

        public int Flags { get; set; }
        public bool Attached { get; set; }
        public int Hash { get; set; }
        public Messages.TLAbsRecentStickers Response { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.Attached ? (this.Flags | 1) : (this.Flags & ~1);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            this.Attached = (this.Flags & 1) != 0;
            this.Hash = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);

            bw.Write(this.Hash);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            this.Response = (Messages.TLAbsRecentStickers)ObjectUtils.DeserializeObject(br);

        }
    }
}
