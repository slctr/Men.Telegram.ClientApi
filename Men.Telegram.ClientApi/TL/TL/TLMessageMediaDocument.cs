using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-203411800)]
    public class TLMessageMediaDocument : TLAbsMessageMedia
    {
        public override int Constructor
        {
            get
            {
                return -203411800;
            }
        }

        public TLAbsDocument Document { get; set; }
        public string Caption { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Document = (TLAbsDocument)ObjectUtils.DeserializeObject(br);
            this.Caption = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            ObjectUtils.SerializeObject(this.Document, bw);
            StringUtil.Serialize(this.Caption, bw);

        }
    }
}
