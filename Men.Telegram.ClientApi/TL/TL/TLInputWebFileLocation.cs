using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1036396922)]
    public class TLInputWebFileLocation : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -1036396922;
            }
        }

        public string Url { get; set; }
        public long AccessHash { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Url = StringUtil.Deserialize(br);
            this.AccessHash = br.ReadInt64();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            StringUtil.Serialize(this.Url, bw);
            bw.Write(this.AccessHash);

        }
    }
}
