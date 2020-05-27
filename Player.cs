using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Jeu_de_Socitété___Izulmha
{
    class Player
    {
        #region public properties
        //Name/class/race
        public string PlayerName;
        public Classes PlayerClass;
        public int ClassLevel = 1;
        public Races PlayerRace;
        public int RaceLevel = 1;
        public int PlayerNumber;
        //Stats
            //Vie
        public int BasicHP = 8;
        public int HP;
            //Physique
        public int BasicStrength = 1;
        public int Strength;
            //Magique
        public int BasicPower = 1;
        public int Power;
            //Mana
        public int ManaGemme = 2;
        public int Mana;

        //Objets
        public int MaxHands = 2;
        public List<Weapon> _weaponsPlayed = new List<Weapon>();
        public Armor _armorPlayed = null;
        public Helmet _helmetPlayed = null;
        public Shoe _shoesPlayed = null;
        public List<Amulette> _amulettePlayed = new List<Amulette>();
        public List<Pets> _petsPlayed = new List<Pets>();

        // Cartes
        public PlayerHand Cards = new PlayerHand();
        public int maxCardInHand = 10;

        //Vente
        public int SellingGold = 0;
        public int cardToDraw = 0;

        //Etat
        public enum PlayerStatsEnum 
        { 
            NotIsTurn, 
            //1
            Drawing, 
            ChoosingPile, 
            //2
            PlayingObject, 
            ChoosingObject, 
            ChoosingSpell, 
            ChangingWeapon, 
            ChoosingWeapon, 
            ChangingArmor, 
            ChangingHelmet, 
            ChangingShoe,
            //3
            ChoosingPets,
            ChoosingDamage,
            Fighting,
            //4
            SellingGivingCard,
            //Autre
            ChoosingPlayer
        }
        public PlayerStatsEnum PlayerState;
        public PlayerStatsEnum LastStates;

        //Fight
        public bool FightWithStrength = true;
        public NormalMonster FightMonster = null;

        #endregion public properties


        public Player(string nameInput, Classes classofplayer, Races raceofplayer)
        {
            PlayerName = nameInput;
            PlayerClass = classofplayer;
            PlayerRace = raceofplayer;
            HP = BasicHP;
            Mana = ManaGemme;
            Strength = BasicStrength;
            Power = BasicPower;
        }

        //Crée un joueur
        public static Player CreatNewRandomPlayer()
        {
            //Name
            Console.Write("Enter your name: \n --> ");
            string rep1 = Console.ReadLine();
            //string rep1 = ChooseName();
            //Class
            Classes classofplayer = null;
            int n = Aleatoire.RandomInt(Classes.ClassPoss.Count);
            classofplayer = Classes.ClassPoss[n];
            Classes.ClassPoss.RemoveAt(n);

            //Race
            Races raceofplayer = null;
            n = Aleatoire.RandomInt(Races.RacePoss.Count);
            raceofplayer = Races.RacePoss[n];
            Races.RacePoss.RemoveAt(n);

            //Instancie le Player
            Player p1 = new Player(rep1, classofplayer, raceofplayer);
            return p1;
        }
        private static void CreatNewPlayer()
        {

        }

        private static string ChooseName()
        {
            while (true)
            {
                Console.Write("Enter your name: \n --> ");
                string rep1 = Console.ReadLine();
                //Check si correct
                Console.WriteLine("Are you sure {0} is your name ? (yes)", rep1);
                Console.Write("--> ");
                string rep2 = Console.ReadLine();
                if (rep2 == "yes")
                {
                    return rep1;
                }
                else
                {
                    continue;
                }
            }
        }

        internal void Play(PilesdeCarte pilesdeCartes, Commande C)
        {
            ManaGemme += 1;
            Mana = ManaGemme;
            Console.WriteLine("You get 1 Mana Gemme. And the other are fill. You got {0} Mana.", ManaGemme);
            //1
            Console.Write("\n1. Draw Card");
            PlayerState = PlayerStatsEnum.Drawing;
            C.AllCommande(this);
            C.AllCommande(this);


            //2
            Console.Write("\n2. Play Card");
            PlayerState = PlayerStatsEnum.PlayingObject;
            C.AllCommande(this);


            //3
            Console.Write("\n3. Fight a random Monster. ");
            Monster m1 =  Cards.DrawMonster(pilesdeCartes);
            
            if (m1 is NormalMonster)
            {
                FightMonster = m1 as NormalMonster;
            }
            else
            {
                //Consigne pour le boss
            }
            Console.Write("You are facing ", FightMonster.Name);
            FightMonster.ShowCard();

            PlayerState = PlayerStatsEnum.ChoosingDamage;
            C.AllCommande(this); 
            PlayerState = PlayerStatsEnum.ChoosingPets;
            C.AllCommande(this);
            PlayerState = PlayerStatsEnum.Fighting;
            C.AllCommande(this);


            //4
            Console.Write("\n4. Selling Object (per 500 gold draw a Card).");
            while (Cards.Cards.Count > maxCardInHand)
            {
                PlayerState = PlayerStatsEnum.SellingGivingCard;
                C.AllCommande(this);
                if(Cards.Cards.Count > maxCardInHand)
                {
                    Console.WriteLine("You have to much Cards. Sell them, or throw some.");
                }
            }
            while(cardToDraw > 0)
            {
                C.AllCommande(this);
                cardToDraw--;
            }

            //5
            PlayerState = PlayerStatsEnum.NotIsTurn;

        }



        //Methode du player en jeu
        public void WritePlayerDescritpion()
        {
            Console.WriteLine("Hello, my name is {0} the {1} and I am a {2}.", PlayerName, PlayerRace.Name, PlayerClass.Name);
        }
        public void WritePlayerStats()
        {
            int x = 1;
            x += 2;
            if (PlayerState != PlayerStatsEnum.Fighting)
            {
                Console.WriteLine("{0} the {1} {2}:", PlayerName, PlayerClass.Name, PlayerRace.Name);
                int? totalStrength = Strength;
                int? totalPower = Power;
                int? totalMana = Mana;
                int? totalManaGemme = ManaGemme;
                int longWeapon = _weaponsPlayed.Count;
                for (int i = 0; i < longWeapon; i++)
                {
                    if (_weaponsPlayed[i].Mana.HasValue)
                    {
                        totalMana += _weaponsPlayed[i].Mana;
                        totalManaGemme += _weaponsPlayed[i].Mana;
                    }
                    Console.Write("Weapon {0}: {1}. ", i + 1, _weaponsPlayed[i].Name);
                }
                if (longWeapon == 0) { Console.Write("No Weapons. "); }

                if (_armorPlayed != null)
                {
                    if (_armorPlayed.Mana.HasValue)
                    {
                        totalMana += _armorPlayed.Mana;
                        totalManaGemme += _armorPlayed.Mana;
                    }
                    Console.Write("Armor: {0}. ", _armorPlayed.Name);
                }
                else { Console.Write("No Armor. "); }

                if (_helmetPlayed != null)
                {
                    if (_helmetPlayed.Mana.HasValue)
                    {
                        totalMana += _helmetPlayed.Mana;
                        totalManaGemme += _helmetPlayed.Mana;
                    }
                    Console.Write("Helmet: {0}. ", _helmetPlayed.Name);
                }
                else { Console.Write("No Helmet. "); }

                int longAmulette = _amulettePlayed.Count;
                for (int i = 0; i < longAmulette; i++)
                {
                    if (_amulettePlayed[i].Mana.HasValue)
                    {
                        totalMana += _amulettePlayed[i].Mana;
                        totalManaGemme += _amulettePlayed[i].Mana;
                    }
                    Console.Write("Amulette {0}: {1}. ", i + 1, _amulettePlayed[i].Name);
                }
                if (longAmulette == 0) { Console.Write("No Amulette. "); }

                int longPets = _petsPlayed.Count;
                for (int i = 0; i < longPets; i++)
                {
                    if (_petsPlayed[i].Mana.HasValue)
                    {
                        totalMana += _petsPlayed[i].Mana;
                        totalManaGemme += _petsPlayed[i].Mana;
                    }
                    Console.Write("Pets {0}: {1}. ", i + 1, _petsPlayed[i].Name);
                }
                if (longPets == 0) { Console.Write("No Pets. "); }

                Console.WriteLine();
                if (PlayerState != PlayerStatsEnum.Fighting)
                {
                    Console.WriteLine("Strength: {0}, Power: {1}, Mana: {2}/{3}.", GetTotalPower(), GetTotalPower(), totalMana, totalManaGemme);
                }
                else if (FightWithStrength)
                {
                    Console.WriteLine("Puissance: {0}.", GetTotalPower());
                }
                else
                {
                    Console.WriteLine("Puissance: {0}.", GetTotalPower());
                }
            }
        }
        public int? GetTotalStrength()
        {
            int? totalStrength = Strength;
            int longWeapon = _weaponsPlayed.Count;
            for (int i = 0; i < longWeapon; i++)
            {
                if (_weaponsPlayed[i].Strength.HasValue)
                {
                    totalStrength += _weaponsPlayed[i].Strength;
                }
            }

            if (_armorPlayed != null)
            {
                if (_armorPlayed.Strength.HasValue)
                {
                    totalStrength += _armorPlayed.Strength;
                }
            }

            if (_helmetPlayed != null)
            {
                if (_helmetPlayed.Strength.HasValue)
                {
                    totalStrength += _helmetPlayed.Strength;
                }
            }

            int longAmulette = _amulettePlayed.Count;
            for (int i = 0; i < longAmulette; i++)
            {
                if (_amulettePlayed[i].Strength.HasValue)
                {
                    totalStrength += _amulettePlayed[i].Strength;
                }
            }

            int longPets = _petsPlayed.Count;
            for (int i = 0; i < longPets; i++)
            {
                if (_petsPlayed[i].inFight)
                {
                    totalStrength += _petsPlayed[i].Puissance;
                }
                if (_petsPlayed[i] is TotemOfStrength)
                {
                    totalStrength += (_petsPlayed[i] as TotemOfStrength).GivePlayerStrength();
                }
            }
            return totalStrength;
        }
        public int? GetTotalPower()
        {
            int? totalPower = Power;
            int longWeapon = _weaponsPlayed.Count;
            for (int i = 0; i < longWeapon; i++)
            {
                if (_weaponsPlayed[i].Power.HasValue)
                {
                    totalPower += _weaponsPlayed[i].Power;
                }
            }

            if (_armorPlayed != null)
            {
                if (_armorPlayed.Power.HasValue)
                {
                    totalPower += _armorPlayed.Power;
                }
            }

            if (_helmetPlayed != null)
            {
                if (_helmetPlayed.Power.HasValue)
                {
                    totalPower += _helmetPlayed.Power;
                }
            }

            int longAmulette = _amulettePlayed.Count;
            for (int i = 0; i < longAmulette; i++)
            {
                if (_amulettePlayed[i].Power.HasValue)
                {
                    totalPower += _amulettePlayed[i].Power;
                }
            }

            int longPets = _petsPlayed.Count;
            for (int i = 0; i < longPets; i++)
            {
                if (_petsPlayed[i].inFight)
                {
                    totalPower += _petsPlayed[i].Puissance;
                }
            }
            return totalPower;
        }

        //Ajoute des Cartes
        public PlayCardResult PlayWeapon(Weapon w1)
        {
            int totalHand = _weaponsPlayed.Select(x => x.HandTake).Sum() + w1.HandTake;
            if (totalHand > MaxHands)
            {
                return PlayCardResult.NoEnoughHand;
            }
            _weaponsPlayed.Add(w1);
            return PlayCardResult.OK;
        }
        public PlayCardResult PlayArmor(Armor a1)
        {
            if (_armorPlayed != null)
            {
                return PlayCardResult.NoEnoughBody;
            }
            _armorPlayed = a1;
            return PlayCardResult.OK;
        }
        public PlayCardResult PlayHelmet(Helmet h1)
        {
            if (_helmetPlayed != null)
            {
                return PlayCardResult.NoEnoughHead;
            }
            _helmetPlayed = h1;
            return PlayCardResult.OK;
        }
        public PlayCardResult PlayShoe (Shoe s1)
        {
            if (_shoesPlayed != null)
            {
                return PlayCardResult.NoEnoughFeet;
            }
        _shoesPlayed = s1;
            return PlayCardResult.OK;
        }
        public void PlayAmulette(Amulette a1)
        {
            _amulettePlayed.Add(a1);
        }
        public void PlayPets(Pets p1)
        {
            _petsPlayed.Add(p1);
        }
    }
}
