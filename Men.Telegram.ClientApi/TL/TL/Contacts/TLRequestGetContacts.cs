using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Contacts
{
    [TLObject(583445000)]
    public class TLRequestGetContacts : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 583445000;
            }
        }

        public string Hash { get; set; }
        public Contacts.TLAbsContacts Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Hash = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            StringUtil.Serialize(this.Hash, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            this.Response = (Contacts.TLAbsContacts)ObjectUtils.DeserializeObject(br);

        }
    }
}
