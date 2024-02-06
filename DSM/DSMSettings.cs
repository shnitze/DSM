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
            datePicker.MouseDown += DatePicker_MouseDown;
            datePicker.GotFocus += DatePicker_GotFocus;
            datePicker.MinDate = DateTime.Now.Date;
            //When setting the DSM toggle date, we don't want the Send Later button
            if (!_toggle)
            {
                lblNote.Visible = false;
                this.Height = this.Height - lblNote.Height;
            }
        }

        private void DatePicker_MouseDown(object sender, MouseEventArgs e)
        {
            //Making sure the calendar button wasn't pressed...
            if (e.X < datePicker.Width - 35)
            {
                SendKeys.Send("%{DOWN}");
            }
                
        }

        //When a date is selected, the focus should move to the next Control
        private void DatePicker_ValueChanged(object sender, EventArgs e)
        {
            Control ctrl = (Control)sender;
            this.SelectNextControl(ctrl, true, false, false, false);
        }

        private void DatePicker_GotFocus(object sender, EventArgs e)
        {
            if (MouseButtons == MouseButtons.None)
                SendKeys.Send("%{DOWN}");
        }

        public DateTime SendDateTime => datePicker.Value.Date + timePicker.Value.TimeOfDay;

        private void DSMSettings_OnLoad(object sender, EventArgs e)
        {
            //Populate the fields on load
            if (_toggle)
            {
                if (Properties.Settings.Default.ToggleSendDateTime.Date > DateTime.Now)
                {
                    datePicker.Value = Properties.Settings.Default.ToggleSendDateTime.Date;
                    timePicker.Value = Properties.Settings.Default.ToggleSendDateTime;
                }
                else
                {
                    datePicker.Value = DateTime.Now.Date;
                    timePicker.Value = DateTime.Now;
                }
                
            }
            else
            {
                var inspector = Globals.ThisAddIn.Application.ActiveInspector();
                
                if (Globals.ThisAddIn.InspectorWrappers[inspector].SendDateTime != DateTime.MinValue)
                {
                    datePicker.Value = Globals.ThisAddIn.InspectorWrappers[inspector].SendDateTime.Date;
                    timePicker.Value = Globals.ThisAddIn.InspectorWrappers[inspector].SendDateTime;
                }
                else if (Properties.Settings.Default.ToggleSendDateTime > DateTime.Now)
                {
                    datePicker.Value = Properties.Settings.Default.ToggleSendDateTime.Date;
                    timePicker.Value = Properties.Settings.Default.ToggleSendDateTime;
                }
                else
                {
                    datePicker.Value = DateTime.Now.Date;
                    timePicker.Value = DateTime.Now;
                }
            }
            chkWarningMessage.Checked = Properties.Settings.Default.WarningMessage;
            datePicker.CloseUp += DatePicker_CloseUp;
        }

        private void DatePicker_CloseUp(object sender, EventArgs e)
        {
            this.SelectNextControl((Control)sender, true, true, true, true);
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
            //Before we move along with this... we need to make sure the date
            //is not in the past... We already have a MinDate for the DateTimePicker 
            //but not for the time picker
            DateTime selectedDateTime = datePicker.Value.Date + timePicker.Value.TimeOfDay;
            if (selectedDateTime < DateTime.Now)
            {
                MessageBox.Show(Properties.Resources.dateSelectError, Properties.Resources.error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
                return;
            }

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
                var mailItem = inspector.CurrentItem as MailItem;
                if (mailItem.UserProperties.Find("DSM", true) == null)
                {
                    mailItem.UserProperties.Add("DSM", OlUserPropertyType.olYesNo, false, false);
                    mailItem.UserProperties["DSM"].Value = true;
                }
                wrapper.DelaySingleEmail = true;
                wrapper.SendDateTime = datePicker.Value.Date + timePicker.Value.TimeOfDay;
                //We also need to reset the disable flag in the InspectorWrapper
                wrapper.Disable = false;
                //The user set a new send time... we need to make the disable button visible again...
                foreach (var ribbon in Globals.Ribbons)
                {
                    if (ribbon is DSMRibbon dsmRibbon)
                    {
                        //get ribbon inspector
                        var ribbonInspector = (Inspector)dsmRibbon.Context;
                        if (ribbonInspector.CurrentItem.Equals(inspector.CurrentItem))
                        {
                            dsmRibbon.btnDisable.Visible = true;
                            break;
                        }
                    }
                }
            }

            Properties.Settings.Default.WarningMessage = chkWarningMessage.Checked;
            Properties.Settings.Default.Save();

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
