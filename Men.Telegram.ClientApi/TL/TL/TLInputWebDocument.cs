using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1678949555)]
    public class TLInputWebDocument : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -1678949555;
            }
        }

        public string Url { get; set; }
        public int Size { get; set; }
        public string MimeType { get; set; }
        public TLVector<TLAbsDocumentAttribute> Attributes { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Url = StringUtil.Deserialize(br);
            this.Size = br.ReadInt32();
            this.MimeType = StringUtil.Deserialize(br);
            this.Attributes = (TLVector<TLAbsDocumentAttribute>)ObjectUtils.DeserializeVector<TLAbsDocumentAttribute>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            StringUtil.Serialize(this.Url, bw);
            bw.Write(this.Size);
            StringUtil.Serialize(this.MimeType, bw);
            ObjectUtils.SerializeObject(this.Attributes, bw);

        }
    }
}
