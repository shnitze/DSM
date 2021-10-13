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
            if (Properties.Settings.Default.EnableDSM)
            {
                var warning = new WarningTaskPane();
                Globals.ThisAddIn.CustomTaskPanes.Add(warning, "Warning");
            }
        }

        private void btnDSMSettings_Click(object sender, RibbonControlEventArgs e)
        {
            var frmSettings = new DSMSettings();

            //Only need to show the dialog as it will deal with saving the values
            frmSettings.Show();
        }
    }
}
