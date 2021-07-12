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
            
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
            // Note: Outlook no longer raises this event. If you have code that 
            //    must run when Outlook shuts down, see https://go.microsoft.com/fwlink/?LinkId=506785
        }

        /// <summary>
        /// Event triggered when an email is being sent 
        /// <br /><br />
        /// If DSM is enabled, the email is defered to the configured time/date.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="cancel"></param>
        private void DeferSend_ItemSend(object item, ref bool cancel)
        {
            if (Properties.Settings.Default.EnableDSM)
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
        }

        /// <summary>
        /// Returns a DateTime following the configured Time/Day
        /// </summary>
        /// <returns></returns>
        public DateTime GetSendTime()
        {
            string time = Properties.Settings.Default.Time;
            Day day = (Day)Enum.Parse(typeof(Day), Properties.Settings.Default.Day);

            DateTime sendTime = DateTime.Now;

            sendTime = sendTime.SetTime(time);

            switch (day)
            {
                case Day.Day:
                    //TODO: add check if Now < sendtime
                    sendTime.AddDays(1);
                    break;
                case Day.WeekDay:
                    sendTime = sendTime.ToNextWeekDay();
                    break;

                case Day.Monday:
                case Day.Tuesday:
                case Day.Wednesday:
                case Day.Thursday:
                case Day.Friday:
                case Day.Saturday:
                case Day.Sunday:
                    sendTime.ToNextDayOfWeek(day);
                    break;
            }

            return sendTime;
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
