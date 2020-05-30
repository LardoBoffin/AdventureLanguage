using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace AdventureLanguage.Output
{
    public static class CreateProject
    {

        public static bool CreateProjectFolder(string folderLocation, string projectName, string folderDivider)
        {

            try
            {

                Console.WriteLine("Checking for folder");

                if (Directory.Exists(folderLocation + folderDivider + projectName))
                {
                    Console.WriteLine("The specified project (" + folderLocation + folderDivider + projectName + ") already exists.");
                    return false;
                }

                string path = folderLocation + folderDivider + projectName;

                //create parent folder - folderLocation + projectName
                Console.WriteLine("Creating project folder");
                DirectoryInfo di = Directory.CreateDirectory(path);

                //create FileOutput 
                Console.WriteLine("Creating FileOutput folder");
                di = Directory.CreateDirectory(path + folderDivider + "FileOutput");

                Console.WriteLine("Creating !boot");
                StreamWriter buildBoot = new StreamWriter(File.Open(path + folderDivider + "FileOutput" + folderDivider + "!boot", FileMode.Create));
                buildBoot.NewLine = "\r";//\n
                if (folderDivider == @"\")
                {
                    //windows
                    buildBoot.WriteLine("CHAIN\"" + projectName.Left(7) + "\"");
                    buildBoot.WriteLine("");
                }
                else
                {
                    //mac
                    buildBoot.WriteLine("*KEY 0 *EXEC " + projectName.Left(7) + "|MSAVE \"" + projectName.Left(7) + "\"|MRUN|M");
                    //buildBoot.WriteLine("*KEY 1 SAVE \"" + projectName.Left(7) + "\"|MRUN|M");

                    //add in *KEY 0, 1 and 2 calls to *EXEC rawtext
                }


                buildBoot.Flush();

                //create Source
                Console.WriteLine("Creating Source folder");
                di = Directory.CreateDirectory(path + folderDivider + "Source");


                //adventureData.xml
                try
                {
                    // copy in AdventureData.xml
                    Console.WriteLine("Creating AdventureData.xml");
                    File.Copy("AdventureData.xml", Path.Combine(path + folderDivider + "Source", "AdventureData.xml"), true);

                    //remap the tokeniser and ssd maker etc.
                    XmlDocument xml = new XmlDocument();
                    Console.WriteLine(path + folderDivider + "Source" + folderDivider + "AdventureData.xml");
                    xml.Load(path + folderDivider + "Source" + folderDivider + "AdventureData.xml");

                    if (folderDivider == @"\")
                    {
                        xml.SelectSingleNode("//Section/Tokeniser").InnerText = Directory.GetCurrentDirectory() + folderDivider + "tokenise.exe";
                        xml.SelectSingleNode("//Section/SSDBuilder").InnerText = Directory.GetCurrentDirectory() + folderDivider + "MkImg.exe";
                    }
                    else
                    {
                        xml.SelectSingleNode("//Section/OutputFile").InnerText = projectName.Left(7) + ".txt";
                    }

                    xml.SelectSingleNode("//Section/GameTitle").InnerText = projectName;
                    xml.SelectSingleNode("//Section/TokenisedFileName").InnerText = projectName.Left(7);
                    xml.SelectSingleNode("//Section/SSDName").InnerText = projectName + ".SSD";
                    xml.Save(path + folderDivider + "Source" + folderDivider + "AdventureData.xml");


                }
                catch (Exception e)
                {
                    Console.WriteLine("Error reported while creating AdventureData.xml : " + e.Message);
                    return false;
                }

                //source.txt
                try
                {
                    // copy in source.txt
                    Console.WriteLine("Creating Source.txt");
                    File.Copy("source.txt", Path.Combine(path + folderDivider + "Source", "source.txt"), true);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error reported while creating Source.txt : " + e.Message);
                    return false;
                }

                //create SSD
                Console.WriteLine("Creating SSD folder");
                di = Directory.CreateDirectory(path + folderDivider + "SSD");

                //create compile batch file
                if (folderDivider == @"\")
                {
                    Console.WriteLine("Creating build.bat");
                    StreamWriter buildBatchFileWin = new StreamWriter(File.Open(path + folderDivider + "build.bat", FileMode.Create));
                    buildBatchFileWin.WriteLine("\"" + Directory.GetCurrentDirectory() + folderDivider + @"AdventureLanguage.exe"" -b """ + path + "\"");
                    buildBatchFileWin.Flush();
                }
                else
                {
                    Console.WriteLine("Creating build.command");
                    //mac
                    StreamWriter buildBatchFileMac = new StreamWriter(File.Open(path + folderDivider + "build.command", FileMode.Create));
                    buildBatchFileMac.WriteLine("#!/bin/bash");
                    buildBatchFileMac.WriteLine("cd " + (char)34 + Directory.GetCurrentDirectory() + (char)34);
                    buildBatchFileMac.WriteLine("dotnet al.dll -b " + (char)34 + path + (char)34);
                    //buildBatchFile.WriteLine(Directory.GetCurrentDirectory() + folderDivider + "AdventureLanguage -b " + path);
                    buildBatchFileMac.Flush();
                }

            }
            catch
            {

                return false;
            }

            Console.WriteLine("Project created at " + folderLocation + @"\" + projectName);

            return true;
        }

    }
}
