using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Stickers
{
    [TLObject(-1680314774)]
    public class TLRequestCreateStickerSet : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -1680314774;
            }
        }

        public int Flags { get; set; }
        public bool Masks { get; set; }
        public TLAbsInputUser UserId { get; set; }
        public string Title { get; set; }
        public string ShortName { get; set; }
        public TLVector<TLInputStickerSetItem> Stickers { get; set; }
        public Messages.TLStickerSet Response { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.Masks ? (this.Flags | 1) : (this.Flags & ~1);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            this.Masks = (this.Flags & 1) != 0;
            this.UserId = (TLAbsInputUser)ObjectUtils.DeserializeObject(br);
            this.Title = StringUtil.Deserialize(br);
            this.ShortName = StringUtil.Deserialize(br);
            this.Stickers = (TLVector<TLInputStickerSetItem>)ObjectUtils.DeserializeVector<TLInputStickerSetItem>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);

            ObjectUtils.SerializeObject(this.UserId, bw);
            StringUtil.Serialize(this.Title, bw);
            StringUtil.Serialize(this.ShortName, bw);
            ObjectUtils.SerializeObject(this.Stickers, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            this.Response = (Messages.TLStickerSet)ObjectUtils.DeserializeObject(br);

        }
    }
}
