using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1670052855)]
    public class TLFoundGifCached : TLAbsFoundGif
    {
        public override int Constructor
        {
            get
            {
                return -1670052855;
            }
        }

        public string Url { get; set; }
        public TLAbsPhoto Photo { get; set; }
        public TLAbsDocument Document { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Url = StringUtil.Deserialize(br);
            this.Photo = (TLAbsPhoto)ObjectUtils.DeserializeObject(br);
            this.Document = (TLAbsDocument)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            StringUtil.Serialize(this.Url, bw);
            ObjectUtils.SerializeObject(this.Photo, bw);
            ObjectUtils.SerializeObject(this.Document, bw);

        }
    }
}
