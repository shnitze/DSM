using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Outlook = Microsoft.Office.Interop.Outlook;
using Office = Microsoft.Office.Core;

namespace DSM
{
    public partial class ThisAddIn
    {
        private Outlook.Inspectors inspectors;

        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            this.Application.ItemSend += DeferSend_ItemSend;

            Properties.Settings.Default.SettingChanging += Default_SettingChanging;
            
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
            // Note: Outlook no longer raises this event. If you have code that 
            //    must run when Outlook shuts down, see https://go.microsoft.com/fwlink/?LinkId=506785
        }

        private void Default_SettingChanging(object sender, System.Configuration.SettingChangingEventArgs e)
        {
            switch (e.SettingName)
            {
                case "EnableDSM":
                    Globals.Ribbons.DSMRibbon.Enabled = (bool)e.NewValue;
                    break;
            }
        }

        private void DeferSend_ItemSend(object item, ref bool cancel)
        {
            DateTime sendTime = GetSendTime();
            if (item is Outlook.MailItem mailItem)
            {

                if (Properties.Settings.Default.EnableDSM)
                {

                    mailItem.DeferredDeliveryTime = NextBusinessDay();
                }
            }
        }

        public DateTime GetSendTime()
        {
            string time = Properties.Settings.Default.Time;
            Day day = (Day)Enum.Parse(typeof(Day), Properties.Settings.Default.Day);

            DateTime sendTime = DateTime.Now;
            sendTime.

            switch (day)
            {
                case Day.Day:
                    //TODO: add check if Now < sendtime
                    sendTime.AddDays(1);
                    break;
                case Day.WeekDay:
                    sendTime = sendTime.ToNextWeekDay();
                    break;
            }
        }

        public DateTime NextBusinessDay()
        {
            var date = DateTime.Now;

            return date;
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }
        
        #endregion
    }
}
