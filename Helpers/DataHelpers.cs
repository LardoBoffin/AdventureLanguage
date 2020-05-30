using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using AdventureLanguage.Data;

namespace AdventureLanguage.Helpers
{
    public static class DataHelpers
    {

        public static int AddMessage(Collection<Message> messageList, string text, string IDString)
        {

            foreach (Message m in messageList) { if (m.MessageText() == text) { return m.IDNumber(); } }

            //not found so add it
            messageList.Add(new Message(IDString, messageList.Count + 1, text));

            return messageList.Count;
        }

        public static bool AddVariable(DataItems gameData, string IDString, string text, int value, bool isGlobal)
        {

            if (VariableFound(gameData.varList, IDString) == -1)
            {
                //not found so add it
                gameData.varList.Add(new Variable(IDString, gameData.varList.Count+1, text, value, isGlobal));
            }
            else
            {
                gameData.eventList.Add(new EventLog("Variable '" + IDString + "' already exists."));
                return false;
            }

            return true;
        }

        public static int VariableFound(Collection<Variable> varList, string IDString)
        {
            foreach (Variable v in varList) { if (v.IDString().ToUpper() == IDString.ToUpper()) { return v.IDNumber(); } }
            return -1;
        }

        public static string VerbText(Collection<Verb> varList, int IDNUmber)
        {
            foreach (Verb v in varList) { if (v.IDNumber() == IDNUmber) { return v.VerbText(); } }

            return "Not found!";
        }

        public static int VerbNumber(Collection<Verb> varList, string verbText)
        {
            foreach (Verb v in varList) { if (v.VerbText().ToUpper() == verbText.ToUpper()) { return v.IDNumber(); } }

            return -1;
        }

        public static int AdverbNumber(Collection<Adverb> varList, string verbText)
        {
            foreach (Adverb v in varList) { if (v.AdverbText().ToUpper() == verbText.ToUpper()) { return v.IDNumber(); } }

            return -1;
        }

        public static int NounNumber(Collection<Noun> nounList, string nounText)
        {
            foreach (Noun n in nounList) { if (n.NounText() == nounText) { return n.IDNumber(); } }

            return -1;
        }

        public static int ObjectFound(Collection<ObjectItem> objectList, string IDString)
        {
            foreach (ObjectItem o in objectList) { if (o.IDString().ToUpper() == IDString.ToUpper()) { return o.IDNumber(); } }
            return -1;
        }

        public static bool ItemAlreadyExists(DataItems gameData, string IDString)
        {


            //check to see if this @ is in use anywhere else
            int foundNumber;
            foundNumber = DataHelpers.VerbNumber(gameData.verbList, IDString);
            if (foundNumber > -1)
            {
                gameData.eventList.Add(new EventLog("The item '" + IDString + "' already exists as a Verb"));
                return true;
            }

            foundNumber = DataHelpers.ObjectFound(gameData.objectList, IDString);
            if (foundNumber > -1)
            {
                gameData.eventList.Add(new EventLog("The item '" + IDString + "' already exists as an Object"));
                return true;
            }

            foundNumber = DataHelpers.NounNumber(gameData.nounList, IDString);
            if (foundNumber > -1)
            {
                gameData.eventList.Add(new EventLog("The item '" + IDString + "' already exists as a Noun"));
                return true;
            }

            foundNumber = DataHelpers.AdverbNumber(gameData.adverbList, IDString);
            if (foundNumber > -1)
            {
                gameData.eventList.Add(new EventLog("The item '" + IDString + "' already exists as an Adverb"));
                return true;
            }

            return false;

        }

        public static char[] ReverseString(string s)
        {
            char[] arr = s.ToCharArray();
            Array.Reverse(arr);
            return arr;
        }
    }
}
