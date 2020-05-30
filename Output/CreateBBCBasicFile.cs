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

        public static bool CreateOutputProgram(DataItems gameData)
        {


            try
            {
                int iNumLines = gameData.SourceBBCBasicProgram.Count;
                int iNumUserLines = gameData.UserBBCBasicProgram.Count;

                for (int i = 0; i < iNumLines; i++)
                {
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
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(gameData.folderLocation + gameData.folderDivider + gameData.outputFile,false,System.Text.Encoding.ASCII))
                    {
                        if (gameData.folderDivider=="/")
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
