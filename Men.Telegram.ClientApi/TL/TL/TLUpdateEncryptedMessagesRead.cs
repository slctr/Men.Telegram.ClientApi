using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(956179895)]
    public class TLUpdateEncryptedMessagesRead : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return 956179895;
            }
        }

        public int ChatId { get; set; }
        public int MaxDate { get; set; }
        public int Date { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.ChatId = br.ReadInt32();
            this.MaxDate = br.ReadInt32();
            this.Date = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            bw.Write(this.ChatId);
            bw.Write(this.MaxDate);
            bw.Write(this.Date);

        }
    }
}
