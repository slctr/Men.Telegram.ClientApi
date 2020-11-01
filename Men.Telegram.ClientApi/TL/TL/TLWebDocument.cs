using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-971322408)]
    public class TLWebDocument : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -971322408;
            }
        }

        public string Url { get; set; }
        public long AccessHash { get; set; }
        public int Size { get; set; }
        public string MimeType { get; set; }
        public TLVector<TLAbsDocumentAttribute> Attributes { get; set; }
        public int DcId { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Url = StringUtil.Deserialize(br);
            this.AccessHash = br.ReadInt64();
            this.Size = br.ReadInt32();
            this.MimeType = StringUtil.Deserialize(br);
            this.Attributes = (TLVector<TLAbsDocumentAttribute>)ObjectUtils.DeserializeVector<TLAbsDocumentAttribute>(br);
            this.DcId = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            StringUtil.Serialize(this.Url, bw);
            bw.Write(this.AccessHash);
            bw.Write(this.Size);
            StringUtil.Serialize(this.MimeType, bw);
            ObjectUtils.SerializeObject(this.Attributes, bw);
            bw.Write(this.DcId);

        }
    }
}
