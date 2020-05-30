using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AdventureLanguage.Data
{
    public class Message
    {
        private readonly string iDString;

        private readonly int iDNumber;

        private readonly string messageText;

        public Message(string idString, int idNumber, string text)
        {
            iDString = idString;
            iDNumber = idNumber;
            messageText = text;
        }

        public string IDString()
        {
            return iDString;
        }

        public int IDNumber()
        {
            return iDNumber;
        }

        public string MessageText()
        {
            return messageText;
        }

    }

}
