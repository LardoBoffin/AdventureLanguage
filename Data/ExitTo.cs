using System;
using System.Collections.Generic;
using System.Text;

namespace AdventureLanguage.Data
{
    public class ExitTo
    {

        private readonly int _directionVerb;

        private readonly int _locationID;

        private readonly bool _locked; 

        public ExitTo(int verb, int location, bool locked)
        {
            _directionVerb = verb;
            _locationID = location;
            _locked = locked;
        }

        public int Verb()
        {
            return _directionVerb;
        }

        public int LocationID()
        {
            return _locationID;
        }

        public bool Locked()
        {
            return _locked;
        }

    }
}
