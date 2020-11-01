using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(182649427)]
    public class TLMessageRange : TLObject
    {
        public override int Constructor
        {
            get
            {
                return 182649427;
            }
        }

        public int MinId { get; set; }
        public int MaxId { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.MinId = br.ReadInt32();
            this.MaxId = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            bw.Write(this.MinId);
            bw.Write(this.MaxId);

        }
    }
}