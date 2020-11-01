using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(1475442322)]
    public class TLRequestGetArchivedStickers : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 1475442322;
            }
        }

        public int Flags { get; set; }
        public bool Masks { get; set; }
        public long OffsetId { get; set; }
        public int Limit { get; set; }
        public Messages.TLArchivedStickers Response { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.Masks ? (this.Flags | 1) : (this.Flags & ~1);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            this.Masks = (this.Flags & 1) != 0;
            this.OffsetId = br.ReadInt64();
            this.Limit = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);

            bw.Write(this.OffsetId);
            bw.Write(this.Limit);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            this.Response = (Messages.TLArchivedStickers)ObjectUtils.DeserializeObject(br);

        }
    }
}
