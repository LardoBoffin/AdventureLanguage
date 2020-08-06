using AdventureLanguage.Data;
using System;
using System.Collections.Generic;
using System.Text;
using AdventureLanguage.Helpers;
using System.IO;

namespace AdventureLanguage.Output
{
    public static class CreateBBCBasicFile
    {
        public static bool ProcessTokenisedFile(DataItems gameData)
        {
            //open a tokenised file and step through each line calling remove spaces

            string fd = gameData.folderDivider;

            gameData.eventList.Add(new EventLog(""));
            gameData.eventList.Add(new EventLog("Crunching tokenised file."));

            BinaryReader tokenisedFile = new BinaryReader(File.Open(gameData.folderLocation + fd + "FileOutput" + fd + gameData.tokenisedFileName, FileMode.Open));
            long originalLength = tokenisedFile.BaseStream.Length;
            BinaryWriter outputFile = new BinaryWriter(File.Open(gameData.folderLocation + fd + "FileOutput" + fd + "_tmp", FileMode.Create));

            try
            {

                //load tokenised file and save without spaces
                while (true)
                {

                    byte[] buffer = new byte[originalLength];

                    //get length of file and read it into buf
                    int sz = tokenisedFile.Read(buffer, 0, (int)originalLength);

                    //empty file
                    if (sz <= 0)
                        break;

                    //step through buffer and remove unwanted space characters
                    buffer = StepThroughFile(buffer);

                    //write data
                    outputFile.Write(buffer, 0, buffer.Length);

                    //last chunk of large file?
                    if (sz < 100000)
                        break; // eof reached
                }

                tokenisedFile.Close();
                long nl = outputFile.BaseStream.Length;
                outputFile.Close();


                try
                {
                    //delete tokenised file
                    File.Delete(gameData.folderLocation + fd + "FileOutput" + fd + gameData.tokenisedFileName);

                    //Rename temp to tokenised file
                    FileInfo file = new FileInfo(gameData.folderLocation + fd + "FileOutput" + fd + "_tmp");
                    file.Rename(gameData.tokenisedFileName);

                    gameData.eventList.Add(new EventLog("Crunched and reduced by " + (nl - originalLength) + " bytes"));
                    gameData.eventList.Add(new EventLog(""));
                }
                catch (Exception e)
                {
                    Console.Write(e.Message);
                }


                return true;
            }
            catch (Exception exp)
            {
                Console.Write(exp.Message);
            }

            return false;
        }

        private static byte[] StepThroughFile(byte[] buffer)
        {
            byte[] returnBuffer = new byte[buffer.Length];
            int returnBufferCount = 0;
            bool isString = false;
            bool startOfLine = true;
            int lineCount = 1;
            int lineLenPos = 0;
            int lineLen = 0;

            //the program starts with a CR and ends with &FF
            returnBuffer[returnBufferCount] = 13; returnBufferCount++;

            //MSB of line number
            //LSB of line number
            //Length of line (which needs to be adjusted when the line is shortened) 
            //Text ......
            //CR

            try
            {
                for (int x = 1; x < buffer.Length; x++)
                {

                    byte b = buffer[x];

                    if (b == 255)
                    {
                        startOfLine = false;
                    }

                    if (startOfLine)
                    {
                        for (int header = 0; header < 3; header++)
                        {
                            returnBuffer[returnBufferCount + header] = buffer[x];

                            x++;
                        }

                        //store where the line length is for this line
                        lineLen = returnBuffer[returnBufferCount + 2];
                        lineLenPos = returnBufferCount + 2;
                        returnBufferCount += 3;
                        startOfLine = false;
                    }

                    b = buffer[x];

                    if (b == '"')
                    {
                        //set string mode
                        if (isString)
                        {
                            isString = false;
                        }
                        else { isString = true; }
                    }

                    if (b == ' ')
                    {
                        //space so only add it if we are in a string
                        if (isString)
                        {
                            returnBuffer[returnBufferCount] = b;
                            returnBufferCount++;
                        }
                        else
                        {
                            //the character is not added so adjust the length
                            lineLen--;
                        }
                    }
                    else
                    {
                        returnBuffer[returnBufferCount] = b;
                        returnBufferCount++;
                    }

                    if (b == 13)
                    {
                        //deal with end of line
                        returnBuffer[lineLenPos] = (byte)lineLen;
                        startOfLine = true;
                        lineCount++;
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            byte[] tmpreturnBuffer = new byte[returnBufferCount];
            Buffer.BlockCopy(returnBuffer, 0, tmpreturnBuffer, 0, returnBufferCount);

            return tmpreturnBuffer;

        }
        public static bool CreateOutputProgram(DataItems gameData)
        {

            try
            {
                int iNumLines = gameData.SourceBBCBasicProgram.Count;
                int iNumUserLines = gameData.UserBBCBasicProgram.Count;

                for (int i = 0; i < iNumLines; i++)
                {

                    if (gameData.SourceBBCBasicProgram[i].LineText().IndexOf("@NumObjects@") > -1)
                    {
                        //replace the room index size with the actual number used
                        string tmp = gameData.SourceBBCBasicProgram[i].LineText();
                        gameData.SourceBBCBasicProgram[i].SetLineText(tmp.Replace("@NumObjects@", gameData.objectList.Count.ToString()));
                    }

                    if (gameData.SourceBBCBasicProgram[i].LineText().IndexOf("@ObjectData@") > -1)
                    {
                        //replace the room index size with the actual number used
                        string tmp = gameData.SourceBBCBasicProgram[i].LineText();
                        gameData.SourceBBCBasicProgram[i].SetLineText(tmp.Replace("@ObjectData@", (gameData.objectList.Count * 8).ToString()));
                    }

                    if (gameData.SourceBBCBasicProgram[i].LineText().IndexOf("@RMIndex@") > -1)
                    {
                        //replace the room index size with the actual number used
                        string tmp = gameData.SourceBBCBasicProgram[i].LineText();
                        gameData.SourceBBCBasicProgram[i].SetLineText(tmp.Replace("@RMIndex@", gameData.roomIndexLength.ToString()));
                    }

                    if (gameData.SourceBBCBasicProgram[i].LineText().IndexOf("@Room@") > -1)
                    {
                        //replace the room index size with the actual number used
                        string tmp = gameData.SourceBBCBasicProgram[i].LineText();
                        gameData.SourceBBCBasicProgram[i].SetLineText(tmp.Replace("@Room@", gameData.roomDataLength.ToString()));
                    }

                    if (gameData.SourceBBCBasicProgram[i].LineText().IndexOf("@NumVar@") > -1)
                    {
                        //replace the 256 with the actual number used
                        string tmp = gameData.SourceBBCBasicProgram[i].LineText();
                        gameData.SourceBBCBasicProgram[i].SetLineText(tmp.Replace("@NumVar@", gameData.varList.Count.ToString()));
                    }

                    if (gameData.SourceBBCBasicProgram[i].LineText().IndexOf("@ScreenMode@") > -1)
                    {
                        //replace the screen mode
                        string tmp = gameData.SourceBBCBasicProgram[i].LineText();
                        gameData.SourceBBCBasicProgram[i].SetLineText(tmp.Replace("@ScreenMode@", gameData.screenMode.ToString()));
                    }

                    if (gameData.SourceBBCBasicProgram[i].LineText().IndexOf("@Title@") > -1)
                    {
                        //replace the game title
                        string tmp = gameData.SourceBBCBasicProgram[i].LineText();
                        gameData.SourceBBCBasicProgram[i].SetLineText(tmp.Replace("@Title@", gameData.gameTitle));
                    }

                    if (gameData.SourceBBCBasicProgram[i].LineText().IndexOf("@NumMssages@") > -1)
                    {
                        //replace the number of messages with the actual number used
                        string tmp = gameData.SourceBBCBasicProgram[i].LineText();
                        gameData.SourceBBCBasicProgram[i].SetLineText(tmp.Replace("@NumMssages@", gameData.messageList.Count.ToString()));
                    }

                    if (gameData.SourceBBCBasicProgram[i].LineText().IndexOf("@ScreenWidth@") > -1)
                    {
                        //replace the screen width
                        string tmp = gameData.SourceBBCBasicProgram[i].LineText();
                        gameData.SourceBBCBasicProgram[i].SetLineText(tmp.Replace("@ScreenWidth@", gameData.screenWidth.ToString()));
                    }

                    gameData.TargetBBCBasicProgram.Add(new BBCBasicLine(gameData.SourceBBCBasicProgram[i].OriginalLineNumber(), gameData.SourceBBCBasicProgram[i].LineText(), BBCBasicLine.lineType.SourceLine));


                    //loop through each line of the following items:
                    //gameData.SourceBBCBasicProgram
                    //gameData.UserBBCBasicProgram.PreRoom
                    //gameData.UserBBCBasicProgram.Init
                    //gameData.UserBBCBasicProgram.HighPriority
                    //gameData.UserBBCBasicProgram.LowPriority

                    if (gameData.SourceBBCBasicProgram[i].LineText().ToUpper() == "DEFPROCPREROOM")
                    {
                        //merge in the preroom stuff, if any
                        for (int line = 0; line < iNumUserLines; line++)
                        {
                            if (gameData.UserBBCBasicProgram[line].GetLineType() == (int)BBCBasicLine.lineType.PreRoom)
                            {
                                gameData.TargetBBCBasicProgram.Add(new BBCBasicLine(gameData.UserBBCBasicProgram[line].OriginalLineNumber(), gameData.UserBBCBasicProgram[line].LineText(), BBCBasicLine.lineType.PreRoom));
                            }
                        }
                    }

                    if (gameData.SourceBBCBasicProgram[i].LineText().ToUpper() == "DEFPROCINIT")
                    {
                        //merge in the preroom stuff, if any
                        for (int line = 0; line < iNumUserLines; line++)
                        {
                            if (gameData.UserBBCBasicProgram[line].GetLineType() == (int)BBCBasicLine.lineType.Init)
                            {
                                gameData.TargetBBCBasicProgram.Add(new BBCBasicLine(gameData.UserBBCBasicProgram[line].OriginalLineNumber(), gameData.UserBBCBasicProgram[line].LineText(), BBCBasicLine.lineType.Init));
                            }
                        }
                    }

                    if (gameData.SourceBBCBasicProgram[i].LineText().ToUpper() == "DEFFNHIGHPTY")
                    {
                        //merge in the preroom stuff, if any
                        for (int line = 0; line < iNumUserLines; line++)
                        {
                            if (gameData.UserBBCBasicProgram[line].GetLineType() == (int)BBCBasicLine.lineType.HighPriority)
                            {
                                gameData.TargetBBCBasicProgram.Add(new BBCBasicLine(gameData.UserBBCBasicProgram[line].OriginalLineNumber(), gameData.UserBBCBasicProgram[line].LineText(), BBCBasicLine.lineType.PreRoom));
                            }
                        }
                    }

                    if (gameData.SourceBBCBasicProgram[i].LineText().ToUpper() == "DEFFNLOWPTY")
                    {
                        //merge in the preroom stuff, if any
                        for (int line = 0; line < iNumUserLines; line++)
                        {
                            if (gameData.UserBBCBasicProgram[line].GetLineType() == (int)BBCBasicLine.lineType.LowPriority)
                            {
                                gameData.TargetBBCBasicProgram.Add(new BBCBasicLine(gameData.UserBBCBasicProgram[line].OriginalLineNumber(), gameData.UserBBCBasicProgram[line].LineText(), BBCBasicLine.lineType.PreRoom));
                            }
                        }
                    }
                }

                //renumber the lines
                BBCBasicFunctions.RenumberLines(gameData);

                return true;
            }
            catch (Exception e)
            {
                gameData.eventList.Add(new EventLog(e.Message));
                return false;
            }

        }

        public static bool WriteProgramToFile(DataItems gameData)
        {
            try
            {
                int iNumLines = gameData.TargetBBCBasicProgram.Count;

                {
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(gameData.folderLocation + gameData.folderDivider + gameData.outputFile, false, System.Text.Encoding.ASCII))
                    {
                        if (gameData.folderDivider == "/")
                        {
                            file.NewLine = "\r";
                        }
                        else
                        {
                            file.NewLine = "\r\n";
                        }

                        for (int i = 0; i < iNumLines; i++)
                        {
                            file.WriteLine(gameData.TargetBBCBasicProgram[i].NewLineNumber().ToString() + gameData.TargetBBCBasicProgram[i].LineText());
                            gameData.eventList.Add(new EventLog((gameData.TargetBBCBasicProgram[i].NewLineNumber().ToString() + gameData.TargetBBCBasicProgram[i].LineText())));
                        }
                    }
                }

                return true;
            }
            catch (Exception e)
            {
                gameData.eventList.Add(new EventLog(e.Message));
                return false;
            }

        }
    }
}
