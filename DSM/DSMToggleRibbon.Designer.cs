
namespace DSM
{
    partial class DSMToggleRibbon : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public DSMToggleRibbon()
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DSMToggleRibbon));
            this.tab1 = this.Factory.CreateRibbonTab();
            this.grpDSM = this.Factory.CreateRibbonGroup();
            this.btnToggleDSM = this.Factory.CreateRibbonButton();
            this.tab1.SuspendLayout();
            this.grpDSM.SuspendLayout();
            this.SuspendLayout();
            // 
            // tab1
            // 
            this.tab1.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.tab1.ControlId.OfficeId = "TabMail";
            this.tab1.Groups.Add(this.grpDSM);
            resources.ApplyResources(this.tab1, "tab1");
            this.tab1.Name = "tab1";
            // 
            // grpDSM
            // 
            this.grpDSM.Items.Add(this.btnToggleDSM);
            resources.ApplyResources(this.grpDSM, "grpDSM");
            this.grpDSM.Name = "grpDSM";
            // 
            // btnToggleDSM
            // 
            this.btnToggleDSM.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            resources.ApplyResources(this.btnToggleDSM, "btnToggleDSM");
            this.btnToggleDSM.Image = global::DSM.Properties.Resources.delaySendIcon;
            this.btnToggleDSM.Name = "btnToggleDSM";
            this.btnToggleDSM.ShowImage = true;
            this.btnToggleDSM.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnToggleDSM_Click);
            // 
            // DSMToggleRibbon
            // 
            this.Name = "DSMToggleRibbon";
            this.RibbonType = "Microsoft.Outlook.Explorer";
            this.Tabs.Add(this.tab1);
            resources.ApplyResources(this, "$this");
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.DSMToggleRibbon_Load);
            this.tab1.ResumeLayout(false);
            this.tab1.PerformLayout();
            this.grpDSM.ResumeLayout(false);
            this.grpDSM.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tab1;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup grpDSM;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnToggleDSM;
    }

    partial class ThisRibbonCollection
    {
        internal DSMToggleRibbon DSMToggleRibbon
        {
            get { return this.GetRibbon<DSMToggleRibbon>(); }
        }
    }
}
