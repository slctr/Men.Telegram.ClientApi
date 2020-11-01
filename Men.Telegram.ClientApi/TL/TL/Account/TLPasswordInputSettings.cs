using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Account
{
    [TLObject(-2037289493)]
    public class TLPasswordInputSettings : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -2037289493;
            }
        }

        public int Flags { get; set; }
        public byte[] NewSalt { get; set; }
        public byte[] NewPasswordHash { get; set; }
        public string Hint { get; set; }
        public string Email { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.NewSalt != null ? (this.Flags | 1) : (this.Flags & ~1);
            this.Flags = this.NewPasswordHash != null ? (this.Flags | 1) : (this.Flags & ~1);
            this.Flags = this.Hint != null ? (this.Flags | 1) : (this.Flags & ~1);
            this.Flags = this.Email != null ? (this.Flags | 2) : (this.Flags & ~2);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            if ((this.Flags & 1) != 0)
                this.NewSalt = BytesUtil.Deserialize(br);
            else
                this.NewSalt = null;

            if ((this.Flags & 1) != 0)
                this.NewPasswordHash = BytesUtil.Deserialize(br);
            else
                this.NewPasswordHash = null;

            if ((this.Flags & 1) != 0)
                this.Hint = StringUtil.Deserialize(br);
            else
                this.Hint = null;

            if ((this.Flags & 2) != 0)
                this.Email = StringUtil.Deserialize(br);
            else
                this.Email = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);
            if ((this.Flags & 1) != 0)
                BytesUtil.Serialize(this.NewSalt, bw);
            if ((this.Flags & 1) != 0)
                BytesUtil.Serialize(this.NewPasswordHash, bw);
            if ((this.Flags & 1) != 0)
                StringUtil.Serialize(this.Hint, bw);
            if ((this.Flags & 2) != 0)
                StringUtil.Serialize(this.Email, bw);

        }
    }
}
