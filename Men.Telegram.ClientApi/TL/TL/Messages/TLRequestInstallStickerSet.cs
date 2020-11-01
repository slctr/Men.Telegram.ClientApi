using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(-946871200)]
    public class TLRequestInstallStickerSet : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -946871200;
            }
        }

        public TLAbsInputStickerSet Stickerset { get; set; }
        public bool Archived { get; set; }
        public Messages.TLAbsStickerSetInstallResult Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Stickerset = (TLAbsInputStickerSet)ObjectUtils.DeserializeObject(br);
            this.Archived = BoolUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            ObjectUtils.SerializeObject(this.Stickerset, bw);
            BoolUtil.Serialize(this.Archived, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            this.Response = (Messages.TLAbsStickerSetInstallResult)ObjectUtils.DeserializeObject(br);

        }
    }
}
