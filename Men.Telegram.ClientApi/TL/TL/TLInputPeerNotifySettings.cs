using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(949182130)]
    public class TLInputPeerNotifySettings : TLObject
    {
        public override int Constructor
        {
            get
            {
                return 949182130;
            }
        }

        public int Flags { get; set; }
        public bool ShowPreviews { get; set; }
        public bool Silent { get; set; }
        public int MuteUntil { get; set; }
        public string Sound { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.ShowPreviews ? (this.Flags | 1) : (this.Flags & ~1);
            this.Flags = this.Silent ? (this.Flags | 2) : (this.Flags & ~2);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            this.ShowPreviews = (this.Flags & 1) != 0;
            this.Silent = (this.Flags & 2) != 0;
            this.MuteUntil = br.ReadInt32();
            this.Sound = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);


            bw.Write(this.MuteUntil);
            StringUtil.Serialize(this.Sound, bw);

        }
    }
}
