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
        private bool _toggle;
        /// <summary>
        /// if toggle is true, we set the settings for the DSM toggle
        /// </summary>
        /// <param name="toggle"></param>
        public DSMSettings(bool toggle)
        {
            _toggle = toggle;

            InitializeComponent();

            //When setting the DSM toggle date, we don't want the Send Later button
            if (_toggle)
            {
                btnSave.Enabled = false;
                btnSave.Visible = false;
            }
        }

        public DateTime SendDateTime => datePicker.Value.Date + timePicker.Value.TimeOfDay;

        private void DSMSettings_OnLoad(object sender, EventArgs e)
        {
            //Populate the fields on load
            datePicker.Value = Properties.Settings.Default.SendDateTime.Date;
            timePicker.Value = Properties.Settings.Default.SendDateTime;
        }

        /// <summary>
        /// Send Later button event. Saves settings entered and sends the email.
        /// </summary>
        /// <remarks>Note: This sends an email and triggers the Application.ItemSend event listener</remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            //combine both DateTimePicker values to get the Date and Time in a single variable...
            DateTime sendTime = datePicker.Value.Date + timePicker.Value.TimeOfDay;

            Properties.Settings.Default.SendDateTime = sendTime;
            Properties.Settings.Default.Save();

            //Get current MailItem
            var inspector = Globals.ThisAddIn.Application.ActiveInspector();

            var mailItem = (MailItem)inspector.CurrentItem;

            //We don't need to defer the send time here, it'll be done in the Application.ItemSend 
            if (!_toggle)
            {
                Globals.ThisAddIn.delaySingleEmail = true;
            }

            //Note: Errors might be thrown here if the email is invalid (ie. no recipient)
            mailItem.Send();

            this.Close();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// OK button event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.EnableDSM = _toggle;
            Properties.Settings.Default.SendDateTime = datePicker.Value.Date + timePicker.Value.TimeOfDay;
            Properties.Settings.Default.Save();

            this.Close();
        }
    }
}
