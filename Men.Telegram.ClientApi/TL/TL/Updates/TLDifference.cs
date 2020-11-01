using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Updates
{
    [TLObject(16030880)]
    public class TLDifference : TLAbsDifference
    {
        public override int Constructor
        {
            get
            {
                return 16030880;
            }
        }

        public TLVector<TLAbsMessage> NewMessages { get; set; }
        public TLVector<TLAbsEncryptedMessage> NewEncryptedMessages { get; set; }
        public TLVector<TLAbsUpdate> OtherUpdates { get; set; }
        public TLVector<TLAbsChat> Chats { get; set; }
        public TLVector<TLAbsUser> Users { get; set; }
        public Updates.TLState State { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.NewMessages = (TLVector<TLAbsMessage>)ObjectUtils.DeserializeVector<TLAbsMessage>(br);
            this.NewEncryptedMessages = (TLVector<TLAbsEncryptedMessage>)ObjectUtils.DeserializeVector<TLAbsEncryptedMessage>(br);
            this.OtherUpdates = (TLVector<TLAbsUpdate>)ObjectUtils.DeserializeVector<TLAbsUpdate>(br);
            this.Chats = (TLVector<TLAbsChat>)ObjectUtils.DeserializeVector<TLAbsChat>(br);
            this.Users = (TLVector<TLAbsUser>)ObjectUtils.DeserializeVector<TLAbsUser>(br);
            this.State = (Updates.TLState)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            ObjectUtils.SerializeObject(this.NewMessages, bw);
            ObjectUtils.SerializeObject(this.NewEncryptedMessages, bw);
            ObjectUtils.SerializeObject(this.OtherUpdates, bw);
            ObjectUtils.SerializeObject(this.Chats, bw);
            ObjectUtils.SerializeObject(this.Users, bw);
            ObjectUtils.SerializeObject(this.State, bw);

        }
    }
}
