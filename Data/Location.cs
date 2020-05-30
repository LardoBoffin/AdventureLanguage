﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AdventureLanguage.Data
{
    public class Location
    {
        private readonly string iDString;

        private readonly int iDNumber;

        private readonly string messageText;

        private List<int> messages = new List<int>();

        public Collection<ExitTo> exitList = new Collection<ExitTo>();

        public void AddExit(int verb, int location)
        {
            exitList.Add(new ExitTo(verb, location));
        }

        public Location(string idString, int idNumber, string text)
        {
            iDString = idString;
            iDNumber = idNumber;
            messageText = text;
        }

        public void AddMessage(int messageID)
        {
            messages.Add(messageID);
        }

        public int Message(int number)
        {
            return messages[number-1];       
        }

        public ExitTo ExitItem(int number)
        {
            return exitList[number];
        }

        public List<int> Messages()
        {
            return messages;
        }

        public Collection<ExitTo> Exits()
        {
            return exitList;
        }

        public string IDString()
        {
            return iDString;
        }

        public int IDNumber()
        {
            return iDNumber;
        }

        public string MessageText()
        {
            return messageText;
        }
    }
}
