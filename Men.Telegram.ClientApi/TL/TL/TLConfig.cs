using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-882895228)]
    public class TLConfig : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -882895228;
            }
        }

        public int Flags { get; set; }
        public bool PhonecallsEnabled { get; set; }
        public int Date { get; set; }
        public int Expires { get; set; }
        public bool TestMode { get; set; }
        public int ThisDc { get; set; }
        public TLVector<TLDcOption> DcOptions { get; set; }
        public int ChatSizeMax { get; set; }
        public int MegagroupSizeMax { get; set; }
        public int ForwardedCountMax { get; set; }
        public int OnlineUpdatePeriodMs { get; set; }
        public int OfflineBlurTimeoutMs { get; set; }
        public int OfflineIdleTimeoutMs { get; set; }
        public int OnlineCloudTimeoutMs { get; set; }
        public int NotifyCloudDelayMs { get; set; }
        public int NotifyDefaultDelayMs { get; set; }
        public int ChatBigSize { get; set; }
        public int PushChatPeriodMs { get; set; }
        public int PushChatLimit { get; set; }
        public int SavedGifsLimit { get; set; }
        public int EditTimeLimit { get; set; }
        public int RatingEDecay { get; set; }
        public int StickersRecentLimit { get; set; }
        public int? TmpSessions { get; set; }
        public int PinnedDialogsCountMax { get; set; }
        public int CallReceiveTimeoutMs { get; set; }
        public int CallRingTimeoutMs { get; set; }
        public int CallConnectTimeoutMs { get; set; }
        public int CallPacketTimeoutMs { get; set; }
        public string MeUrlPrefix { get; set; }
        public TLVector<TLDisabledFeature> DisabledFeatures { get; set; }


        public void ComputeFlags()
        {
            this.Flags = 0;
            this.Flags = this.PhonecallsEnabled ? (this.Flags | 2) : (this.Flags & ~2);
            this.Flags = this.TmpSessions != null ? (this.Flags | 1) : (this.Flags & ~1);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            this.Flags = br.ReadInt32();
            this.PhonecallsEnabled = (this.Flags & 2) != 0;
            this.Date = br.ReadInt32();
            this.Expires = br.ReadInt32();
            this.TestMode = BoolUtil.Deserialize(br);
            this.ThisDc = br.ReadInt32();
            this.DcOptions = (TLVector<TLDcOption>)ObjectUtils.DeserializeVector<TLDcOption>(br);
            this.ChatSizeMax = br.ReadInt32();
            this.MegagroupSizeMax = br.ReadInt32();
            this.ForwardedCountMax = br.ReadInt32();
            this.OnlineUpdatePeriodMs = br.ReadInt32();
            this.OfflineBlurTimeoutMs = br.ReadInt32();
            this.OfflineIdleTimeoutMs = br.ReadInt32();
            this.OnlineCloudTimeoutMs = br.ReadInt32();
            this.NotifyCloudDelayMs = br.ReadInt32();
            this.NotifyDefaultDelayMs = br.ReadInt32();
            this.ChatBigSize = br.ReadInt32();
            this.PushChatPeriodMs = br.ReadInt32();
            this.PushChatLimit = br.ReadInt32();
            this.SavedGifsLimit = br.ReadInt32();
            this.EditTimeLimit = br.ReadInt32();
            this.RatingEDecay = br.ReadInt32();
            this.StickersRecentLimit = br.ReadInt32();
            if ((this.Flags & 1) != 0)
                this.TmpSessions = br.ReadInt32();
            else
                this.TmpSessions = null;

            this.PinnedDialogsCountMax = br.ReadInt32();
            this.CallReceiveTimeoutMs = br.ReadInt32();
            this.CallRingTimeoutMs = br.ReadInt32();
            this.CallConnectTimeoutMs = br.ReadInt32();
            this.CallPacketTimeoutMs = br.ReadInt32();
            this.MeUrlPrefix = StringUtil.Deserialize(br);
            this.DisabledFeatures = (TLVector<TLDisabledFeature>)ObjectUtils.DeserializeVector<TLDisabledFeature>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            this.ComputeFlags();
            bw.Write(this.Flags);

            bw.Write(this.Date);
            bw.Write(this.Expires);
            BoolUtil.Serialize(this.TestMode, bw);
            bw.Write(this.ThisDc);
            ObjectUtils.SerializeObject(this.DcOptions, bw);
            bw.Write(this.ChatSizeMax);
            bw.Write(this.MegagroupSizeMax);
            bw.Write(this.ForwardedCountMax);
            bw.Write(this.OnlineUpdatePeriodMs);
            bw.Write(this.OfflineBlurTimeoutMs);
            bw.Write(this.OfflineIdleTimeoutMs);
            bw.Write(this.OnlineCloudTimeoutMs);
            bw.Write(this.NotifyCloudDelayMs);
            bw.Write(this.NotifyDefaultDelayMs);
            bw.Write(this.ChatBigSize);
            bw.Write(this.PushChatPeriodMs);
            bw.Write(this.PushChatLimit);
            bw.Write(this.SavedGifsLimit);
            bw.Write(this.EditTimeLimit);
            bw.Write(this.RatingEDecay);
            bw.Write(this.StickersRecentLimit);
            if ((this.Flags & 1) != 0)
                bw.Write(this.TmpSessions.Value);
            bw.Write(this.PinnedDialogsCountMax);
            bw.Write(this.CallReceiveTimeoutMs);
            bw.Write(this.CallRingTimeoutMs);
            bw.Write(this.CallConnectTimeoutMs);
            bw.Write(this.CallPacketTimeoutMs);
            StringUtil.Serialize(this.MeUrlPrefix, bw);
            ObjectUtils.SerializeObject(this.DisabledFeatures, bw);

        }
    }
}
