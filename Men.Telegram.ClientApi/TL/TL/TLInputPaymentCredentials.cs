using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(873977640)]
    public class TLInputPaymentCredentials : TLAbsInputPaymentCredentials
    {
        public override int Constructor
        {
            get
            {
                return 873977640;
            }
        }

        public int Flags { get; set; }
        public bool Save { get; set; }
        public TLDataJSON Data { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.Save ? (this.Flags | 1) : (this.Flags & ~1);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            this.Save = (this.Flags & 1) != 0;
            this.Data = (TLDataJSON)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);

            ObjectUtils.SerializeObject(this.Data, bw);

        }
    }
}
