using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(1888354709)]
    public class TLRequestForwardMessages : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 1888354709;
            }
        }

        public int Flags { get; set; }
        public bool Silent { get; set; }
        public bool Background { get; set; }
        public bool WithMyScore { get; set; }
        public TLAbsInputPeer FromPeer { get; set; }
        public TLVector<int> Id { get; set; }
        public TLVector<long> RandomId { get; set; }
        public TLAbsInputPeer ToPeer { get; set; }
        public TLAbsUpdates Response { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.Silent ? (this.Flags | 32) : (this.Flags & ~32);
            this.Flags = this.Background ? (this.Flags | 64) : (this.Flags & ~64);
            this.Flags = this.WithMyScore ? (this.Flags | 256) : (this.Flags & ~256);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            this.Silent = (this.Flags & 32) != 0;
            this.Background = (this.Flags & 64) != 0;
            this.WithMyScore = (this.Flags & 256) != 0;
            this.FromPeer = (TLAbsInputPeer)ObjectUtils.DeserializeObject(br);
            this.Id = (TLVector<int>)ObjectUtils.DeserializeVector<int>(br);
            this.RandomId = (TLVector<long>)ObjectUtils.DeserializeVector<long>(br);
            this.ToPeer = (TLAbsInputPeer)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);



            ObjectUtils.SerializeObject(this.FromPeer, bw);
            ObjectUtils.SerializeObject(this.Id, bw);
            ObjectUtils.SerializeObject(this.RandomId, bw);
            ObjectUtils.SerializeObject(this.ToPeer, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            this.Response = (TLAbsUpdates)ObjectUtils.DeserializeObject(br);

        }
    }
}
