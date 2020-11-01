using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(771925524)]
    public class TLChatFull : TLAbsChatFull
    {
        public override int Constructor
        {
            get
            {
                return 771925524;
            }
        }

        public int Id { get; set; }
        public TLAbsChatParticipants Participants { get; set; }
        public TLAbsPhoto ChatPhoto { get; set; }
        public TLAbsPeerNotifySettings NotifySettings { get; set; }
        public TLAbsExportedChatInvite ExportedInvite { get; set; }
        public TLVector<TLBotInfo> BotInfo { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Id = br.ReadInt32();
            this.Participants = (TLAbsChatParticipants)ObjectUtils.DeserializeObject(br);
            this.ChatPhoto = (TLAbsPhoto)ObjectUtils.DeserializeObject(br);
            this.NotifySettings = (TLAbsPeerNotifySettings)ObjectUtils.DeserializeObject(br);
            this.ExportedInvite = (TLAbsExportedChatInvite)ObjectUtils.DeserializeObject(br);
            this.BotInfo = (TLVector<TLBotInfo>)ObjectUtils.DeserializeVector<TLBotInfo>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            bw.Write(this.Id);
            ObjectUtils.SerializeObject(this.Participants, bw);
            ObjectUtils.SerializeObject(this.ChatPhoto, bw);
            ObjectUtils.SerializeObject(this.NotifySettings, bw);
            ObjectUtils.SerializeObject(this.ExportedInvite, bw);
            ObjectUtils.SerializeObject(this.BotInfo, bw);

        }
    }
}
