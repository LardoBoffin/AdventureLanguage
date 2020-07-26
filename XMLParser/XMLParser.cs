using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Xml.Linq;
using System.IO;
using System.Xml;
using System.Linq;
using AdventureLanguage.Data;
using AdventureLanguage.Helpers;
using AdventureLanguage.BBCBasicInput;

namespace AdventureLanguage
{
    internal class XMLParser
    {

        public static bool ParseXML(XElement adventureXML, DataItems gameData)
        {
            foreach (XElement xm in adventureXML.Descendants("GlobalVariables"))
            {
                //get global variables
                gameData.eventList.Add(new EventLog("<GlobalVariables>"));
                if (!LocalVariables(xm, gameData, true))
                {
                    return false;
                }
                gameData.eventList.Add(new EventLog("</GlobalVariables>")); ;
            }

            foreach (XElement xm in adventureXML.Descendants("Sections"))
            {
                foreach (XElement xs in adventureXML.Descendants("Section"))
                {
                    //parse the XML
                    if (!ParseMainSection(xs, gameData)) { return false; }

                    //load the framework BBC Basic
                    if (!ReadBBCBasicFile.ReadBasicFile(gameData)) { return false; }

                }

            }
            gameData.eventList.Add(new EventLog());

            return true;
        }

        private static bool ParseMainSection(XElement xm, DataItems gameData)
        {

            foreach (XElement xe in xm.Descendants())
            {


                switch (xe.Name.ToString().ToUpper())
                {
                    case "GAMETITLE":
                        //id = xe.Value;
                        gameData.eventList.Add(new EventLog(xe));
                        gameData.gameTitle = xe.Value;
                        if (gameData.gameTitle.Length > 12)
                        {
                            gameData.eventList.Add(new EventLog("Maximum game title length is 12 characetrs - this is used for the disc title."));
                            return false;
                        }
                        break;

                    case "OUTPUTFILE":
                        gameData.eventList.Add(new EventLog(xe));
                        gameData.outputFile = xe.Value;
                        break;

                    case "TOKENISER":
                        gameData.eventList.Add(new EventLog(xe));
                        gameData.tokeniser = xe.Value;
                        break;

                    case "TOKENISEDFILENAME":
                        gameData.eventList.Add(new EventLog(xe));
                        gameData.tokenisedFileName = xe.Value;
                        break;

                    case "SCREENWIDTH":
                        gameData.eventList.Add(new EventLog(xe));
                        gameData.screenWidth = Int32.Parse(xe.Value);
                        break;

                    case "SCREENMODE":
                        gameData.eventList.Add(new EventLog(xe));
                        gameData.screenMode = Int32.Parse(xe.Value);
                        break;

                    case "SOURCEFILE":
                        gameData.eventList.Add(new EventLog(xe));
                        gameData.sourceFile = xe.Value;
                        break;

                    case "SSDBUILDER":
                        gameData.eventList.Add(new EventLog(xe));
                        gameData.SSDBuilder = xe.Value;
                        break;

                    case "SSDNAME":
                        gameData.eventList.Add(new EventLog(xe));
                        gameData.SSDName = xe.Value;
                        break;

                    case "BEEBEMPATH":
                        gameData.eventList.Add(new EventLog(xe));
                        gameData.BeebEmPath = xe.Value;
                        break;

                    case "BUILDTYPE":
                        gameData.eventList.Add(new EventLog(xe));

                        if (xe.Value.ToUpper() == "DEBUG")
                        {
                            gameData.debugMode = true;
                        }
                        else
                        {
                            gameData.debugMode = false;
                        }
                        gameData.BeebEmPath = xe.Value;
                        break;

                    case "SYSTEMMESSAGES":
                        gameData.eventList.Add(new EventLog("<SystemMessages>"));
                        if (!SystemMessges(xe, gameData.messageList, gameData))
                        {
                            return false;
                        }
                        gameData.eventList.Add(new EventLog("</SystemMessages>"));
                        break;

                    case "SYSTEMVERBS":
                        gameData.eventList.Add(new EventLog("<SystemVerbs>"));
                        if (!Verbs(xe, gameData))
                        {
                            return false;
                        }
                        gameData.eventList.Add(new EventLog("</SystemVerbs>"));
                        break;

                    case "GAMEVERBS":
                        gameData.eventList.Add(new EventLog("<GameVerbs>"));
                        if (!Verbs(xe, gameData))
                        {
                            return false;
                        }
                        gameData.eventList.Add(new EventLog("</GameVerbs>"));
                        break;

                    case "ADVERBS":
                        gameData.eventList.Add(new EventLog("<Adverbs>"));
                        if (!Adverbs(xe, gameData))
                        {
                            return false;
                        }
                        gameData.eventList.Add(new EventLog("</Adverbs>"));
                        break;

                    case "OBJECTS":
                        gameData.eventList.Add(new EventLog("<Objects>"));
                        if (!Objects(xe, gameData))
                        {
                            return false;
                        }
                        gameData.eventList.Add(new EventLog("</Objects>"));
                        break;

                    case "NOUNS":
                        gameData.eventList.Add(new EventLog("<Nouns>"));
                        if (!Nouns(xe, gameData))
                        {
                            return false;
                        }
                        gameData.eventList.Add(new EventLog("</Nouns>"));
                        break;

                    case "GLOBALVARIABLES":
                        gameData.eventList.Add(new EventLog("<GlobalVariables>"));
                        if (!LocalVariables(xe, gameData, true))
                        {
                            return false;
                        }
                        gameData.eventList.Add(new EventLog("</GlobalVariables>"));
                        break;

                    case "VARIABLES":
                        gameData.eventList.Add(new EventLog("<Variables>"));
                        if (!LocalVariables(xe, gameData, false))
                        {
                            return false;
                        }
                        gameData.eventList.Add(new EventLog("</Variables>"));
                        break;

                    case "LOCATIONS":
                        gameData.eventList.Add(new EventLog("<Locations>"));
                        if (!Locations(xe, gameData))
                        {
                            return false;
                        }
                        gameData.eventList.Add(new EventLog("</Locations>"));
                        break;

                    case "INIT":
                        gameData.eventList.Add(new EventLog("<Init>"));
                        if (!BBCBasicLines(xe, gameData, BBCBasicLine.lineType.Init))
                        {
                            return false;
                        }
                        gameData.eventList.Add(new EventLog("</Init>"));
                        break;

                    case "PREROOM":
                        gameData.eventList.Add(new EventLog("<PreRoom>"));
                        if (!BBCBasicLines(xe, gameData, BBCBasicLine.lineType.PreRoom))
                        {
                            return false;
                        }
                        gameData.eventList.Add(new EventLog("</PreRoom>"));
                        break;

                    case "HIGHPRIORITY":
                        gameData.eventList.Add(new EventLog("<HighPriority>"));
                        if (!BBCBasicLines(xe, gameData, BBCBasicLine.lineType.HighPriority))
                        {
                            return false;
                        }
                        gameData.eventList.Add(new EventLog("</HighPriority>"));
                        break;

                    case "LOWPRIORITY":
                        gameData.eventList.Add(new EventLog("<LowPriority>"));
                        if (!BBCBasicLines(xe, gameData, BBCBasicLine.lineType.LowPriority))
                        {
                            return false;
                        }
                        gameData.eventList.Add(new EventLog("</LowPriority>"));
                        break;
                }
            }

            return true;
        }

        private static bool BBCBasicLines(XElement xm, DataItems gameData, BBCBasicLine.lineType lt)
        {
            //remove < and > escape characters
            string text = xm.Value;
            text = text.Replace("&lt;", "<");
            text = text.Replace("&gt;", ">");
            string lineText = "";
            //breaks the variou user code chunks into separate lines and saves as BBC Basic lines
            //string[] lines = xm.Value.Trim().Split(new[] { "\n" }, StringSplitOptions.None);
            string[] lines = text.Trim().Split(new[] { "\n" }, StringSplitOptions.None);
            foreach (string line in lines)
            {
                lineText = line.Trim();

                if (lineText != null && lineText.Length >= 2 && lineText.Substring(0, 1) != "'")
                {
                    gameData.UserBBCBasicProgram.Add(new BBCBasicLine(0, lineText.Trim(), lt));
                    if (!BBCBasicFunctions.ParseLine(gameData.UserBBCBasicProgram[gameData.UserBBCBasicProgram.Count - 1], gameData))
                    {
                        return false;
                    };
                    gameData.eventList.Add(new EventLog(gameData.UserBBCBasicProgram[gameData.UserBBCBasicProgram.Count - 1].LineText()));
                }
            }

            return true;
        }

        private static bool Locations(XElement xm, DataItems gameData)
        {
            //steps through all the system messages in the section and calls a create message for each node found
            int len;
            int childLen;
            len = xm.Descendants().Count();

            for (int i = 0; i < len; i++)
            {
                XElement xe = xm.Descendants().ElementAt(i);
                childLen = xe.Descendants().Count();
                if (xe.Name.ToString().ToUpper() != "LOCATION") { gameData.eventList.Add(new EventLog("Invalid XML - element not AdventureData \\ Game \\ Locations \\ Location is : " + xe.Name)); return false; }
                if (!GenerateLocation(xe, gameData)) { return false; }
                i += childLen;
            }

            return true;
        }

        private static bool GenerateLocation(XElement xe, DataItems gameData)
        {
            //takes a message node and extracts the data (including the optional ID) and adds if not already present
            bool found = false;
            string IDString = "";
            int locationID;

            if (xe.HasAttributes)
            {
                //check for an ID value
                IEnumerable<XAttribute> attList = from at in xe.Attributes() select at;
                foreach (XAttribute att in attList)
                {
                    if (att.Name.ToString().ToUpper() == "ID") { IDString = att.Value; found = true; break; }
                }
            }

            if (!found)
            {
                Console.Write("The Location must has an ID attribute.");
                return false;
            }

            byte locationFlags;

            locationFlags = 0;

            locationID = Int32.Parse(IDString);
            gameData.locationList.Add(new Location(IDString, locationID, "", locationFlags));
            gameData.eventList.Add(new EventLog("<Location Id = " + IDString + ">"));
            //there can be message, object and action present in the location

            int len;
            int childLen;
            int messageNumber;
            int flagID;
            //int flagValue;

            len = xe.Descendants().Count();

            for (int i = 0; i < len; i++)
            {
                XElement xc = xe.Descendants().ElementAt(i);
                childLen = xc.Descendants().Count();

                switch (xc.Name.ToString().ToUpper())
                {

                    case "MESSAGE":
                        gameData.eventList.Add(new EventLog(xc));
                        messageNumber = GenerateMessage(xc, gameData.messageList);
                        if (messageNumber < 0)
                        {
                            return false;
                        }
                        gameData.locationList[gameData.locationList.Count() - 1].AddMessage(messageNumber);
                        break;

                    case "EXIT":
                        gameData.eventList.Add(new EventLog(xc));
                        if (!GenerateExit(xc, gameData.verbList, gameData.locationList, locationID, gameData))
                        {
                            return false;
                        }
                        break;

                    case "FLAG":
                        gameData.eventList.Add(new EventLog(xc));
                        if (xc.HasAttributes)
                        {
                            //check for an ID value
                            IEnumerable<XAttribute> attList = from at in xc.Attributes() select at;
                            foreach (XAttribute att in attList)
                            {
                                if (att.Name.ToString().ToUpper() == "ID")
                                {
                                    flagID = Int32.Parse(att.Value);
                                    //flagValue = Int32.Parse(xc.Value);
                                    if (flagID > 7)
                                    {
                                        gameData.eventList.Add(new EventLog("Flag ID cannot exceed 7 in a byte field."));
                                        return false;
                                    }
                                    locationFlags = GetFlagsByte(locationFlags, flagID, Int32.Parse(xc.Value));
                                    gameData.locationList[gameData.locationList.Count() - 1].SetFlags(locationFlags);
                                }
                                else
                                {
                                    gameData.eventList.Add(new EventLog("Incorrectly formed location information flag."));
                                    return false;
                                }
                            }
                        }

                        break;
                }

                i += childLen;
            }

            gameData.eventList.Add(new EventLog("</Location>"));
            return true;
        }

        private static byte GetFlagsByte(byte currentFlag, int bitToSet, int flagValue)
        {
            //update flags with current value

            if (flagValue == 1)
            {
                currentFlag = currentFlag.SetBit(bitToSet, true);
            }
            else
            {
                currentFlag = currentFlag.SetBit(bitToSet, false);
            }

            return currentFlag;
        }

        private static bool GenerateExit(XElement xe, Collection<Verb> verbList, Collection<Location> locationList, int locationID, DataItems gameData)

        {
            //takes a message node and extracts the data (including the optional ID) and adds if not already present

            int verbNumber = -1;
            int exitTo = -1;
            string verb;
            bool locked = false;

            if (xe.HasAttributes)
            {
                //check for an ID value
                IEnumerable<XAttribute> attList = from at in xe.Attributes() select at;
                foreach (XAttribute att in attList)
                {
                    if (att.Name.ToString().ToUpper() == "TO") { exitTo = Int32.Parse(att.Value); }
                    if (att.Name.ToString().ToUpper() == "LOCKED")
                    {
                        if (att.Value == "1") { locked = true; }
                    }
                }
            }

            if (exitTo < 0) { gameData.eventList.Add(new EventLog("No To value for current exit")); return false; }

            //get verb (adding it if necessary)
            try
            {
                XElement xc = xe.Descendants().ElementAt(0);
                verb = xc.Value.ToUpper();
                //check for object existing
                foreach (Verb v in verbList)
                {
                    if (v.VerbText().ToUpper() == verb)
                    {

                        if (verbNumber > 127) { gameData.eventList.Add(new EventLog("Exit verbs can only be in the first 127 verbs of the list.")); return false; }

                        verbNumber = v.IDNumber();
                        if (locked)
                        {
                            verbNumber += 128;
                        }//add 128 to verb number - this marks the direction as locked
                        break;
                    }
                }

                if (verbNumber < 0) { gameData.eventList.Add(new EventLog("Verb not recognised for current exit")); return false; }
            }
            catch
            {
                gameData.eventList.Add(new EventLog("No 'To' node in Exit."));
                return false;
            }

            locationList[locationID].AddExit(verbNumber, exitTo, locked);

            //exitList.Add(new ExitTo(verbNumber, exitTo));

            return true;
        }

        private static bool Objects(XElement xm, DataItems gameData)
        {
            //steps through all the system messages in the section and calls a create message for each node found
            int len;
            len = xm.Descendants().Count();

            for (int i = 0; i < len; i++)
            {
                XElement xe = xm.Descendants().ElementAt(i);
                gameData.eventList.Add(new EventLog(xe));
                //if (xe.Name.ToString().ToUpper() != "OBJECT") { gameData.eventList.Add(new EventLog("Invalid XML - element not AdventureData \\ Game \\ Objects \\ Object is : " + xe.Name); return false; }
                if (!GenerateObject(xe, gameData))
                { return false; }
            }

            return true;

        }


        private static bool GenerateObject(XElement xe, DataItems gameData)
        {
            //takes a message node and extracts the data (including the optional ID) and adds if not already present
            //bool found = false;
            string IDString = "";
            int value = 0;
            int weight = 0;
            int messageNumber = 0;
            int ID = 0;
            int locationID = -1;
            string message = "";
            bool found = false;

            if (xe.HasAttributes)
            {
                //check for an ID value
                IEnumerable<XAttribute> attList = from at in xe.Attributes() select at;
                foreach (XAttribute att in attList)
                {
                    if (att.Name.ToString().ToUpper() == "ID") { IDString = att.Value; }
                    if (att.Name.ToString().ToUpper() == "WEIGHT") { weight = Int32.Parse(att.Value); }
                    if (att.Name.ToString().ToUpper() == "VALUE") { value = Int32.Parse(att.Value); }
                    if (att.Name.ToString().ToUpper() == "NUMBER") { ID = Int32.Parse(att.Value); }
                    if (att.Name.ToString().ToUpper() == "LOCATION") { locationID = Int32.Parse(att.Value); }
                    if (att.Name.ToString().ToUpper() == "MESSAGE") { message = att.Value; }
                }
            }

            if (IDString == "") { gameData.eventList.Add(new EventLog("No ID value for current object")); return false; }

            if (locationID == -1) { gameData.eventList.Add(new EventLog("No Location value for current object")); return false; }

            try
            {

                //check for message existing
                foreach (Message m in gameData.messageList) { if (m.MessageText() == message) { found = true; messageNumber = m.IDNumber(); } }

                if (!found)
                {
                    //not found so add it
                    gameData.messageList.Add(new Message(IDString, gameData.messageList.Count + 1, message));
                    messageNumber = gameData.messageList.Count;
                }

            }
            catch
            {
                gameData.eventList.Add(new EventLog("No Message node in object."));
                return false;
            }

            //check for object existing

            if (!DataHelpers.ItemAlreadyExists(gameData, IDString))
            {
                //not found so add it                
                gameData.objectList.Add(new ObjectItem(IDString, ID, messageNumber, weight, value, locationID));
            }
            else
            { return false; }

            return true;
        }

        private static bool LocalVariables(XElement xm, DataItems gameData, bool isGlobal)
        {
            //steps through all the system messages in the section and calls a create message for each node found
            int len;
            len = xm.Descendants().Count();

            for (int i = 0; i < len; i++)
            {
                XElement xe = xm.Descendants().ElementAt(i);
                gameData.eventList.Add(new EventLog(xe));
                if (xe.Name.ToString().ToUpper() != "VAR") { gameData.eventList.Add(new EventLog("Invalid XML - element not AdventureData \\ Game \\ Variables \\ Var is : " + xe.Name)); return false; }
                if (!GenerateVariable(xe, gameData, isGlobal)) { return false; }
            }

            return true;
        }

        private static bool GenerateVariable(XElement xe, DataItems gameData, bool isGlobal)
        {
            //takes a variable node and extracts the data (including the optional ID) and adds if not already present
            string IDString = "";
            string text;
            int value = 0;  //if no value is supplied then assume it is 0

            if (xe.HasAttributes)
            {
                //check for an ID value
                IEnumerable<XAttribute> attList = from at in xe.Attributes() select at;
                foreach (XAttribute att in attList)
                {
                    if (att.Name.ToString().ToUpper() == "ID") { IDString = att.Value; }
                    if (att.Name.ToString().ToUpper() == "VALUE") { value = Int32.Parse(att.Value); }
                }
            }

            if (IDString == "") { gameData.eventList.Add(new EventLog("No ID value for current variable")); return false; }

            text = xe.Value;

            //check for variable existing and add if not

            if (!DataHelpers.ItemAlreadyExists(gameData, IDString))
            {
                //not found so add it
                if (!DataHelpers.AddVariable(gameData, IDString, text, value, isGlobal)) { return false; };

                return true;
            }
            else
            { return false; }

        }

        private static bool Nouns(XElement xm, DataItems gameData)
        {
            //steps through all the system messages in the section and calls a create message for each node found
            int len;
            len = xm.Descendants().Count();

            for (int i = 0; i < len; i++)
            {
                XElement xe = xm.Descendants().ElementAt(i);
                gameData.eventList.Add(new EventLog(xe));
                if (xe.Name.ToString().ToUpper() != "NOUN") { gameData.eventList.Add(new EventLog("Invalid XML - element not AdventureData \\ Game \\ Nouns \\ Noun is : " + xe.Name)); return false; }
                if (!GenerateNoun(xe, gameData)) { return false; }
            }

            return true;

        }

        private static bool GenerateNoun(XElement xe, DataItems gameData)
        {

            // takes a noun node and extracts the data (including the optional ID) and adds if not already present
            string IDString = "";
            string text;
            int iCount = 1;
            string[] verbs;
            if (gameData.nounList.Count > 0)
            {
                iCount = gameData.nounList[gameData.nounList.Count - 1].IDNumber() + 1;
            }

            if (xe.HasAttributes)
            {
                //check for an ID value
                IEnumerable<XAttribute> attList = from at in xe.Attributes() select at;
                foreach (XAttribute att in attList) { if (att.Name.ToString().ToUpper() == "ID") { IDString = att.Value; break; } }
            }

            text = xe.Value;

            //check for semi colon in the text and add multiple entries as appropriate
            if (text.IndexOf(";") > -1)
            { verbs = text.Split(";"); }
            else
            { verbs = new string[] { text }; }

            //check for verb list
            foreach (var verb in verbs)
            {

                if (!DataHelpers.ItemAlreadyExists(gameData, verb))
                {
                    //not found so add it
                    gameData.nounList.Add(new Noun(IDString, iCount, verb));
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        private static bool Adverbs(XElement xm, DataItems gameData)
        {
            //steps through all the system messages in the section and calls a create message for each node found
            int len;
            len = xm.Descendants().Count();

            for (int i = 0; i < len; i++)
            {
                XElement xe = xm.Descendants().ElementAt(i);
                gameData.eventList.Add(new EventLog(xe));
                if (xe.Name.ToString().ToUpper() != "ADVERB") { gameData.eventList.Add(new EventLog("Invalid XML - element not AdventureData \\ Game \\ Adverbs \\ Adverb is : " + xe.Name)); return false; }
                if (!GenerateAdverb(xe, gameData)) { return false; }
            }

            return true;

        }

        private static bool GenerateAdverb(XElement xe, DataItems gameData)
        {

            // takes an adverb node and extracts the data(including the optional ID) and adds if not already present
            string IDString = "";
            string text;
            int iCount = 1; //verbList.IDNumber(0);//.Count + 1;
            string[] verbs;
            if (gameData.adverbList.Count > 0)
            {
                iCount = gameData.adverbList[gameData.adverbList.Count - 1].IDNumber() + 1;
            }

            if (xe.HasAttributes)
            {
                //check for an ID value
                IEnumerable<XAttribute> attList = from at in xe.Attributes() select at;
                foreach (XAttribute att in attList) { if (att.Name.ToString().ToUpper() == "ID") { IDString = att.Value; break; } }
            }

            text = xe.Value;

            //check for semi colon in the text and add multiple entries as appropriate
            if (text.IndexOf(";") > -1)
            { verbs = text.Split(";"); }
            else
            { verbs = new string[] { text }; }

            //check for verb list
            foreach (var verb in verbs)
            {

                if (!DataHelpers.ItemAlreadyExists(gameData, verb))
                {
                    //not found so add it
                    gameData.adverbList.Add(new Adverb(IDString, iCount, verb));
                }
                else
                { return false; }

            }

            return true;
        }


        private static bool Verbs(XElement xm, DataItems gameData)
        {
            //steps through all the system messages in the section and calls a create message for each node found
            int len;
            len = xm.Descendants().Count();

            for (int i = 0; i < len; i++)
            {
                XElement xe = xm.Descendants().ElementAt(i);
                gameData.eventList.Add(new EventLog(xe));
                if (xe.Name.ToString().ToUpper() != "VERB") { gameData.eventList.Add(new EventLog("Invalid XML - element not AdventureData \\ Game \\ SystemVerbs \\ Verb is : " + xe.Name)); return false; }
                if (!GenerateVerb(xe, gameData)) { return false; }
            }

            return true;

        }

        private static bool GenerateVerb(XElement xe, DataItems gameData)
        {

            // takes a message node and extracts the data(including the optional ID) and adds if not already present
            string IDString = "";
            string text;
            int iCount = 1; //verbList.IDNumber(0);//.Count + 1;
            string[] verbs;
            if (gameData.verbList.Count > 0)
            {
                iCount = gameData.verbList[gameData.verbList.Count - 1].IDNumber() + 1;
            }

            if (xe.HasAttributes)
            {
                //check for an ID value
                IEnumerable<XAttribute> attList = from at in xe.Attributes() select at;
                foreach (XAttribute att in attList) { if (att.Name.ToString().ToUpper() == "ID") { IDString = att.Value; break; } }
            }

            text = xe.Value;

            //check for semi colon in the text and add multiple entries as appropriate
            if (text.IndexOf(";") > -1)
            { verbs = text.Split(";"); }
            else
            { verbs = new string[] { text }; }

            //check for verb list
            foreach (var verb in verbs)
            {

                if (!DataHelpers.ItemAlreadyExists(gameData, verb))
                {
                    //not found so add it
                    gameData.verbList.Add(new Verb(IDString, iCount, verb));
                }
                else
                { return false; }

            }

            return true;
        }

        private static bool SystemMessges(XElement xm, Collection<Message> messageList, DataItems gameData)
        {
            //steps through all the system messages in the section and calls a create message for each node found
            int len;
            len = xm.Descendants().Count();

            for (int i = 0; i < len; i++)
            {
                XElement xe = xm.Descendants().ElementAt(i);
                gameData.eventList.Add(new EventLog(xe));
                if (xe.Name.ToString().ToUpper() != "MESSAGE") { gameData.eventList.Add(new EventLog("Invalid XML - element not AdventureData \\ Game \\ SystemMessages \\ Message is : " + xe.Name)); return false; }
                if (GenerateMessage(xe, messageList) < 0) { return false; }
            }

            return true;
        }

        private static int GenerateMessage(XElement xe, Collection<Message> messageList)
        {
            //takes a message node and extracts the data (including the optional ID) and adds if not already present
            bool found = false;
            string IDString = "";
            string text;

            if (xe.HasAttributes)
            {
                //check for an ID value
                IEnumerable<XAttribute> attList = from at in xe.Attributes() select at;
                foreach (XAttribute att in attList) { if (att.Name.ToString().ToUpper() == "ID") { IDString = att.Value; break; } }
            }

            text = xe.Value;

            //check for message existing
            foreach (Message m in messageList) { if (m.MessageText() == text) { found = true; return m.IDNumber(); } }

            if (!found)
            {
                //not found so add it
                messageList.Add(new Message(IDString, messageList.Count + 1, text));
            }
            return messageList.Count;
        }

    }
}
