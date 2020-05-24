using System;
using System.Collections.Generic;
using System.Text;

namespace Jeu_de_Socitété___Izulmha
{
    class Commande
    {
        private PilesdeCarte _pilesdeCartes;

        public Commande(PilesdeCarte pilesdeCartes)
        {
            _pilesdeCartes = pilesdeCartes;
        }


        public void AllCommande(Player p1 )
        {
            while (true)
            { 
                ShowOptions(p1);
                Console.Write(" --> ");
                string rep = Console.ReadLine();
                //Piocher
                if (p1.PlayerState == Player.PlayerStatsEnum.Drawing && rep == "dc")
                {
                    p1.PlayerState = Player.PlayerStatsEnum.ChoosingPile;
                    continue;
                }
                //Choisir la pioche
                if (p1.PlayerState == Player.PlayerStatsEnum.ChoosingPile && (rep == "o" || rep == "s")) 
                {
                    string choosenPile = "";
                        if (rep == "s")
                        {
                            choosenPile = "Spell";
                        }
                        else if (rep == "o")
                        {
                            choosenPile = "Object";
                        }
                    p1.Cards.DrawCard(_pilesdeCartes, 1, choosenPile);
                    p1.PlayerState = Player.PlayerStatsEnum.Drawing;
                    break;
                }
                //Jouer un objet
                if (p1.PlayerState == Player.PlayerStatsEnum.PlayingObject && rep == "po")
                {
                    p1.Cards.ShowHandCards();
                    p1.PlayerState = Player.PlayerStatsEnum.ChoosingObject;
                    continue;
                }
                //Choisir l'objet
                if (p1.PlayerState == Player.PlayerStatsEnum.ChoosingObject && p1.Cards.Cards.Count != 0 && (rep == "c1" || rep == "c2" || rep == "c3" || rep == "c4" || rep == "c5" || rep == "c6" ))
                {
                    int leng = p1.Cards.Cards.Count;
                    for (int i = 0; i < leng; i++)
                    {
                        string card = "c" + Convert.ToString(i+1); 
                        if (leng > 0 && p1.Cards.Cards[i].PileCarte == "Object" && p1.Cards.Cards[i].Cost < p1.ManaGemme && rep == card)
                        {
                            PlayCardResult pcr =  p1.Cards.PlayCard(p1.Cards.Cards[i], p1);
                            p1.PlayerState = Player.PlayerStatsEnum.PlayingObject;
                            continue;
                        }
                    }
                }
                //Changer d'arme
                
                //Play spell
                if (p1.PlayerState != Player.PlayerStatsEnum.ChoosingPile && p1.PlayerState != Player.PlayerStatsEnum.ChoosingObject && rep == "ps")
                {
                    p1.Cards.ShowHandCards();
                    p1.PlayerState = Player.PlayerStatsEnum.ChoosingSpell;
                    continue;
                }
                //choose spell
                if (p1.PlayerState == Player.PlayerStatsEnum.ChoosingSpell && p1.Cards.Cards.Count != 0 && (rep == "c1" || rep == "c2" || rep == "c3" || rep == "c4" || rep == "c5" || rep == "c6"))
                {
                    int leng = p1.Cards.Cards.Count;
                    if (leng > 0 && p1.Cards.Cards[0].PileCarte == "Spell" && rep == "c1")
                    {
                        p1.Cards.PlayCard(p1.Cards.Cards[0], p1);
                        p1.PlayerState = Player.PlayerStatsEnum.PlayingObject;
                        continue;
                    }
                    if (leng > 1 && p1.Cards.Cards[1].PileCarte == "Spell" && rep == "c2")
                    {
                        p1.Cards.PlayCard(p1.Cards.Cards[1], p1);
                        p1.PlayerState = Player.PlayerStatsEnum.PlayingObject;
                        continue;
                    }
                    if (leng > 2 && p1.Cards.Cards[2].PileCarte == "Spell" && rep == "c3")
                    {
                        p1.Cards.PlayCard(p1.Cards.Cards[2], p1);
                        p1.PlayerState = Player.PlayerStatsEnum.PlayingObject;
                        continue;
                    }
                    if (leng > 3 && p1.Cards.Cards[3].PileCarte == "Spell" && rep == "c4")
                    {
                        p1.Cards.PlayCard(p1.Cards.Cards[3], p1);
                        p1.PlayerState = Player.PlayerStatsEnum.PlayingObject;
                        continue;

                    }
                    if (leng > 4 && p1.Cards.Cards[4].PileCarte == "Spell" && rep == "c5")
                    {
                        p1.Cards.PlayCard(p1.Cards.Cards[4], p1);
                        p1.PlayerState = Player.PlayerStatsEnum.PlayingObject;
                        continue;
                    }
                    if (leng > 5 && p1.Cards.Cards[5].PileCarte == "Spell" && rep == "c6")
                    {
                        p1.Cards.PlayCard(p1.Cards.Cards[5], p1);
                        p1.PlayerState = Player.PlayerStatsEnum.PlayingObject;
                        continue;
                    }
                }
                //se battre contre un monstre
                if (p1.PlayerState == Player.PlayerStatsEnum.Fighting)
                {
                }
                if (p1.PlayerState == Player.PlayerStatsEnum.GivingCard)
                {
                }
                if (p1.PlayerState == Player.PlayerStatsEnum.PlayingObject && rep == "co")
                {
                    break;
                }
                Console.WriteLine("Choose a correct commande.");
            }
            
        }
        public void ShowOptions(Player p1)
        {
            Console.WriteLine();
            Console.WriteLine("Here are the posibility: ");
            //Drawing
            if (p1.PlayerState == Player.PlayerStatsEnum.Drawing )
            {
                Console.WriteLine(" - You can draw a card. (Draw Card: dc)");
            }
            //choos pile
            if (p1.PlayerState == Player.PlayerStatsEnum.ChoosingPile)
            {
                Console.WriteLine(" - You can choose de stack. (Object: o - Spell: s)");
            }
            //Play object
            if (p1.PlayerState == Player.PlayerStatsEnum.PlayingObject && p1.Cards.Cards.Count != 0 )
            {
                bool stilobjet = false;
                for (int i = 0; i < p1.Cards.Cards.Count; i ++)
                {
                    if (p1.Cards.Cards[i].PileCarte == "Object")
                    {
                        stilobjet = true;
                        break;
                    }
                    if (i + 1 == p1.Cards.Cards.Count)
                    {
                        Console.WriteLine(" - You do not have Object to play.");
                    }
                }
                if (stilobjet)
                {
                    Console.WriteLine(" - You can play an Object. (Play Object: po)");
                }
            }
            //Choose object
            if (p1.PlayerState == Player.PlayerStatsEnum.ChoosingObject)
            {
                Console.Write(" - You can choose which Object to play ? ( ");
                for (int i = 0; i < p1.Cards.Cards.Count; i++)
                {
                    if (p1.Cards.Cards[i].PileCarte == "Object")
                    {
                        if (i != 0)
                        {
                            Console.Write(", ");
                        }
                        Console.Write("c{0}", i + 1);
                    }
                }
                Console.WriteLine(" )");
            }
            //Play spell
            if (p1.PlayerState != Player.PlayerStatsEnum.ChoosingPile && p1.PlayerState != Player.PlayerStatsEnum.ChoosingObject && p1.PlayerState != Player.PlayerStatsEnum.ChoosingSpell)
            {
                bool stillspell = false;
                for (int i = 0; i < p1.Cards.Cards.Count; i++)
                {
                    if (p1.Cards.Cards[i].PileCarte == "Spell")
                    {
                        stillspell = true;
                        break;
                    }
                }
                if (stillspell)
                {
                    Console.WriteLine(" - You can play a Spell. (Play Spell: ps)");
                }
            }
            //choose spell
            if (p1.PlayerState == Player.PlayerStatsEnum.ChoosingSpell)
            {
                Console.Write(" - You can choose which Spell to play ? ( ");
                for (int i = 0; i < p1.Cards.Cards.Count; i++)
                {
                    if (p1.Cards.Cards[i].PileCarte == "Spell")
                    {
                        if (i != 0)
                        {
                            Console.Write(", ");
                        }
                        Console.Write("c{0}", i + 1);
                    }
                }
                Console.WriteLine(" )");
            }
            //Combattre monstre
            if (p1.PlayerState == Player.PlayerStatsEnum.Fighting)
            {
                Console.WriteLine(" - You can choose the object you will use this fight. Choose Object: co)");
            }
            //Donner des cartes
            if (p1.PlayerState == Player.PlayerStatsEnum.GivingCard)
            {
                Console.WriteLine(" - You can give card. (Give Card: gc)");
            }
            //Stop Play object
            if (p1.PlayerState == Player.PlayerStatsEnum.PlayingObject)
            {
                Console.WriteLine(" - Continue: co");
            }
            //Go back
            if (true)
            {
                Console.WriteLine(" - Go back: gb. ");
            }
        }

    }
}
