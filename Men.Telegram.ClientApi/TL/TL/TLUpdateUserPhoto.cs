using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1791935732)]
    public class TLUpdateUserPhoto : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return -1791935732;
            }
        }

        public int UserId { get; set; }
        public int Date { get; set; }
        public TLAbsUserProfilePhoto Photo { get; set; }
        public bool Previous { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.UserId = br.ReadInt32();
            this.Date = br.ReadInt32();
            this.Photo = (TLAbsUserProfilePhoto)ObjectUtils.DeserializeObject(br);
            this.Previous = BoolUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            bw.Write(this.UserId);
            bw.Write(this.Date);
            ObjectUtils.SerializeObject(this.Photo, bw);
            BoolUtil.Serialize(this.Previous, bw);

        }
    }
}
