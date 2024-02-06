using Microsoft.Office.Interop.Outlook;
using Microsoft.Office.Tools.Ribbon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace DSM
{
    public partial class DSMRibbon
    {
        private void DSMRibbon_Load(object sender, RibbonUIEventArgs e)
        {
            //We should only have the disable button if DSM is enabled or if Deferred is enabled
            //var wrapper = Globals.ThisAddIn.InspectorWrappers[Globals.ThisAddIn.Application.ActiveInspector()];

            //btnDisable.Visible = Properties.Settings.Default.EnableDSM || wrapper.SendDateTime != default;

            //Add tool tips for the buttons...
            btnDSMSettings.ScreenTip = Properties.Resources.dsmTitle;
            btnDSMSettings.SuperTip = Properties.Resources.dsmSingleEmailTip;

            btnDisable.ScreenTip = Properties.Resources.dsmTitle;
            btnDisable.SuperTip = Properties.Resources.disableDSM;

            var inspector = this.Context as Inspector;
            
            if (Globals.ThisAddIn.InspectorWrappers.TryGetValue(inspector, out InspectorWrapper value))
            {
                btnDisable.Visible = Properties.Settings.Default.EnableDSM || value.SendDateTime != default;
            }
            else
            {
                btnDisable.Visible = Properties.Settings.Default.EnableDSM;
            }

            
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
            wrapper.DelaySingleEmail = false;

            if (inspector.CurrentItem is MailItem mailItem
                && mailItem.UserProperties.Find("DSM", true) != null)
            {
                mailItem.UserProperties["DSM"].Value = false;
            }

            btnDisable.Visible = false;
            
        }
    }
}
