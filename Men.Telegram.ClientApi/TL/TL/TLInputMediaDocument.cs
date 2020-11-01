using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(444068508)]
    public class TLInputMediaDocument : TLAbsInputMedia
    {
        public override int Constructor
        {
            get
            {
                return 444068508;
            }
        }

        public TLAbsInputDocument Id { get; set; }
        public string Caption { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Id = (TLAbsInputDocument)ObjectUtils.DeserializeObject(br);
            this.Caption = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            ObjectUtils.SerializeObject(this.Id, bw);
            StringUtil.Serialize(this.Caption, bw);

        }
    }
}
