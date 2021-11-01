﻿using Microsoft.Office.Interop.Outlook;
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
            //if (Properties.Settings.Default.EnableDSM)
            //{
            //    var warning = new WarningTaskPane();
            //    Globals.ThisAddIn.CustomTaskPanes.Add(warning, "Warning");
            //}
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

            if (wrapper.Disable)
            {
                wrapper.Disable = false;
                tglDisable.Checked = false;
                tglDisable.Label = Properties.Resources.disableDSM;
            }
            else
            {
                wrapper.Disable = true;
                tglDisable.Checked = true;
                tglDisable.Label = Properties.Resources.enableDSM;
            }
        }
    }
}
