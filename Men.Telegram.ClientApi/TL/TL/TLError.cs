using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-994444869)]
    public class TLError : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -994444869;
            }
        }

        public int Code { get; set; }
        public string Text { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Code = br.ReadInt32();
            this.Text = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            bw.Write(this.Code);
            StringUtil.Serialize(this.Text, bw);

        }
    }
}
