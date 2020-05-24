using System;
using System.Collections.Generic;
using System.Text;

namespace Jeu_de_Socitété___Izulmha
{
    class Commande
    {
        public static void AllCommande(Player p1, PilesdeCarte pilesdeCartes)
        {
            while (true)
            {
                ShowOptions(p1);
                Console.Write(" --> ");
                string rep = Console.ReadLine();
                //Piocher
                if (p1.PlayerStats == Player.PlayerStatsEnum.Drawing && rep == "dc")
                {
                    p1.PlayerStats = Player.PlayerStatsEnum.ChoosingPile;
                    continue;
                }
                //Choisir la pioche
                if (p1.PlayerStats == Player.PlayerStatsEnum.ChoosingPile && (rep == "o" || rep == "s")) 
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
                    p1.Cards.DrawCard(pilesdeCartes, 1, choosenPile);
                    p1.PlayerStats = Player.PlayerStatsEnum.Drawing;
                    break;
                }
                //Jouer un objet
                if (p1.PlayerStats == Player.PlayerStatsEnum.PlayingObject && rep == "po")
                {
                    p1.Cards.ShowHandCards();
                    p1.PlayerStats = Player.PlayerStatsEnum.ChoosingObject;
                    continue;
                }
                //Coisir l'objet
                if (p1.PlayerStats == Player.PlayerStatsEnum.ChoosingObject && p1.Cards.Cards.Count != 0 && (rep == "c1" || rep == "c2" || rep == "c3" || rep == "c4" || rep == "c5" || rep == "c6" ))
                {
                    int leng = p1.Cards.Cards.Count;
                    if(leng > 0 && p1.Cards.Cards[0].PileCarte == "Object" && p1.Cards.Cards[0].Cost < p1.ManaGemme && rep == "c1")
                    {
                        p1.Cards.PlayCard(p1.Cards.Cards[0], p1);
                        p1.PlayerStats = Player.PlayerStatsEnum.PlayingObject;
                        continue;
                    }
                    if (leng > 1 && p1.Cards.Cards[1].PileCarte == "Object" && p1.Cards.Cards[1].Cost < p1.ManaGemme && rep == "c2")
                    {
                        p1.Cards.PlayCard(p1.Cards.Cards[1], p1);
                        p1.PlayerStats = Player.PlayerStatsEnum.PlayingObject;
                        continue;
                    }
                    if (leng > 2 && p1.Cards.Cards[2].PileCarte == "Object" && p1.Cards.Cards[2].Cost < p1.ManaGemme && rep == "c3")
                    {
                        p1.Cards.PlayCard(p1.Cards.Cards[2], p1);
                        p1.PlayerStats = Player.PlayerStatsEnum.PlayingObject;
                        continue;
                    }
                    if (leng > 3 && p1.Cards.Cards[3].PileCarte == "Object" && p1.Cards.Cards[3].Cost < p1.ManaGemme && rep == "c4")
                    {
                        p1.Cards.PlayCard(p1.Cards.Cards[3], p1);
                        p1.PlayerStats = Player.PlayerStatsEnum.PlayingObject;
                        continue;

                    }
                    if (leng > 4 && p1.Cards.Cards[4].PileCarte == "Object" && p1.Cards.Cards[4].Cost < p1.ManaGemme && rep == "c5")
                    {
                        p1.Cards.PlayCard(p1.Cards.Cards[4], p1);
                        p1.PlayerStats = Player.PlayerStatsEnum.PlayingObject;
                        continue;
                    }
                    if (leng > 5 && p1.Cards.Cards[5].PileCarte == "Object" && p1.Cards.Cards[5].Cost < p1.ManaGemme && rep == "c6")
                    {
                        p1.Cards.PlayCard(p1.Cards.Cards[5], p1);
                        p1.PlayerStats = Player.PlayerStatsEnum.PlayingObject;
                        continue;
                    }
                }
                //Play spell
                if (p1.PlayerStats != Player.PlayerStatsEnum.ChoosingPile && p1.PlayerStats != Player.PlayerStatsEnum.ChoosingObject && rep == "ps")
                {
                    p1.Cards.ShowHandCards();
                    p1.PlayerStats = Player.PlayerStatsEnum.ChoosingSpell;
                    continue;
                }
                //choose spell
                if (p1.PlayerStats == Player.PlayerStatsEnum.ChoosingSpell && p1.Cards.Cards.Count != 0 && (rep == "c1" || rep == "c2" || rep == "c3" || rep == "c4" || rep == "c5" || rep == "c6"))
                {
                    int leng = p1.Cards.Cards.Count;
                    if (leng > 0 && p1.Cards.Cards[0].PileCarte == "Spell" && rep == "c1")
                    {
                        p1.Cards.PlayCard(p1.Cards.Cards[0], p1);
                        p1.PlayerStats = Player.PlayerStatsEnum.PlayingObject;
                        continue;
                    }
                    if (leng > 1 && p1.Cards.Cards[1].PileCarte == "Spell" && rep == "c2")
                    {
                        p1.Cards.PlayCard(p1.Cards.Cards[1], p1);
                        p1.PlayerStats = Player.PlayerStatsEnum.PlayingObject;
                        continue;
                    }
                    if (leng > 2 && p1.Cards.Cards[2].PileCarte == "Spell" && rep == "c3")
                    {
                        p1.Cards.PlayCard(p1.Cards.Cards[2], p1);
                        p1.PlayerStats = Player.PlayerStatsEnum.PlayingObject;
                        continue;
                    }
                    if (leng > 3 && p1.Cards.Cards[3].PileCarte == "Spell" && rep == "c4")
                    {
                        p1.Cards.PlayCard(p1.Cards.Cards[3], p1);
                        p1.PlayerStats = Player.PlayerStatsEnum.PlayingObject;
                        continue;

                    }
                    if (leng > 4 && p1.Cards.Cards[4].PileCarte == "Spell" && rep == "c5")
                    {
                        p1.Cards.PlayCard(p1.Cards.Cards[4], p1);
                        p1.PlayerStats = Player.PlayerStatsEnum.PlayingObject;
                        continue;
                    }
                    if (leng > 5 && p1.Cards.Cards[5].PileCarte == "Spell" && rep == "c6")
                    {
                        p1.Cards.PlayCard(p1.Cards.Cards[5], p1);
                        p1.PlayerStats = Player.PlayerStatsEnum.PlayingObject;
                        continue;
                    }
                }
                //se battre contre un monstre
                if (p1.PlayerStats == Player.PlayerStatsEnum.Fighting)
                {
                }
                if (p1.PlayerStats == Player.PlayerStatsEnum.GivingCard)
                {
                }
                if (p1.PlayerStats == Player.PlayerStatsEnum.PlayingObject && rep == "co")
                {
                    break;
                }
                Console.WriteLine("Choose a correct commande.");
            }
            
        }
        public static void ShowOptions(Player p1)
        {
            Console.WriteLine();
            Console.WriteLine("Here are the posibility: ");
            //Drawing
            if (p1.PlayerStats == Player.PlayerStatsEnum.Drawing )
            {
                Console.WriteLine(" - You can draw a card. (Draw Card: dc)");
            }
            //choos pile
            if (p1.PlayerStats == Player.PlayerStatsEnum.ChoosingPile)
            {
                Console.WriteLine(" - You can choose de stack. (Object: o - Spell: s)");
            }
            //Play object
            if (p1.PlayerStats == Player.PlayerStatsEnum.PlayingObject && p1.Cards.Cards.Count != 0 )
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
            if (p1.PlayerStats == Player.PlayerStatsEnum.ChoosingObject)
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
            if (p1.PlayerStats != Player.PlayerStatsEnum.ChoosingPile && p1.PlayerStats != Player.PlayerStatsEnum.ChoosingObject && p1.PlayerStats != Player.PlayerStatsEnum.ChoosingSpell)
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
            if (p1.PlayerStats == Player.PlayerStatsEnum.ChoosingSpell)
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
            if (p1.PlayerStats == Player.PlayerStatsEnum.Fighting)
            {
                Console.WriteLine(" - You can choose the object you will use this fight. Choose Object: co)");
            }
            //Donner des cartes
            if (p1.PlayerStats == Player.PlayerStatsEnum.GivingCard)
            {
                Console.WriteLine(" - You can give card. (Give Card: gc)");
            }
            //Stop Play object
            if (p1.PlayerStats == Player.PlayerStatsEnum.PlayingObject)
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
