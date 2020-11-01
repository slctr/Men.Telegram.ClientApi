using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(639215886)]
    public class TLRequestGetStickerSet : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 639215886;
            }
        }

        public TLAbsInputStickerSet Stickerset { get; set; }
        public Messages.TLStickerSet Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Stickerset = (TLAbsInputStickerSet)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            ObjectUtils.SerializeObject(this.Stickerset, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            this.Response = (Messages.TLStickerSet)ObjectUtils.DeserializeObject(br);

        }
    }
}
