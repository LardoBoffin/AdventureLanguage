using System;
using System.Collections.Generic;
using System.Text;

namespace AdventureLanguage.Data
{
    public class Variable
    {

        private readonly string _iDString;

        private readonly int _iDNumber;

        private readonly string _variableText;

        private readonly int _initValue;

        private readonly bool _global;

        public Variable(string idString, int idNumber, string text, int value, bool global)
        {
            _iDString = idString;
            _iDNumber = idNumber;
            _variableText = text;
            _initValue = value;
            _global = global;
        }

        public string IDString()
        {
            return _iDString;
        }

        public int IDNumber()
        {
            return _iDNumber;
        }

        public int InitValue()
        {
            return _initValue;
        }

        public string VariableText()
        {
            return _variableText;
        }

        public bool IsGlobal()
        {
            return _global;
        }
    }
}
