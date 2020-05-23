using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Jeu_de_Socitété___Izulmha
{
    class StartGame
    {
        static void Main(string[] args)
        {
            Classes.FillClassList();
            Races.FillRaceList();
            //début du jeu
            Console.WriteLine("Hello challenger ! You enter this fantastique World which is Izulmha. In this World you will embody a character with a Race and a Class. Good Game !");
            int NumberOfPlayer = GameMethod.NumberPlayer();
            for (int i = 0; i < NumberOfPlayer; i++)
            {
                Console.WriteLine("Player {0}:\n", i);
                Player p1 = Player.CreatNewRandomPlayer();
                Player.ListOfPlayer.Add(p1);
                p1.WritePlayerDescritpion();
                p1.WritePlayerStats();
            }


        }
    }


    class GameMethod
    {
        public static int NumberPlayer()
        {
            while(true)
            {
                Console.WriteLine("How many Challenger want to play ? (max {0})", 4);
                Console.Write(" --> ");
                string rep1 = Console.ReadLine();
                int NumberOfPlayer = 0;
                try
                {
                    NumberOfPlayer = Convert.ToInt32(rep1);
                    if (NumberOfPlayer <= 4 && NumberOfPlayer > 0)
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
