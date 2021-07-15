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
            //Save settings
            Properties.Settings.Default.EnableDSM = chkEnabled.Checked;
            Properties.Settings.Default.Time = cbxTime.Text;
            Properties.Settings.Default.Day = cbxDay.Text;

            this.Close();
        }
    }
}
