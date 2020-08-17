using AdventureLanguage.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using AdventureLanguage.Helpers;

namespace AdventureLanguage.BBCBasicInput
{
    static class ReadBBCBasicFile
    {
        public static bool ReadBasicFile(DataItems gameData)
        {
            string line;
            string fd = gameData.folderDivider;

            try
            {
                // Read the file and display it line by line.  
                System.IO.StreamReader file = new System.IO.StreamReader(gameData.folderLocation + fd + "Source" + fd + gameData.sourceFile);
                while ((line = file.ReadLine()) != null)
                {
                    line = line.Trim();
                    if (line != null && line.Length >= 2 && line.Trim().Substring(0, 1) != "'")
                    {

                        if (line.Trim().Substring(0, 1) == "-")
                        {
                            if (gameData.debugMode)
                            {
                                line = line.Right(1, line.Length - 1);
                                gameData.SourceBBCBasicProgram.Add(new BBCBasicLine(0, line, BBCBasicLine.LineType.SourceLine));
                                BBCBasicFunctions.GetLineNumber(gameData.SourceBBCBasicProgram[gameData.SourceBBCBasicProgram.Count - 1]);
                            }
                        }
                        else
                        {
                            gameData.SourceBBCBasicProgram.Add(new BBCBasicLine(0, line, BBCBasicLine.LineType.SourceLine));
                            BBCBasicFunctions.GetLineNumber(gameData.SourceBBCBasicProgram[gameData.SourceBBCBasicProgram.Count - 1]);
                        }

                    }
                }

                file.Close();
                return true;
            }
            catch (Exception e)
            {
                gameData.eventList.Add(new EventLog("Error processing file : " + gameData.sourceFile + ". " + e.Message));
                return false;
            }
        }
    }
}