using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-208488460)]
    public class TLInputPhoneContact : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -208488460;
            }
        }

        public long ClientId { get; set; }
        public string Phone { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.ClientId = br.ReadInt64();
            this.Phone = StringUtil.Deserialize(br);
            this.FirstName = StringUtil.Deserialize(br);
            this.LastName = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            bw.Write(this.ClientId);
            StringUtil.Serialize(this.Phone, bw);
            StringUtil.Serialize(this.FirstName, bw);
            StringUtil.Serialize(this.LastName, bw);

        }
    }
}
