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
            btnDisable.Visible = Properties.Settings.Default.EnableDSM;
        }

        private void btnDSMSettings_Click(object sender, RibbonControlEventArgs e)
        {
            //There should only be one settings dialog per inspector...
            var inspector = (Inspector)e.Control.Context;
            var wrapper = Globals.ThisAddIn.InspectorWrappers[inspector];

            var frmSettings = wrapper.DSMSettings;

            if (frmSettings.Visible)
            {
                frmSettings.Focus();
            }
            else
            {
                frmSettings.Show();
            }
        }

        private void btnDisable_Click(object sender, RibbonControlEventArgs e)
        {
            var inspector = (Inspector)e.Control.Context;
            var wrapper = Globals.ThisAddIn.InspectorWrappers[inspector];
            
            wrapper.Disable = true;
            btnDisable.Visible = false;
            
        }
    }
}
