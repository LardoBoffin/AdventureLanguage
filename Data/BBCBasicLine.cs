using System;
using System.Collections.Generic;
using System.Text;

namespace AdventureLanguage.Data
{
    public class BBCBasicLine
    {
        private int _originalLineNumber;
        private int _newLineNumber;
        private string _lineText;
        private int _lineType;

        public enum lineType
        {
            SourceLine,
            PreRoom,
            Init,
            HighPriority,
            LowPriority,
            UserCode
        }

        public BBCBasicLine(int originalLineNumber, string lineText, lineType type)
        {
            _originalLineNumber = originalLineNumber;
            _lineText = lineText;
            _lineType = (int)type;
        }

        public int GetLineType()
        {
            return _lineType;
        }

        public void SetLineNumber(int number)
        {
            _originalLineNumber = number;
        }

        public void SetNewLineNumber(int number)
        {
            _newLineNumber = number;
        }

        public void SetLineText(string text)
        {
            _lineText = text;
        }

        public string LineText()
        {
            return _lineText;
        }

        public int OriginalLineNumber()
        {
            return _originalLineNumber;
        }

        public int NewLineNumber()
        {
            return _newLineNumber;
        }
    }
}
