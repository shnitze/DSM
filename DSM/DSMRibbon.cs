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

            if (frmSettings.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //The user set a new send time... we need to make the disable button visible again...
                tglDisable.Visible = true;

                //We also need to reset the disable flag in the InspectorWrapper
                var inspector = (Inspector)e.Control.Context;
                var wrapper = Globals.ThisAddIn.InspectorWrappers[inspector];

                wrapper.Disable = false;
            }
        }

        private void tglDisable_Click(object sender, RibbonControlEventArgs e)
        {
            var inspector = (Inspector)e.Control.Context;
            var wrapper = Globals.ThisAddIn.InspectorWrappers[inspector];
            
            wrapper.Disable = true;
            tglDisable.Visible = false;
            
        }
    }
}
