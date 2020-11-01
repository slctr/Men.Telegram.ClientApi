using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-459324)]
    public class TLInputBotInlineResultDocument : TLAbsInputBotInlineResult
    {
        public override int Constructor
        {
            get
            {
                return -459324;
            }
        }

        public int Flags { get; set; }
        public string Id { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TLAbsInputDocument Document { get; set; }
        public TLAbsInputBotInlineMessage SendMessage { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.Title != null ? (this.Flags | 2) : (this.Flags & ~2);
            this.Flags = this.Description != null ? (this.Flags | 4) : (this.Flags & ~4);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            this.Id = StringUtil.Deserialize(br);
            this.Type = StringUtil.Deserialize(br);
            if ((this.Flags & 2) != 0)
                this.Title = StringUtil.Deserialize(br);
            else
                this.Title = null;

            if ((this.Flags & 4) != 0)
                this.Description = StringUtil.Deserialize(br);
            else
                this.Description = null;

            this.Document = (TLAbsInputDocument)ObjectUtils.DeserializeObject(br);
            this.SendMessage = (TLAbsInputBotInlineMessage)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);
            StringUtil.Serialize(this.Id, bw);
            StringUtil.Serialize(this.Type, bw);
            if ((this.Flags & 2) != 0)
                StringUtil.Serialize(this.Title, bw);
            if ((this.Flags & 4) != 0)
                StringUtil.Serialize(this.Description, bw);
            ObjectUtils.SerializeObject(this.Document, bw);
            ObjectUtils.SerializeObject(this.SendMessage, bw);

        }
    }
}
