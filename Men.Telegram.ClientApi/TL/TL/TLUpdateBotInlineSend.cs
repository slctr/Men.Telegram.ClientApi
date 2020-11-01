using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(239663460)]
    public class TLUpdateBotInlineSend : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return 239663460;
            }
        }

        public int Flags { get; set; }
        public int UserId { get; set; }
        public string Query { get; set; }
        public TLAbsGeoPoint Geo { get; set; }
        public string Id { get; set; }
        public TLInputBotInlineMessageID MsgId { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.Geo != null ? (this.Flags | 1) : (this.Flags & ~1);
            this.Flags = this.MsgId != null ? (this.Flags | 2) : (this.Flags & ~2);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            this.UserId = br.ReadInt32();
            this.Query = StringUtil.Deserialize(br);
            if ((this.Flags & 1) != 0)
                this.Geo = (TLAbsGeoPoint)ObjectUtils.DeserializeObject(br);
            else
                this.Geo = null;

            this.Id = StringUtil.Deserialize(br);
            if ((this.Flags & 2) != 0)
                this.MsgId = (TLInputBotInlineMessageID)ObjectUtils.DeserializeObject(br);
            else
                this.MsgId = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);
            bw.Write(this.UserId);
            StringUtil.Serialize(this.Query, bw);
            if ((this.Flags & 1) != 0)
                ObjectUtils.SerializeObject(this.Geo, bw);
            StringUtil.Serialize(this.Id, bw);
            if ((this.Flags & 2) != 0)
                ObjectUtils.SerializeObject(this.MsgId, bw);

        }
    }
}
