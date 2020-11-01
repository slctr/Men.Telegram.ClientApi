using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Account
{
    [TLObject(2018596725)]
    public class TLRequestUpdateProfile : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 2018596725;
            }
        }

        public int Flags { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string About { get; set; }
        public TLAbsUser Response { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.FirstName != null ? (this.Flags | 1) : (this.Flags & ~1);
            this.Flags = this.LastName != null ? (this.Flags | 2) : (this.Flags & ~2);
            this.Flags = this.About != null ? (this.Flags | 4) : (this.Flags & ~4);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            if ((this.Flags & 1) != 0)
                this.FirstName = StringUtil.Deserialize(br);
            else
                this.FirstName = null;

            if ((this.Flags & 2) != 0)
                this.LastName = StringUtil.Deserialize(br);
            else
                this.LastName = null;

            if ((this.Flags & 4) != 0)
                this.About = StringUtil.Deserialize(br);
            else
                this.About = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);
            if ((this.Flags & 1) != 0)
                StringUtil.Serialize(this.FirstName, bw);
            if ((this.Flags & 2) != 0)
                StringUtil.Serialize(this.LastName, bw);
            if ((this.Flags & 4) != 0)
                StringUtil.Serialize(this.About, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            this.Response = (TLAbsUser)ObjectUtils.DeserializeObject(br);

        }
    }
}
