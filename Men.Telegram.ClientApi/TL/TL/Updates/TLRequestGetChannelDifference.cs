using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Updates
{
    [TLObject(51854712)]
    public class TLRequestGetChannelDifference : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 51854712;
            }
        }

        public int Flags { get; set; }
        public bool Force { get; set; }
        public TLAbsInputChannel Channel { get; set; }
        public TLAbsChannelMessagesFilter Filter { get; set; }
        public int Pts { get; set; }
        public int Limit { get; set; }
        public Updates.TLAbsChannelDifference Response { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.Force ? (this.Flags | 1) : (this.Flags & ~1);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            this.Force = (this.Flags & 1) != 0;
            this.Channel = (TLAbsInputChannel)ObjectUtils.DeserializeObject(br);
            this.Filter = (TLAbsChannelMessagesFilter)ObjectUtils.DeserializeObject(br);
            this.Pts = br.ReadInt32();
            this.Limit = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);

            ObjectUtils.SerializeObject(this.Channel, bw);
            ObjectUtils.SerializeObject(this.Filter, bw);
            bw.Write(this.Pts);
            bw.Write(this.Limit);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            this.Response = (Updates.TLAbsChannelDifference)ObjectUtils.DeserializeObject(br);

        }
    }
}
