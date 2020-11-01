using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(2106086025)]
    public class TLRequestExportChatInvite : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 2106086025;
            }
        }

        public int ChatId { get; set; }
        public TLAbsExportedChatInvite Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.ChatId = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            bw.Write(this.ChatId);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            this.Response = (TLAbsExportedChatInvite)ObjectUtils.DeserializeObject(br);

        }
    }
}