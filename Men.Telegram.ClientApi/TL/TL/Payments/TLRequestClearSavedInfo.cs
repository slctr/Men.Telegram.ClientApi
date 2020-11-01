using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Payments
{
    [TLObject(-667062079)]
    public class TLRequestClearSavedInfo : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -667062079;
            }
        }

        public int Flags { get; set; }
        public bool Credentials { get; set; }
        public bool Info { get; set; }
        public bool Response { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.Credentials ? (this.Flags | 1) : (this.Flags & ~1);
            this.Flags = this.Info ? (this.Flags | 2) : (this.Flags & ~2);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            this.Credentials = (this.Flags & 1) != 0;
            this.Info = (this.Flags & 2) != 0;

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);



        }
        public override void DeserializeResponse(BinaryReader br)
        {
            this.Response = BoolUtil.Deserialize(br);

        }
    }
}
