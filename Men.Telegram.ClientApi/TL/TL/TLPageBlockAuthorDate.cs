using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1162877472)]
    public class TLPageBlockAuthorDate : TLAbsPageBlock
    {
        public override int Constructor
        {
            get
            {
                return -1162877472;
            }
        }

        public TLAbsRichText Author { get; set; }
        public int PublishedDate { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Author = (TLAbsRichText)ObjectUtils.DeserializeObject(br);
            this.PublishedDate = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            ObjectUtils.SerializeObject(this.Author, bw);
            bw.Write(this.PublishedDate);

        }
    }
}
