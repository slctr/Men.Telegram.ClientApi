using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(-1707344487)]
    public class TLHighScores : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -1707344487;
            }
        }

        public TLVector<TLHighScore> Scores { get; set; }
        public TLVector<TLAbsUser> Users { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Scores = (TLVector<TLHighScore>)ObjectUtils.DeserializeVector<TLHighScore>(br);
            this.Users = (TLVector<TLAbsUser>)ObjectUtils.DeserializeVector<TLAbsUser>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            ObjectUtils.SerializeObject(this.Scores, bw);
            ObjectUtils.SerializeObject(this.Users, bw);

        }
    }
}
