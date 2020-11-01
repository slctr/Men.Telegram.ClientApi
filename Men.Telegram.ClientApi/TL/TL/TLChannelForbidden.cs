using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-2059962289)]
    public class TLChannelForbidden : TLAbsChat
    {
        public override int Constructor
        {
            get
            {
                return -2059962289;
            }
        }

        public int Flags { get; set; }
        public bool Broadcast { get; set; }
        public bool Megagroup { get; set; }
        public int Id { get; set; }
        public long AccessHash { get; set; }
        public string Title { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.Broadcast ? (this.Flags | 32) : (this.Flags & ~32);
            this.Flags = this.Megagroup ? (this.Flags | 256) : (this.Flags & ~256);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            this.Broadcast = (this.Flags & 32) != 0;
            this.Megagroup = (this.Flags & 256) != 0;
            this.Id = br.ReadInt32();
            this.AccessHash = br.ReadInt64();
            this.Title = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);


            bw.Write(this.Id);
            bw.Write(this.AccessHash);
            StringUtil.Serialize(this.Title, bw);

        }
    }
}
