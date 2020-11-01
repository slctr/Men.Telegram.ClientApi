using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-847783593)]
    public class TLChannelMessagesFilter : TLAbsChannelMessagesFilter
    {
        public override int Constructor
        {
            get
            {
                return -847783593;
            }
        }

        public int Flags { get; set; }
        public bool ExcludeNewMessages { get; set; }
        public TLVector<TLMessageRange> Ranges { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.ExcludeNewMessages ? (this.Flags | 2) : (this.Flags & ~2);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            this.ExcludeNewMessages = (this.Flags & 2) != 0;
            this.Ranges = (TLVector<TLMessageRange>)ObjectUtils.DeserializeVector<TLMessageRange>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);

            ObjectUtils.SerializeObject(this.Ranges, bw);

        }
    }
}
