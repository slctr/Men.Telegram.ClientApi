using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(-1784678844)]
    public class TLRequestReorderPinnedDialogs : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -1784678844;
            }
        }

        public int Flags { get; set; }
        public bool Force { get; set; }
        public TLVector<TLAbsInputPeer> Order { get; set; }
        public bool Response { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.Force ? (this.Flags | 1) : (this.Flags & ~1);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            this.Force = (this.Flags & 1) != 0;
            this.Order = (TLVector<TLAbsInputPeer>)ObjectUtils.DeserializeVector<TLAbsInputPeer>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);

            ObjectUtils.SerializeObject(this.Order, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            this.Response = BoolUtil.Deserialize(br);

        }
    }
}
