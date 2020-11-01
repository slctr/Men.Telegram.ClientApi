﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TeleSharp.TL;
namespace TeleSharp.TL
{
    public abstract class TLMethod : TLObject
    {

        public abstract void DeserializeResponse(BinaryReader stream);
        #region MTPROTO
        public long MessageId { get; set; }
        public int Sequence { get; set; }
        public bool Dirty { get; set; }
        public bool Sended { get; private set; }
        public DateTime SendTime { get; private set; }
        public bool ConfirmReceived { get; set; }
        public virtual bool Confirmed { get; } = true;
        public virtual bool Responded { get; } = false;

        public virtual void OnSendSuccess()
        {
            this.SendTime = DateTime.Now;
            this.Sended = true;
        }

        public virtual void OnConfirm()
        {
            this.ConfirmReceived = true;
        }

        public bool NeedResend
        {
            get
            {
                return this.Dirty || (this.Confirmed && !this.ConfirmReceived && DateTime.Now - this.SendTime > TimeSpan.FromSeconds(3));
            }
        }
        #endregion

    }
}