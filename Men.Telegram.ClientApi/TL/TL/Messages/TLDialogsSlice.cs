using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(1910543603)]
    public class TLDialogsSlice : TLAbsDialogs
    {
        public override int Constructor
        {
            get
            {
                return 1910543603;
            }
        }

        public int Count { get; set; }
        public TLVector<TLDialog> Dialogs { get; set; }
        public TLVector<TLAbsMessage> Messages { get; set; }
        public TLVector<TLAbsChat> Chats { get; set; }
        public TLVector<TLAbsUser> Users { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Count = br.ReadInt32();
            this.Dialogs = (TLVector<TLDialog>)ObjectUtils.DeserializeVector<TLDialog>(br);
            this.Messages = (TLVector<TLAbsMessage>)ObjectUtils.DeserializeVector<TLAbsMessage>(br);
            this.Chats = (TLVector<TLAbsChat>)ObjectUtils.DeserializeVector<TLAbsChat>(br);
            this.Users = (TLVector<TLAbsUser>)ObjectUtils.DeserializeVector<TLAbsUser>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            bw.Write(this.Count);
            ObjectUtils.SerializeObject(this.Dialogs, bw);
            ObjectUtils.SerializeObject(this.Messages, bw);
            ObjectUtils.SerializeObject(this.Chats, bw);
            ObjectUtils.SerializeObject(this.Users, bw);

        }
    }
}
