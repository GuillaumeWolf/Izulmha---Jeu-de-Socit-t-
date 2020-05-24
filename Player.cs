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
        public Races PlayerRace;
        //Stats
            //Vie
        public int BasicHP = 10;
        public int HP;
            //Physique
        public int BasicStrength = 1;
        public int Strength;
            //Magique
        public int BasicPower = 1;
        public int Power;
            //Mana
        public int ManaGemme = 4;
        public int Mana;
        //Objets
        private int MaxHands = 2;
        private List<Weapon> _weaponsPlayed = new List<Weapon>();


        // Cartes
        public PlayerHand Cards = new PlayerHand();

        //Etat
        public enum PlayerStatsEnum { NotIsTurn, Drawing, ChoosingPile, PlayingObject, ChoosingObject, ChoosingSpell, Fighting, GivingCard }
        public PlayerStatsEnum PlayerState;

        #endregion



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
            Console.WriteLine("You get 1 Mana Gemme. And the other are full. You got {0} Mana.", ManaGemme);
            //1
            Console.Write("\n1. Draw Card");
            PlayerState = PlayerStatsEnum.Drawing;
            C.AllCommande(this);
            C.AllCommande(this);


            //2
            Console.Write("\n2. Play Card");
            PlayerState = PlayerStatsEnum.PlayingObject;
            C.AllCommande(this);
        }



        //Methode du player en jeu
        public void WritePlayerDescritpion()
        {
            Console.WriteLine("Hello, my name is {0} the {1} and I am a {2}.", PlayerName, PlayerRace.Name, PlayerClass.Name);
        }   
        public void WritePlayerStats()
        {
            Console.WriteLine("Strength: {0}, Power: {1}, Mana: {2}.", Strength, Power, ManaGemme);
        }


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


    }
}
