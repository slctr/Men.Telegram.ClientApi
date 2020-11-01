using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-564523562)]
    public class TLTextEmail : TLAbsRichText
    {
        public override int Constructor
        {
            get
            {
                return -564523562;
            }
        }

        public TLAbsRichText Text { get; set; }
        public string Email { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Text = (TLAbsRichText)ObjectUtils.DeserializeObject(br);
            this.Email = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            ObjectUtils.SerializeObject(this.Text, bw);
            StringUtil.Serialize(this.Email, bw);

        }
    }
}
