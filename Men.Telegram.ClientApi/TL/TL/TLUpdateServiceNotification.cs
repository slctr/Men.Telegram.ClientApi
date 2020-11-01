using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-337352679)]
    public class TLUpdateServiceNotification : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return -337352679;
            }
        }

        public int Flags { get; set; }
        public bool Popup { get; set; }
        public int? InboxDate { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }
        public TLAbsMessageMedia Media { get; set; }
        public TLVector<TLAbsMessageEntity> Entities { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.Popup ? (this.Flags | 1) : (this.Flags & ~1);
            this.Flags = this.InboxDate != null ? (this.Flags | 2) : (this.Flags & ~2);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            this.Popup = (this.Flags & 1) != 0;
            if ((this.Flags & 2) != 0)
                this.InboxDate = br.ReadInt32();
            else
                this.InboxDate = null;

            this.Type = StringUtil.Deserialize(br);
            this.Message = StringUtil.Deserialize(br);
            this.Media = (TLAbsMessageMedia)ObjectUtils.DeserializeObject(br);
            this.Entities = (TLVector<TLAbsMessageEntity>)ObjectUtils.DeserializeVector<TLAbsMessageEntity>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);

            if ((this.Flags & 2) != 0)
                bw.Write(this.InboxDate.Value);
            StringUtil.Serialize(this.Type, bw);
            StringUtil.Serialize(this.Message, bw);
            ObjectUtils.SerializeObject(this.Media, bw);
            ObjectUtils.SerializeObject(this.Entities, bw);

        }
    }
}
