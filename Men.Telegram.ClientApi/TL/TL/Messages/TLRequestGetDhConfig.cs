using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(651135312)]
    public class TLRequestGetDhConfig : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 651135312;
            }
        }

        public int Version { get; set; }
        public int RandomLength { get; set; }
        public Messages.TLAbsDhConfig Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Version = br.ReadInt32();
            this.RandomLength = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            bw.Write(this.Version);
            bw.Write(this.RandomLength);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            this.Response = (Messages.TLAbsDhConfig)ObjectUtils.DeserializeObject(br);

        }
    }
}
