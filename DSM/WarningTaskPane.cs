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
            InitializeComponent();
        }

        private void btnDismiss_Click(object sender, EventArgs e)
        {
            Visible = false;
        }
    }
}
