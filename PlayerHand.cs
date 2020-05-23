using System;
using System.Collections.Generic;
using System.Text;

namespace Jeu_de_Socitété___Izulmha
{
    class PlayerHand
    {
        public List<Carte> Cards = new List<Carte>();
        public int NumberOfCard = 0;

        public void PlayCard(Carte c1)
        {
            Cards.Remove(c1);
        }
        public void DrawCard(PilesdeCarte pilesdeCartes,  int n, string name)
        {
            Carte c;
            for (int i = 0; i < n; i++)
            {
                c = pilesdeCartes.GetRandomCard(name);
                Cards.Add(c);
            }
        }

        public void ShowHandCards()
        {
            for (int i = 0; i < Cards.Count; i++)
            {
                Console.WriteLine("Card {0}:", i+1);
                Cards[i].ShowCard();
                Console.WriteLine();
            }
        }
    }
}
