using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(1769565673)]
    public class TLRequestInitConnection : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 1769565673;
            }
        }

        public int ApiId { get; set; }
        public string DeviceModel { get; set; }
        public string SystemVersion { get; set; }
        public string AppVersion { get; set; }
        public string LangCode { get; set; }
        public TLObject Query { get; set; }
        public TLObject Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.ApiId = br.ReadInt32();
            this.DeviceModel = StringUtil.Deserialize(br);
            this.SystemVersion = StringUtil.Deserialize(br);
            this.AppVersion = StringUtil.Deserialize(br);
            this.LangCode = StringUtil.Deserialize(br);
            this.Query = (TLObject)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            bw.Write(this.ApiId);
            StringUtil.Serialize(this.DeviceModel, bw);
            StringUtil.Serialize(this.SystemVersion, bw);
            StringUtil.Serialize(this.AppVersion, bw);
            StringUtil.Serialize(this.LangCode, bw);
            ObjectUtils.SerializeObject(this.Query, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            this.Response = (TLObject)ObjectUtils.DeserializeObject(br);

        }
    }
}
