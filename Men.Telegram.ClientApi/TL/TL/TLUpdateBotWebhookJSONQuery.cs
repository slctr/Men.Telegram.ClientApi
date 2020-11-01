using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1684914010)]
    public class TLUpdateBotWebhookJSONQuery : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return -1684914010;
            }
        }

        public long QueryId { get; set; }
        public TLDataJSON Data { get; set; }
        public int Timeout { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.QueryId = br.ReadInt64();
            this.Data = (TLDataJSON)ObjectUtils.DeserializeObject(br);
            this.Timeout = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            bw.Write(this.QueryId);
            ObjectUtils.SerializeObject(this.Data, bw);
            bw.Write(this.Timeout);

        }
    }
}
