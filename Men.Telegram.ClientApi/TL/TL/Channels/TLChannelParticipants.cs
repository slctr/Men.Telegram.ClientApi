using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Channels
{
    [TLObject(-177282392)]
    public class TLChannelParticipants : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -177282392;
            }
        }

        public int Count { get; set; }
        public TLVector<TLAbsChannelParticipant> Participants { get; set; }
        public TLVector<TLAbsUser> Users { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Count = br.ReadInt32();
            this.Participants = (TLVector<TLAbsChannelParticipant>)ObjectUtils.DeserializeVector<TLAbsChannelParticipant>(br);
            this.Users = (TLVector<TLAbsUser>)ObjectUtils.DeserializeVector<TLAbsUser>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            bw.Write(this.Count);
            ObjectUtils.SerializeObject(this.Participants, bw);
            ObjectUtils.SerializeObject(this.Users, bw);

        }
    }
}
