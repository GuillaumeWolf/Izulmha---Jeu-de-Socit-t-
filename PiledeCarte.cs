using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json;

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
                //Legendary
                PileObject.Add(new TrackingChain());
                PileObject.Add(new ArthurLegendarySword());
                PileObject.Add(new Exoskeleton());
                PileObject.Add(new HatofManipulation());
                PileObject.Add(new GhostlySaddle());
                PileObject.Add(new SpellHole());

            }
            for (int i = 0; i < 2; i++)
            {
                //Common
                    //Weapon
                PileObject.Add(new GreekSword());
                PileObject.Add(new SimpleDagger());
                PileObject.Add(new BewitchedAx());
                PileObject.Add(new MagicOrb());
                PileObject.Add(new DoubleSword());
                PileObject.Add(new MagicGauntlet());
                PileObject.Add(new GreekShield());
                PileObject.Add(new SmallShield());
                PileObject.Add(new MagicShield());
                    //Armor
                PileObject.Add(new GreekArmor());
                PileObject.Add(new MagicChestplate());
                PileObject.Add(new Chestplate());
                    //Helemt
                PileObject.Add(new GreekHelmet());
                PileObject.Add(new BasicHelmet());
                PileObject.Add(new MagicHelmet());
                    //Shoe
                PileObject.Add(new GreekShoes());
                PileObject.Add(new NormalShoes());

                //Rare
                    //Weapon
                PileObject.Add(new VikingSword());
                PileObject.Add(new BigMagicOrb());
                PileObject.Add(new BigSpear());
                PileObject.Add(new DoubleManaSword());
                PileObject.Add(new VikingShield());
                PileObject.Add(new GiantShield());
                    //Armor
                PileObject.Add(new VikingArmor());
                PileObject.Add(new ReinforcedChestplate());
                PileObject.Add(new MagicArmor());
                    //Helmet
                PileObject.Add(new VikingHelmet());
                PileObject.Add(new IronHat());
                    //Shoes
                PileObject.Add(new VikingShoes());
                PileObject.Add(new SpeedShoes());
                PileObject.Add(new ArmedShoes());
                    //Amulette
                PileObject.Add(new WitchBracelet());
                PileObject.Add(new LeaderCape());
                    //Pets
                PileObject.Add(new VikingBear());
                PileObject.Add(new WolfPack());
                PileObject.Add(new WildParrot());
                PileObject.Add(new Reel());

                //Mythical
                    //Weapon
                PileObject.Add(new KnifeInMithril());
                PileObject.Add(new GodKillerSword());
                PileObject.Add(new MillennialMagicBook());
                PileObject.Add(new ShieldInMithril());
                PileObject.Add(new BigWall());
                    //Armor
                PileObject.Add(new ChainmailInMithril());
                PileObject.Add(new BreatplateOfSelfConfident());
                    //Helmet
                PileObject.Add(new HelmetInMithril());
                PileObject.Add(new MagicHat());
                //Shoes
                PileObject.Add(new ShoesInMithril());
                PileObject.Add(new FlyingShoes());
                    //Amulette
                PileObject.Add(new BeltInMithril());
                PileObject.Add(new TheGraal());
                PileObject.Add(new PetsTotem());
                    //Pets
                PileObject.Add(new GreatWhiteWolf());
            }
            for (int i = 0; i < 3; i++)
            {
                //Common
                    //Amulette
                PileObject.Add(new GreekNecklace());
                PileObject.Add(new StrengthCollar());
                PileObject.Add(new MagicRing());
                    //Pets
                PileObject.Add(new SmallWolf());
                PileObject.Add(new TotemOfStrength());
                PileObject.Add(new GratefulTraveler());
            }
            
            //Spell
            for (int i = 0; i < 1; i++)
            {
                PileSpell.Add(new AntiWeaponSpell());
                PileSpell.Add(new AntiArmorSpell());
                PileSpell.Add(new AntiHelmetSpell());
                PileSpell.Add(new AntiAmuletteSpell());
                PileSpell.Add(new DamagePetsSpell());
            }
            for (int i = 0; i < 2; i++)
            {

            }
            for (int i = 0; i < 3; i++)
            {
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
            for (int i = 0; i < 1; i++)
            {
                PileMonster.Add(new Slime());
                PileMonster.Add(new GiantTortoise());
            }
            for (int i = 0; i < 2; i++)
            {
                PileMonster.Add(new GreenGobelin());
                PileMonster.Add(new GreatSandScorpion());
                PileMonster.Add(new GraveHaunt());
                PileMonster.Add(new MutantRats());
                PileMonster.Add(new BrigandGroup());
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
            else if (name == "Monster")
            {
                if (PileSpell.Count == 0)
                {
                    foreach (var c2 in DeffausseMonster)
                    {
                        PileMonster.Add(c2);
                    }
                    DeffausseSpell.Clear();
                    Console.WriteLine("You fill the Monster Stack. There is {0} cards left. And in the deffause : {1}.", PileMonster.Count, DeffausseMonster.Count);
                }
                if (PileMonster.Count == 0)
                {
                    Console.WriteLine("There isn't cards in Monster Stack");
                    return null;
                }

                x = Aleatoire.RandomInt(PileMonster.Count);
                c = PileMonster[x];
                PileSpell.RemoveAt(x);
                return c;
            }
            Console.Write("You draw a {0}. ", c.Name);
            Console.WriteLine();
            return c;
        }
    }
}
