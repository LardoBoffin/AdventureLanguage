using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Dynamic;
using AdventureLanguage.Data;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices.WindowsRuntime;

namespace AdventureLanguage.Tokeniser
{
    public static class Tokeniser
    {

        static bool conditional = false;
        static bool middle = false;
        static bool start = true;
        static bool FNPROC = false;
        static bool line = false;
        static bool REM = false;
        static bool pseudo = false;
        static byte token = 0;
        static int iBytePOS = 0;
        public static byte[] Tokenise(string BBCBasicLine, int lineNumber, Data.DataItems gameData)
        {
            //step through text and convert to tokens

            //MSB of line number
            //LSB of line number
            //Length of line (which needs to be adjusted when the line is shortened) 
            //Text ......
            //CR


            // string keyword;
            //bool isMatch = false;

            string text = "";
            string character;
            int iLenLine;
            int iCurrentChar = 1;
            char tmpChar;
            bool tokenising = false;

            byte[] tokenisedBASIC = new byte[255];
            //int iBytePOS = 0;

            try
            {

                //example line
                //  MODE 7:VDU23;8202;0;0;0;:PRINT"Loading";:T%=0:Q%=0:DIM IX% 60:DIM RM% 252

                tokenisedBASIC[0] = (byte)(lineNumber / 256);                        //MSB of line number
                tokenisedBASIC[1] = (byte)(lineNumber - ((lineNumber / 256) * 256)); //LSB of line number
                //tokenised position 2 is linelength

                //read characters until reaching control or other characters or end of line
                iLenLine = BBCBasicLine.Length;
                iBytePOS = 3;

                while (iCurrentChar <= iLenLine)
                {
                    //text = "";

                    character = BBCBasicLine.Mid(iCurrentChar, 1);

                    //deal with string
                    if (character == "\"")
                    {
                        //iBytePOS += 1;
                        tokenisedBASIC = PushByte(tokenisedBASIC, (byte)character.MidChar(1, 1));
                        iCurrentChar += 1;
                        character = BBCBasicLine.Mid(iCurrentChar, 1);

                        while (character != "\"")
                        {
                            //iBytePOS = GetBytePos(tokenisedBASIC);
                            tokenisedBASIC = PushByte(tokenisedBASIC, (byte)character.MidChar(1, 1));
                            iCurrentChar += 1;
                            character = BBCBasicLine.Mid(iCurrentChar, 1);

                            if (iCurrentChar == iLenLine)
                            {
                                //error!
                                throw new System.ArgumentException("Unclosed quotation marks", "Syntax error");
                            }
                        }

                    }

                    if (character == ":")
                    {
                        FNPROC = false;
                    }

                    if (Regex.IsMatch(character, "^[a-zA-Z$(]") && !FNPROC)
                    {
                        text += character;


                        if (text.Length > 1)
                        {

                            //check to see if the next character is alphanumeric (if some text is left)
                            if (iCurrentChar < iLenLine)
                            {

                                if (GetToken(text))
                                {
                                    //PROC or similar
                                    if (FNPROC)
                                    {
                                        tokenisedBASIC = PushByte(tokenisedBASIC, token);
                                        text = "";
                                    }
                                }

                                if (Regex.IsMatch(BBCBasicLine.Mid(iCurrentChar + 1, 1), "^[a-zA-Z$(]"))
                                {
                                    //next character is alpha so don't tokenise yet?

                                }
                                else
                                {
                                    //check for token

                                    if (GetToken(text))
                                    {
                                        tokenisedBASIC = PushByte(tokenisedBASIC, token);
                                        text = "";
                                        //iCurrentChar -= 2;
                                    }

                                }

                            }
                        }
                        else
                        {
                            if (iCurrentChar == iLenLine)
                            {
                                //last character in line
                                tokenisedBASIC = PushByte(tokenisedBASIC, (byte)character.MidChar(1, 1));
                            }
                        }
                    }
                    else
                    {
                        if (text.Length > 0 || iCurrentChar == iLenLine)
                        {
                            //write out the bytes so far

                            for (int i = 1; i <= text.Length; i++)
                            {
                                tokenisedBASIC = PushByte(tokenisedBASIC, (byte)text.MidChar(i, 1));
                            }
                        }
                        tokenisedBASIC = PushByte(tokenisedBASIC, (byte)character.MidChar(1, 1));
                        text = "";
                    }


                    iCurrentChar += 1;

                }

                tokenisedBASIC = PushByte(tokenisedBASIC, (byte)0x0D);  //CR
                tokenisedBASIC[2] = (byte)(iBytePOS);                   //Length of line (set last), taken from 0 bound array length

            }
            catch (Exception e)
            {
                gameData.eventList.Add(new EventLog(e.Message));

            }

            Output.WriteDataToFiles.WriteTokenisedLine(tokenisedBASIC, gameData);
            return tokenisedBASIC;
        }

        private static byte[] PushByte(byte[] tokenisedLine, byte token)
        {
            tokenisedLine[iBytePOS] = token;
            if (token == 0)
            {
                throw new System.ArgumentException("Token cannot be 0", "Token");
            }

            //move byte pointer
            iBytePOS += 1;
            return tokenisedLine;
        }

        private static int GetBytePos(byte[] tokenisedLine)
        {
            for (int i = 3; i < tokenisedLine.Length; i++)
            {
                if (tokenisedLine[i] == 0)
                {
                    return i;
                }
            }

            return 0;
        }

        private static bool GetToken(string text)
        {

            //get list of matching keywords
            List<Token> SortedList = (List<Token>)TokenList.tokenList
                .Where(o => o.Keyword() == text).ToList();

            foreach (Token t in SortedList)
            {
                if (t.Keyword() == text)
                {
                    //match so check the token data
                    token = t.TokenValue();
                    conditional = t.Conditional();
                    middle = t.Middle();
                    start = t.Start();
                    FNPROC = t.FNPROC();
                    line = t.Line();
                    REM = t.REM();
                    pseudo = t.Pseudo();
                    return true;
                }
            }

            return false;
        }

    }
}
