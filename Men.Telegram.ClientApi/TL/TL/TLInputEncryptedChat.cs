using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-247351839)]
    public class TLInputEncryptedChat : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -247351839;
            }
        }

        public int ChatId { get; set; }
        public long AccessHash { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.ChatId = br.ReadInt32();
            this.AccessHash = br.ReadInt64();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            bw.Write(this.ChatId);
            bw.Write(this.AccessHash);

        }
    }
}
