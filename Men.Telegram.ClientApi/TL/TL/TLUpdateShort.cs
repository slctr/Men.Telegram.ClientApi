using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(2027216577)]
    public class TLUpdateShort : TLAbsUpdates
    {
        public override int Constructor
        {
            get
            {
                return 2027216577;
            }
        }

        public TLAbsUpdate Update { get; set; }
        public int Date { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Update = (TLAbsUpdate)ObjectUtils.DeserializeObject(br);
            this.Date = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            ObjectUtils.SerializeObject(this.Update, bw);
            bw.Write(this.Date);

        }
    }
}
