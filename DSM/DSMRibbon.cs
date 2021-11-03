using Microsoft.Office.Interop.Outlook;
using Microsoft.Office.Tools.Ribbon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace DSM
{
    public partial class DSMRibbon
    {
        private void DSMRibbon_Load(object sender, RibbonUIEventArgs e)
        {
            //We should only have the disable button if DSM is enabled
            tglDisable.Visible = Properties.Settings.Default.EnableDSM;
        }

        private void btnDSMSettings_Click(object sender, RibbonControlEventArgs e)
        {
            var frmSettings = new DSMSettings(false);

            //If the user enables single email DSM, we should update the warning message with the new send time...
            frmSettings.Show();
        }

        private void tglDisable_Click(object sender, RibbonControlEventArgs e)
        {
            var inspector = (Inspector)e.Control;
            var wrapper = Globals.ThisAddIn.InspectorWrappers[inspector];
            
            wrapper.Disable = true;
            tglDisable.Visible = false;
            tglDisable.Label = Properties.Resources.enableDSM;
            
        }
    }
}
