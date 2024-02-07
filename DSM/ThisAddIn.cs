using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Outlook = Microsoft.Office.Interop.Outlook;
using Office = Microsoft.Office.Core;
using System.Windows.Forms;
using Microsoft.Office.Tools;
using System.Windows.Threading;
using System.Threading;
using System.Globalization;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.Outlook;
using System.Runtime.InteropServices;

namespace DSM
{
    public class InspectorWrapper
    {
        private readonly Outlook.Inspector inspector;
        private Microsoft.Office.Tools.CustomTaskPane taskPane;
        private DSMSettings settingsDialog;
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
            taskPane.DockPositionRestrict = Office.MsoCTPDockPositionRestrict.msoCTPDockPositionRestrictNoChange;
            taskPane.VisibleChanged += TaskPane_VisibleChanged;

            taskPane.Height = userControl.Height * 2;

            //We should only make it visible if DSM is enabled...
            //only check for the toggle since the single email isn't initialized yet...
            if (Properties.Settings.Default.EnableDSM)
            {
                taskPane.Visible = true;
            }
        }

        private void TaskPane_VisibleChanged(object sender, EventArgs e)
        {
            if (!taskPane.Visible && !disableDSM)
            {
                Dispatcher.CurrentDispatcher.BeginInvoke(new System.Action(() => { taskPane.Visible = true; }));
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

            Marshal.FinalReleaseComObject(inspector);
        }

        public Microsoft.Office.Tools.CustomTaskPane CustomTaskPane => taskPane;
        public bool DelaySingleEmail
        {
            get => delaySingleEmail;
            set
            {
                delaySingleEmail = value;
                if (delaySingleEmail && !taskPane.Visible)
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

        public DSMSettings DSMSettings
        {
            get
            {
                if (settingsDialog == null || settingsDialog.IsDisposed)
                    settingsDialog = new DSMSettings(false);

                return settingsDialog;
            }
        }

    }

    public partial class ThisAddIn
    {
        private Dictionary<Outlook.Inspector, InspectorWrapper> inspectorWrappersValue = new Dictionary<Outlook.Inspector, InspectorWrapper>();

        private Outlook.Inspectors inspectors;

        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            LanguageSettings languageSettings = null;

            try
            {
                languageSettings = Application.LanguageSettings;
                
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(languageSettings.LanguageID[Office.MsoAppLanguageID.msoLanguageIDUI]);
                Thread.CurrentThread.CurrentCulture = new CultureInfo(languageSettings.LanguageID[Office.MsoAppLanguageID.msoLanguageIDUI]);
                inspectors = Application.Inspectors;
                Application.ItemSend += Application_ItemSend;
                inspectors.NewInspector += Inspectors_NewInspector;

                foreach (Outlook.Inspector inspector in inspectors)
                {
                    Inspectors_NewInspector(inspector);
                }

                //To avoid errors, we should make sure the SendDateTime in the Settings has a value
                //if not set it to today...
                if (Properties.Settings.Default.ToggleSendDateTime.Equals(default))
                {
                    Properties.Settings.Default.ToggleSendDateTime = DateTime.Now;
                    Properties.Settings.Default.Save();
                }
            }
            finally
            {
                if (languageSettings != null) Marshal.ReleaseComObject(languageSettings);
            }
        }

        protected override IRibbonExtensibility CreateRibbonExtensibilityObject()
        {
            Outlook.Application app = null; 
            LanguageSettings languageSettings = null;
            try
            {
                app = this.GetHostItem<Outlook.Application>(typeof(Outlook.Application), "Application");
                languageSettings = app.LanguageSettings;
                int lcid = languageSettings.LanguageID[Office.MsoAppLanguageID.msoLanguageIDUI];
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(lcid);

                return base.CreateRibbonExtensibilityObject();
            }
            finally
            {
                if (languageSettings != null) Marshal.ReleaseComObject(languageSettings);
                if (app != null) Marshal.ReleaseComObject(app);
            }
            
        }

        /// <summary>
        /// When a user Creates a new email, check if DSM is enabled.
        /// If it is, show the warning
        /// </summary>
        /// <param name="inspector"></param>
        private void Inspectors_NewInspector(Outlook.Inspector inspector)
        {
            MailItem mailItem = null;
            Folder folder = null;
            UserProperties userProperties = null;
            try
            {
                mailItem = inspector.CurrentItem as MailItem;
                if (mailItem != null && !mailItem.Sent)
                {
                    var wrapper = new InspectorWrapper(inspector);

                    folder = mailItem.Parent as Folder;

                    if (folder != null)
                    {

                        userProperties = mailItem.UserProperties;
                        //1/1/4501 12:00PM is the default DateTIme in Outlook...
                        if (mailItem.DeferredDeliveryTime != new DateTime(4501, 1, 1))
                        {
                            //The email is being defferred... check if DSM is responsible
                            if (userProperties.Find("DSM", true) != null && userProperties["DSM"].Value)
                            {
                                wrapper.DelaySingleEmail = true;
                                wrapper.SendDateTime = mailItem.DeferredDeliveryTime;

                                foreach (var ribbon in Globals.Ribbons)
                                {
                                    if (ribbon is DSMRibbon dsmRibbon && dsmRibbon.Context == inspector)
                                    {
                                        dsmRibbon.btnDisable.Visible = true;
                                        break;
                                    }
                                }
                            }
                        }
                        else if (Properties.Settings.Default.EnableDSM)
                        {
                            foreach (var ribbon in Globals.Ribbons)
                            {
                                if (ribbon is DSMRibbon dsmRibbon && dsmRibbon.Context == inspector)
                                {
                                    userProperties.Add("DSM", OlUserPropertyType.olYesNo, false, 1);
                                    userProperties["DSM"].Value = true;
                                    dsmRibbon.btnDisable.Visible = Properties.Settings.Default.EnableDSM;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            foreach (var ribbon in Globals.Ribbons)
                            {
                                if (ribbon is DSMRibbon dsmRibbon && dsmRibbon.Context == inspector)
                                {
                                    dsmRibbon.btnDisable.Visible = false;
                                    break;
                                }
                            }
                        }

                    }

                    inspectorWrappersValue.Add(inspector, wrapper);
                }
            }
            finally
            {
                if (userProperties != null) Marshal.ReleaseComObject(userProperties);
                if (folder != null) Marshal.ReleaseComObject(folder);
                if (mailItem != null) Marshal.ReleaseComObject(mailItem);
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

                if (sendDateTime != DateTime.MinValue && Properties.Settings.Default.WarningMessage)
                {
                    if (MessageBox.Show(string.Format(Properties.Resources.sendDialog, sendDateTime.ToString("dd/MM/yyyy hh:mm tt")), Properties.Resources.warning, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        mailItem.DeferredDeliveryTime = sendDateTime;
                    }
                    else
                    {
                        Cancel = true;
                    }
                }
                else if (sendDateTime != DateTime.MinValue && !Properties.Settings.Default.WarningMessage)
                {
                    mailItem.DeferredDeliveryTime = sendDateTime;
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
