using System;
using System.Collections.Generic;
using System.Text;

namespace Jeu_de_Socitété___Izulmha
{
    class PilesdeCarte
    {
        public List<Object> PileObject = new List<Object>();
        public List<Spell> PileSpell = new List<Spell>();
        public List<Object> DeffausseObject = new List<Object>();
        public List<Spell> DeffausseSpell = new List<Spell>();

        public PilesdeCarte()
        {
            for (int i = 0; i < 20; i++)
            {
                PileObject.Add(new BasicSword());
            }
            for (int i = 0; i < 5; i++)
            {
                DeffausseObject.Add(new BasicSword());
            }
        }

        //Methodes
        public Carte GetRandomCard(string name)
        {
            int x;
            Carte c = null;
            if (name == "Object")
            {
                if(PileObject.Count == 0)
                {
                    foreach(var c2 in DeffausseObject)
                    {
                        PileObject.Add(c2);
                    }
                    DeffausseObject.Clear();
                    Console.WriteLine("You fill the Object Stack. There is {0} cards left. in the deffause : {1}.", PileObject.Count, DeffausseObject.Count);
                }
                if (PileObject.Count == 0)
                {
                    Console.WriteLine("There isn't cards in Object Stack");
                    return null;
                }

                    x = Aleatoire.RandomInt(PileObject.Count);
                c = PileObject[x];
                PileObject.RemoveAt(x);
            }
            else if (name == "Spell")
            {
                if (PileSpell.Count == 0)
                {
                    foreach (var c2 in DeffausseSpell)
                    {
                        PileSpell.Add(c2);
                    }
                    DeffausseSpell.Clear();
                    Console.WriteLine("You fill the Spell Stack. There is {0} cards left. in the deffause : {1}.", PileObject.Count, DeffausseSpell.Count);
                }
                if (PileSpell.Count == 0)
                {
                    Console.WriteLine("There isn't cards in Spell Stack");
                    return null;
                }
                x = Aleatoire.RandomInt(PileSpell.Count);
                c = PileSpell[x];
                PileSpell.RemoveAt(x);
            }
            Console.Write("You draw a {0}. ", c.Name);
            Console.Write("There is {1} cards.", PileObject.Count);
            Console.WriteLine();
            return c;
        }
    }
}
