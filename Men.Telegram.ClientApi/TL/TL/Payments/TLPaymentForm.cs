using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Payments
{
    [TLObject(1062645411)]
    public class TLPaymentForm : TLObject
    {
        public override int Constructor
        {
            get
            {
                return 1062645411;
            }
        }

        public int Flags { get; set; }
        public bool CanSaveCredentials { get; set; }
        public bool PasswordMissing { get; set; }
        public int BotId { get; set; }
        public TLInvoice Invoice { get; set; }
        public int ProviderId { get; set; }
        public string Url { get; set; }
        public string NativeProvider { get; set; }
        public TLDataJSON NativeParams { get; set; }
        public TLPaymentRequestedInfo SavedInfo { get; set; }
        public TLPaymentSavedCredentialsCard SavedCredentials { get; set; }
        public TLVector<TLAbsUser> Users { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.CanSaveCredentials ? (this.Flags | 4) : (this.Flags & ~4);
            this.Flags = this.PasswordMissing ? (this.Flags | 8) : (this.Flags & ~8);
            this.Flags = this.NativeProvider != null ? (this.Flags | 16) : (this.Flags & ~16);
            this.Flags = this.NativeParams != null ? (this.Flags | 16) : (this.Flags & ~16);
            this.Flags = this.SavedInfo != null ? (this.Flags | 1) : (this.Flags & ~1);
            this.Flags = this.SavedCredentials != null ? (this.Flags | 2) : (this.Flags & ~2);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            this.CanSaveCredentials = (this.Flags & 4) != 0;
            this.PasswordMissing = (this.Flags & 8) != 0;
            this.BotId = br.ReadInt32();
            this.Invoice = (TLInvoice)ObjectUtils.DeserializeObject(br);
            this.ProviderId = br.ReadInt32();
            this.Url = StringUtil.Deserialize(br);
            if ((this.Flags & 16) != 0)
                this.NativeProvider = StringUtil.Deserialize(br);
            else
                this.NativeProvider = null;

            if ((this.Flags & 16) != 0)
                this.NativeParams = (TLDataJSON)ObjectUtils.DeserializeObject(br);
            else
                this.NativeParams = null;

            if ((this.Flags & 1) != 0)
                this.SavedInfo = (TLPaymentRequestedInfo)ObjectUtils.DeserializeObject(br);
            else
                this.SavedInfo = null;

            if ((this.Flags & 2) != 0)
                this.SavedCredentials = (TLPaymentSavedCredentialsCard)ObjectUtils.DeserializeObject(br);
            else
                this.SavedCredentials = null;

            this.Users = (TLVector<TLAbsUser>)ObjectUtils.DeserializeVector<TLAbsUser>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);


            bw.Write(this.BotId);
            ObjectUtils.SerializeObject(this.Invoice, bw);
            bw.Write(this.ProviderId);
            StringUtil.Serialize(this.Url, bw);
            if ((this.Flags & 16) != 0)
                StringUtil.Serialize(this.NativeProvider, bw);
            if ((this.Flags & 16) != 0)
                ObjectUtils.SerializeObject(this.NativeParams, bw);
            if ((this.Flags & 1) != 0)
                ObjectUtils.SerializeObject(this.SavedInfo, bw);
            if ((this.Flags & 2) != 0)
                ObjectUtils.SerializeObject(this.SavedCredentials, bw);
            ObjectUtils.SerializeObject(this.Users, bw);

        }
    }
}
