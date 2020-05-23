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
            //Dégats Physique
        public int AP = 0;
        public int RP = 0;
            //Dégats Magique
        public int AM = 0;
        public int RM = 0;
            //Mana
        public int Mana = 0;

        #endregion


        #region public static Properties and Array
        public static List<Player> ListOfPlayer = new List<Player>();


        #endregion


        public Player(string nameInput, Classes classofplayer, Races raceofplayer)
        {
            PlayerName = nameInput;
            PlayerClass = classofplayer;
            PlayerRace = raceofplayer;
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
        


        //Methode du player en jeu
        public void WritePlayerDescritpion()
        {
            Console.WriteLine("Hello, my name is {0} the {1} and I am a {2}.", PlayerName, PlayerRace.Name, PlayerClass.Name);
        }
        public void WritePlayerStats()
        {
            Console.WriteLine("Attack Physic: {0}, Resistance Physic: {1}, Attack Magic: {2},Restistance Magic: {3}, Mana: {4}.", AP, RP, AM, RM, Mana);
        }



    }
}
