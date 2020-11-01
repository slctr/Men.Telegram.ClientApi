using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1729618630)]
    public class TLBotInfo : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -1729618630;
            }
        }

        public int UserId { get; set; }
        public string Description { get; set; }
        public TLVector<TLBotCommand> Commands { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.UserId = br.ReadInt32();
            this.Description = StringUtil.Deserialize(br);
            this.Commands = (TLVector<TLBotCommand>)ObjectUtils.DeserializeVector<TLBotCommand>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            bw.Write(this.UserId);
            StringUtil.Serialize(this.Description, bw);
            ObjectUtils.SerializeObject(this.Commands, bw);

        }
    }
}
