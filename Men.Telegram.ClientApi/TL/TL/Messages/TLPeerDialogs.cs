using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(863093588)]
    public class TLPeerDialogs : TLObject
    {
        public override int Constructor
        {
            get
            {
                return 863093588;
            }
        }

        public TLVector<TLDialog> Dialogs { get; set; }
        public TLVector<TLAbsMessage> Messages { get; set; }
        public TLVector<TLAbsChat> Chats { get; set; }
        public TLVector<TLAbsUser> Users { get; set; }
        public Updates.TLState State { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Dialogs = (TLVector<TLDialog>)ObjectUtils.DeserializeVector<TLDialog>(br);
            this.Messages = (TLVector<TLAbsMessage>)ObjectUtils.DeserializeVector<TLAbsMessage>(br);
            this.Chats = (TLVector<TLAbsChat>)ObjectUtils.DeserializeVector<TLAbsChat>(br);
            this.Users = (TLVector<TLAbsUser>)ObjectUtils.DeserializeVector<TLAbsUser>(br);
            this.State = (Updates.TLState)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            ObjectUtils.SerializeObject(this.Dialogs, bw);
            ObjectUtils.SerializeObject(this.Messages, bw);
            ObjectUtils.SerializeObject(this.Chats, bw);
            ObjectUtils.SerializeObject(this.Users, bw);
            ObjectUtils.SerializeObject(this.State, bw);

        }
    }
}
