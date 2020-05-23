using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Jeu_de_Socitété___Izulmha
{
    interface ICartes
    {
        public void PlayCard();
        public void DrawCard();

    }


    class Sorts : ICartes
    {
        public static string type;
        public Sorts()
        {
            type = "Cartes";
        }

        public void PlayCard()
        {
            
        }
        public void DrawCard()
        {
        }

    }

    class Objets : ICartes
    {
        public static string type;
        public Objets()
        {
            type = "Objets";
        }

        public void PlayCard()
        {

        }
        public void DrawCard()
        {

        }

    }

}
