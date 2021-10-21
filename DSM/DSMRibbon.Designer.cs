
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
            this.tab1 = this.Factory.CreateRibbonTab();
            this.grpProperties = this.Factory.CreateRibbonGroup();
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
            this.tab1.Label = "TabNewMailMessage";
            this.tab1.Name = "tab1";
            // 
            // grpProperties
            // 
            this.grpProperties.Items.Add(this.btnDSMSettings);
            this.grpProperties.Label = "DSM";
            this.grpProperties.Name = "grpProperties";
            this.grpProperties.Position = this.Factory.RibbonPosition.BeforeOfficeId("DSMGroup");
            // 
            // btnDSMSettings
            // 
            this.btnDSMSettings.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnDSMSettings.Image = global::DSM.Properties.Resources.delaySendIcon;
            this.btnDSMSettings.Label = "Send Later";
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
    }

    partial class ThisRibbonCollection
    {
        internal DSMRibbon DSMRibbon
        {
            get { return this.GetRibbon<DSMRibbon>(); }
        }
    }
}
