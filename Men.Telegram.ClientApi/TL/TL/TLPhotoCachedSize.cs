using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-374917894)]
    public class TLPhotoCachedSize : TLAbsPhotoSize
    {
        public override int Constructor
        {
            get
            {
                return -374917894;
            }
        }

        public string Type { get; set; }
        public TLAbsFileLocation Location { get; set; }
        public int W { get; set; }
        public int H { get; set; }
        public byte[] Bytes { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Type = StringUtil.Deserialize(br);
            this.Location = (TLAbsFileLocation)ObjectUtils.DeserializeObject(br);
            this.W = br.ReadInt32();
            this.H = br.ReadInt32();
            this.Bytes = BytesUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            StringUtil.Serialize(this.Type, bw);
            ObjectUtils.SerializeObject(this.Location, bw);
            bw.Write(this.W);
            bw.Write(this.H);
            BytesUtil.Serialize(this.Bytes, bw);

        }
    }
}
