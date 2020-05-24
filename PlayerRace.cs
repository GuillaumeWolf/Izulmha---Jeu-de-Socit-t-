using System;
using System.Collections.Generic;
using System.Text;

namespace Jeu_de_Socitété___Izulmha
{
    class Races
    {
        public string Name;

        public static List<Races> RacePoss = new List<Races>();
        public static void FillRaceList()
        {
            RacePoss.Add(new Elf());
            RacePoss.Add(new Nain());
            RacePoss.Add(new Orc());
            RacePoss.Add(new Creature());
            RacePoss.Add(new Poisson());
            RacePoss.Add(new Ange());
        }

    }


    class Elf : Races
    {
        public Elf()
        {
            Name = "Elf";
        }
    }
    class Nain : Races
    {
        public Nain()
        {
            Name = "Dwarf";
        }
    }
    class Orc : Races
    {
        public Orc()
        {
            Name = "Orc";
        }
    }
    class Creature : Races
    {
        public Creature()
        {
            Name = "Creature";
        }
    }
    class Poisson : Races
    {
        public Poisson()
        {
            Name = "Fish";
        }
    }
    class Ange : Races
    {
        public Ange()
        {
            Name = "Angel";
        }
    }

}
