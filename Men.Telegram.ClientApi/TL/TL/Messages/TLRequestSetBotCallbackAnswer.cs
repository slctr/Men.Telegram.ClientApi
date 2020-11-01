using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(-712043766)]
    public class TLRequestSetBotCallbackAnswer : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -712043766;
            }
        }

        public int Flags { get; set; }
        public bool Alert { get; set; }
        public long QueryId { get; set; }
        public string Message { get; set; }
        public string Url { get; set; }
        public int CacheTime { get; set; }
        public bool Response { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.Alert ? (this.Flags | 2) : (this.Flags & ~2);
            this.Flags = this.Message != null ? (this.Flags | 1) : (this.Flags & ~1);
            this.Flags = this.Url != null ? (this.Flags | 4) : (this.Flags & ~4);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            this.Alert = (this.Flags & 2) != 0;
            this.QueryId = br.ReadInt64();
            if ((this.Flags & 1) != 0)
            {
                this.Message = StringUtil.Deserialize(br);
            }
            else
            {
                this.Message = null;
            }

            if ((this.Flags & 4) != 0)
            {
                this.Url = StringUtil.Deserialize(br);
            }
            else
            {
                this.Url = null;
            }

            this.CacheTime = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);

            bw.Write(this.QueryId);
            if ((this.Flags & 1) != 0)
            {
                StringUtil.Serialize(this.Message, bw);
            }

            if ((this.Flags & 4) != 0)
            {
                StringUtil.Serialize(this.Url, bw);
            }

            bw.Write(this.CacheTime);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            this.Response = BoolUtil.Deserialize(br);

        }
    }
}
