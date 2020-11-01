using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Channels
{
    [TLObject(-1490162350)]
    public class TLRequestUpdatePinnedMessage : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -1490162350;
            }
        }

        public int Flags { get; set; }
        public bool Silent { get; set; }
        public TLAbsInputChannel Channel { get; set; }
        public int Id { get; set; }
        public TLAbsUpdates Response { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.Silent ? (this.Flags | 1) : (this.Flags & ~1);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            this.Silent = (this.Flags & 1) != 0;
            this.Channel = (TLAbsInputChannel)ObjectUtils.DeserializeObject(br);
            this.Id = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);

            ObjectUtils.SerializeObject(this.Channel, bw);
            bw.Write(this.Id);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            this.Response = (TLAbsUpdates)ObjectUtils.DeserializeObject(br);

        }
    }
}
