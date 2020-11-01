using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(1364105629)]
    public class TLRequestGetInlineBotResults : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 1364105629;
            }
        }

        public int Flags { get; set; }
        public TLAbsInputUser Bot { get; set; }
        public TLAbsInputPeer Peer { get; set; }
        public TLAbsInputGeoPoint GeoPoint { get; set; }
        public string Query { get; set; }
        public string Offset { get; set; }
        public Messages.TLBotResults Response { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.GeoPoint != null ? (this.Flags | 1) : (this.Flags & ~1);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            this.Bot = (TLAbsInputUser)ObjectUtils.DeserializeObject(br);
            this.Peer = (TLAbsInputPeer)ObjectUtils.DeserializeObject(br);
            if ((this.Flags & 1) != 0)
            {
                this.GeoPoint = (TLAbsInputGeoPoint)ObjectUtils.DeserializeObject(br);
            }
            else
            {
                this.GeoPoint = null;
            }

            this.Query = StringUtil.Deserialize(br);
            this.Offset = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);
            ObjectUtils.SerializeObject(this.Bot, bw);
            ObjectUtils.SerializeObject(this.Peer, bw);
            if ((this.Flags & 1) != 0)
            {
                ObjectUtils.SerializeObject(this.GeoPoint, bw);
            }

            StringUtil.Serialize(this.Query, bw);
            StringUtil.Serialize(this.Offset, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            this.Response = (Messages.TLBotResults)ObjectUtils.DeserializeObject(br);

        }
    }
}
