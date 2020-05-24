using System;
using System.Collections.Generic;
using System.Text;

namespace Jeu_de_Socitété___Izulmha
{
    class PlayerHand
    {
        public List<Carte> Cards = new List<Carte>();

        public void PlayCard(Carte c1, Player p1)
        {
            Cards.Remove(c1);
            p1.Mana -= c1.Cost;
            Console.WriteLine("You play a {0}. It cost {1} Mana. You have {2} Mana.", c1.Name, c1.Cost, p1.Mana);
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
                Console.WriteLine();
                Console.WriteLine("Card {0}:", i+1);
                Cards[i].ShowCard();
                Console.WriteLine();
            }
        }
    }
}
