using System;
using System.Collections.Generic;
using System.Text;

namespace AdventureLanguage.Data
{
    public class Adverb
    {
        private readonly string iDString;

        private readonly int iDNumber;

        private readonly string adverbText;

        public Adverb(string idString, int idNumber, string text)
        {
            iDString = idString;
            iDNumber = idNumber;
            adverbText = text;
        }

        public string IDString()
        {
            return iDString;
        }

        public int IDNumber()
        {
            return iDNumber;
        }

        public string AdverbText()
        {
            return adverbText;
        }
    }
}
