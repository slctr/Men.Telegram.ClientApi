using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-190472735)]
    public class TLInputBotInlineMessageMediaGeo : TLAbsInputBotInlineMessage
    {
        public override int Constructor
        {
            get
            {
                return -190472735;
            }
        }

        public int Flags { get; set; }
        public TLAbsInputGeoPoint GeoPoint { get; set; }
        public TLAbsReplyMarkup ReplyMarkup { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.ReplyMarkup != null ? (this.Flags | 4) : (this.Flags & ~4);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            this.GeoPoint = (TLAbsInputGeoPoint)ObjectUtils.DeserializeObject(br);
            if ((this.Flags & 4) != 0)
                this.ReplyMarkup = (TLAbsReplyMarkup)ObjectUtils.DeserializeObject(br);
            else
                this.ReplyMarkup = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);
            ObjectUtils.SerializeObject(this.GeoPoint, bw);
            if ((this.Flags & 4) != 0)
                ObjectUtils.SerializeObject(this.ReplyMarkup, bw);

        }
    }
}
