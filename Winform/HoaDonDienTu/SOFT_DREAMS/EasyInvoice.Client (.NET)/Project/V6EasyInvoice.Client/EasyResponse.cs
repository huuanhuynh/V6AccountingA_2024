// Decompiled with JetBrains decompiler
// Type: EasyInvoice.Client.EasyResponse
// Assembly: EasyInvoice.Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9D591C1A-17CB-4CA4-A650-8459834356C1
// Assembly location: E:\Copy\Code\HoaDonDienTu\SOFT_DREAMS\EasyInvoice.Client (.NET)\EasyInvoice.Client.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace V6EasyInvoice.Client
{
    public class EasyResponse<TResult> : EasyResponse
    {
        public virtual TResult Data { get; internal set; }
    }
    public class EasyResponse
    {
        private string _content;

        public virtual WebHeaderCollection Headers { get; internal set; }

        public virtual long ContentLength { get; internal set; }

        public virtual string ContentEncoding { get; internal set; }

        public virtual string Content
        {
            get
            {
                if (Utils.IsNullOrWhiteSpace(this._content) && Enumerable.Any<byte>((IEnumerable<byte>)this.RawBytes))
                    this._content = Utils.AsString(this.RawBytes);
                return this._content;
            }
            internal set
            {
                this._content = value;
            }
        }

        public virtual HttpStatusCode HttpStatusCode { get; internal set; }

        public virtual string StatusDescription { get; internal set; }

        public virtual byte[] RawBytes { get; internal set; }

        public virtual string ErrorMessage { get; internal set; }

        public virtual string ContentType { get; internal set; }

        public virtual Exception Exception { get; internal set; }
    }
}
