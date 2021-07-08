using Microsoft.Office.Tools.Ribbon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DSM
{
    public enum Day
    {
        Day,
        WeekDay,
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday
    }

    public partial class DSMRibbon
    {
        private void DSMRibbon_Load(object sender, RibbonUIEventArgs e)
        {
            Enabled = Properties.Settings.Default.EnableDSM;
        }

        //Don't think we'll really need this since we can just fetch the values in the .settings file...
        //TODO: Remove?
        public bool Enabled { get; set; }

        private void btnDSMSettings_Click(object sender, RibbonControlEventArgs e)
        {
            var frmSettings = new DSMSettings();

            if (frmSettings.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

            }
        }
    }
}
