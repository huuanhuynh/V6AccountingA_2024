﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace V6ThuePostXmlApi.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "12.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("https://heung-aadmin.vnpt-invoice.com.vn/PublishService.asmx")]
        public string V6ThuePostApi_nuocbdgservice_PublishService {
            get {
                return ((string)(this["V6ThuePostApi_nuocbdgservice_PublishService"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("https://heung-aadmin.vnpt-invoice.com.vn/PortalService.asmx")]
        public string V6ThuePostApi_PortalService_PortalService {
            get {
                return ((string)(this["V6ThuePostApi_PortalService_PortalService"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("https://heung-aadmin.vnpt-invoice.com.vn/BusinessService.asmx")]
        public string V6ThuePostApi_BusinessService_BusinessService {
            get {
                return ((string)(this["V6ThuePostApi_BusinessService_BusinessService"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("https://nuocbdgservicedemo.vnpt-invoice.com.vn/AttachmentService.asmx")]
        public string V6ThuePostXmlApi_V6ThuePostXmlApiS_AttachmentService {
            get {
                return ((string)(this["V6ThuePostXmlApi_V6ThuePostXmlApiS_AttachmentService"]));
            }
        }
    }
}
