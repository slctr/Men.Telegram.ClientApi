using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(-1080395925)]
    public class TLRequestSearchGifs : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -1080395925;
            }
        }

        public string Q { get; set; }
        public int Offset { get; set; }
        public Messages.TLFoundGifs Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Q = StringUtil.Deserialize(br);
            this.Offset = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            StringUtil.Serialize(this.Q, bw);
            bw.Write(this.Offset);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            this.Response = (Messages.TLFoundGifs)ObjectUtils.DeserializeObject(br);

        }
    }
}
