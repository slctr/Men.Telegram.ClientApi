using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-2089411356)]
    public class TLPhoneCallRequested : TLAbsPhoneCall
    {
        public override int Constructor
        {
            get
            {
                return -2089411356;
            }
        }

        public long Id { get; set; }
        public long AccessHash { get; set; }
        public int Date { get; set; }
        public int AdminId { get; set; }
        public int ParticipantId { get; set; }
        public byte[] GAHash { get; set; }
        public TLPhoneCallProtocol Protocol { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Id = br.ReadInt64();
            this.AccessHash = br.ReadInt64();
            this.Date = br.ReadInt32();
            this.AdminId = br.ReadInt32();
            this.ParticipantId = br.ReadInt32();
            this.GAHash = BytesUtil.Deserialize(br);
            this.Protocol = (TLPhoneCallProtocol)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            bw.Write(this.Id);
            bw.Write(this.AccessHash);
            bw.Write(this.Date);
            bw.Write(this.AdminId);
            bw.Write(this.ParticipantId);
            BytesUtil.Serialize(this.GAHash, bw);
            ObjectUtils.SerializeObject(this.Protocol, bw);

        }
    }
}
