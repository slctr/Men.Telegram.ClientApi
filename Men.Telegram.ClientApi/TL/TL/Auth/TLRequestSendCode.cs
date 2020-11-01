using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Auth
{
    [TLObject(-2035355412)]
    public class TLRequestSendCode : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -2035355412;
            }
        }

        public int Flags { get; set; }
        public bool AllowFlashcall { get; set; }
        public string PhoneNumber { get; set; }
        public bool? CurrentNumber { get; set; }
        public int ApiId { get; set; }
        public string ApiHash { get; set; }
        public Auth.TLSentCode Response { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.AllowFlashcall ? (this.Flags | 1) : (this.Flags & ~1);
            this.Flags = this.CurrentNumber != null ? (this.Flags | 1) : (this.Flags & ~1);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            this.AllowFlashcall = (this.Flags & 1) != 0;
            this.PhoneNumber = StringUtil.Deserialize(br);
            if ((this.Flags & 1) != 0)
                this.CurrentNumber = BoolUtil.Deserialize(br);
            else
                this.CurrentNumber = null;

            this.ApiId = br.ReadInt32();
            this.ApiHash = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);

            StringUtil.Serialize(this.PhoneNumber, bw);
            if ((this.Flags & 1) != 0)
                BoolUtil.Serialize(this.CurrentNumber.Value, bw);
            bw.Write(this.ApiId);
            StringUtil.Serialize(this.ApiHash, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            this.Response = (Auth.TLSentCode)ObjectUtils.DeserializeObject(br);

        }
    }
}
