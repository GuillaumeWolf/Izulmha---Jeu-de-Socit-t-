using System;
using System.Collections.Generic;
using System.Text;

namespace Jeu_de_Socitété___Izulmha
{
    class Classes
    {
        public string Name;

        public static List<Classes> ClassPoss = new List<Classes>();
        public static void FillClassList()
        {
            ClassPoss.Add(new Pretre());
            ClassPoss.Add(new Sorcier());
            ClassPoss.Add(new Chevalier());
            ClassPoss.Add(new Archer());
            ClassPoss.Add(new Traqueur());
            ClassPoss.Add(new Dresseur());
        }
    }




    class Pretre : Classes
    {
        public Pretre ()
        {
            Name = "Priest";
        }
    }

    class Sorcier : Classes
    {
        public Sorcier()
        {
            Name = "Sorcer";
        }
    }
    class Chevalier : Classes
    {
        public Chevalier()
        {
            Name = "Chevalier";
        }
    }
    class Archer : Classes
    {
        public Archer()
        {
            Name = "Archer";
        }
    }
    class Traqueur : Classes
    {
        public Traqueur()
        {
            Name = "Tracker";
        }
    }
    class Dresseur : Classes
    {
        public Dresseur()
        {
            Name = "Trainer";
        }
    }
}
