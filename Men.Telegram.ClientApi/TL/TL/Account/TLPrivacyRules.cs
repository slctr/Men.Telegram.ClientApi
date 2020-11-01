using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Account
{
    [TLObject(1430961007)]
    public class TLPrivacyRules : TLObject
    {
        public override int Constructor
        {
            get
            {
                return 1430961007;
            }
        }

        public TLVector<TLAbsPrivacyRule> Rules { get; set; }
        public TLVector<TLAbsUser> Users { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Rules = (TLVector<TLAbsPrivacyRule>)ObjectUtils.DeserializeVector<TLAbsPrivacyRule>(br);
            this.Users = (TLVector<TLAbsUser>)ObjectUtils.DeserializeVector<TLAbsUser>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            ObjectUtils.SerializeObject(this.Rules, bw);
            ObjectUtils.SerializeObject(this.Users, bw);

        }
    }
}
