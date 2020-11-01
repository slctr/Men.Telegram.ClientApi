using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-75283823)]
    public class TLTopPeerCategoryPeers : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -75283823;
            }
        }

        public TLAbsTopPeerCategory Category { get; set; }
        public int Count { get; set; }
        public TLVector<TLTopPeer> Peers { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Category = (TLAbsTopPeerCategory)ObjectUtils.DeserializeObject(br);
            this.Count = br.ReadInt32();
            this.Peers = (TLVector<TLTopPeer>)ObjectUtils.DeserializeVector<TLTopPeer>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            ObjectUtils.SerializeObject(this.Category, bw);
            bw.Write(this.Count);
            ObjectUtils.SerializeObject(this.Peers, bw);

        }
    }
}
