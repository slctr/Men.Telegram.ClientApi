using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(904138920)]
    public class TLStickerSetInstallResultArchive : TLAbsStickerSetInstallResult
    {
        public override int Constructor
        {
            get
            {
                return 904138920;
            }
        }

        public TLVector<TLAbsStickerSetCovered> Sets { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Sets = (TLVector<TLAbsStickerSetCovered>)ObjectUtils.DeserializeVector<TLAbsStickerSetCovered>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            ObjectUtils.SerializeObject(this.Sets, bw);

        }
    }
}
