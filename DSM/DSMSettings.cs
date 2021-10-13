using Microsoft.Office.Interop.Outlook;
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
    public partial class DSMSettings : Form
    {
        public DSMSettings()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.EnableDSM = chkEnabled.Checked;
            Properties.Settings.Default.SendDateTime = dateTimePicker1.Value;

            DateTime sendTime = dateTimePicker1.Value;

            //Get current MailItem
            var inspector = Globals.ThisAddIn.Application.ActiveInspector();

            var mailItem = (MailItem)inspector.CurrentItem;

            mailItem.DeferredDeliveryTime = sendTime;

            mailItem.Send();

            this.Close();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
