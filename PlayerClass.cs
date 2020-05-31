using System;
using System.Collections.Generic;

namespace Jeu_de_Socitété___Izulmha
{
    public enum YESNO { YES, NO };
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

        public virtual void PasseLevel2()
        {
            ClassLevel = 2;
            Console.WriteLine("You passed Class Level 2.");
        }

        public virtual void ApplyAbilitiyStartGame(Player p1)
        {
            Console.WriteLine("Player {0}:", p1.PlayerNumber);
        }

        public virtual void ApplyAbilitiyBeforeDrawing(Player p1)
        {
        }

        public virtual void ApplyAbilitiyDrawing(Player p1, Carte c1)
        {

        }

        public virtual void ApplyAbilityPlayingPets(Player p1)
        {

        }

        public virtual void ApplyAbilityStartFight(Player p1)
        {

        }

        public virtual int ApplyAbilityCountStrength(Player p1)
        {
            return 0;
        }

        public virtual Monster ApplyAbilityDrawMonster(Player p1, Monster m1, PilesdeCarte pilesdeCarte)
        {
            return m1;
        }

        public virtual int ApplyAbilityFightMontser(Player p1, Monster m1)
        {
            return 0;
        }

        public virtual void ApplyAbilityKillMontser(Player p1, Monster m1)
        {

        }

        public virtual void PlayingSpell(Player p1)
        {

        }

        public virtual void AddFollower(Player p1)
        {
            p1._petsPlayed.Add(new Follower());
        }

    }




    class Priest : Classe
    {
        private int _followersSummonedCount = 0;

        public Priest()
        {
            Name = "Priest";
            PowerBonus = 1;
            ManaBonus = 2;

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
                PasseLevel2();
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
        public int SpellCastTarget = 6;

        public Sorcier()
        {
            Name = "Sorcer";
            PowerBonus = 3;

            // Niveau de Class 1
            Ability = "One time a turn, the Sorcer can choose to spent " + ManaCost.ToString() + " Mana for that his next Spell will apply" + TimeCastSpell.ToString() + " his effect. ";
            //Condition de level up
            LevelUpCondition = "Cast Spell "+ SpellCastTarget.ToString() +" times";
            //Niveau de Class 2
            NewAbility = "One time a turn, the Sorcer can choose to spent " + ManaCost.ToString() + " Mana for that his next Spell will apply" + TimeCastSpell.ToString() + " his effect. ";
        }
        public override void PasseLevel2()
        {
            base.PasseLevel2();
            ManaCost = 1;
            TimeCastSpell = 3;
        }

        public override void PlayingSpell(Player p1)
        {
            SpellCastCount++;
            if (SpellCastCount == SpellCastTarget)
            {
                PasseLevel2();
            }
        }
    }

    class Chevalier : Classe
    {
        public Chevalier()
        {
            Name = "Knight";
            StrengthBonus = 3;
            

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
            if (StrengthSupp - p1._weaponsPlayed.Count + 1 == (4) && p1._weaponsPlayed.Count != 0)
            {
                PasseLevel2();
            }
            return StrengthSupp;
        }

        public override Monster ApplyAbilityDrawMonster(Player p1, Monster m1, PilesdeCarte pilesdeCarte)
        {
            base.ApplyAbilityDrawMonster(p1, m1, pilesdeCarte);
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
                    if (rep == "m1")
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

        public int CardsNotDrawn = 0;
        public int RareLevel = 4;
        public int MythicalLevel = 9;
        public int LegendaryLevel = 16;
        public int NextLevel;
        public bool finished = false;

        public Archer()
        {
            Name = "Archer";
            NextLevel = RareLevel;
            StrengthBonus = 1;
            PowerBonus = 1;
            ManaBonus = 1;

        // Niveau de Class 1
        Ability = "The archer begins the game with a Bow or a Crossbow. Instead of drawing cards he can increase the mastery bar (one by car not drawn). If it reaches a level, it takes the higher level Bow/Crossbow.";
            //Condition de level up
            LevelUpCondition = "Have a Mythical Bow/Crossbow";
            //Niveau de Class 2
            NewAbility = "";
        }

        public override void ApplyAbilitiyStartGame(Player p1)
        {
            base.ApplyAbilitiyStartGame(p1);
            while (true)
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
                else if (rep == "c")
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
        public override void ApplyAbilitiyBeforeDrawing(Player p1)
        {
            CardsNotDrawn++;
            if (CardsNotDrawn == RareLevel)
            {
                p1._weaponsPlayed.Clear();
                Weapon w1 = null;
                if (archerWeapon == ArcherWeaponType.Bow)
                {
                    w1 = new RareBow();
                    Console.WriteLine("You get a new {0}.", w1.Name);
                }
                else
                {
                    w1 = new RareCrossBow();
                    Console.WriteLine("You get a new {0}.", w1.Name);
                }
                p1._weaponsPlayed.Add(w1);
                NextLevel = MythicalLevel;
            }
            else if (CardsNotDrawn == MythicalLevel)
            {
                p1._weaponsPlayed.Clear();
                Weapon w1 = null;
                if (archerWeapon == ArcherWeaponType.Bow)
                {
                    w1 = new MythicalBow();
                    Console.WriteLine("You get a new {0}.", w1.Name);
                }
                else
                {
                    w1 = new MythicalCrossBow();
                    Console.WriteLine("You get a new {0}.", w1.Name);
                }
                p1._weaponsPlayed.Add(w1);
                NextLevel = LegendaryLevel;
                PasseLevel2();
            }
            else if (CardsNotDrawn == LegendaryLevel)
            {
                p1._weaponsPlayed.Clear();
                Weapon w1 = null;
                if (archerWeapon == ArcherWeaponType.Bow)
                {
                    w1 = new LegendaryBow();
                    Console.WriteLine("You get a new {0}.", w1.Name);
                }
                else
                {
                    w1 = new LegendaryCrossBow();
                    Console.WriteLine("You get a new {0}.", w1.Name);
                }
                p1._weaponsPlayed.Add(w1);
                finished = true;
            }
            else
            {
                Console.WriteLine("{0} left to the next step.", NextLevel - CardsNotDrawn);
            }
        }


    }

    class Traqueur : Classe
    {
        public int PuissanceBonus = 1;

        public Traqueur()
        {
            Name = "Tracker";
            StrengthBonus = 2;
            PowerBonus = 1;

            // Niveau de Class 1
            Ability = "The Tracker is more powerfull against big monster, he is " + PuissanceBonus.ToString() + " Puissance more powerfull in increments of 10 monster Puissance. ";
            //Condition de level up
            LevelUpCondition = "Have a total Trophy equal or superior at 30.";
            //Niveau de Class 2
            NewAbility = "The bonus is 2. And if he draws a monster below level ten, he is instantly beaten.";
        }
        public override void PasseLevel2()
        {
            base.PasseLevel2();
            PuissanceBonus = 2;
        }
        public override Monster ApplyAbilityDrawMonster(Player p1, Monster m1, PilesdeCarte pilesdeCarte)
        {
            if (ClassLevel == 2)
            {
                if ((m1 as NormalMonster).Puissance < 10)
                {
                    return null;
                }
            }
            return m1;
        }
        public override int ApplyAbilityFightMontser(Player p1, Monster m1)
        {
            if (m1 is NormalMonster)
            {
                int total = (m1 as NormalMonster).Puissance / 10;
                total *= PuissanceBonus;
                return total;
            }
            return 0;
        }
        public override void ApplyAbilityKillMontser(Player p1, Monster m1)
        {
            base.ApplyAbilityKillMontser(p1, m1);
            if (m1 is NormalMonster)
            {
                if ( p1.Trophy >= 30)
                {
                    PasseLevel2();
                }
            }
        }
    }

    class Dresseur : Classe
    {
        public int ManaReducOnPets = 1;
        public int TotalPetsSummoned = 0;
        public int TargetPetsSummoned = 6;
        public int NumLegPetsSummon = 2;

        public Dresseur()
        {
            Name = "Trainer";
            StrengthBonus = 1;
            ManaBonus = 2;

            // Niveau de Class 1
            Ability = "The Pets cost " + ManaReducOnPets.ToString() + " Mana less to Summon.";
            LevelUpCondition = "Have Summoned " + TargetPetsSummoned.ToString() + " Pets.";
            //Niveau de Class 2
            NewAbility = "The Pets cost " + ManaReducOnPets.ToString() + " Mana less to Summon. When ha passed level 2, he Summon " + NumLegPetsSummon.ToString() + " Legendary Pets.";
        }

        public override void ApplyAbilitiyDrawing(Player p1, Carte c1)
        {
            base.ApplyAbilitiyDrawing(p1, c1);
            if(c1 is Pets)
            {
                c1.Cost -= 1;
            }
        }

        public override void ApplyAbilityPlayingPets(Player p1)
        {
            base.ApplyAbilityPlayingPets(p1);
            TotalPetsSummoned++;
            if (TotalPetsSummoned == TargetPetsSummoned)
            {
                SummonLegendaryPets(p1);
                PasseLevel2();
            }
        }

        public override void PasseLevel2()
        {
            base.PasseLevel2();
            ManaReducOnPets = 2;   
        }

        public void SummonLegendaryPets(Player p1)
        {
            List<Pets> lp1 = new List<Pets>();
            lp1.Add(new MamaBear());
            lp1.Add(new WhiteDragon());
            lp1.Add(new TheDefencer());
            lp1.Add(new SaberToothTiger());
            for (int i = 0; i < NumLegPetsSummon; i++)
            {
                int counnt = lp1.Count;
                int rand = Aleatoire.RandomInt(counnt);
                p1._petsPlayed.Add(lp1[rand]);
                lp1.RemoveAt(rand);
                Console.WriteLine("You Summoned a {0}.", lp1[rand].Name);
                Console.WriteLine("");
            }
        }
    }


}
