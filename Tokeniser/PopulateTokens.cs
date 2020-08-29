using System;
using System.Collections.Generic;
using System.Text;

namespace AdventureLanguage.Tokeniser
{
    public static class PopulateTokens
    {
        public static bool populateTokens()
        {
            //create the list of tokens and associated flags

            TokenList.tokenList.Add(new Token(0x80, "AND", false, false, false, false, false, false, false));



            TokenList.tokenList.Add(new Token(0x80, "MODE", false, true, false, false, false, false, false));

            return false;

        }
    }
}
