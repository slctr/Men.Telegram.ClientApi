using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-852477119)]
    public class TLStickerSet : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -852477119;
            }
        }

        public int Flags { get; set; }
        public bool Installed { get; set; }
        public bool Archived { get; set; }
        public bool Official { get; set; }
        public bool Masks { get; set; }
        public long Id { get; set; }
        public long AccessHash { get; set; }
        public string Title { get; set; }
        public string ShortName { get; set; }
        public int Count { get; set; }
        public int Hash { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.Installed ? (this.Flags | 1) : (this.Flags & ~1);
            this.Flags = this.Archived ? (this.Flags | 2) : (this.Flags & ~2);
            this.Flags = this.Official ? (this.Flags | 4) : (this.Flags & ~4);
            this.Flags = this.Masks ? (this.Flags | 8) : (this.Flags & ~8);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            this.Installed = (this.Flags & 1) != 0;
            this.Archived = (this.Flags & 2) != 0;
            this.Official = (this.Flags & 4) != 0;
            this.Masks = (this.Flags & 8) != 0;
            this.Id = br.ReadInt64();
            this.AccessHash = br.ReadInt64();
            this.Title = StringUtil.Deserialize(br);
            this.ShortName = StringUtil.Deserialize(br);
            this.Count = br.ReadInt32();
            this.Hash = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);




            bw.Write(this.Id);
            bw.Write(this.AccessHash);
            StringUtil.Serialize(this.Title, bw);
            StringUtil.Serialize(this.ShortName, bw);
            bw.Write(this.Count);
            bw.Write(this.Hash);

        }
    }
}
