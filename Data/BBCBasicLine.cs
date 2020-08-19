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
        private readonly int _lineType;

        public enum LineType
        {
            SourceLine,
            PreRoom,
            Init,
            HighPriority,
            LowPriority,
            UserCode,
            RoomMsg
        }

        public BBCBasicLine(int OriginalLineNumber, string LineText, LineType Type)
        {
            _originalLineNumber = OriginalLineNumber;
            _lineText = LineText;
            _lineType = (int)Type;
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
