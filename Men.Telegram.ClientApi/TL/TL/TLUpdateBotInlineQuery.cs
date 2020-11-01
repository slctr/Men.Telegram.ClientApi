using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(1417832080)]
    public class TLUpdateBotInlineQuery : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return 1417832080;
            }
        }

        public int Flags { get; set; }
        public long QueryId { get; set; }
        public int UserId { get; set; }
        public string Query { get; set; }
        public TLAbsGeoPoint Geo { get; set; }
        public string Offset { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.Geo != null ? (this.Flags | 1) : (this.Flags & ~1);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            this.QueryId = br.ReadInt64();
            this.UserId = br.ReadInt32();
            this.Query = StringUtil.Deserialize(br);
            if ((this.Flags & 1) != 0)
                this.Geo = (TLAbsGeoPoint)ObjectUtils.DeserializeObject(br);
            else
                this.Geo = null;

            this.Offset = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);
            bw.Write(this.QueryId);
            bw.Write(this.UserId);
            StringUtil.Serialize(this.Query, bw);
            if ((this.Flags & 1) != 0)
                ObjectUtils.SerializeObject(this.Geo, bw);
            StringUtil.Serialize(this.Offset, bw);

        }
    }
}
