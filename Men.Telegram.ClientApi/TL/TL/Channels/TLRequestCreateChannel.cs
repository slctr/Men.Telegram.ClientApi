using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Channels
{
    [TLObject(-192332417)]
    public class TLRequestCreateChannel : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -192332417;
            }
        }

        public int Flags { get; set; }
        public bool Broadcast { get; set; }
        public bool Megagroup { get; set; }
        public string Title { get; set; }
        public string About { get; set; }
        public TLAbsUpdates Response { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.Broadcast ? (this.Flags | 1) : (this.Flags & ~1);
            this.Flags = this.Megagroup ? (this.Flags | 2) : (this.Flags & ~2);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            this.Broadcast = (this.Flags & 1) != 0;
            this.Megagroup = (this.Flags & 2) != 0;
            this.Title = StringUtil.Deserialize(br);
            this.About = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);


            StringUtil.Serialize(this.Title, bw);
            StringUtil.Serialize(this.About, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            this.Response = (TLAbsUpdates)ObjectUtils.DeserializeObject(br);

        }
    }
}
