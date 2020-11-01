using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(313694676)]
    public class TLStickerPack : TLObject
    {
        public override int Constructor
        {
            get
            {
                return 313694676;
            }
        }

        public string Emoticon { get; set; }
        public TLVector<long> Documents { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Emoticon = StringUtil.Deserialize(br);
            this.Documents = (TLVector<long>)ObjectUtils.DeserializeVector<long>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            StringUtil.Serialize(this.Emoticon, bw);
            ObjectUtils.SerializeObject(this.Documents, bw);

        }
    }
}
