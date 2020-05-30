using System;
using System.Collections.Generic;
using System.Text;

namespace AdventureLanguage.Data
{
    public  class Noun
    {
        private readonly string iDString;

        private readonly int iDNumber;

        private readonly string nounText;

        public Noun(string idString, int idNumber, string text)
        {
            iDString = idString;
            iDNumber = idNumber;
            nounText = text;
        }

        public string IDString()
        {
            return iDString;
        }

        public int IDNumber()
        {
            return iDNumber;
        }

        public string NounText()
        {
            return nounText;
        }
    }
}
