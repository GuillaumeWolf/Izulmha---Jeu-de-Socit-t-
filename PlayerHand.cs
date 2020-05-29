using System;
using System.Collections.Generic;
using System.Text;

namespace Jeu_de_Socitété___Izulmha
{
    public enum PlayCardResult { OK, NoEnoughHand, NoEnoughBody, NoEnoughHead, NoEnoughFeet , CantCastSpell, IsAnArcher };
    class PlayerHand
    {
        public List<Carte> Cards = new List<Carte>();

        public PlayCardResult PlayCard(Carte c1, Player p1)
        {
            PlayCardResult canplay = PlayCardResult.OK;

            //Joue la carte dans player
            if (c1 is Weapon)
            {
                canplay = p1.PlayWeapon(c1 as Weapon);
            }
            if (c1 is Armor)
            {
                canplay = p1.PlayArmor(c1 as Armor);
            }
            if (c1 is Helmet)
            {
                canplay = p1.PlayHelmet(c1 as Helmet);
            }
            if (c1 is Shoe)
            {
                canplay = p1.PlayShoe (c1 as Shoe);
            }
            if (c1 is Amulette)
            {
                p1.PlayAmulette(c1 as Amulette);
            }
            if (c1 is Pets)
            {
                p1.PlayPets(c1 as Pets);
            }
            if (c1 is Spell)
            {
                p1.PlaySpell(c1 as Spell);
            }
            //Regarde sil y a pas eu d'erreur sinon return lerreur
            if (canplay == PlayCardResult.NoEnoughHand)
            {
                p1.PlayerState = Player.PlayerStatesEnum.ChangingWeapon;
                return PlayCardResult.NoEnoughHand;
            }
            if (canplay == PlayCardResult.NoEnoughBody)
            {
                p1.PlayerState = Player.PlayerStatesEnum.ChangingArmor;
                return PlayCardResult.NoEnoughBody;
            }
            if (canplay == PlayCardResult.NoEnoughHead)
            {
                p1.PlayerState = Player.PlayerStatesEnum.ChangingHelmet;
                return PlayCardResult.NoEnoughHead;
            }
            if (canplay == PlayCardResult.NoEnoughFeet)
            {
                p1.PlayerState = Player.PlayerStatesEnum.ChangingShoe;
                return PlayCardResult.NoEnoughFeet;
            }
            if(canplay == PlayCardResult.CantCastSpell)
            {
                p1.PlayerState = p1.LastStates;
                return PlayCardResult.NoEnoughFeet;
            }

            if (c1 is Object)
            {
                Cards.Remove(c1);
                p1.Mana -= c1.Cost;
                Console.WriteLine("You play a {0}. It cost {1} Mana. You have {2} Mana.", c1.Name, c1.Cost, p1.Mana);
            }
            else if ( c1 is Spell)
            {
                Console.WriteLine("You play a {0}. ", c1.Name);
            }
            return PlayCardResult.OK;
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
        public Monster DrawMonster(PilesdeCarte pilesdeCartes)
        {
            Monster m1 = pilesdeCartes.GetRandomCard("Monster") as Monster;
            return m1;
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
