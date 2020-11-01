using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1844103547)]
    public class TLInputMediaInvoice : TLAbsInputMedia
    {
        public override int Constructor
        {
            get
            {
                return -1844103547;
            }
        }

        public int Flags { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TLInputWebDocument Photo { get; set; }
        public TLInvoice Invoice { get; set; }
        public byte[] Payload { get; set; }
        public string Provider { get; set; }
        public string StartParam { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.Photo != null ? (this.Flags | 1) : (this.Flags & ~1);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            this.Title = StringUtil.Deserialize(br);
            this.Description = StringUtil.Deserialize(br);
            if ((this.Flags & 1) != 0)
                this.Photo = (TLInputWebDocument)ObjectUtils.DeserializeObject(br);
            else
                this.Photo = null;

            this.Invoice = (TLInvoice)ObjectUtils.DeserializeObject(br);
            this.Payload = BytesUtil.Deserialize(br);
            this.Provider = StringUtil.Deserialize(br);
            this.StartParam = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);
            StringUtil.Serialize(this.Title, bw);
            StringUtil.Serialize(this.Description, bw);
            if ((this.Flags & 1) != 0)
                ObjectUtils.SerializeObject(this.Photo, bw);
            ObjectUtils.SerializeObject(this.Invoice, bw);
            BytesUtil.Serialize(this.Payload, bw);
            StringUtil.Serialize(this.Provider, bw);
            StringUtil.Serialize(this.StartParam, bw);

        }
    }
}
