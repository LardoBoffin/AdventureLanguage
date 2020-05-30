using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using AdventureLanguage.Data;

namespace AdventureLanguage.Output
{
    public static class WriteEventLog
    {

        public static void WriteLogToFile(DataItems gameData)
        {
            //step through the events and write to file

            try
            {

                StreamWriter eventLogWriter = new StreamWriter(File.Open(gameData.folderLocation + gameData.folderDivider + "EventLog.txt", FileMode.Create));

                try
                {
                    foreach (EventLog eventItem in gameData.eventList)
                    {
                        eventLogWriter.WriteLine(eventItem.EventItem());
                    }

                    eventLogWriter.Dispose();
                }
                catch
                {
                    eventLogWriter.Dispose();
                }
            }
            catch
            {

            }

        }
    }
}
