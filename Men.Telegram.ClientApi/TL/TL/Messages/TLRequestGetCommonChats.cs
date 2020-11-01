using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(218777796)]
    public class TLRequestGetCommonChats : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 218777796;
            }
        }

        public TLAbsInputUser UserId { get; set; }
        public int MaxId { get; set; }
        public int Limit { get; set; }
        public Messages.TLAbsChats Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.UserId = (TLAbsInputUser)ObjectUtils.DeserializeObject(br);
            this.MaxId = br.ReadInt32();
            this.Limit = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            ObjectUtils.SerializeObject(this.UserId, bw);
            bw.Write(this.MaxId);
            bw.Write(this.Limit);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            this.Response = (Messages.TLAbsChats)ObjectUtils.DeserializeObject(br);

        }
    }
}
