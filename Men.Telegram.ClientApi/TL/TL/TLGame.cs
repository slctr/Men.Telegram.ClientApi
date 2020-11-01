using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1107729093)]
    public class TLGame : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -1107729093;
            }
        }

        public int Flags { get; set; }
        public long Id { get; set; }
        public long AccessHash { get; set; }
        public string ShortName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TLAbsPhoto Photo { get; set; }
        public TLAbsDocument Document { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.Document != null ? (this.Flags | 1) : (this.Flags & ~1);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            this.Id = br.ReadInt64();
            this.AccessHash = br.ReadInt64();
            this.ShortName = StringUtil.Deserialize(br);
            this.Title = StringUtil.Deserialize(br);
            this.Description = StringUtil.Deserialize(br);
            this.Photo = (TLAbsPhoto)ObjectUtils.DeserializeObject(br);
            if ((this.Flags & 1) != 0)
            {
                this.Document = (TLAbsDocument)ObjectUtils.DeserializeObject(br);
            }
            else
            {
                this.Document = null;
            }
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);
            bw.Write(this.Id);
            bw.Write(this.AccessHash);
            StringUtil.Serialize(this.ShortName, bw);
            StringUtil.Serialize(this.Title, bw);
            StringUtil.Serialize(this.Description, bw);
            ObjectUtils.SerializeObject(this.Photo, bw);
            if ((this.Flags & 1) != 0)
            {
                ObjectUtils.SerializeObject(this.Document, bw);
            }
        }
    }
}
