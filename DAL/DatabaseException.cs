using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DatabaseException : ApplicationException
    {
       

        public DatabaseException( string message):base(message)
        {
            if (!EventLog.SourceExists("Northwind Application"))
            {

                EventLog.CreateEventSource("Northwind Application", "DAL Sample");
            }

            EventLog myLog = new EventLog();
            myLog.Source = "Northwind Application";
            myLog.WriteEntry(message, EventLogEntryType.Error);

        }
    }
}
