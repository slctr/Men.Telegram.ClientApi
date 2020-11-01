using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(1957577280)]
    public class TLUpdates : TLAbsUpdates
    {
        public override int Constructor
        {
            get
            {
                return 1957577280;
            }
        }

        public TLVector<TLAbsUpdate> Updates { get; set; }
        public TLVector<TLAbsUser> Users { get; set; }
        public TLVector<TLAbsChat> Chats { get; set; }
        public int Date { get; set; }
        public int Seq { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Updates = (TLVector<TLAbsUpdate>)ObjectUtils.DeserializeVector<TLAbsUpdate>(br);
            this.Users = (TLVector<TLAbsUser>)ObjectUtils.DeserializeVector<TLAbsUser>(br);
            this.Chats = (TLVector<TLAbsChat>)ObjectUtils.DeserializeVector<TLAbsChat>(br);
            this.Date = br.ReadInt32();
            this.Seq = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            ObjectUtils.SerializeObject(this.Updates, bw);
            ObjectUtils.SerializeObject(this.Users, bw);
            ObjectUtils.SerializeObject(this.Chats, bw);
            bw.Write(this.Date);
            bw.Write(this.Seq);

        }
    }
}
