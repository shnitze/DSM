using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            lblWarningMessage.Text = $"Delay Send Mode is enabled. This email will be sent at {Properties.Settings.Default.ToggleSendDateTime}. Outlook must be open and connected to the VPN at the time of sending.";
        }

        public void UpdateDateTime(DateTime dateTime)
        {
            WarningMessage = $"Delay Send Mode is enabled. This email will be sent at {dateTime}. Outlook must be open and connected to the VPN at the time of sending.";
            Update();
        }

        private void WarningTaskPane_SizeChanged(object sender, EventArgs e)
        {
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
