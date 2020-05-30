using System;
using System.Collections.Generic;
using System.Text;

namespace AdventureLanguage.Data
{
    public class ObjectItem
    {
        private readonly string iDString;

        private readonly int iDNumber;

        private readonly int _messageNumber;

        private readonly int _weight;

        private readonly int _value;

        private readonly int _locationNumber;

        public ObjectItem(string idString, int idNumber, int messageNumber, int weight, int value, int locationNumber)
        {
            iDString = idString;
            iDNumber = idNumber;
            _messageNumber = messageNumber;
            _weight = weight;
            _value = value;
            _locationNumber = locationNumber;
        }

        public string IDString()
        {
            return iDString;
        }

        public int IDNumber()
        {
            return iDNumber;
        }

        public int Value()
        {
            return _value;
        }

        public int Weight()
        {
            return _weight;
        }

        public int MessageNumber()
        {
            return _messageNumber;
        }

        public int LocationNumber()
        {
            return _locationNumber;
        }

    }
}
