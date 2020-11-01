using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(958863608)]
    public class TLRequestSaveRecentSticker : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 958863608;
            }
        }

        public int Flags { get; set; }
        public bool Attached { get; set; }
        public TLAbsInputDocument Id { get; set; }
        public bool Unsave { get; set; }
        public bool Response { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.Attached ? (this.Flags | 1) : (this.Flags & ~1);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            this.Attached = (this.Flags & 1) != 0;
            this.Id = (TLAbsInputDocument)ObjectUtils.DeserializeObject(br);
            this.Unsave = BoolUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);

            ObjectUtils.SerializeObject(this.Id, bw);
            BoolUtil.Serialize(this.Unsave, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            this.Response = BoolUtil.Deserialize(br);

        }
    }
}
