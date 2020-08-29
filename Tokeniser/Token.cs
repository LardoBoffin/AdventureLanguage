using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Tracing;
using System.Text;

namespace AdventureLanguage.Tokeniser
{
    public class Token
    {

        private readonly byte _token;

        private readonly string _keyword;

        private readonly bool _conditional;

        private readonly bool _middle;

        private readonly bool _start;

        private readonly bool _FNPROC;

        private readonly bool _line;

        private readonly bool _REM;

        private readonly bool _pseudo;

        public Token(byte token, string keyword, bool conditional, bool middle, bool start, bool FNPROC, bool line, bool REM, bool pseudo)
        {
            _token = token;
            _keyword = keyword;
            _conditional = conditional;
            _middle = middle;
            _start = start;
            _FNPROC = FNPROC;
            _line = line;
            _REM = REM;
            _pseudo = pseudo;

        }

        public byte TokenValue()
        {
            return _token;
        }

        public string Keyword()
        {
            return _keyword;

        }
        public bool Conditional()
        {
            return _conditional;
        }

        public bool Middle()
        {
            return _middle;
        }

        public bool Start()
        {
            return _start;
        }

        public bool FNPROC()
        {
            return _FNPROC;
        }

        public bool Line()
        {
            return _line;
        }

        public bool REM()
        {
            return _REM;
        }
        public bool Pseudo()
        {
            return _pseudo;
        }

    }
}
