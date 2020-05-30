using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace AdventureLanguage.Data
{
    public class EventLog
    {
        private readonly string _event;


        public EventLog(string eventText)
        {
            _event = eventText;
            Console.WriteLine(eventText);

        }

        public EventLog()
        {
            _event = "";
            Console.WriteLine("");

        }

        public EventLog(System.Xml.Linq.XElement xe)
        {
            _event = xe.ToString();
            Console.WriteLine(xe);
        }

        public string EventItem()
        {
            return _event;
        }
    }
}
