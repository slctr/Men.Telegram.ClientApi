using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Stickers
{
    [TLObject(-2041315650)]
    public class TLRequestAddStickerToSet : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -2041315650;
            }
        }

        public TLAbsInputStickerSet Stickerset { get; set; }
        public TLInputStickerSetItem Sticker { get; set; }
        public Messages.TLStickerSet Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Stickerset = (TLAbsInputStickerSet)ObjectUtils.DeserializeObject(br);
            this.Sticker = (TLInputStickerSetItem)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            ObjectUtils.SerializeObject(this.Stickerset, bw);
            ObjectUtils.SerializeObject(this.Sticker, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            this.Response = (Messages.TLStickerSet)ObjectUtils.DeserializeObject(br);

        }
    }
}
