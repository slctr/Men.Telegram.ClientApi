using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(1662637586)]
    public class TLDocumentAttributeSticker : TLAbsDocumentAttribute
    {
        public override int Constructor
        {
            get
            {
                return 1662637586;
            }
        }

        public int Flags { get; set; }
        public bool Mask { get; set; }
        public string Alt { get; set; }
        public TLAbsInputStickerSet Stickerset { get; set; }
        public TLMaskCoords MaskCoords { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.Mask ? (this.Flags | 2) : (this.Flags & ~2);
            this.Flags = this.MaskCoords != null ? (this.Flags | 1) : (this.Flags & ~1);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            this.Mask = (this.Flags & 2) != 0;
            this.Alt = StringUtil.Deserialize(br);
            this.Stickerset = (TLAbsInputStickerSet)ObjectUtils.DeserializeObject(br);
            if ((this.Flags & 1) != 0)
                this.MaskCoords = (TLMaskCoords)ObjectUtils.DeserializeObject(br);
            else
                this.MaskCoords = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);

            StringUtil.Serialize(this.Alt, bw);
            ObjectUtils.SerializeObject(this.Stickerset, bw);
            if ((this.Flags & 1) != 0)
                ObjectUtils.SerializeObject(this.MaskCoords, bw);

        }
    }
}
