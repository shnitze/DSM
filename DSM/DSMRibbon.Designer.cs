
using System.Globalization;
using System.Threading;

namespace DSM
{
    partial class DSMRibbon : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public DSMRibbon()
            : base(Globals.Factory.GetRibbonFactory())
        {
            //For some reason, this is executed in a different thread or before the correct culture is set...
            //Let's force it to the correct culture

            InitializeComponent();
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DSMRibbon));
            this.tab1 = this.Factory.CreateRibbonTab();
            this.grpProperties = this.Factory.CreateRibbonGroup();
            this.btnDisable = this.Factory.CreateRibbonButton();
            this.btnDSMSettings = this.Factory.CreateRibbonButton();
            this.tab1.SuspendLayout();
            this.grpProperties.SuspendLayout();
            this.SuspendLayout();
            // 
            // tab1
            // 
            this.tab1.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.tab1.ControlId.OfficeId = "TabNewMailMessage";
            this.tab1.Groups.Add(this.grpProperties);
            resources.ApplyResources(this.tab1, "tab1");
            this.tab1.Name = "tab1";
            // 
            // grpProperties
            // 
            this.grpProperties.Items.Add(this.btnDisable);
            this.grpProperties.Items.Add(this.btnDSMSettings);
            resources.ApplyResources(this.grpProperties, "grpProperties");
            this.grpProperties.Name = "grpProperties";
            // 
            // btnDisable
            // 
            this.btnDisable.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnDisable.Image = global::DSM.Properties.Resources.disableDSMIcon;
            resources.ApplyResources(this.btnDisable, "btnDisable");
            this.btnDisable.Name = "btnDisable";
            this.btnDisable.ShowImage = true;
            this.btnDisable.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnDisable_Click);
            // 
            // btnDSMSettings
            // 
            this.btnDSMSettings.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnDSMSettings.Image = global::DSM.Properties.Resources.delaySendIcon;
            resources.ApplyResources(this.btnDSMSettings, "btnDSMSettings");
            this.btnDSMSettings.Name = "btnDSMSettings";
            this.btnDSMSettings.ShowImage = true;
            this.btnDSMSettings.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnDSMSettings_Click);
            // 
            // DSMRibbon
            // 
            this.Name = "DSMRibbon";
            this.RibbonType = "Microsoft.Outlook.Mail.Compose";
            this.Tabs.Add(this.tab1);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.DSMRibbon_Load);
            this.tab1.ResumeLayout(false);
            this.tab1.PerformLayout();
            this.grpProperties.ResumeLayout(false);
            this.grpProperties.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tab1;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup grpProperties;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnDSMSettings;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnDisable;
    }

    partial class ThisRibbonCollection
    {
        internal DSMRibbon DSMRibbon
        {
            get { return this.GetRibbon<DSMRibbon>(); }
        }
    }
}
