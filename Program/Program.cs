

//define MAC for the OSX build
#define MAC
//comment out for Windows

#define BuildFromVS


using System;
using System.Xml.Linq;
using System.IO;
using System.Xml;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using AdventureLanguage.Data;
using AdventureLanguage.BBCBasicInput;
using AdventureLanguage.Output;

namespace AdventureLanguage
{
    public class Program
    {

        static void Main(string[] args)
        {

            //set to either \ for Windows or / for Mac

#if MAC
            string folderDivider = @"/";
#else
            string folderDivider = @"\";
#endif
            try
            {
                if (args.Length == 0)
                {
                    UsageOptions();
                    return;
                }
            }
            catch
            {
                UsageOptions();
                return;
            }

            int i = 0;
            string argument = args[i];

            if (argument == "-?")
            {
                UsageOptions();
                return;
            }

            try
            {
                if (argument == "-b")
                {
                    argument = args[i + 1];
                    BuildProject(argument, folderDivider);
                    return;
                }

                if (argument == "-new")
                {
                    CreateNewProject(args[i + 1], args[i + 2], folderDivider);
                    return;
                }

                Console.WriteLine("Invalid command line arguments.");
                Console.WriteLine();
                UsageOptions();
                return;

            }
            catch (Exception e)
            {
                Console.WriteLine("Error processing command line call. " + e.Message);
                return;
            }

        }

        private static void UsageOptions()
        {
            Console.WriteLine("Usage options:");
            Console.WriteLine("To create a new project...");
            Console.WriteLine(@"-new <path> <projectname>, e.g.adventurelanguage.exe -new ""C:\Beeb\Adventures"" Ransom ");
            Console.WriteLine("To build a project...");
            Console.WriteLine(@"-b <path> , e.g.adventurelanguage.exe -b ""C:\Beeb\Adventures\Ransom""");
        }

        public static void CreateNewProject(string folderLocation, string projectName, string folderDivider)
        {

            if (folderLocation.Length == 0)
            {
                Console.WriteLine("Please enter a folder location for the new project.");
                return;
            }

            string path = Directory.GetCurrentDirectory();

            Console.WriteLine("Calling AL.dll at at : " + path);
            Console.WriteLine("");

            if (!CreateProject.CreateProjectFolder(folderLocation, projectName, folderDivider))
            {
                return;
            }
        }

        public static void BuildProject(string folderLocation, string folderDivider)
        {

            DataItems gameData = new DataItems();

#if BuildFromVS
            //switch from the passed in folder to the Visual Studio file
            string oldLocation = folderLocation;
            folderLocation = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
#endif

            string path = Directory.GetCurrentDirectory();

            gameData.eventList.Add(new EventLog("Calling AL.dll at at : " + path));
            gameData.eventList.Add(new EventLog(""));


            gameData.eventList.Add(new EventLog("Processing AdventureData.xml at : " + folderLocation + folderDivider + "Source" + folderDivider + "AdventureData.xml"));
            gameData.eventList.Add(new EventLog(""));

            gameData.eventList.Add(new EventLog("Compile XML data to adventure data:"));
            gameData.eventList.Add(new EventLog());

            gameData.folderLocation = folderLocation;
            gameData.debugMode = false;
            gameData.folderDivider = folderDivider;

            //get the XML input document
            XElement adventureCode = XElement.Load(gameData.folderLocation + folderDivider + "Source" + folderDivider + "AdventureData.xml");

            //parse the XML
            if (!XMLParser.ParseXML(adventureCode, gameData))
            { return; };


#if BuildFromVS
            gameData.folderLocation = oldLocation;
#endif

            try
            {
                File.Delete(gameData.folderLocation + folderDivider + "EventLog.txt");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            //merge the user code into the source file
            if (!CreateBBCBasicFile.CreateOutputProgram(gameData))
            { return; };

            //write out to text only file
            if (!CreateBBCBasicFile.WriteProgramToFile(gameData))
            { return; };

            gameData.eventList.Add(new EventLog());
            gameData.eventList.Add(new EventLog("Writing base data to files"));
            //write verbs, adverbs and nouns
            if (!WriteDataToFiles.WriteWordsToFile(gameData))
            { return; };

            //write messages to file (including index)
            if (!WriteDataToFiles.WriteMessagesToFile(gameData))
            { return; }

            //write objects to file 
            if (!WriteDataToFiles.WriteObjectsToFile(gameData))
            { return; }

            WriteDataToFiles.WriteVariablesToOutput(gameData);

            //write locations to file
            if (!WriteDataToFiles.WriteLocationsToFile(gameData))
            { return; }

            gameData.eventList.Add(new EventLog());
            gameData.eventList.Add(new EventLog("Data compiled to: " + gameData.outputFile));
            gameData.eventList.Add(new EventLog());

            //run tokeniser
            if (!ToolChain.Tokeniser(gameData))
            { return; }

            //run SSD creation(if present)
            if (!ToolChain.SSDCreation(gameData))
            { return; }

            gameData.eventList.Add(new EventLog());
            gameData.eventList.Add(new EventLog("File creation completed."));
            gameData.eventList.Add(new EventLog());

            WriteEventLog.WriteLogToFile(gameData);

            //run BeebEm (if present)
            if (!ToolChain.RunBeebEm(gameData))
            { return; }

        }
    }
}