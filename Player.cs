using System;
using System.Collections.Generic;
using System.Security.Cryptography;
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
        //Physique
        public int BasicStrength = 1;
        public int Strength;
        //Magique
        public int BasicPower = 1;
        public int Power;
            //Mana
        public int ManaGemme = 10;
        public int Mana;

        // Cartes
        public PlayerHand Cards = new PlayerHand();

        //Etat
        public enum PlayerStatsEnum { NotIsTurn, Drawing, ChoosingPile, PlayingObject, ChoosingObject, ChoosingSpell, Fighting, GivingCard }
        public PlayerStatsEnum PlayerStats;

        #endregion



        public Player(string nameInput, Classes classofplayer, Races raceofplayer)
        {
            PlayerName = nameInput;
            PlayerClass = classofplayer;
            PlayerRace = raceofplayer;
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

        internal void Play(PilesdeCarte pilesdeCartes)
        {
            ManaGemme += 1;
            Console.WriteLine("You get 1 Mana Gemme. And the other are full. You got {0} Mana.", ManaGemme);
            //1
            Console.Write("\n1. Draw Card");
            PlayerStats = PlayerStatsEnum.Drawing;
            Commande.AllCommande(this, pilesdeCartes);
            Commande.AllCommande(this, pilesdeCartes);
            Commande.AllCommande(this, pilesdeCartes);
            Commande.AllCommande(this, pilesdeCartes);


            //2
            Console.Write("\n2. Play Card");
            PlayerStats = PlayerStatsEnum.PlayingObject;
            Commande.AllCommande(this, pilesdeCartes);
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



    }
}
