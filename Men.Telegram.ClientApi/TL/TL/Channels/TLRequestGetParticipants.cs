using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Channels
{
    [TLObject(618237842)]
    public class TLRequestGetParticipants : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 618237842;
            }
        }

        public TLAbsInputChannel Channel { get; set; }
        public TLAbsChannelParticipantsFilter Filter { get; set; }
        public int Offset { get; set; }
        public int Limit { get; set; }
        public Channels.TLChannelParticipants Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Channel = (TLAbsInputChannel)ObjectUtils.DeserializeObject(br);
            this.Filter = (TLAbsChannelParticipantsFilter)ObjectUtils.DeserializeObject(br);
            this.Offset = br.ReadInt32();
            this.Limit = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            ObjectUtils.SerializeObject(this.Channel, bw);
            ObjectUtils.SerializeObject(this.Filter, bw);
            bw.Write(this.Offset);
            bw.Write(this.Limit);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            this.Response = (Channels.TLChannelParticipants)ObjectUtils.DeserializeObject(br);

        }
    }
}
