using Microsoft.Office.Interop.Outlook;
using Microsoft.Office.Tools.Ribbon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DSM
{
    public partial class DSMToggleRibbon
    {
        private void DSMToggleRibbon_Load(object sender, RibbonUIEventArgs e)
        {
            if (Properties.Settings.Default.EnableDSM)
            {
                btnToggleDSM.Label = Properties.Resources.disableDSM;
                btnToggleDSM.Image = Properties.Resources.disableDSMIcon;
            }
            else
            {
                btnToggleDSM.Label = Properties.Resources.enableDSM;
                btnToggleDSM.Image = Properties.Resources.delaySendIcon;
            }
        }

        /// <summary>
        /// Toggle the Delay Send Mode for all emails...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnToggleDSM_Click(object sender, RibbonControlEventArgs e)
        {
            //Toggle is on, we need to disable it...
            if (Properties.Settings.Default.EnableDSM)
            {
                Properties.Settings.Default.EnableDSM = false;
                Properties.Settings.Default.Save();

                btnToggleDSM.Label = Properties.Resources.enableDSM;
                btnToggleDSM.Image = Properties.Resources.delaySendIcon;
            }
            else
            {
                //Open settings window to configure
                var settings = new DSMSettings(true);

                if (settings.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Properties.Settings.Default.EnableDSM = true;
                    Properties.Settings.Default.ToggleSendDateTime = settings.SendDateTime;
                    Properties.Settings.Default.Save();

                    //We should also update the UI so the user knows the addin is enabled...
                    btnToggleDSM.Label = Properties.Resources.disableDSM;
                    btnToggleDSM.Image = Properties.Resources.disableDSMIcon;

                    //If there are any Inspectors open, we need to update it...
                    var wrappers = Globals.ThisAddIn.InspectorWrappers;
                    foreach(var inspector in wrappers.Keys)
                    {
                        //If DSM is fully disabled for this inspector, change the SendDateTime
                        if (!wrappers[inspector].DelaySingleEmail)
                        {
                            wrappers[inspector].SendDateTime = settings.SendDateTime;
                            wrappers[inspector].Disable = false;

                            //we also need to enable the disable button
                            foreach (var ribbon in Globals.Ribbons)
                            {
                                if (ribbon is DSMRibbon dsmRibbon)
                                {
                                    var ribbonInspector = (Inspector)dsmRibbon.Context;
                                    if (ribbonInspector.CurrentItem.Equals(inspector.CurrentItem))
                                    {
                                        dsmRibbon.btnDisable.Enabled = true;
                                        
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }

        }
    }
}
