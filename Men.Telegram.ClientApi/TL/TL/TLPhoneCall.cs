using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1660057)]
    public class TLPhoneCall : TLAbsPhoneCall
    {
        public override int Constructor
        {
            get
            {
                return -1660057;
            }
        }

        public long Id { get; set; }
        public long AccessHash { get; set; }
        public int Date { get; set; }
        public int AdminId { get; set; }
        public int ParticipantId { get; set; }
        public byte[] GAOrB { get; set; }
        public long KeyFingerprint { get; set; }
        public TLPhoneCallProtocol Protocol { get; set; }
        public TLPhoneConnection Connection { get; set; }
        public TLVector<TLPhoneConnection> AlternativeConnections { get; set; }
        public int StartDate { get; set; }


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
            this.GAOrB = BytesUtil.Deserialize(br);
            this.KeyFingerprint = br.ReadInt64();
            this.Protocol = (TLPhoneCallProtocol)ObjectUtils.DeserializeObject(br);
            this.Connection = (TLPhoneConnection)ObjectUtils.DeserializeObject(br);
            this.AlternativeConnections = (TLVector<TLPhoneConnection>)ObjectUtils.DeserializeVector<TLPhoneConnection>(br);
            this.StartDate = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            bw.Write(this.Id);
            bw.Write(this.AccessHash);
            bw.Write(this.Date);
            bw.Write(this.AdminId);
            bw.Write(this.ParticipantId);
            BytesUtil.Serialize(this.GAOrB, bw);
            bw.Write(this.KeyFingerprint);
            ObjectUtils.SerializeObject(this.Protocol, bw);
            ObjectUtils.SerializeObject(this.Connection, bw);
            ObjectUtils.SerializeObject(this.AlternativeConnections, bw);
            bw.Write(this.StartDate);

        }
    }
}
