using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace Jeu_de_Socitété___Izulmha
{
    class PilesdeCarte
    {
        public List<Object> PileObject = new List<Object>();
        public List<Spell> PileSpell = new List<Spell>();
        public List<Monster> PileMonster = new List<Monster>();

        public List<Object> DeffausseObject = new List<Object>();
        public List<Spell> DeffausseSpell = new List<Spell>();
        public List<Monster> DeffausseMonster = new List<Monster>();

        public PilesdeCarte()
        {
            //Objet
            for (int i = 0; i < 1; i++)
            {
            }
            for (int i = 0; i < 2; i++)
            {
                PileObject.Add(new DoubleSword());
                PileObject.Add(new MagicGauntlet());
            }
            for (int i = 0; i < 3; i++)
            {
                PileObject.Add(new BasicSword());
                PileObject.Add(new MagicOrb());
                PileObject.Add(new SmallShield());
                PileObject.Add(new MagicShield());
                PileObject.Add(new SmallArmor());
                PileObject.Add(new MagicArmor());
                PileObject.Add(new BasicHelmet());
                PileObject.Add(new StrengthCollar());
                PileObject.Add(new MagicRing());
            }
            
            //Spell
            for (int i = 0; i < 1; i++)
            {
                PileSpell.Add(new AntiWeaponSpell());
                PileSpell.Add(new AntiArmorSpell());
                PileSpell.Add(new AntiHelmetSpell());
                PileSpell.Add(new AntiAmuletteSpell());
                PileSpell.Add(new AntiPetsSpell());
            }
            for (int i = 0; i < 2; i++)
            {

            }
            for (int i = 0; i < 3; i++)
            {
                PileSpell.Add(new CounterSpell()); 
                PileSpell.Add(new MonsterBoostCommon());
                PileSpell.Add(new HealFlask());
            }
            for (int i = 0; i < 4; i++)
            {
                PileSpell.Add(new StrengthFlask());
                PileSpell.Add(new PowerFlask());
                PileSpell.Add(new ManaFlask());
            }
            //Monster
            for (int i = 0; i < 20; i++)
            {
                PileMonster.Add(new Gobelin());
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
                    Console.WriteLine("You fill the Object Stack. There is {0} cards left. And in the deffause : {1}.", PileObject.Count, DeffausseObject.Count);
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
                    Console.WriteLine("You fill the Spell Stack. There is {0} cards left. And in the deffause : {1}.", PileSpell.Count, DeffausseSpell.Count);
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
            Console.WriteLine();
            return c;
        }
    }
}
