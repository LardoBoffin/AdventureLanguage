using System;
using System.Collections.Generic;
using System.Text;
using AdventureLanguage.Data;
using System.IO;
using AdventureLanguage.Helpers;
using System.Collections.ObjectModel;
using System.Diagnostics;
using AdventureLanguage.Output;

namespace AdventureLanguage.Output
{
    public static class ToolChain
    {

        public static bool Tokeniser(DataItems gameData)
        {

            if (gameData.tokeniser == "" || gameData.tokeniser == null) { return true; }

            string fd = gameData.folderDivider;

            if (fd == "/")
            {


                ///Users/philjones/Documents/BasicTool/basictool --help
                //philjones$ / Users / philjones / Documents / BasicTool / basictool - t / Users / philjones / Documents / BasicTool / banzai.txt / Users / philjones / Documents / BasicTool / basic.tok

                // /philjones$/Users/philjones/Documents/BasicTool/ basictool - t
                // /Users/philjones/Documents/BasicTool/banzai.txt
                // /Users/philjones/Documents/BasicTool/basic.tok

                //path to basictool
                //path to source document
                //path to destination document

            }
            else
            {
                //string folderDivider = @"\";

                string fileOutput = gameData.folderLocation + @"\fileOutput";

                string tokeniserCall = "\"" + gameData.tokeniser + "\"" + " " + "\"" + gameData.folderLocation + @"\" + gameData.outputFile + "\"" + " " + "\"" + fileOutput + @"\" + gameData.tokenisedFileName + "\"";

                gameData.eventList.Add(new EventLog("BBC Basic tokeniser thanks to Richard Russel. See Stardot topic 'BASIC Tokenizer' in '8-bit acorn software: other'"));
                gameData.eventList.Add(new EventLog());
                gameData.eventList.Add(new EventLog("Calling tokeniser at " + tokeniserCall));

                try
                {
                    File.Delete(fileOutput + "\\" + gameData.tokenisedFileName + ".BBC"); // Delete the existing file if exists

                    //check for file exists
                    gameData.eventList.Add(new EventLog());
                    gameData.eventList.Add(new EventLog("Response from tokeniser:"));

                    using (Process tokeniseFile = Process.Start(tokeniserCall))
                    {

                        tokeniseFile.WaitForExit(100000);     //wait up to ten seconds
                        int errorCode = tokeniseFile.ExitCode;

                        if (errorCode != 0)
                        {
                            Console.Write("Failed to tokenise.");
                            return false;
                        }

                        if (!File.Exists(fileOutput + @"\" + gameData.tokenisedFileName + ".BBC"))
                        {
                            return false;
                        }

                        File.Delete(fileOutput + "\\" + gameData.tokenisedFileName); // Delete the existing file if exists

                        //remove the .BBC extension
                        FileInfo file = new FileInfo(fileOutput + @"\" + gameData.tokenisedFileName + ".BBC");
                        file.Rename(gameData.tokenisedFileName);

                        if (gameData.crunchTokenisedFile)
                        {
                            if (!CreateBBCBasicFile.ProcessTokenisedFile(gameData))
                            {
                                return false;
                            }
                        }

                    }
                }
                catch (Exception e)
                {
                    gameData.eventList.Add(new EventLog(e.Message));
                    return false;
                }

                gameData.eventList.Add(new EventLog());
                gameData.eventList.Add(new EventLog("File tokenised : " + gameData.folderLocation + "\\" + gameData.tokenisedFileName));
                gameData.eventList.Add(new EventLog());
            }


            return true;
        }

        public static bool SSDCreation(DataItems gameData)
        {

            if (gameData.SSDBuilder == "" || gameData.SSDBuilder == null) { return true; }

            string fd = gameData.folderDivider;

            string pathToSSD;

            if (fd == "/")
            {
                //mac so copy raw text file to fileoutput and remove .txt extension
                try
                {
                    string targetFile = Path.Combine(gameData.folderLocation + gameData.folderDivider + "fileOutput", gameData.outputFile.Left(gameData.outputFile.Length - 4));
                    File.Delete(targetFile);
                    File.Copy(Path.Combine(gameData.folderLocation, gameData.outputFile), targetFile, true);
                }
                catch (Exception e)
                {
                    gameData.eventList.Add(new EventLog(e.Message));
                    return false;
                }

                //create SSD    ./blank_ssd.pl  file path
                string createSSD = "./blank_ssd.pl";

                //add files     ./putfile.pl
                string putFiles = "./putfile.pl";

                //opt 4         ./opt4.pl
                string opt4 = "./opt4.pl";

                //*title        ./title.pl
                string title = "./title.pl";

                string targetSSD = (char)34 + gameData.folderLocation + fd + "SSD" + fd + (char)34 + gameData.SSDName + " ";

                pathToSSD = (char)34 + gameData.SSDBuilder + fd + (char)34 + createSSD + " " + targetSSD;// gameData.folderLocation + fd + "SSD" + fd + gameData.SSDName;

                gameData.eventList.Add(new EventLog("SSD file builder thanks to sweh. See https://sweh.spuddy.org/Beeb/mmb_utils.html"));
                gameData.eventList.Add(new EventLog());
                gameData.eventList.Add(new EventLog("Calling SSD Builder at " + pathToSSD));
                gameData.eventList.Add(new EventLog());

                try
                {
                    File.Delete(gameData.folderLocation + fd + "SSD" + fd + gameData.SSDName); // Delete the existing file if exists
                }
                catch (Exception e)
                {
                    gameData.eventList.Add(new EventLog(e.Message));
                    return false;
                }

                try
                {
                    //create SSD
                    var test = pathToSSD.Bash();

                    //add files
                    pathToSSD = (char)34 + gameData.SSDBuilder + fd + (char)34 + putFiles + " " + targetSSD + gameData.folderLocation + fd + "fileOutput/*";
                    test = pathToSSD.Bash();

                    //set opt 4 to exec (3)
                    pathToSSD = (char)34 + gameData.SSDBuilder + fd + (char)34 + opt4 + " " + targetSSD + " 3";
                    test = pathToSSD.Bash();

                    //set title
                    pathToSSD = (char)34 + gameData.SSDBuilder + fd + (char)34 + title + " " + targetSSD + gameData.gameTitle;
                    test = pathToSSD.Bash();

                }
                catch (Exception e)
                {
                    gameData.eventList.Add(new EventLog(e.Message));
                    return false;
                }


            }
            else
            {
                //windows
                pathToSSD = gameData.folderLocation + @"\SSD\" + gameData.SSDName;

                string SSDBuilderCall = "\"" + gameData.SSDBuilder + "\"" + " " + "\"" + pathToSSD + "\"" + " " + "\"" + gameData.folderLocation + "\\fileOutput \"-o 3 -t " + gameData.gameTitle + "\"";

                gameData.eventList.Add(new EventLog("SSD file builder thanks to JGHarston. See http://mdfs.net/Apps/DiskTools"));
                gameData.eventList.Add(new EventLog());
                gameData.eventList.Add(new EventLog("Calling SSD Builder at " + SSDBuilderCall));

                try
                {
                    File.Delete(pathToSSD); // Delete the existing file if exists

                    //check for file exists
                    gameData.eventList.Add(new EventLog());
                    gameData.eventList.Add(new EventLog("Response from SSD Creation:"));

                    using Process createSSD = Process.Start(SSDBuilderCall);
                    createSSD.WaitForExit(100000);     //wait up to ten seconds
                    int errorCode = createSSD.ExitCode;

                    if (errorCode != 0)
                    {
                        Console.Write("Failed to created SSD file.");
                        return false;
                    }

                    if (!File.Exists(pathToSSD))
                    {
                        return false;
                    }
                }
                catch (Exception e)
                {
                    gameData.eventList.Add(new EventLog(e.Message));
                    return false;
                }

                gameData.eventList.Add(new EventLog());
                gameData.eventList.Add(new EventLog("SSD file created : " + gameData.folderLocation + "\\SSD\\" + gameData.SSDName));
                gameData.eventList.Add(new EventLog());
            }

            return true;
        }

        public static bool RunBeebEm(DataItems gameData)
        {

            if (gameData.BeebEmPath == "" || gameData.BeebEmPath == null) { return true; }

            string fd = gameData.folderDivider;

            string RunBeebEm;

            if (fd == "/")
            {
                //mac
                RunBeebEm = (char)34 + gameData.BeebEmPath + (char)34 + " " + fd + gameData.folderLocation + fd + "SSD" + fd + gameData.SSDName;

                gameData.eventList.Add(new EventLog("BeebEm thanks to ..."));
                gameData.eventList.Add(new EventLog());
                gameData.eventList.Add(new EventLog("Calling BeebEm at " + RunBeebEm));

                try
                {
                    RunBeebEm = "open " + RunBeebEm;
                    var test = RunBeebEm.Bash();

                }
                catch (Exception e)
                {
                    gameData.eventList.Add(new EventLog(e.Message));
                    return false;
                }

            }
            else
            {
                //Windows
                RunBeebEm = "\"" + gameData.BeebEmPath + "\" \"" + gameData.folderLocation + @"\SSD\" + gameData.SSDName + "\"";

                gameData.eventList.Add(new EventLog("BeebEm thanks to ..."));
                gameData.eventList.Add(new EventLog());
                gameData.eventList.Add(new EventLog("Calling BeebEm at " + RunBeebEm));

                try
                {

                    //check for file exists
                    gameData.eventList.Add(new EventLog());
                    gameData.eventList.Add(new EventLog("Response from BeebEm:"));

                    using (Process tokeniseFile = Process.Start(RunBeebEm))
                    {
                        tokeniseFile.WaitForExit(100000);     //wait up to ten seconds
                        int errorCode = tokeniseFile.ExitCode;

                        if (errorCode != 0)
                        {
                            Console.Write("Failed to start BeebEm.");
                            return false;
                        }
                    }
                }
                catch (Exception e)
                {
                    gameData.eventList.Add(new EventLog(e.Message));
                    return false;
                }
            }

            return true;
        }

    }
}




