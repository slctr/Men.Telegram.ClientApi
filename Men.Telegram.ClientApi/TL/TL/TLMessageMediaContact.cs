using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(1585262393)]
    public class TLMessageMediaContact : TLAbsMessageMedia
    {
        public override int Constructor
        {
            get
            {
                return 1585262393;
            }
        }

        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int UserId { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.PhoneNumber = StringUtil.Deserialize(br);
            this.FirstName = StringUtil.Deserialize(br);
            this.LastName = StringUtil.Deserialize(br);
            this.UserId = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            StringUtil.Serialize(this.PhoneNumber, bw);
            StringUtil.Serialize(this.FirstName, bw);
            StringUtil.Serialize(this.LastName, bw);
            bw.Write(this.UserId);

        }
    }
}
