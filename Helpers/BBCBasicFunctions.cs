using System;
using System.Collections.ObjectModel;
using AdventureLanguage.Data;


namespace AdventureLanguage.Helpers
{
    public static class BBCBasicFunctions
    {

        public static void GetLineNumber(BBCBasicLine lineCode)
        {
            string lineNumber = "";
            string line = lineCode.LineText().Trim();

            foreach (char c in line)
            {
                if (c >= '0' && c <= '9')
                {
                    lineNumber += c;
                }
                else
                {
                    break;
                }
            }

            if (lineNumber == "")
            {
                //no line number provided so set it to 10
                lineNumber = 10.ToString();
                line = lineNumber + line;
            }

            lineCode.SetLineNumber(Int32.Parse(lineNumber));    //set the line number as extracted
            lineCode.SetLineText(line[lineNumber.Length..]);    //trim off line number from start of line

        }

        public static bool ParseLine(BBCBasicLine lineCode, DataItems gameData)
        {
            //step through a line and replace any @variables with their actual value, e.g.
            //PROCsetCtr(@IsDark,0) becomes PROCsetCtr(1,0)7
            //step throught a line and search for "PROCmsg(", create a message and then replace the string with the number

            bool variableMode = false;
            string variableText = "";
            int variableNumber;
            string lineOutput = "";

            foreach (char c in lineCode.LineText())
            {
                if (c == '@')
                {
                    if (variableMode)
                    {
                        variableText += c.ToString();
                    }
                    else { variableText = ""; }

                    variableMode = true;
                }
                else
                {
                    if (variableMode)
                    {
                        //we are building a variable name
                        if ((c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '_')
                        {
                            variableText += c.ToString();
                        }
                        else
                        {
                            variableMode = false;

                            //try the variable list
                            variableNumber = DataHelpers.VariableFound(gameData.varList, variableText);

                            if (variableNumber > -1)
                            {
                                lineOutput += variableNumber.ToString();
                                lineOutput += c.ToString();
                                continue;
                            }

                            //try the object list
                            variableNumber = DataHelpers.ObjectFound(gameData.objectList, variableText);
                            if (variableNumber > -1)
                            {
                                lineOutput += variableNumber.ToString();
                                lineOutput += c.ToString();
                                continue;
                            }

                            //try the verb list
                            variableNumber = DataHelpers.VerbNumber(gameData.verbList, variableText);
                            if (variableNumber > -1)
                            {
                                lineOutput += variableNumber.ToString();
                                lineOutput += c.ToString();
                                continue;
                            }

                            //try the noun list
                            variableNumber = DataHelpers.NounNumber(gameData.nounList, variableText);
                            if (variableNumber > -1)
                            {
                                lineOutput += variableNumber.ToString();
                                lineOutput += c.ToString();
                                continue;
                            }

                            //try the message list
                            variableNumber = DataHelpers.MessageNumber(gameData.messageList, variableText);
                            if (variableNumber > -1)
                            {
                                lineOutput += variableNumber.ToString();
                                lineOutput += c.ToString();
                                continue;
                            }

                            gameData.eventList.Add(new EventLog("Item " + variableText + " not found."));
                            return false;
                        }
                    }
                    else
                    {
                        lineOutput += c.ToString();
                    }
                }
            }

            lineCode.SetLineText(GetMessage(lineOutput, gameData.messageList));

            return true;
        }

        private static string GetMessage(string lineText, Collection<Message> messageList)
        {
            //loop through a line and return a new string with messages converted to numbers

            string returnMessage = "";
            string messageToCreate = "";
            bool stringMode = false;

            foreach (char c in lineText)
            {

                if (c == '"')
                {
                    if (stringMode)
                    {
                        //we are already in string mode so this is the end of the string
                        stringMode = false;
                        returnMessage += DataHelpers.AddMessage(messageList, messageToCreate[1..], "").ToString();
                        continue;//do not include the trailing " in the return string
                    }

                    messageToCreate = "";

                    if (returnMessage.Length > 6 && !stringMode)
                    {
                        //not at start of a line

                        if (returnMessage.Substring(returnMessage.Length - 8, 7) == "PROCmsg")
                        {
                            stringMode = true;
                        }
                    }
                }

                if (!stringMode)
                {
                    returnMessage += c;
                }
                else
                {
                    messageToCreate += c;
                }

            }

            return returnMessage;
        }

        public static void TokeniseLines(DataItems gameData)
        {
            //loop through each line and call tokeniser
            int iNumLines = gameData.TargetBBCBasicProgram.Count;

            for (int i = 0; i < iNumLines; i++)
            {
                gameData.TargetBBCBasicProgram[i].SetTokenisedLine(Tokeniser.Tokeniser.Tokenise(gameData.TargetBBCBasicProgram[i].LineText(), gameData.TargetBBCBasicProgram[i].NewLineNumber(),gameData));
            }

        }

        public static void RenumberLines(DataItems gameData)
        {
            //loop through each line and renumber in steps of 10
            int iNumLines = gameData.TargetBBCBasicProgram.Count;

            for (int i = 0; i < iNumLines; i++)
            {
                gameData.TargetBBCBasicProgram[i].SetNewLineNumber((i + 1) * 10);
            }
        }

    }
}
