using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;

namespace Jeu_de_Socitété___Izulmha
{
    class Game
    {
        private List<Player> _listOfPlayer = new List<Player>();
        public PilesdeCarte _pilesdeCartes = new PilesdeCarte();
        private int _TourCount = 0;
        private Player _CurrentPlayer;


        static void Main(string[] args)
        {
            var G = new Game();
            G.Prepare();
            G.PlayGame();

        }

        private void Prepare()
        {
            //Préparation
            Classes.FillClassList();
            Races.FillRaceList();

            //début du jeu
            Console.WriteLine("Hello challenger ! You enter this fantastique World which is Izulmha. In this World you will embody a character with a Race and a Class. Good Game !");
            int NumberOfPlayer = GameMethod.NumberPlayer();
            for (int i = 0; i < NumberOfPlayer; i++)
            {
                Console.WriteLine("\nPlayer {0}:\n", i+1);
                Player p1 = Player.CreatNewRandomPlayer();
                _listOfPlayer.Add(p1);

            }
            for (int i = 0; i < NumberOfPlayer; i++)
            {
                _listOfPlayer[i].WritePlayerDescritpion();
                _listOfPlayer[i].WritePlayerStats();
            }


        }
        private void PlayGame()
        {
            while(true)
            {                
                _TourCount++;
                Console.WriteLine("\n(Turn {0})\n", _TourCount);

                for (int i = 0; i< _listOfPlayer.Count; i++)
                {
                    Console.WriteLine(" - Player {0} is playing. - ", i + 1);
                    _listOfPlayer[i].Play(_pilesdeCartes);
                    Console.WriteLine(" - Player {0} finished his turn. - \n", i + 1);
                }

            }
        }
    }


    class GameMethod
    {
        public static int NumberPlayer()
        {
            while(true)
            {
                Console.WriteLine("How many Challenger want to play ? (min {1} - max {0})", 4, 2);
                Console.Write(" --> ");
                string rep1 = Console.ReadLine();
                int NumberOfPlayer = 0;
                try
                {
                    NumberOfPlayer = Convert.ToInt32(rep1);
                    if (NumberOfPlayer <= 4 && NumberOfPlayer > 1)
                    {
                        return NumberOfPlayer;
                    }
                    else Console.WriteLine("The choosen number is to big.");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Choose a number pls");
                }
            }
        }

    }
}
