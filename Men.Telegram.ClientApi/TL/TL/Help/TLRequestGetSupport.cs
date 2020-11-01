using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Help
{
    [TLObject(-1663104819)]
    public class TLRequestGetSupport : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -1663104819;
            }
        }

        public Help.TLSupport Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            this.Response = (Help.TLSupport)ObjectUtils.DeserializeObject(br);

        }
    }
}
