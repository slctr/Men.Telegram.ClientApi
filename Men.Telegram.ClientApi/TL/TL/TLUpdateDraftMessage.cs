using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-299124375)]
    public class TLUpdateDraftMessage : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return -299124375;
            }
        }

        public TLAbsPeer Peer { get; set; }
        public TLAbsDraftMessage Draft { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Peer = (TLAbsPeer)ObjectUtils.DeserializeObject(br);
            this.Draft = (TLAbsDraftMessage)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            ObjectUtils.SerializeObject(this.Peer, bw);
            ObjectUtils.SerializeObject(this.Draft, bw);

        }
    }
}
