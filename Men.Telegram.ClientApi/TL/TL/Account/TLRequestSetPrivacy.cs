using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Account
{
    [TLObject(-906486552)]
    public class TLRequestSetPrivacy : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -906486552;
            }
        }

        public TLAbsInputPrivacyKey Key { get; set; }
        public TLVector<TLAbsInputPrivacyRule> Rules { get; set; }
        public Account.TLPrivacyRules Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Key = (TLAbsInputPrivacyKey)ObjectUtils.DeserializeObject(br);
            this.Rules = (TLVector<TLAbsInputPrivacyRule>)ObjectUtils.DeserializeVector<TLAbsInputPrivacyRule>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            ObjectUtils.SerializeObject(this.Key, bw);
            ObjectUtils.SerializeObject(this.Rules, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            this.Response = (Account.TLPrivacyRules)ObjectUtils.DeserializeObject(br);

        }
    }
}
