using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-372860542)]
    public class TLPageBlockPhoto : TLAbsPageBlock
    {
        public override int Constructor
        {
            get
            {
                return -372860542;
            }
        }

        public long PhotoId { get; set; }
        public TLAbsRichText Caption { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.PhotoId = br.ReadInt64();
            this.Caption = (TLAbsRichText)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            bw.Write(this.PhotoId);
            ObjectUtils.SerializeObject(this.Caption, bw);

        }
    }
}
