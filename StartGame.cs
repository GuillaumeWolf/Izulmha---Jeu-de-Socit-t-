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
        private Commande C;

        //Provisoir
        public static int numberOfPlayerInThisGame = 0;

        static void Main(string[] args)
        {
            var G = new Game();
            G.Prepare();
            G.PlayGame();

        }

        private void Prepare()
        {
            //Préparation
            Classe.FillClassList();
            Races.FillRaceList();

            //début du jeu
            Console.WriteLine("Hello challenger ! You enter this fantastique World : Izulmha. In this World you will embody a character with a Race and a Class. Good Game !");
            int NumberOfPlayer = GameMethod.NumberPlayer();
            numberOfPlayerInThisGame = NumberOfPlayer;
            for (int i = 0; i < NumberOfPlayer; i++)
            {
                Console.WriteLine("\nPlayer {0}:\n", i+1);
                Player p1 = Player.CreatNewRandomPlayer(_listOfPlayer);
                _listOfPlayer.Add(p1);
                p1.PlayerNumber = i+1;
                p1._pilesdeCarte = _pilesdeCartes;

            }
            for (int i = 0; i < NumberOfPlayer; i++)
            {
                Console.Write("Player {0}: ", i + 1);
                _listOfPlayer[i].WritePlayerDescritpion();
                Console.WriteLine();
                _listOfPlayer[i].PlayerClass.ApplyAbilitiyStartGame(_listOfPlayer[i]);
                //Piocher des cartes
                //_listOfPlayer[i].Cards.DrawCard(_pilesdeCartes, 3, "Object");
                //_listOfPlayer[i].Cards.DrawCard(_pilesdeCartes, 3, "Spell");
                Console.WriteLine();
            }
            C = new Commande(_pilesdeCartes, _listOfPlayer);

        }
        private void PlayGame()
        {
            while(true)
            {                
                _TourCount++;
                Console.WriteLine("\n(Turn {0})\n", _TourCount);

                for (int i = 0; i< _listOfPlayer.Count; i++)
                {
                    Console.WriteLine(" - {0} is playing. - ", _listOfPlayer[i].PlayerName);
                    _listOfPlayer[i].Play(_pilesdeCartes, C);
                    Console.WriteLine(" - Player {0} finished his turn. - \n\n", i + 1);
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
                Console.WriteLine("How many Challenger want to play ? (min {1} - max {0})", 6, 2);
                Console.Write(" --> ");
                string rep1 = Console.ReadLine();
                int NumberOfPlayer = 0;
                try
                {
                    NumberOfPlayer = Convert.ToInt32(rep1);
                    if (NumberOfPlayer <= 6 && NumberOfPlayer >= 2)
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
