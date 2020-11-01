using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(-341307408)]
    public class TLRequestGetAllChats : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -341307408;
            }
        }

        public TLVector<int> ExceptIds { get; set; }
        public Messages.TLAbsChats Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.ExceptIds = (TLVector<int>)ObjectUtils.DeserializeVector<int>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            ObjectUtils.SerializeObject(this.ExceptIds, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            this.Response = (Messages.TLAbsChats)ObjectUtils.DeserializeObject(br);

        }
    }
}
