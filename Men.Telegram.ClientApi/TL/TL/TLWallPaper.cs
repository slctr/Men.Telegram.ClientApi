using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-860866985)]
    public class TLWallPaper : TLAbsWallPaper
    {
        public override int Constructor
        {
            get
            {
                return -860866985;
            }
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public TLVector<TLAbsPhotoSize> Sizes { get; set; }
        public int Color { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Id = br.ReadInt32();
            this.Title = StringUtil.Deserialize(br);
            this.Sizes = (TLVector<TLAbsPhotoSize>)ObjectUtils.DeserializeVector<TLAbsPhotoSize>(br);
            this.Color = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            bw.Write(this.Id);
            StringUtil.Serialize(this.Title, bw);
            ObjectUtils.SerializeObject(this.Sizes, bw);
            bw.Write(this.Color);

        }
    }
}
