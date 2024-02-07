using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
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
            InitializeComponent();
            //On load the message should display the Toggle send DateTime
            if (Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName.Equals("fr"))
            {
                lblWarningMessage.Text = string.Format(Properties.Resources.warningMessage, Properties.Settings.Default.ToggleSendDateTime.ToString("dd/MM/yyyy HH:mm"));
            }
            else
            {
                lblWarningMessage.Text = string.Format(Properties.Resources.warningMessage, Properties.Settings.Default.ToggleSendDateTime.ToString("dd/MM/yyyy hh:mm tt"));
            }
        }

        public void UpdateDateTime(DateTime dateTime)
        {
            if (Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName.Equals("fr"))
            {
                WarningMessage = string.Format(Properties.Resources.warningMessage, dateTime.ToString("dd/MM/yyyy H:mm"));
            }
            else
            {
                WarningMessage = string.Format(Properties.Resources.warningMessage, dateTime.ToString("dd/MM/yyyy h:mm tt"));
            }
            Update();
        }
    }
}
