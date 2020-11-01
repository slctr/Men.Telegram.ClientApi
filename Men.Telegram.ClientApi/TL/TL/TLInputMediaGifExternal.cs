using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(1212395773)]
    public class TLInputMediaGifExternal : TLAbsInputMedia
    {
        public override int Constructor
        {
            get
            {
                return 1212395773;
            }
        }

        public string Url { get; set; }
        public string Q { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Url = StringUtil.Deserialize(br);
            this.Q = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            StringUtil.Serialize(this.Url, bw);
            StringUtil.Serialize(this.Q, bw);

        }
    }
}
