using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-6249322)]
    public class TLInputStickerSetItem : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -6249322;
            }
        }

        public int Flags { get; set; }
        public TLAbsInputDocument Document { get; set; }
        public string Emoji { get; set; }
        public TLMaskCoords MaskCoords { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.MaskCoords != null ? (this.Flags | 1) : (this.Flags & ~1);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            this.Document = (TLAbsInputDocument)ObjectUtils.DeserializeObject(br);
            this.Emoji = StringUtil.Deserialize(br);
            if ((this.Flags & 1) != 0)
            {
                this.MaskCoords = (TLMaskCoords)ObjectUtils.DeserializeObject(br);
            }
            else
            {
                this.MaskCoords = null;
            }
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);
            ObjectUtils.SerializeObject(this.Document, bw);
            StringUtil.Serialize(this.Emoji, bw);
            if ((this.Flags & 1) != 0)
            {
                ObjectUtils.SerializeObject(this.MaskCoords, bw);
            }
        }
    }
}
