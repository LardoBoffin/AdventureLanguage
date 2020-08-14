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

        private readonly int iHostility;        //0 = 0% chance of attack

        private readonly int iHealth;

        private readonly int iToHit;

        private readonly int iDamage;

        private readonly int iWandering;        //0 to 100. 0 = does not wander. 75 = 75% chance of moving in any turn

        private readonly int iLocation;

        private byte _flags;                    //8 bit flags in one byte that determine behaviour

        public NPC(string noun, int nounNumber, int idNumber, int hostility, int health, int toHit, int damage, int wandering, int location)
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

        public byte Flags()
        {
            return _flags;
        }

        public void SetFlags(byte flags)
        {
            _flags = flags;
        }


    }
}
