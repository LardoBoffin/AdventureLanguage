using System;
using System.Collections.Generic;
using System.Text;

namespace AdventureLanguage.Tokeniser
{
    public static class Tokeniser
    {
        //takes a string of text and returns a stream of tokenised bytes

        public static byte[] tokenise(string BBCBasicLine, int lineNumber)
        {
            //MSB of line number
            //LSB of line number
            //Length of line (which needs to be adjusted when the line is shortened) 
            //Text ......
            //CR

            byte token;
            string keyword;
            bool conditional;
            bool middle;
            bool start;
            bool FNPROC;
            bool line;
            bool REM;
            bool pseudo;
            string text;
            

            byte[] tokenisedBASIC = new byte[255];
            int iBytePOS = 0;

            tokenisedBASIC[0] = (byte)(lineNumber / 256);                        //MSB of line number
            tokenisedBASIC[1] = (byte)(lineNumber - ((lineNumber / 256) * 256)); //LSB of line number

            //step through text and convert to tokens

            text = "MODE";

            foreach (Token t in TokenList.tokenList)
            {
                if (t.Keyword()==text)
                {
                    //match so check the token data

                }
            }

            tokenisedBASIC[2] = (byte)iBytePOS;                                   //Length of line (set last)
            tokenisedBASIC[iBytePOS] = (byte)0x0D;                                //CR

            return tokenisedBASIC;
        }
    }
}
