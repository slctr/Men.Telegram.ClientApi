using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Updates
{
    [TLObject(630429265)]
    public class TLRequestGetDifference : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 630429265;
            }
        }

        public int Flags { get; set; }
        public int Pts { get; set; }
        public int? PtsTotalLimit { get; set; }
        public int Date { get; set; }
        public int Qts { get; set; }
        public Updates.TLAbsDifference Response { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.PtsTotalLimit != null ? (this.Flags | 1) : (this.Flags & ~1);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            this.Pts = br.ReadInt32();
            if ((this.Flags & 1) != 0)
                this.PtsTotalLimit = br.ReadInt32();
            else
                this.PtsTotalLimit = null;

            this.Date = br.ReadInt32();
            this.Qts = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);
            bw.Write(this.Pts);
            if ((this.Flags & 1) != 0)
                bw.Write(this.PtsTotalLimit.Value);
            bw.Write(this.Date);
            bw.Write(this.Qts);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            this.Response = (Updates.TLAbsDifference)ObjectUtils.DeserializeObject(br);

        }
    }
}
