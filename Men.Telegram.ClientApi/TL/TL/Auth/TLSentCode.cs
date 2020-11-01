using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Auth
{
    [TLObject(1577067778)]
    public class TLSentCode : TLObject
    {
        public override int Constructor
        {
            get
            {
                return 1577067778;
            }
        }

        public int Flags { get; set; }
        public bool PhoneRegistered { get; set; }
        public Auth.TLAbsSentCodeType Type { get; set; }
        public string PhoneCodeHash { get; set; }
        public Auth.TLAbsCodeType NextType { get; set; }
        public int? Timeout { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.PhoneRegistered ? (this.Flags | 1) : (this.Flags & ~1);
            this.Flags = this.NextType != null ? (this.Flags | 2) : (this.Flags & ~2);
            this.Flags = this.Timeout != null ? (this.Flags | 4) : (this.Flags & ~4);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            this.PhoneRegistered = (this.Flags & 1) != 0;
            this.Type = (Auth.TLAbsSentCodeType)ObjectUtils.DeserializeObject(br);
            this.PhoneCodeHash = StringUtil.Deserialize(br);
            if ((this.Flags & 2) != 0)
            {
                this.NextType = (Auth.TLAbsCodeType)ObjectUtils.DeserializeObject(br);
            }
            else
            {
                this.NextType = null;
            }

            if ((this.Flags & 4) != 0)
            {
                this.Timeout = br.ReadInt32();
            }
            else
            {
                this.Timeout = null;
            }
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);

            ObjectUtils.SerializeObject(this.Type, bw);
            StringUtil.Serialize(this.PhoneCodeHash, bw);
            if ((this.Flags & 2) != 0)
            {
                ObjectUtils.SerializeObject(this.NextType, bw);
            }

            if ((this.Flags & 4) != 0)
            {
                bw.Write(this.Timeout.Value);
            }
        }
    }
}
