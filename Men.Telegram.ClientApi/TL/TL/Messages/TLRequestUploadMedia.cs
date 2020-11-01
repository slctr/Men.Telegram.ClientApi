using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(1369162417)]
    public class TLRequestUploadMedia : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 1369162417;
            }
        }

        public TLAbsInputPeer Peer { get; set; }
        public TLAbsInputMedia Media { get; set; }
        public TLAbsMessageMedia Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Peer = (TLAbsInputPeer)ObjectUtils.DeserializeObject(br);
            this.Media = (TLAbsInputMedia)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            ObjectUtils.SerializeObject(this.Peer, bw);
            ObjectUtils.SerializeObject(this.Media, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            this.Response = (TLAbsMessageMedia)ObjectUtils.DeserializeObject(br);

        }
    }
}
