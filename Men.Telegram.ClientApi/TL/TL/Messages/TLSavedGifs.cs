using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(772213157)]
    public class TLSavedGifs : TLAbsSavedGifs
    {
        public override int Constructor
        {
            get
            {
                return 772213157;
            }
        }

        public int Hash { get; set; }
        public TLVector<TLAbsDocument> Gifs { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Hash = br.ReadInt32();
            this.Gifs = (TLVector<TLAbsDocument>)ObjectUtils.DeserializeVector<TLAbsDocument>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            bw.Write(this.Hash);
            ObjectUtils.SerializeObject(this.Gifs, bw);

        }
    }
}
