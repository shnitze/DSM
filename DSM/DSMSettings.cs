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
            else
            {
                lblNote.Visible = false;

                //We should check if the email is sendable before enabling the SendLater button

            }
        }

        public DateTime SendDateTime => datePicker.Value.Date + timePicker.Value.TimeOfDay;

        private void DSMSettings_OnLoad(object sender, EventArgs e)
        {
            //Populate the fields on load
            if (_toggle)
            {
                datePicker.Value = Properties.Settings.Default.ToggleSendDateTime.Date;
                timePicker.Value = Properties.Settings.Default.ToggleSendDateTime;
            }
            else
            {
                var inspector = Globals.ThisAddIn.Application.ActiveInspector();
                
                if (Globals.ThisAddIn.InspectorWrappers[inspector].SendDateTime != DateTime.MinValue)
                {
                    datePicker.Value = Globals.ThisAddIn.InspectorWrappers[inspector].SendDateTime.Date;
                    timePicker.Value = Globals.ThisAddIn.InspectorWrappers[inspector].SendDateTime;
                }
                else
                {
                    datePicker.Value = DateTime.Now.Date;
                    timePicker.Value = DateTime.Now;
                }
            }
            
        }

        /// <summary>
        /// Send Later button event. Saves settings entered and sends the email.
        /// </summary>
        /// <remarks>Note: This sends an email and triggers the Application.ItemSend event listener</remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSendLater_Click(object sender, EventArgs e)
        {
            //combine both DateTimePicker values to get the Date and Time in a single variable...
            DateTime sendTime = datePicker.Value.Date + timePicker.Value.TimeOfDay;

            //Get current MailItem
            var inspector = Globals.ThisAddIn.Application.ActiveInspector();

            var mailItem = (MailItem)inspector.CurrentItem;

            //We don't need to defer the send time here, it'll be done in the Application.ItemSend event
            if (!_toggle)
            {
                Globals.ThisAddIn.InspectorWrappers[inspector].DelaySingleEmail = true;
                Globals.ThisAddIn.InspectorWrappers[inspector].SendDateTime = sendTime;
            }

            //Note: Errors might be thrown here if the email is invalid (ie. no recipient)
            mailItem.Send();

            this.Close();
        }

        /// <summary>
        /// OK button event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (_toggle)
            {
                Properties.Settings.Default.EnableDSM = true;
                Properties.Settings.Default.ToggleSendDateTime = datePicker.Value.Date + timePicker.Value.TimeOfDay;
                Properties.Settings.Default.Save();
            }
            else
            {
                //We're deferring a single email, set the send time in the InspectorWrapper
                //Get current MailItem
                var inspector = Globals.ThisAddIn.Application.ActiveInspector();
                var wrapper = Globals.ThisAddIn.InspectorWrappers[inspector];

                wrapper.DelaySingleEmail = true;
                wrapper.SendDateTime = datePicker.Value.Date + timePicker.Value.TimeOfDay;
                //We also need to reset the disable flag in the InspectorWrapper
                wrapper.Disable = false;
                //The user set a new send time... we need to make the disable button visible again...
                Globals.Ribbons.DSMRibbon.btnDisable.Visible = true;
            }



            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// This Focus event triggers when the settings dialog is put in focus.
        /// This checks that there are recipients before enabling the Send Later button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DSMSettings_Activated(object sender, EventArgs e)
        {
            if (!_toggle)
            {
                var inspector = Globals.ThisAddIn.Application.ActiveInspector();

                if (((MailItem)inspector.CurrentItem).Recipients.Count == 0)
                {
                    btnSave.Enabled = false;
                }
                else
                {
                    btnSave.Enabled = true;
                }
            }
        }
    }
}
