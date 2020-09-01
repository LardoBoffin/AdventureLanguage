using System;
using System.Collections.Generic;
using System.Text;


namespace AdventureLanguage.Tokeniser
{
    public static class PopulateTokens
    {
        public static void PopulateTokenList()
        {
            //create the list of tokens and associated flags

            Byte tokenVal = 0x80;

            TokenList.tokenList.Add(new Token(tokenVal, "AND"));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "DIV"));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "EOR"));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "MOD"));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "OR"));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "ERROR", false, false, true, false, false, false, false));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "LINE"));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "OFF"));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "STEP"));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "SPC"));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "TAB("));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "ELSE", false, false, true, false, true, false, false));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "THEN", false, false, true, false, true, false, false));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "line_no."));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "OPENIN"));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "PTR", true, true, false, false, false, false, true));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "PAGE", true, true, false, false, false, false, true));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "TIME", true, true, false, false, false, false, true));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "LOMEM", true, true, false, false, false, false, true));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "HIMEM", true, true, false, false, false, false, true));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "ABS"));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "ACS"));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "ADVAL"));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "ASC"));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "ASN"));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "ATN"));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "BGET", true, false, false, false, false, false, false));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "COS"));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "COUNT", true, false, false, false, false, false, false));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "DEG"));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "ERL", true, false, false, false, false, false, false));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "ERR", true, false, false, false, false, false, false));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "EVAL"));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "EXP"));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "EXT", true, false, false, false, false, false, false));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "FALSE", true, false, false, false, false, false, false));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "FN", false, false, false, false, true, false, false));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "GET"));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "INKEY"));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "INSTR"));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "INT"));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "LEN"));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "LN"));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "LOG"));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "NOT"));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "OPENUP"));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "OPENOUT"));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "PI", true, false, false, false, false, false, false));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "POINT("));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "POS", true, false, false, false, false, false, false));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "RAD"));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "RND", true, false, false, false, false, false, false));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "SGN"));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "SIN"));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "SQR"));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "TAN"));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "TO"));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "TRUE", true, false, false, false, false, false, false));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "USR"));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "VAL"));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "VPOS", true, false, false, false, false, false, false));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "CHR$", true, false, false, false, false, false, false));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "GET$"));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "INKEY$"));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "LEFT$("));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "MID$("));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "RIGHT$("));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "STR$("));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "STRING$("));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "EOF", true, false, false, false, false, false, false));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "AUTO", false, false, false, false, true, false, false));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "DELETE", false, false, false, false, true, false, false));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "LOAD", false, true, false, false, false, false, false));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "LIST", false, false, false, false, true, false, false));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "NEW", true, false, false, false, false, false, false));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "OLD", true, false, false, false, false, false, false));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "RENUMBER", false, false, false, false, true, false, false));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "SAVE", false, true, false, false, false, false, false));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "PUT"));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "PTR"));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "PAGE"));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "TIME"));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "LOMEM"));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "HIMEM"));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "SOUND", false, true, false, false, false, false, false));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "BPUT", true, true, false, false, false, false, false));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "CALL", false, true, false, false, false, false, false));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "CHAIN", false, true, false, false, false, false, false));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "CLEAR", true, false, false, false, false, false, false));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "CLOSE", true, true, false, false, false, false, false));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "CLG", true, false, false, false, false, false, false));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "CLS", true, false, false, false, false, false, false));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "DATA", false, false, false, false, false, true, false));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "DEF"));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "DIM", false, true, false, false, false, false, false));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "DRAW", false, true, false, false, false, false, false));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "END", true, false, false, false, false, false, false));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "ENDPROC", true, false, false, false, false, false, false));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "ENVELOPE", false, true, false, false, false, false, false));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "FOR", false, true, false, false, false, false, false));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "GOSUB", false, true, true, false, false, false, false));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "GOTO", false, true, true, false, false, false, false));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "GCOL", false, true, false, false, false, false, false));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "IF", false, true, false, false, false, false, false));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "INPUT", false, true, false, false, false, false, false));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "LET", false, false, true, false, false, false, false));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "LOCAL", false, true, false, false, false, false, false));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "MODE", false, true, false, false, false, false, false));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "MOVE", false, true, false, false, false, false, false));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "NEXT", false, true, false, false, false, false, false));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "ON", false, false, false, false, false, false, false));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "VDU", false, true, false, false, false, false, false));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "PLOT", false, true, false, false, false, false, false));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "PRINT", false, true, false, false, false, false, false));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "PROC", false, true, false, true, false, false, false));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "READ", false, true, false, false, false, false, false));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "REM", false, false, false, false, false, true, false));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "REPEAT"));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "REPORT", true, false, false, false, false, false, false));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "RESTORE", false, true, false, false, true, false, false));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "RETURN", true, false, false, false, false, false, false));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "RUN", true, false, false, false, false, false, false));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "STOP", true, false, false, false, false, false, false));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "COLOUR", false, true, false, false, false, false, false));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "TRACE", false, true, false, false, true, false, false));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "UNTIL", false, true, false, false, false, false, false));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "WIDTH", false, true, false, false, false, false, false));
            TokenList.tokenList.Add(new Token(tokenVal += 1, "OSCLI", false, true, false, false, false, false, false));

        }
    }
}
