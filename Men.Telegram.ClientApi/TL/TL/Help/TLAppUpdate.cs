using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Help
{
    [TLObject(-1987579119)]
    public class TLAppUpdate : TLAbsAppUpdate
    {
        public override int Constructor
        {
            get
            {
                return -1987579119;
            }
        }

        public int Id { get; set; }
        public bool Critical { get; set; }
        public string Url { get; set; }
        public string Text { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Id = br.ReadInt32();
            this.Critical = BoolUtil.Deserialize(br);
            this.Url = StringUtil.Deserialize(br);
            this.Text = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            bw.Write(this.Id);
            BoolUtil.Serialize(this.Critical, bw);
            StringUtil.Serialize(this.Url, bw);
            StringUtil.Serialize(this.Text, bw);

        }
    }
}
