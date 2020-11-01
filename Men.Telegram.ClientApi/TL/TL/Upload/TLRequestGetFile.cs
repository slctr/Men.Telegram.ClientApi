using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Upload
{
    [TLObject(-475607115)]
    public class TLRequestGetFile : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -475607115;
            }
        }

        public TLAbsInputFileLocation Location { get; set; }
        public int Offset { get; set; }
        public int Limit { get; set; }
        public Upload.TLAbsFile Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Location = (TLAbsInputFileLocation)ObjectUtils.DeserializeObject(br);
            this.Offset = br.ReadInt32();
            this.Limit = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            ObjectUtils.SerializeObject(this.Location, bw);
            bw.Write(this.Offset);
            bw.Write(this.Limit);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            this.Response = (Upload.TLAbsFile)ObjectUtils.DeserializeObject(br);

        }
    }
}
