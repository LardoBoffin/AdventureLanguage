using System;
using System.Collections.Generic;
using System.Text;

namespace AdventureLanguage.Data
{
    public class Verb
    {
        private readonly string iDString;

        private readonly int iDNumber;

        private readonly string verbText;

        public Verb(string idString, int idNumber, string text)
        {
            iDString = idString;
            iDNumber = idNumber;
            verbText = text;
        }

        public string IDString()
        {
            return iDString;
        }

        public int IDNumber()
        {
            return iDNumber;
        }

        public string VerbText()
        {
            return verbText;
        }
    }
}
