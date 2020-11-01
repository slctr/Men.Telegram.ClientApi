using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(-1938715001)]
    public class TLMessages : TLAbsMessages
    {
        public override int Constructor
        {
            get
            {
                return -1938715001;
            }
        }

        public TLVector<TLAbsMessage> Messages { get; set; }
        public TLVector<TLAbsChat> Chats { get; set; }
        public TLVector<TLAbsUser> Users { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Messages = (TLVector<TLAbsMessage>)ObjectUtils.DeserializeVector<TLAbsMessage>(br);
            this.Chats = (TLVector<TLAbsChat>)ObjectUtils.DeserializeVector<TLAbsChat>(br);
            this.Users = (TLVector<TLAbsUser>)ObjectUtils.DeserializeVector<TLAbsUser>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            ObjectUtils.SerializeObject(this.Messages, bw);
            ObjectUtils.SerializeObject(this.Chats, bw);
            ObjectUtils.SerializeObject(this.Users, bw);

        }
    }
}
