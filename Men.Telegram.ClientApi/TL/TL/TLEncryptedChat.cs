using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-94974410)]
    public class TLEncryptedChat : TLAbsEncryptedChat
    {
        public override int Constructor
        {
            get
            {
                return -94974410;
            }
        }

        public int Id { get; set; }
        public long AccessHash { get; set; }
        public int Date { get; set; }
        public int AdminId { get; set; }
        public int ParticipantId { get; set; }
        public byte[] GAOrB { get; set; }
        public long KeyFingerprint { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Id = br.ReadInt32();
            this.AccessHash = br.ReadInt64();
            this.Date = br.ReadInt32();
            this.AdminId = br.ReadInt32();
            this.ParticipantId = br.ReadInt32();
            this.GAOrB = BytesUtil.Deserialize(br);
            this.KeyFingerprint = br.ReadInt64();

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

        }
    }
}
