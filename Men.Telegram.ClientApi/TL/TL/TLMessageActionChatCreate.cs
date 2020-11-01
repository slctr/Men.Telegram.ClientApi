using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1503425638)]
    public class TLMessageActionChatCreate : TLAbsMessageAction
    {
        public override int Constructor
        {
            get
            {
                return -1503425638;
            }
        }

        public string Title { get; set; }
        public TLVector<int> Users { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Title = StringUtil.Deserialize(br);
            this.Users = (TLVector<int>)ObjectUtils.DeserializeVector<int>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            StringUtil.Serialize(this.Title, bw);
            ObjectUtils.SerializeObject(this.Users, bw);

        }
    }
}
