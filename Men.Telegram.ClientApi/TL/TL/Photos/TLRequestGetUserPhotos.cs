using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Photos
{
    [TLObject(-1848823128)]
    public class TLRequestGetUserPhotos : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -1848823128;
            }
        }

        public TLAbsInputUser UserId { get; set; }
        public int Offset { get; set; }
        public long MaxId { get; set; }
        public int Limit { get; set; }
        public Photos.TLAbsPhotos Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.UserId = (TLAbsInputUser)ObjectUtils.DeserializeObject(br);
            this.Offset = br.ReadInt32();
            this.MaxId = br.ReadInt64();
            this.Limit = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            ObjectUtils.SerializeObject(this.UserId, bw);
            bw.Write(this.Offset);
            bw.Write(this.MaxId);
            bw.Write(this.Limit);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            this.Response = (Photos.TLAbsPhotos)ObjectUtils.DeserializeObject(br);

        }
    }
}
