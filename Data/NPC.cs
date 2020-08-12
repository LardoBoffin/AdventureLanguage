using System;
using System.Collections.Generic;
using System.Text;

namespace AdventureLanguage.Data
{
    public class NPC
    {

        private readonly string sNoun;

        public readonly int iNoun;

        private readonly int iDNumber;

        private readonly int iHostility;

        private readonly int iHealth;

        private readonly int iToHit;

        private readonly int iDamage;

        private readonly int iWandering;

        private readonly int iLocation;

        public NPC(string noun,int nounNumber, int idNumber, int hostility, int health, int toHit, int damage, int wandering, int location)
        {
            sNoun = noun;
            iNoun = nounNumber;
            iDNumber = idNumber;
            iHostility = hostility;
            iHealth = health;
            iToHit = toHit;
            iDamage = damage;
            iWandering = wandering;
            iLocation = location;
        }

        public int Location()
        {
            return iLocation;
        }

        public int NounNumber()
        {
            return iNoun;
        }

        public int Wandering()
        {
            return iWandering;
        }

        public int Damage()
        {
            return iDamage;
        }

        public int Health()
        {
            return iHealth;
        }


        public int ToHit()
        {
            return iToHit;
        }


        public string Noun()
        {
            return sNoun;
        }

        public int IDNumber()
        {
            return iDNumber;
        }

        public int Hostility()
        {
            return iHostility;
        }


    }
}
