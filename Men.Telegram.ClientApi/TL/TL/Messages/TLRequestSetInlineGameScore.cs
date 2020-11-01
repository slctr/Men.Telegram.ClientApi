using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(363700068)]
    public class TLRequestSetInlineGameScore : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 363700068;
            }
        }

        public int Flags { get; set; }
        public bool EditMessage { get; set; }
        public bool Force { get; set; }
        public TLInputBotInlineMessageID Id { get; set; }
        public TLAbsInputUser UserId { get; set; }
        public int Score { get; set; }
        public bool Response { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.EditMessage ? (this.Flags | 1) : (this.Flags & ~1);
            this.Flags = this.Force ? (this.Flags | 2) : (this.Flags & ~2);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            this.EditMessage = (this.Flags & 1) != 0;
            this.Force = (this.Flags & 2) != 0;
            this.Id = (TLInputBotInlineMessageID)ObjectUtils.DeserializeObject(br);
            this.UserId = (TLAbsInputUser)ObjectUtils.DeserializeObject(br);
            this.Score = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);


            ObjectUtils.SerializeObject(this.Id, bw);
            ObjectUtils.SerializeObject(this.UserId, bw);
            bw.Write(this.Score);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            this.Response = BoolUtil.Deserialize(br);

        }
    }
}
