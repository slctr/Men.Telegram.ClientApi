using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(546203849)]
    public class TLInputMessageEntityMentionName : TLAbsMessageEntity
    {
        public override int Constructor
        {
            get
            {
                return 546203849;
            }
        }

        public int Offset { get; set; }
        public int Length { get; set; }
        public TLAbsInputUser UserId { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Offset = br.ReadInt32();
            this.Length = br.ReadInt32();
            this.UserId = (TLAbsInputUser)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            bw.Write(this.Offset);
            bw.Write(this.Length);
            ObjectUtils.SerializeObject(this.UserId, bw);

        }
    }
}
