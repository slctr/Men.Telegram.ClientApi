using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(-732523960)]
    public class TLRequestSearch : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -732523960;
            }
        }

        public int Flags { get; set; }
        public TLAbsInputPeer Peer { get; set; }
        public string Q { get; set; }
        public TLAbsMessagesFilter Filter { get; set; }
        public int MinDate { get; set; }
        public int MaxDate { get; set; }
        public int Offset { get; set; }
        public int MaxId { get; set; }
        public int Limit { get; set; }
        public Messages.TLAbsMessages Response { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            this.Peer = (TLAbsInputPeer)ObjectUtils.DeserializeObject(br);
            this.Q = StringUtil.Deserialize(br);
            this.Filter = (TLAbsMessagesFilter)ObjectUtils.DeserializeObject(br);
            this.MinDate = br.ReadInt32();
            this.MaxDate = br.ReadInt32();
            this.Offset = br.ReadInt32();
            this.MaxId = br.ReadInt32();
            this.Limit = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);
            ObjectUtils.SerializeObject(this.Peer, bw);
            StringUtil.Serialize(this.Q, bw);
            ObjectUtils.SerializeObject(this.Filter, bw);
            bw.Write(this.MinDate);
            bw.Write(this.MaxDate);
            bw.Write(this.Offset);
            bw.Write(this.MaxId);
            bw.Write(this.Limit);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            this.Response = (Messages.TLAbsMessages)ObjectUtils.DeserializeObject(br);

        }
    }
}
