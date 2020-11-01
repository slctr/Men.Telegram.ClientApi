using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Contacts
{
    [TLObject(1504393374)]
    public class TLRequestDeleteContacts : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 1504393374;
            }
        }

        public TLVector<TLAbsInputUser> Id { get; set; }
        public bool Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Id = (TLVector<TLAbsInputUser>)ObjectUtils.DeserializeVector<TLAbsInputUser>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            ObjectUtils.SerializeObject(this.Id, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            this.Response = BoolUtil.Deserialize(br);

        }
    }
}
