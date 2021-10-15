using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Outlook = Microsoft.Office.Interop.Outlook;
using Office = Microsoft.Office.Core;

namespace DSM
{
    public partial class ThisAddIn
    {
        private Outlook.Inspectors inspectors;
        internal bool delaySingleEmail;

        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            inspectors = this.Application.Inspectors;
            Application.ItemSend += Application_ItemSend;
            inspectors.NewInspector += Inspectors_NewInspector;
        }

        /// <summary>
        /// When a user Creates a new email, check if DSM is enabled.
        /// If it is, show the warning
        /// </summary>
        /// <param name="Inspector"></param>
        private void Inspectors_NewInspector(Outlook.Inspector Inspector)
        {
            if (Inspector.CurrentItem is Outlook.MailItem mailItem)
            {
                if (mailItem != null)
                {
                    if (Properties.Settings.Default.EnableDSM)
                    {
                        var warning = new WarningTaskPane();
                        //TODO: test that the taskpane appears in the Compose window.
                        var taskPane = this.CustomTaskPanes.Add(warning, "Warning", Inspector);
                        taskPane.Visible = true;
                    }
                }
            }
        }

        private void Application_ItemSend(object Item, ref bool Cancel)
        {
            //Here, we defer the send date 
            if (Item is Outlook.MailItem mailItem)
            {
                //Since we have different settings for the toggle button and the single email delay,
                //we have to set the delay time appropriately.
                //Because the single email delay can be set after the toggle is turned on, it should
                //override the toggle send date time
                if (delaySingleEmail)
                {
                    mailItem.DeferredDeliveryTime = Properties.Settings.Default.SendDateTime;
                }
                else if (Properties.Settings.Default.EnableDSM)
                {
                    mailItem.DeferredDeliveryTime = Properties.Settings.Default.ToggleSendDateTime;
                }
            }

            //reset the single email flag
            delaySingleEmail = false;
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
            // Note: Outlook no longer raises this event. If you have code that 
            //    must run when Outlook shuts down, see https://go.microsoft.com/fwlink/?LinkId=506785
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }
        
        #endregion
    }
}
