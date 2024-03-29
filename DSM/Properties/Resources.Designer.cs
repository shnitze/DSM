﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DSM.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("DSM.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Please select a valid date and time greater than the current date and time..
        /// </summary>
        internal static string dateSelectError {
            get {
                return ResourceManager.GetString("dateSelectError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Icon similar to (Icon).
        /// </summary>
        internal static System.Drawing.Icon delaySend {
            get {
                object obj = ResourceManager.GetObject("delaySend", resourceCulture);
                return ((System.Drawing.Icon)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap delaySendIcon {
            get {
                object obj = ResourceManager.GetObject("delaySendIcon", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Disable Delay Send Mode.
        /// </summary>
        internal static string disableDSM {
            get {
                return ResourceManager.GetString("disableDSM", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap disableDSMIcon {
            get {
                object obj = ResourceManager.GetObject("disableDSMIcon", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Configure the send date..
        /// </summary>
        internal static string dsmSingleEmailTip {
            get {
                return ResourceManager.GetString("dsmSingleEmailTip", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Delay Send Mode.
        /// </summary>
        internal static string dsmTitle {
            get {
                return ResourceManager.GetString("dsmTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Enable Delay Send Mode.
        /// </summary>
        internal static string enableDSM {
            get {
                return ResourceManager.GetString("enableDSM", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Error.
        /// </summary>
        internal static string error {
            get {
                return ResourceManager.GetString("error", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to This email will be sent at {0} and will be moved to the Outbox folder until sending.
        ///
        ///Note: Outlook must be open and connected at the time of sending.
        ///
        ///Do you want to continue?.
        /// </summary>
        internal static string sendDialog {
            get {
                return ResourceManager.GetString("sendDialog", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Warning.
        /// </summary>
        internal static string warning {
            get {
                return ResourceManager.GetString("warning", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Delay Send Mode is enabled. This email will be sent at {0}. Outlook must be open and connected to the network at the time of sending..
        /// </summary>
        internal static string warningMessage {
            get {
                return ResourceManager.GetString("warningMessage", resourceCulture);
            }
        }
    }
}
