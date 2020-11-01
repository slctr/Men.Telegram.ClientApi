using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(-1640190800)]
    public class TLRequestSearchGlobal : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -1640190800;
            }
        }

        public string Q { get; set; }
        public int OffsetDate { get; set; }
        public TLAbsInputPeer OffsetPeer { get; set; }
        public int OffsetId { get; set; }
        public int Limit { get; set; }
        public Messages.TLAbsMessages Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Q = StringUtil.Deserialize(br);
            this.OffsetDate = br.ReadInt32();
            this.OffsetPeer = (TLAbsInputPeer)ObjectUtils.DeserializeObject(br);
            this.OffsetId = br.ReadInt32();
            this.Limit = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            StringUtil.Serialize(this.Q, bw);
            bw.Write(this.OffsetDate);
            ObjectUtils.SerializeObject(this.OffsetPeer, bw);
            bw.Write(this.OffsetId);
            bw.Write(this.Limit);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            this.Response = (Messages.TLAbsMessages)ObjectUtils.DeserializeObject(br);

        }
    }
}
