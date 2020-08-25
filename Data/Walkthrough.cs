using System;
using System.Collections.Generic;
using System.Text;

namespace AdventureLanguage.Data
{
    public class Walkthrough
    {

        private readonly int iDNumber;

        private readonly string messageText;

        public Walkthrough(int idNumber, string text)
        {
            iDNumber = idNumber;
            messageText = text;
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
