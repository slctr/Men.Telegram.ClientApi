using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-298113238)]
    public class TLUpdatePrivacy : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return -298113238;
            }
        }

        public TLAbsPrivacyKey Key { get; set; }
        public TLVector<TLAbsPrivacyRule> Rules { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Key = (TLAbsPrivacyKey)ObjectUtils.DeserializeObject(br);
            this.Rules = (TLVector<TLAbsPrivacyRule>)ObjectUtils.DeserializeVector<TLAbsPrivacyRule>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            ObjectUtils.SerializeObject(this.Key, bw);
            ObjectUtils.SerializeObject(this.Rules, bw);

        }
    }
}
