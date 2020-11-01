using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(1632839530)]
    public class TLChatPhoto : TLAbsChatPhoto
    {
        public override int Constructor
        {
            get
            {
                return 1632839530;
            }
        }

        public TLAbsFileLocation PhotoSmall { get; set; }
        public TLAbsFileLocation PhotoBig { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.PhotoSmall = (TLAbsFileLocation)ObjectUtils.DeserializeObject(br);
            this.PhotoBig = (TLAbsFileLocation)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            ObjectUtils.SerializeObject(this.PhotoSmall, bw);
            ObjectUtils.SerializeObject(this.PhotoBig, bw);

        }
    }
}
