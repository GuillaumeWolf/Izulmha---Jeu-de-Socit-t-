using System;
using System.Collections.Generic;
using System.Globalization;
using System.Numerics;
using System.Text;

namespace Jeu_de_Socitété___Izulmha
{
    class Classe
    {
        public string Name;
        public int StrengthBonus = 0;
        public int PowerBonus = 0;
        public int ManaBonus = 0;
        public int ClassLevel = 1;
        
        //Specialité de la class
        // Niveau de Class 1
        public string Ability;
        //Condition de level up
        public string LevelUpCondition;
        //Niveau de Class 2
        public string NewAbility;




        public static List<Classe> ClassPoss = new List<Classe>();
        public static void FillClassList()
        {
            ClassPoss.Add(new Priest());
            ClassPoss.Add(new Sorcier());
            ClassPoss.Add(new Chevalier());
            ClassPoss.Add(new Archer());
            ClassPoss.Add(new Traqueur());
            ClassPoss.Add(new Dresseur());
        }

        public virtual void ApplayAbilitiyStartGame(Player p1)
        {
            Console.WriteLine("Player {0}:", p1.PlayerNumber);
        }

        public virtual Archer.wantToDraw ApplayAbilitiyDrawing (Player p1)
        {
            return Archer.wantToDraw.YES;
        }

        public virtual PlayCardResult ApplayAbilitiyPlayingWeapon(Player p1)
        {
            return PlayCardResult.OK;
        }

        public virtual void ApplyAbilityStartFight(Player p1)
        {
            
        }

        public virtual int ApplyAbilityCountStrength(Player p1)
        {
            return 0;
        }

        public virtual Monster ApplayAbilityDrawMonster(Player p1, Monster m1, PilesdeCarte pilesdeCarte)
        {
            return m1;
        }



        public virtual void AddFollower(Player p1)
        {
            p1._petsPlayed.Add(new Follower());
        }

    }




    class Priest : Classe
    {
        private int _followersSummonedCount = 0;

        public Priest ()
        {
            Name = "Priest";
            PowerBonus = 1;
            ManaBonus = 1;

            // Niveau de Class 1
            Ability = "When the Priest fight a Monster, Summoned a Follower. ";
            //Condition de level up
            LevelUpCondition = "Summon the 6th Follower. ";
            //Niveau de Class 2
            NewAbility = "When the Priest fight a Monster, Summoned a Follower. ";
        }
        public override void ApplyAbilityStartFight(Player p1)
        {
            base.ApplyAbilityStartFight(p1);
            if (ClassLevel == 1)
            {
                AddFollower(p1);
                Console.WriteLine("You got a Follower for this fight.");
            }
            else if (ClassLevel == 2)
            {
                AddFollower(p1);
                AddFollower(p1);
                Console.WriteLine("You got two Followers for this fight.");
            }
        }
        public override void AddFollower(Player p1)
        {
            base.AddFollower(p1);
            _followersSummonedCount++;
            if (_followersSummonedCount == 6)
            {
                ClassLevel++;
                Console.WriteLine("You passed Class Level 2.");
            }
        }



    }

    class Sorcier : Classe
    {
        public int ManaCost = 3;
        public int TimeCastSpell = 2;
        public bool HasUsedPower = false;
        public bool IsUsingPower = false;
        public int SpellCastCount = 0;

        public Sorcier()
        {
            Name = "Sorcer";
            PowerBonus = 2;

            // Niveau de Class 1
            Ability = "One time a turn, the Sorcer can choose to spent 3 Mana for that his next Spell will appl his effect twice. ";
            //Condition de level up
            LevelUpCondition = "Cast Spell 6 times";
            //Niveau de Class 2
            NewAbility = "One time a turn, the Sorcer can choose to spent 1 Mana for that his next Spell will appl his effect thrice. ";
        }
    }

    class Chevalier : Classe
    {
        public Chevalier()
        {
            Name = "Knight";

            // Niveau de Class 1
            Ability = "Each Weapons, Shield, Armor, Helmet and Shoes give him 1 Strength bonus. ";
            //Condition de level up
            LevelUpCondition = "Have a Weapon/Shield, a Armor, a Helmet and Shoes. ";
            //Niveau de Class 2
            NewAbility = "When you draw a monster card, draw another one and choose between the two.";
        }

        public override int ApplyAbilityCountStrength(Player p1)
        {
            int StrengthSupp = 0;
            StrengthSupp += p1._weaponsPlayed.Count;
            if (p1._armorPlayed != null)
            {
                StrengthSupp += 1;
            }
            if (p1._helmetPlayed != null)
            {
                StrengthSupp += 1;
            }
            if (p1._shoesPlayed != null)
            {
                StrengthSupp += 1;
            }
            return StrengthSupp;
        }

        public override Monster ApplayAbilityDrawMonster(Player p1, Monster m1, PilesdeCarte pilesdeCarte)
        {
            base.ApplayAbilityDrawMonster(p1, m1, pilesdeCarte);
            if (ClassLevel == 2)
            {
                Monster m2 = pilesdeCarte.GetRandomCard("Monster") as Monster;
                while (true)
                {
                    Console.WriteLine("You can choose which Monster to fight. ( m1: {0}, m2: {1} ) ", m1.Name, m2.Name);
                    m1.ShowCard();
                    m2.ShowCard();
                    Console.Write(" --> ");
                    string rep = Console.ReadLine();
                    if ( rep == "m1")
                    {
                        return m1;
                    }
                    else if (rep == "m2")
                    {
                        return m2;
                    }
                    else
                    {
                        Console.WriteLine("Write a correct answer.");
                    }
                }
            }
            return m1;
        }
    }

    class Archer : Classe
    {
        public enum ArcherWeaponType { Bow, CrossBow };
        public ArcherWeaponType archerWeapon;
        public enum wantToDraw { YES, NO };

        public int CardsNotDrawn = 0;
        public int RareLevel = 4;
        public int MythicalLevel = 9;
        public int LegendaryLevel = 16;

        public Archer()
        {
            Name = "Archer";

            // Niveau de Class 1
            Ability = "The archer begins the game with a Bow or a Crossbow. Instead of drawing cards he can increase the mastery bar (one by car not drawn). If it reaches a level, it takes the higher level Bow/Crossbow.";
            //Condition de level up
            LevelUpCondition = "Have a Mythical Bow/Crossbow";
            //Niveau de Class 2
            NewAbility = "";
        }

        public override void ApplayAbilitiyStartGame(Player p1)
        {
            base.ApplayAbilitiyStartGame(p1);
            while(true)
            {
                Console.WriteLine("You can choose between a Bow or a Crossbow. ( Bow: b, Crossbow: c )");
                Console.Write(" --> ");
                string rep = Console.ReadLine();
                if (rep == "b")
                {
                    Console.WriteLine("You choose the Bow Series. And you got your first Bow. ");
                    p1._weaponsPlayed.Add(new CommonBow());
                    archerWeapon = ArcherWeaponType.Bow;
                    break;
                }
                else if (rep == "c" )
                {
                    Console.WriteLine("You choose the Crossbow Series. And you got your first Crossbow. ");
                    p1._weaponsPlayed.Add(new CommonCrossBow());
                    archerWeapon = ArcherWeaponType.CrossBow;
                    break;
                }
                else
                {
                    Console.WriteLine("Choose a correct answer.");
                }
            }
        }
        public override wantToDraw ApplayAbilitiyDrawing(Player p1)
        {
            while (true)
            {
                Console.WriteLine("You can draw a card or not. ( Draw: dr, Not Draw, nd )");
                Console.Write(" --> ");
                string rep = Console.ReadLine();
                if (rep == "dr")
                {
                    return wantToDraw.YES;
                }
                else if (rep == "nd")
                {
                    CardsNotDrawn++;
                    if (CardsNotDrawn == 4)
                    {
                        p1._weaponsPlayed.Clear();
                        Weapon w1 = null;
                        if (archerWeapon == ArcherWeaponType.Bow)
                        {
                            w1 = new RareBow();
                        }
                        else
                        { 
                            w1 = new RareCrossBow(); 
                        }
                        p1._weaponsPlayed.Add(w1);
                    }
                    else if (CardsNotDrawn ==9)
                    {
                        p1._weaponsPlayed.Clear();
                        Weapon w1 = null;
                        if (archerWeapon == ArcherWeaponType.Bow)
                        {
                            w1 = new MythicalBow();
                        }
                        else
                        {
                            w1 = new MythicalCrossBow();
                        }
                        p1._weaponsPlayed.Add(w1);
                    }
                    else if (CardsNotDrawn == 16)
                    {
                        p1._weaponsPlayed.Clear();
                        Weapon w1 = null;
                        if (archerWeapon == ArcherWeaponType.Bow)
                        {
                            w1 = new LegendaryBow();
                        }
                        else
                        {
                            w1 = new LegendaryCrossBow();
                        }
                        p1._weaponsPlayed.Add(w1);
                    }
                    return wantToDraw.NO;
                }
                else
                {
                    Console.WriteLine("Choose a correct answer. ");
                }
            }
        }

        public override PlayCardResult ApplayAbilitiyPlayingWeapon(Player p1)
        {
            return PlayCardResult.IsAnArcher;
        }

    }

    class Traqueur : Classe
    {
        public Traqueur()
        {
            Name = "Tracker";
        }
    }

    class Dresseur : Classe
    {
        public Dresseur()
        {
            Name = "Trainer";
        }
    }


}
