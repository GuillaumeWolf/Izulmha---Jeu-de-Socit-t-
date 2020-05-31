using System;
using System.Collections.Generic;
using System.Text;

namespace Jeu_de_Socitété___Izulmha
{
    class Races
    {
        public string Name;
        public int StrengthBonus = 0;
        public int PowerBonus = 0;
        public int ManaBonus = 0;
        public int RaceLevel = 1;

        public static List<Races> RacePoss = new List<Races>();
        public static void FillRaceList()
        {
            RacePoss.Add(new Elf());
            RacePoss.Add(new Nain());
            RacePoss.Add(new Orc());
            RacePoss.Add(new Creature());
            RacePoss.Add(new Poisson());
            RacePoss.Add(new Angel());
        }

        public virtual void PlayingSpell(Player p1, Spell s1)
        {

        }
        public virtual void AfterDrawingCard(Player p1)
        {

        }

        public virtual void DealingDamage(Player p1, Player p2, int x)
        {

        }
        public virtual void DealingHeal(Player p1, Player p2, int x)
        {

        }

        public virtual void WhenPetsDie(Player p1, Pets pets1)
        {

        }
        public virtual void StartOfTurn(List<Player> playerlist, Player p1)
        {

        }



    }




    #region Elf
    class Elf : Races
    {
        public int IncantationCount = 0;
        public int IncantationTarget = 6;
        public int FlaskCount = 0;
        public int FlaskTarget = 6;

        public Elf()
        {
            Name = "Elf";
            StrengthBonus = 0;
            PowerBonus = 0;
            ManaBonus = 0;
        }
        public override void PlayingSpell(Player p1, Spell s1)
        {
            base.PlayingSpell(p1, s1);
            if (s1 is Flask)
            {
                IncantationCount++;
                if (IncantationCount == 6)
                {
                    p1.PlayerRace = new MoonElf();
                }
            }
            else if (s1 is Incantation)
            {
                FlaskCount++;
                if (FlaskCount == 6)
                {
                    p1.PlayerRace = new SunElf();
                }
            }
        }
    }

    class MoonElf : Elf
    {
        public MoonElf()
        {
            Name = "Moon Elf";
            StrengthBonus = 0;
            PowerBonus = 0;
            ManaBonus = 0;
            Console.WriteLine("You evolve in {0}.", Name);
        }
        public override void AfterDrawingCard(Player p1)
        {
            base.AfterDrawingCard(p1);
            p1.Cards.DrawCard(p1._pilesdeCarte, 1, "Spell", p1);
        }
    }

    class SunElf : Elf
    {
        public SunElf()
        {
            Name = "Sun Elf";
            StrengthBonus = 0;
            PowerBonus = 0;
            ManaBonus = 0;
            Console.WriteLine("You evolve in {0}.", Name);
        }
        public override void AfterDrawingCard(Player p1)
        {
            base.AfterDrawingCard(p1);
            p1.Cards.DrawCard(p1._pilesdeCarte, 1, "Object", p1);
        }
    }
    #endregion Elf


    #region Nain
    class Nain : Races
    {
        public Nain()
        {
            Name = "Dwarf";
        }
    }

    #endregion Nain


    #region Orc
    class Orc : Races
    {
        private int TotalDamageDeal = 0;
        private int TargetDamageDeal = 3;

        private int TotalDeadPets = 0;
        private int TargetDeadPets = 5;
        public List<Pets> listDeadPets;

        public Orc()
        {
            Name = "Orc";
            StrengthBonus = 0;
            PowerBonus = 0;
            listDeadPets = new List<Pets>();
        }

        public override void DealingDamage(Player p1, Player p2, int x)
        {
            base.DealingDamage(p1, p2, x);
            TotalDamageDeal += x;
            if (TotalDamageDeal >= TargetDamageDeal)
            {
                p1.PlayerRace = new WarOrc();
            }
        }

        public override void WhenPetsDie(Player p1, Pets pets1)
        {
            base.WhenPetsDie(p1, pets1);
            listDeadPets.Add(pets1);
            TotalDeadPets++;
            if (TotalDeadPets >= TargetDeadPets)
            {
                p1.PlayerRace = new ChamanOrc(p1, listDeadPets);
            }
        }
    }


    class WarOrc : Orc
    {
        public WarOrc()
        {
            Name = "War Orc";
            StrengthBonus = 0;
            PowerBonus = 0;
            ManaBonus = 0;
            Console.WriteLine("You evolve in {0}.", Name);
        }

        public override void StartOfTurn(List<Player> playerlist, Player p1)
        {
            base.StartOfTurn(playerlist, p1);
            for (int i = 0; i < playerlist.Count; i++)
            {
                if (playerlist[i] != p1)
                {
                    p1.DealDamage(playerlist[i], 1);
                }
            }
        }
    }

    class ChamanOrc : Orc
    {
        public ChamanOrc(Player p1, List<Pets> listsss)
        {
            Name = "Chaman Orc";
            StrengthBonus = 0;
            PowerBonus = 0;
            ManaBonus = 0;
            listDeadPets = listsss;
            Console.WriteLine("You evolve in {0}.", Name);

            for (int i = 0; i < listDeadPets.Count; i++)
            {
                listDeadPets[i].HP = listDeadPets[i].BasicHP;
                listDeadPets[i].Puissance = listDeadPets[i].BasicPuissance + 1;
                listDeadPets[i].WhenSummoned();
                p1._petsPlayed.Add(listDeadPets[i]);
                Console.WriteLine("A {0} come from the death to help you.", listDeadPets[i].Name);
            }

        }
    }

    #endregion Orc


    #region Creature
    class Creature : Races
    {
        public Creature()
        {
            Name = "Creature";
        }
    }
    #endregion Creature


    #region Poisson
    class Poisson : Races
    {
        public Poisson()
        {
            Name = "Fish";
        }
    }
    #endregion Poisson


    #region Angel
    class Angel : Races
    {
        public Angel()
        {
            Name = "Angel";
            StrengthBonus = 0;
            PowerBonus = 0;
            ManaBonus = 0;
        }

        public override void DealingDamage(Player p1, Player p2, int x)
        {
            base.DealingDamage(p1, p2, x);
            if ( p1 != p2)
            {
                p1.PlayerRace = new FallenAngel();
            }
        }
        public override void DealingHeal(Player p1, Player p2, int x)
        {
            base.DealingHeal(p1, p2, x);
            if (p1 != p2)
            {
                p1.PlayerRace = new UpperAngel();
            }
        }
    }

    class UpperAngel : Angel
    {
        public UpperAngel()
        {
            Name = "Upper Angel";
            StrengthBonus = 0;
            PowerBonus = 0;
            ManaBonus = 0;
            Console.WriteLine("You evolve in {0}.", Name);
        }

        public override void StartOfTurn(List<Player> playerlist, Player p1)
        {
            base.StartOfTurn(playerlist, p1);
            for (int i = 0; i < playerlist.Count; i++)
            {
                if (p1 != playerlist[i])
                {
                    int x = Aleatoire.RandomInt(2);
                    string plplplplp = "";
                    if (x == 0){ plplplplp = "Object";}
                    else {plplplplp = "Spell"; }
                    playerlist[i].Cards.DrawCard(playerlist[i]._pilesdeCarte, 1, plplplplp, playerlist[i]);
                }
                for (int j = 0; j < p1.Cards.Cards.Count; j++)
                {
                    p1.Cards.Cards[j].Cost -= 2;
                }
            }
        }
    }

    class FallenAngel : Angel
    {
        public FallenAngel()
        {
            Name = "Fallen Angle";
            StrengthBonus = 0;
            PowerBonus = 0;
            ManaBonus = 0;
            Console.WriteLine("You evolve in {0}.", Name);
        }

        public override void StartOfTurn(List<Player> playerlist, Player p1)
        {
            base.StartOfTurn(playerlist, p1);
            for (int i = 0; i < playerlist.Count; i++)
            {
                if (p1 != playerlist[i])
                {
                    int x = Aleatoire.RandomInt(playerlist[i].Cards.Cards.Count);
                    playerlist[i].Cards.Cards.RemoveAt(x);
                    p1.DealDamage(playerlist[i], 1);
                }
                for (int j = 0; j < p1.Cards.Cards.Count; j++)
                {
                    p1.Cards.Cards[j].Cost += 1;
                }
            }
        }
    }

    #endregion Angel

}
