﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.42000.
// 
#pragma warning disable 1591

namespace V6ThuePostXmlApi.PublishService {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1590.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="PublishServiceSoap", Namespace="http://tempuri.org/")]
    public partial class PublishService : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback ImportAndPublishInvOperationCompleted;
        
        private System.Threading.SendOrPostCallback ImportInvPatternOperationCompleted;
        
        private System.Threading.SendOrPostCallback ImportInvOperationCompleted;
        
        private System.Threading.SendOrPostCallback publishInvOperationCompleted;
        
        private System.Threading.SendOrPostCallback UpdateCusOperationCompleted;
        
        private System.Threading.SendOrPostCallback setCusCertOperationCompleted;
        
        private System.Threading.SendOrPostCallback ImportInvByPatternSerialOperationCompleted;
        
        private System.Threading.SendOrPostCallback publishInvFkeyOperationCompleted;
        
        private System.Threading.SendOrPostCallback deleteInvFkeyOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public PublishService() {
            this.Url = global::V6ThuePostXmlApi.Properties.Settings.Default.V6ThuePostApi_nuocbdgservice_PublishService;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        public PublishService(string url) {
            this.Url = url;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event ImportAndPublishInvCompletedEventHandler ImportAndPublishInvCompleted;
        
        /// <remarks/>
        public event ImportInvPatternCompletedEventHandler ImportInvPatternCompleted;
        
        /// <remarks/>
        public event ImportInvCompletedEventHandler ImportInvCompleted;
        
        /// <remarks/>
        public event publishInvCompletedEventHandler publishInvCompleted;
        
        /// <remarks/>
        public event UpdateCusCompletedEventHandler UpdateCusCompleted;
        
        /// <remarks/>
        public event setCusCertCompletedEventHandler setCusCertCompleted;
        
        /// <remarks/>
        public event ImportInvByPatternSerialCompletedEventHandler ImportInvByPatternSerialCompleted;
        
        /// <remarks/>
        public event publishInvFkeyCompletedEventHandler publishInvFkeyCompleted;
        
        /// <remarks/>
        public event deleteInvFkeyCompletedEventHandler deleteInvFkeyCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/ImportAndPublishInv", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string ImportAndPublishInv(string Account, string ACpass, string xmlInvData, string username, string password, string pattern, string serial, int convert) {
            object[] results = this.Invoke("ImportAndPublishInv", new object[] {
                        Account,
                        ACpass,
                        xmlInvData,
                        username,
                        password,
                        pattern,
                        serial,
                        convert});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void ImportAndPublishInvAsync(string Account, string ACpass, string xmlInvData, string username, string password, string pattern, string serial, int convert) {
            this.ImportAndPublishInvAsync(Account, ACpass, xmlInvData, username, password, pattern, serial, convert, null);
        }
        
        /// <remarks/>
        public void ImportAndPublishInvAsync(string Account, string ACpass, string xmlInvData, string username, string password, string pattern, string serial, int convert, object userState) {
            if ((this.ImportAndPublishInvOperationCompleted == null)) {
                this.ImportAndPublishInvOperationCompleted = new System.Threading.SendOrPostCallback(this.OnImportAndPublishInvOperationCompleted);
            }
            this.InvokeAsync("ImportAndPublishInv", new object[] {
                        Account,
                        ACpass,
                        xmlInvData,
                        username,
                        password,
                        pattern,
                        serial,
                        convert}, this.ImportAndPublishInvOperationCompleted, userState);
        }
        
        private void OnImportAndPublishInvOperationCompleted(object arg) {
            if ((this.ImportAndPublishInvCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ImportAndPublishInvCompleted(this, new ImportAndPublishInvCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/ImportInvPattern", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string ImportInvPattern(string xmlInvData, string username, string password, int convert, string pattern, string serial) {
            object[] results = this.Invoke("ImportInvPattern", new object[] {
                        xmlInvData,
                        username,
                        password,
                        convert,
                        pattern,
                        serial});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void ImportInvPatternAsync(string xmlInvData, string username, string password, int convert, string pattern, string serial) {
            this.ImportInvPatternAsync(xmlInvData, username, password, convert, pattern, serial, null);
        }
        
        /// <remarks/>
        public void ImportInvPatternAsync(string xmlInvData, string username, string password, int convert, string pattern, string serial, object userState) {
            if ((this.ImportInvPatternOperationCompleted == null)) {
                this.ImportInvPatternOperationCompleted = new System.Threading.SendOrPostCallback(this.OnImportInvPatternOperationCompleted);
            }
            this.InvokeAsync("ImportInvPattern", new object[] {
                        xmlInvData,
                        username,
                        password,
                        convert,
                        pattern,
                        serial}, this.ImportInvPatternOperationCompleted, userState);
        }
        
        private void OnImportInvPatternOperationCompleted(object arg) {
            if ((this.ImportInvPatternCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ImportInvPatternCompleted(this, new ImportInvPatternCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/ImportInv", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string ImportInv(string xmlInvData, string username, string password, int convert) {
            object[] results = this.Invoke("ImportInv", new object[] {
                        xmlInvData,
                        username,
                        password,
                        convert});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void ImportInvAsync(string xmlInvData, string username, string password, int convert) {
            this.ImportInvAsync(xmlInvData, username, password, convert, null);
        }
        
        /// <remarks/>
        public void ImportInvAsync(string xmlInvData, string username, string password, int convert, object userState) {
            if ((this.ImportInvOperationCompleted == null)) {
                this.ImportInvOperationCompleted = new System.Threading.SendOrPostCallback(this.OnImportInvOperationCompleted);
            }
            this.InvokeAsync("ImportInv", new object[] {
                        xmlInvData,
                        username,
                        password,
                        convert}, this.ImportInvOperationCompleted, userState);
        }
        
        private void OnImportInvOperationCompleted(object arg) {
            if ((this.ImportInvCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ImportInvCompleted(this, new ImportInvCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/publishInv", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string publishInv(int[] invIDs, string username, string password, string pattern, string serial) {
            object[] results = this.Invoke("publishInv", new object[] {
                        invIDs,
                        username,
                        password,
                        pattern,
                        serial});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void publishInvAsync(int[] invIDs, string username, string password, string pattern, string serial) {
            this.publishInvAsync(invIDs, username, password, pattern, serial, null);
        }
        
        /// <remarks/>
        public void publishInvAsync(int[] invIDs, string username, string password, string pattern, string serial, object userState) {
            if ((this.publishInvOperationCompleted == null)) {
                this.publishInvOperationCompleted = new System.Threading.SendOrPostCallback(this.OnpublishInvOperationCompleted);
            }
            this.InvokeAsync("publishInv", new object[] {
                        invIDs,
                        username,
                        password,
                        pattern,
                        serial}, this.publishInvOperationCompleted, userState);
        }
        
        private void OnpublishInvOperationCompleted(object arg) {
            if ((this.publishInvCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.publishInvCompleted(this, new publishInvCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/UpdateCus", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int UpdateCus(string XMLCusData, string username, string pass, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] System.Nullable<int> convert) {
            object[] results = this.Invoke("UpdateCus", new object[] {
                        XMLCusData,
                        username,
                        pass,
                        convert});
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public void UpdateCusAsync(string XMLCusData, string username, string pass, System.Nullable<int> convert) {
            this.UpdateCusAsync(XMLCusData, username, pass, convert, null);
        }
        
        /// <remarks/>
        public void UpdateCusAsync(string XMLCusData, string username, string pass, System.Nullable<int> convert, object userState) {
            if ((this.UpdateCusOperationCompleted == null)) {
                this.UpdateCusOperationCompleted = new System.Threading.SendOrPostCallback(this.OnUpdateCusOperationCompleted);
            }
            this.InvokeAsync("UpdateCus", new object[] {
                        XMLCusData,
                        username,
                        pass,
                        convert}, this.UpdateCusOperationCompleted, userState);
        }
        
        private void OnUpdateCusOperationCompleted(object arg) {
            if ((this.UpdateCusCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.UpdateCusCompleted(this, new UpdateCusCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/setCusCert", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int setCusCert(string certSerial, string certString, string cusCode, string username, string pass) {
            object[] results = this.Invoke("setCusCert", new object[] {
                        certSerial,
                        certString,
                        cusCode,
                        username,
                        pass});
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public void setCusCertAsync(string certSerial, string certString, string cusCode, string username, string pass) {
            this.setCusCertAsync(certSerial, certString, cusCode, username, pass, null);
        }
        
        /// <remarks/>
        public void setCusCertAsync(string certSerial, string certString, string cusCode, string username, string pass, object userState) {
            if ((this.setCusCertOperationCompleted == null)) {
                this.setCusCertOperationCompleted = new System.Threading.SendOrPostCallback(this.OnsetCusCertOperationCompleted);
            }
            this.InvokeAsync("setCusCert", new object[] {
                        certSerial,
                        certString,
                        cusCode,
                        username,
                        pass}, this.setCusCertOperationCompleted, userState);
        }
        
        private void OnsetCusCertOperationCompleted(object arg) {
            if ((this.setCusCertCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.setCusCertCompleted(this, new setCusCertCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/ImportInvByPatternSerial", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string ImportInvByPatternSerial(string xmlInvData, string username, string pass, string pattern, string serial, int convert) {
            object[] results = this.Invoke("ImportInvByPatternSerial", new object[] {
                        xmlInvData,
                        username,
                        pass,
                        pattern,
                        serial,
                        convert});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void ImportInvByPatternSerialAsync(string xmlInvData, string username, string pass, string pattern, string serial, int convert) {
            this.ImportInvByPatternSerialAsync(xmlInvData, username, pass, pattern, serial, convert, null);
        }
        
        /// <remarks/>
        public void ImportInvByPatternSerialAsync(string xmlInvData, string username, string pass, string pattern, string serial, int convert, object userState) {
            if ((this.ImportInvByPatternSerialOperationCompleted == null)) {
                this.ImportInvByPatternSerialOperationCompleted = new System.Threading.SendOrPostCallback(this.OnImportInvByPatternSerialOperationCompleted);
            }
            this.InvokeAsync("ImportInvByPatternSerial", new object[] {
                        xmlInvData,
                        username,
                        pass,
                        pattern,
                        serial,
                        convert}, this.ImportInvByPatternSerialOperationCompleted, userState);
        }
        
        private void OnImportInvByPatternSerialOperationCompleted(object arg) {
            if ((this.ImportInvByPatternSerialCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ImportInvByPatternSerialCompleted(this, new ImportInvByPatternSerialCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/publishInvFkey", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string publishInvFkey(string Account, string ACpass, string lsFkey, string userName, string pass, string pattern, string serial) {
            object[] results = this.Invoke("publishInvFkey", new object[] {
                        Account,
                        ACpass,
                        lsFkey,
                        userName,
                        pass,
                        pattern,
                        serial});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void publishInvFkeyAsync(string Account, string ACpass, string lsFkey, string userName, string pass, string pattern, string serial) {
            this.publishInvFkeyAsync(Account, ACpass, lsFkey, userName, pass, pattern, serial, null);
        }
        
        /// <remarks/>
        public void publishInvFkeyAsync(string Account, string ACpass, string lsFkey, string userName, string pass, string pattern, string serial, object userState) {
            if ((this.publishInvFkeyOperationCompleted == null)) {
                this.publishInvFkeyOperationCompleted = new System.Threading.SendOrPostCallback(this.OnpublishInvFkeyOperationCompleted);
            }
            this.InvokeAsync("publishInvFkey", new object[] {
                        Account,
                        ACpass,
                        lsFkey,
                        userName,
                        pass,
                        pattern,
                        serial}, this.publishInvFkeyOperationCompleted, userState);
        }
        
        private void OnpublishInvFkeyOperationCompleted(object arg) {
            if ((this.publishInvFkeyCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.publishInvFkeyCompleted(this, new publishInvFkeyCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/deleteInvFkey", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string deleteInvFkey(string lsFkey, string Account, string ACpass, string userName, string pass, string pattern) {
            object[] results = this.Invoke("deleteInvFkey", new object[] {
                        lsFkey,
                        Account,
                        ACpass,
                        userName,
                        pass,
                        pattern});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void deleteInvFkeyAsync(string lsFkey, string Account, string ACpass, string userName, string pass, string pattern) {
            this.deleteInvFkeyAsync(lsFkey, Account, ACpass, userName, pass, pattern, null);
        }
        
        /// <remarks/>
        public void deleteInvFkeyAsync(string lsFkey, string Account, string ACpass, string userName, string pass, string pattern, object userState) {
            if ((this.deleteInvFkeyOperationCompleted == null)) {
                this.deleteInvFkeyOperationCompleted = new System.Threading.SendOrPostCallback(this.OndeleteInvFkeyOperationCompleted);
            }
            this.InvokeAsync("deleteInvFkey", new object[] {
                        lsFkey,
                        Account,
                        ACpass,
                        userName,
                        pass,
                        pattern}, this.deleteInvFkeyOperationCompleted, userState);
        }
        
        private void OndeleteInvFkeyOperationCompleted(object arg) {
            if ((this.deleteInvFkeyCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.deleteInvFkeyCompleted(this, new deleteInvFkeyCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1590.0")]
    public delegate void ImportAndPublishInvCompletedEventHandler(object sender, ImportAndPublishInvCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1590.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ImportAndPublishInvCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ImportAndPublishInvCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1590.0")]
    public delegate void ImportInvPatternCompletedEventHandler(object sender, ImportInvPatternCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1590.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ImportInvPatternCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ImportInvPatternCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1590.0")]
    public delegate void ImportInvCompletedEventHandler(object sender, ImportInvCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1590.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ImportInvCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ImportInvCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1590.0")]
    public delegate void publishInvCompletedEventHandler(object sender, publishInvCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1590.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class publishInvCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal publishInvCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1590.0")]
    public delegate void UpdateCusCompletedEventHandler(object sender, UpdateCusCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1590.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class UpdateCusCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal UpdateCusCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public int Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1590.0")]
    public delegate void setCusCertCompletedEventHandler(object sender, setCusCertCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1590.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class setCusCertCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal setCusCertCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public int Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1590.0")]
    public delegate void ImportInvByPatternSerialCompletedEventHandler(object sender, ImportInvByPatternSerialCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1590.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ImportInvByPatternSerialCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ImportInvByPatternSerialCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1590.0")]
    public delegate void publishInvFkeyCompletedEventHandler(object sender, publishInvFkeyCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1590.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class publishInvFkeyCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal publishInvFkeyCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1590.0")]
    public delegate void deleteInvFkeyCompletedEventHandler(object sender, deleteInvFkeyCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1590.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class deleteInvFkeyCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal deleteInvFkeyCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591