using System;
using System.Collections.Generic;
using System.Text;

namespace AdventureLanguage.Data
{
    public class Walkthrough
    {

        private readonly int iDNumber;

        private readonly string walkthroughText;

        private readonly string commentText;

        public Walkthrough(int idNumber, string walkThroughText,string comment)
        {
            iDNumber = idNumber;
            walkthroughText = walkThroughText;
            commentText = comment;
        }

        public int IDNumber()
        {
            return iDNumber;
        }

        public string WalkthroughText()
        {
            return walkthroughText;
        }

    }
}
