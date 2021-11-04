using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Office = Microsoft.Office.Core;


namespace DSM
{
    public partial class WarningUserControl : UserControl
    {
        public string WarningMessage 
        { 
            get
            {
                return lblWarningMessage.Text;
            }
            set
            {
                lblWarningMessage.Text = value;
            }
        }

        public WarningUserControl()
        {
            //TODO: Get string from resources...
            //TODO: 
            InitializeComponent();
            //On load the message should display the Toggle send DateTime
            lblWarningMessage.Text = string.Format(Properties.Resources.warningMessage, Properties.Settings.Default.ToggleSendDateTime.ToString("dd/MM/yyyy hh:mm tt"));
        }

        public void UpdateDateTime(DateTime dateTime)
        {
            WarningMessage = string.Format(Properties.Resources.warningMessage, dateTime.ToString("dd/MM/yyyy hh:mm tt"));
            Update();
        }

        private void WarningTaskPane_SizeChanged(object sender, EventArgs e)
        {
            //This can cause an exception when the component is initialized
            //For now, absorb the exception
            try
            {
                var inspector = Globals.ThisAddIn.Application.ActiveInspector();
                var wrapper = Globals.ThisAddIn.InspectorWrappers[inspector];
                //we're really only concerned with the height...
                if (wrapper.CustomTaskPane.DockPosition == Office.MsoCTPDockPosition.msoCTPDockPositionTop
                    && wrapper.CustomTaskPane.Height != 80)
                {
                    //if the user is dragging the taskPane, cancel it...
                    SendKeys.Send("{ESC}");
                    //Set it's height back to original
                    wrapper.CustomTaskPane.Height = 80;
                }
            }
            catch (Exception)
            {
            }
        }

        private void btnDismiss_Click(object sender, EventArgs e)
        {
            //To remove the taskpane we have to remove it from the CustomTaskPanes
            //For now we only have one taskpane so we just remove the first one in 
            //the list... This may be an issue if we have more than one task pane
            Globals.ThisAddIn.CustomTaskPanes.RemoveAt(0);
        }
    }
}
