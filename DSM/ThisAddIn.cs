using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Outlook = Microsoft.Office.Interop.Outlook;
using Office = Microsoft.Office.Core;
using System.Windows.Forms;
using Microsoft.Office.Tools;

namespace DSM
{
    public class InspectorWrapper
    {
        private Outlook.Inspector inspector;
        private CustomTaskPane taskPane;
        private bool delaySingleEmail;
        private bool disableDSM;
        private DateTime sendDateTime;

        public InspectorWrapper(Outlook.Inspector inspector)
        {
            this.inspector = inspector;
            ((Outlook.InspectorEvents_Event)this.inspector).Close += new Outlook.InspectorEvents_CloseEventHandler(InspectorWrapper_Close);

            var userControl = new WarningUserControl();

            taskPane = Globals.ThisAddIn.CustomTaskPanes.Add(new WarningUserControl(), Properties.Resources.warning, inspector);
            taskPane.DockPosition = Office.MsoCTPDockPosition.msoCTPDockPositionTop;
            taskPane.Height = 80;

            //We should only make it visible if DSM is enabled...
            //only check for the toggle since the single email isn't initialized yet...
            if (Properties.Settings.Default.EnableDSM)
            {
                taskPane.Visible = true;
            }
        }

        private void InspectorWrapper_Close()
        {
            if (taskPane != null)
            {
                Globals.ThisAddIn.CustomTaskPanes.Remove(taskPane);
            }

            taskPane = null;
            Globals.ThisAddIn.InspectorWrappers.Remove(inspector);
            ((Outlook.InspectorEvents_Event)this.inspector).Close -= new Outlook.InspectorEvents_CloseEventHandler(InspectorWrapper_Close);
        }

        public CustomTaskPane CustomTaskPane => taskPane;
        public bool DelaySingleEmail
        {
            get => delaySingleEmail;
            set
            {
                delaySingleEmail = value;
                if (!taskPane.Visible)
                {
                    taskPane.Visible = true;
                }
            }
        }
        public DateTime SendDateTime
        {
            get => sendDateTime;
            set
            {
                //If we get a new value update the taskPane at the same time...
                sendDateTime = value;
                ((WarningUserControl)taskPane.Control).UpdateDateTime(sendDateTime);
                //The toggle might be set to true... set it to false and make sure the taskpane is visible
                if (disableDSM)
                {
                    //task pane visibility is set in the Disable setter
                    Disable = false;
                }
            }
        }
        public bool Disable
        {
            get => disableDSM;
            set
            {
                disableDSM = value;
                taskPane.Visible = !disableDSM;
            }
        }

    }

    public partial class ThisAddIn
    {
        private Dictionary<Outlook.Inspector, InspectorWrapper> inspectorWrappersValue = new Dictionary<Outlook.Inspector, InspectorWrapper>();

        private Outlook.Inspectors inspectors;
        //internal bool delaySingleEmail;
        //internal CustomTaskPane warningTaskPane;
        //internal WarningTaskPane warningUserControl;

        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            inspectors = this.Application.Inspectors;
            Application.ItemSend += Application_ItemSend;
            inspectors.NewInspector += Inspectors_NewInspector;

            foreach (Outlook.Inspector inspector in inspectors)
            {
                Inspectors_NewInspector(inspector);
            }
        }

        /// <summary>
        /// When a user Creates a new email, check if DSM is enabled.
        /// If it is, show the warning
        /// </summary>
        /// <param name="Inspector"></param>
        private void Inspectors_NewInspector(Outlook.Inspector Inspector)
        {
            if (Inspector.CurrentItem is Outlook.MailItem)
            {
                inspectorWrappersValue.Add(Inspector, new InspectorWrapper(Inspector));
            }
        }

        public Dictionary<Outlook.Inspector, InspectorWrapper> InspectorWrappers => inspectorWrappersValue;

        private void Application_ItemSend(object Item, ref bool Cancel)
        {
            //Here, we defer the send date 
            if (Item is Outlook.MailItem mailItem)
            {
                DateTime sendDateTime = DateTime.MinValue;

                //Since we have different settings for the toggle button and the single email delay,
                //we have to set the delay time appropriately.
                //Because the single email delay can be set after the toggle is turned on, it should
                //override the toggle send date time

                

                if (Properties.Settings.Default.EnableDSM)
                {
                    sendDateTime = Properties.Settings.Default.ToggleSendDateTime;
                }

                //Look in the InspectorWrapper to see if we have a single email DSM or disable
                var inspectorWrapper = InspectorWrappers[mailItem.GetInspector];
                if (inspectorWrapper != null)
                {
                    if (inspectorWrapper.DelaySingleEmail)
                    {
                        sendDateTime = inspectorWrapper.SendDateTime;
                    }
                    if (inspectorWrapper.Disable)
                    {
                        //We don't need to set a deferred send time...
                        return;
                    }
                }

                if (sendDateTime != DateTime.MinValue)
                {
                    if (MessageBox.Show(string.Format(Properties.Resources.sendDialog, sendDateTime), Properties.Resources.warning, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        mailItem.DeferredDeliveryTime = sendDateTime;
                    }
                    else
                    {
                        Cancel = true;
                    }
                }
            }
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
            // Note: Outlook no longer raises this event. If you have code that 
            //    must run when Outlook shuts down, see https://go.microsoft.com/fwlink/?LinkId=506785
            inspectors.NewInspector -= new Outlook.InspectorsEvents_NewInspectorEventHandler(Inspectors_NewInspector);
            inspectors = null;
            inspectorWrappersValue = null;
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
