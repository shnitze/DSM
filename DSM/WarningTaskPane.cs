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
    public partial class WarningTaskPane : UserControl
    {
        public WarningTaskPane()
        {
            //TODO: Get string from resources...
            //TODO: 
            InitializeComponent();
            this.SizeChanged += WarningTaskPane_SizeChanged;
        }

        private void WarningTaskPane_SizeChanged(object sender, EventArgs e)
        {
            //we're really only concerned with the height...
            if (Globals.ThisAddIn.warningTaskPane.DockPosition == Microsoft.Office.Core.MsoCTPDockPosition.msoCTPDockPositionTop 
                && Globals.ThisAddIn.warningTaskPane.Height != 80)
            {
                //if the user is dragging the taskPane, cancel it...
                SendKeys.Send("{ESC}");
                //Set it's height back to original
                Globals.ThisAddIn.warningTaskPane.Height = 80;
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
