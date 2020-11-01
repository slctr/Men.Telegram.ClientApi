using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(-1970352846)]
    public class TLStickers : TLAbsStickers
    {
        public override int Constructor
        {
            get
            {
                return -1970352846;
            }
        }

        public string Hash { get; set; }
        public TLVector<TLAbsDocument> Stickers { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Hash = StringUtil.Deserialize(br);
            this.Stickers = (TLVector<TLAbsDocument>)ObjectUtils.DeserializeVector<TLAbsDocument>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            StringUtil.Serialize(this.Hash, bw);
            ObjectUtils.SerializeObject(this.Stickers, bw);

        }
    }
}