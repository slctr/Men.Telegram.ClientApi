using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-40996577)]
    public class TLDraftMessage : TLAbsDraftMessage
    {
        public override int Constructor
        {
            get
            {
                return -40996577;
            }
        }

        public int Flags { get; set; }
        public bool NoWebpage { get; set; }
        public int? ReplyToMsgId { get; set; }
        public string Message { get; set; }
        public TLVector<TLAbsMessageEntity> Entities { get; set; }
        public int Date { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.NoWebpage ? (this.Flags | 2) : (this.Flags & ~2);
            this.Flags = this.ReplyToMsgId != null ? (this.Flags | 1) : (this.Flags & ~1);
            this.Flags = this.Entities != null ? (this.Flags | 8) : (this.Flags & ~8);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            this.NoWebpage = (this.Flags & 2) != 0;
            if ((this.Flags & 1) != 0)
            {
                this.ReplyToMsgId = br.ReadInt32();
            }
            else
            {
                this.ReplyToMsgId = null;
            }

            this.Message = StringUtil.Deserialize(br);
            if ((this.Flags & 8) != 0)
            {
                this.Entities = (TLVector<TLAbsMessageEntity>)ObjectUtils.DeserializeVector<TLAbsMessageEntity>(br);
            }
            else
            {
                this.Entities = null;
            }

            this.Date = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);

            if ((this.Flags & 1) != 0)
            {
                bw.Write(this.ReplyToMsgId.Value);
            }

            StringUtil.Serialize(this.Message, bw);
            if ((this.Flags & 8) != 0)
            {
                ObjectUtils.SerializeObject(this.Entities, bw);
            }

            bw.Write(this.Date);

        }
    }
}
