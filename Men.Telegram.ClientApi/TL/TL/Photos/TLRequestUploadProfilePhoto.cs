using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Photos
{
    [TLObject(1328726168)]
    public class TLRequestUploadProfilePhoto : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 1328726168;
            }
        }

        public TLAbsInputFile File { get; set; }
        public Photos.TLPhoto Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.File = (TLAbsInputFile)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            ObjectUtils.SerializeObject(this.File, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            this.Response = (Photos.TLPhoto)ObjectUtils.DeserializeObject(br);

        }
    }
}
