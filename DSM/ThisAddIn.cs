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
        private DateTime sendDateTime;

        public InspectorWrapper(Outlook.Inspector inspector)
        {
            this.inspector = inspector;
            ((Outlook.InspectorEvents_Event)this.inspector).Close += new Outlook.InspectorEvents_CloseEventHandler(InspectorWrapper_Close);

            var userControl = new WarningUserControl();
            userControl.ClientSizeChanged += UserControl_SizeChanged;

            taskPane = Globals.ThisAddIn.CustomTaskPanes.Add(new WarningUserControl(), "Warning", inspector);
            taskPane.DockPosition = Office.MsoCTPDockPosition.msoCTPDockPositionTop;
            taskPane.Height = 80;

            //We should only make it visible if DSM is enabled...
            //only check for the toggle since the single email isn't initialized yet...
            if (Properties.Settings.Default.EnableDSM)
            {
                taskPane.Visible = true;
            }
        }

        private void UserControl_SizeChanged(object sender, EventArgs e)
        {
            //This can cause an exception when the component is initialized
            //For now, absorb the exception
            try
            {
                //we're really only concerned with the height...
                if (taskPane.DockPosition == Office.MsoCTPDockPosition.msoCTPDockPositionTop
                    && taskPane.Height != 80)
                {
                    //if the user is dragging the taskPane, cancel it...
                    SendKeys.Send("{ESC}");
                    //Set it's height back to original
                    taskPane.Height = 80;
                }
            }
            catch (Exception)
            {
            }
        }

        private void TaskPane_VisibleChanged(object sender, EventArgs e)
        {
            //this may not be needed
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

                //Look in the InspectorWrapper to see if we have a single email DSM or disable[TODO]
                var inspectorWrapper = InspectorWrappers[mailItem.GetInspector];
                if (inspectorWrapper != null)
                {
                    if (inspectorWrapper.DelaySingleEmail)
                    {
                        sendDateTime = inspectorWrapper.SendDateTime;
                    }
                }

                if (sendDateTime != DateTime.MinValue)
                {
                    if (MessageBox.Show($"This email will be sent at {sendDateTime} and will be moved to the Outbox folder until sending. Do you want to continue?", "Delay Send Warning", MessageBoxButtons.YesNo) == DialogResult.Yes)
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
