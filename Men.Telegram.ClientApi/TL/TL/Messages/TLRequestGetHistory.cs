using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(-1347868602)]
    public class TLRequestGetHistory : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -1347868602;
            }
        }

        public TLAbsInputPeer Peer { get; set; }
        public int OffsetId { get; set; }
        public int OffsetDate { get; set; }
        public int AddOffset { get; set; }
        public int Limit { get; set; }
        public int MaxId { get; set; }
        public int MinId { get; set; }
        public Messages.TLAbsMessages Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Peer = (TLAbsInputPeer)ObjectUtils.DeserializeObject(br);
            this.OffsetId = br.ReadInt32();
            this.OffsetDate = br.ReadInt32();
            this.AddOffset = br.ReadInt32();
            this.Limit = br.ReadInt32();
            this.MaxId = br.ReadInt32();
            this.MinId = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            ObjectUtils.SerializeObject(this.Peer, bw);
            bw.Write(this.OffsetId);
            bw.Write(this.OffsetDate);
            bw.Write(this.AddOffset);
            bw.Write(this.Limit);
            bw.Write(this.MaxId);
            bw.Write(this.MinId);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            this.Response = (Messages.TLAbsMessages)ObjectUtils.DeserializeObject(br);

        }
    }
}
