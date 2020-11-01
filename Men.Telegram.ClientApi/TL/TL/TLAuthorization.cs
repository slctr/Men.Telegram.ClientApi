using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(2079516406)]
    public class TLAuthorization : TLObject
    {
        public override int Constructor
        {
            get
            {
                return 2079516406;
            }
        }

        public long Hash { get; set; }
        public int Flags { get; set; }
        public string DeviceModel { get; set; }
        public string Platform { get; set; }
        public string SystemVersion { get; set; }
        public int ApiId { get; set; }
        public string AppName { get; set; }
        public string AppVersion { get; set; }
        public int DateCreated { get; set; }
        public int DateActive { get; set; }
        public string Ip { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Hash = br.ReadInt64();
            this.Flags = br.ReadInt32();
            this.DeviceModel = StringUtil.Deserialize(br);
            this.Platform = StringUtil.Deserialize(br);
            this.SystemVersion = StringUtil.Deserialize(br);
            this.ApiId = br.ReadInt32();
            this.AppName = StringUtil.Deserialize(br);
            this.AppVersion = StringUtil.Deserialize(br);
            this.DateCreated = br.ReadInt32();
            this.DateActive = br.ReadInt32();
            this.Ip = StringUtil.Deserialize(br);
            this.Country = StringUtil.Deserialize(br);
            this.Region = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);
            bw.Write(this.Hash);
            StringUtil.Serialize(this.DeviceModel, bw);
            StringUtil.Serialize(this.Platform, bw);
            StringUtil.Serialize(this.SystemVersion, bw);
            bw.Write(this.ApiId);
            StringUtil.Serialize(this.AppName, bw);
            StringUtil.Serialize(this.AppVersion, bw);
            bw.Write(this.DateCreated);
            bw.Write(this.DateActive);
            StringUtil.Serialize(this.Ip, bw);
            StringUtil.Serialize(this.Country, bw);
            StringUtil.Serialize(this.Region, bw);

        }
    }
}
