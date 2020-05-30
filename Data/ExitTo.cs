using System;
using System.Collections.Generic;
using System.Text;

namespace AdventureLanguage.Data
{
    public class ExitTo
    {

        private readonly int _directionVerb;

        private readonly int _locationID;

        public ExitTo(int verb, int location)
        {
            _directionVerb = verb;
            _locationID = location;
        }

        public int Verb()
        {
            return _directionVerb;
        }

        public int LocationID()
        {
            return _locationID;
        }

    }
}
