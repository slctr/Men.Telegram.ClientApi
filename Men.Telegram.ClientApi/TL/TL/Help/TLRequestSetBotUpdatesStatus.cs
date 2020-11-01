using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Help
{
    [TLObject(-333262899)]
    public class TLRequestSetBotUpdatesStatus : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -333262899;
            }
        }

        public int PendingUpdatesCount { get; set; }
        public string Message { get; set; }
        public bool Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.PendingUpdatesCount = br.ReadInt32();
            this.Message = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            bw.Write(this.PendingUpdatesCount);
            StringUtil.Serialize(this.Message, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            this.Response = BoolUtil.Deserialize(br);

        }
    }
}
