using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1239335713)]
    public class TLShippingOption : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -1239335713;
            }
        }

        public string Id { get; set; }
        public string Title { get; set; }
        public TLVector<TLLabeledPrice> Prices { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Id = StringUtil.Deserialize(br);
            this.Title = StringUtil.Deserialize(br);
            this.Prices = (TLVector<TLLabeledPrice>)ObjectUtils.DeserializeVector<TLLabeledPrice>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            StringUtil.Serialize(this.Id, bw);
            StringUtil.Serialize(this.Title, bw);
            ObjectUtils.SerializeObject(this.Prices, bw);

        }
    }
}
