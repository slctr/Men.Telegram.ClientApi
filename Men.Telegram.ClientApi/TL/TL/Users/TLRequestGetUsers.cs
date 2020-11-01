using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Users
{
    [TLObject(227648840)]
    public class TLRequestGetUsers : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 227648840;
            }
        }

        public TLVector<TLAbsInputUser> Id { get; set; }
        public TLVector<TLAbsUser> Response { get; set; }


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
            this.Response = (TLVector<TLAbsUser>)ObjectUtils.DeserializeVector<TLAbsUser>(br);

        }
    }
}
