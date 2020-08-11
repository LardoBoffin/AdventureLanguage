using System;
using System.Collections.Generic;
using System.Text;

namespace AdventureLanguage.Data
{
    public class ProceduresAndFunctions
    {

        private readonly string functionNameText;

        private string replaceNameText;

        private readonly Type functionType; //1=procedure, 2 = function

        private readonly int iDNumber;

        private int countOfUsage = 0;

        public enum Type
        {
            Procedure,
            Function
        }
        public ProceduresAndFunctions(string functionName, Type type, int IDNumber)
        {
            functionNameText = functionName;

            functionType = type;

            iDNumber = IDNumber;

        }

        public string FunctionName()
        {
            return functionNameText;
        }

        public int IDNumber()
        {
            return iDNumber;
        }

        public Type FunctionType()
        {
            return functionType;
        }

        public int CountOfUsage
        {
            get { return countOfUsage; }   // get method
            set { countOfUsage = value; }  // set method
        }

        public string ReplaceNameText
        {
            get { return replaceNameText; }   // get method
            set { replaceNameText = value; }  // set method
        }
    }
}
