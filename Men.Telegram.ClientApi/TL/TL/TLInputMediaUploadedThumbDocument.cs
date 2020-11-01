using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(1356369070)]
    public class TLInputMediaUploadedThumbDocument : TLAbsInputMedia
    {
        public override int Constructor
        {
            get
            {
                return 1356369070;
            }
        }

        public int Flags { get; set; }
        public TLAbsInputFile File { get; set; }
        public TLAbsInputFile Thumb { get; set; }
        public string MimeType { get; set; }
        public TLVector<TLAbsDocumentAttribute> Attributes { get; set; }
        public string Caption { get; set; }
        public TLVector<TLAbsInputDocument> Stickers { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.Stickers != null ? (this.Flags | 1) : (this.Flags & ~1);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            this.File = (TLAbsInputFile)ObjectUtils.DeserializeObject(br);
            this.Thumb = (TLAbsInputFile)ObjectUtils.DeserializeObject(br);
            this.MimeType = StringUtil.Deserialize(br);
            this.Attributes = (TLVector<TLAbsDocumentAttribute>)ObjectUtils.DeserializeVector<TLAbsDocumentAttribute>(br);
            this.Caption = StringUtil.Deserialize(br);
            if ((this.Flags & 1) != 0)
                this.Stickers = (TLVector<TLAbsInputDocument>)ObjectUtils.DeserializeVector<TLAbsInputDocument>(br);
            else
                this.Stickers = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);
            ObjectUtils.SerializeObject(this.File, bw);
            ObjectUtils.SerializeObject(this.Thumb, bw);
            StringUtil.Serialize(this.MimeType, bw);
            ObjectUtils.SerializeObject(this.Attributes, bw);
            StringUtil.Serialize(this.Caption, bw);
            if ((this.Flags & 1) != 0)
                ObjectUtils.SerializeObject(this.Stickers, bw);

        }
    }
}
