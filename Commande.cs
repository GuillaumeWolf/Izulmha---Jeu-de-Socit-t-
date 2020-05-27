using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks.Dataflow;

namespace Jeu_de_Socitété___Izulmha
{
    class Commande
    {
        private PilesdeCarte _pilesdeCartes;
        private List<Player> _listOfPlayer;
        private Carte _carteToExchange = null;
        private bool FightWithStrength = true;

        public Commande(PilesdeCarte pilesdeCartes, List<Player> listOfPlayer)
        {
            _pilesdeCartes = pilesdeCartes;
            _listOfPlayer = listOfPlayer;
        }


        public void AllCommande(Player p1)
        {
            while (true)
            { 
                ShowOptions(p1);
                Console.Write(" --> ");
                string rep = Console.ReadLine();
                //Piocher
                if (p1.PlayerState == Player.PlayerStatsEnum.Drawing && rep == "dc")
                {
                    p1.LastStates = p1.PlayerState;
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
                    if(p1.LastStates == Player.PlayerStatsEnum.SellingGivingCard)
                    {
                        break;
                    }
                    p1.PlayerState = Player.PlayerStatsEnum.Drawing;
                    break;
                }
                //Jouer un objet
                if (p1.PlayerState == Player.PlayerStatsEnum.PlayingObject && rep == "po")
                {
                    bool stilobjet = false;
                    for (int i = 0; i < p1.Cards.Cards.Count; i++)
                    {
                        if (p1.Cards.Cards[i].PileCarte == "Object" && p1.Cards.Cards[i].Cost <= p1.Mana)
                        {
                            stilobjet = true;
                            break;
                        }
                    }
                    if (stilobjet)
                    {
                        p1.LastStates = p1.PlayerState;
                        p1.PlayerState = Player.PlayerStatsEnum.ChoosingObject;
                        continue;
                    }
                }
                //Choisir l'objet
                if (p1.PlayerState == Player.PlayerStatsEnum.ChoosingObject && p1.Cards.Cards.Count != 0 )
                {
                    bool hasPassed = false;
                    int leng = p1.Cards.Cards.Count;
                    for (int i = 0; i < leng; i++)
                    {
                        string card = "c" + Convert.ToString(i+1); 
                        if (leng > 0 && p1.Cards.Cards[i].PileCarte == "Object" && p1.Cards.Cards[i].Cost <= p1.Mana && rep == card)
                        {
                            PlayCardResult pcr =  p1.Cards.PlayCard(p1.Cards.Cards[i], p1);
                            hasPassed = true;
                            if (pcr == PlayCardResult.NoEnoughHand || pcr == PlayCardResult.NoEnoughBody || pcr == PlayCardResult.NoEnoughHead || pcr == PlayCardResult.NoEnoughFeet)
                            {
                                _carteToExchange = p1.Cards.Cards[i];
                                break;
                            }
                            p1.PlayerState = Player.PlayerStatsEnum.PlayingObject;
                            break;
                        }
                    }
                    if (hasPassed)
                    {
                        continue;
                    }
                }
                //Changer d'arme
                if(p1.PlayerState == Player.PlayerStatsEnum.ChangingWeapon)
                {
                    if(rep == "yes")
                    {
                        p1.LastStates = p1.PlayerState;
                        p1.PlayerState = Player.PlayerStatsEnum.ChoosingWeapon;
                        continue;
                    }
                    if (rep == "no")
                    {
                        p1.PlayerState = Player.PlayerStatsEnum.ChoosingObject;
                        continue;
                    }
                }
                //Choisir L'arme a jeter
                if (p1.PlayerState == Player.PlayerStatsEnum.ChoosingWeapon)
                {
                    int leng = p1._weaponsPlayed.Count;
                    for (int i = 0; i < leng; i++)
                    {
                        if (_carteToExchange is Weapon)
                        {
                            Weapon lplplp = _carteToExchange as Weapon;
                            string card = "w" + Convert.ToString(i + 1);
                            if (lplplp.HandTake - (p1.MaxHands - p1._weaponsPlayed.Count) - p1._weaponsPlayed[i].HandTake <= 0 && rep == card)
                            {
                                Console.WriteLine("You throw a {0} away.", p1._weaponsPlayed[i].Name);
                                p1._weaponsPlayed.Remove(p1._weaponsPlayed[i]);
                                p1.Cards.PlayCard(_carteToExchange, p1);
                                p1.PlayerState = Player.PlayerStatsEnum.PlayingObject;
                                break;
                            }
                            else if (rep == card)
                            {
                                Console.WriteLine("You throw a {0} away.", p1._weaponsPlayed[i].Name);
                                p1._weaponsPlayed.Remove(p1._weaponsPlayed[i]);
                                break; ;
                            }
                        }
                    }
                    continue;                    
                }
                //Changer de Armor
                if (p1.PlayerState == Player.PlayerStatsEnum.ChangingArmor)
                {
                    if(rep == "yes")
                    {
                        Console.WriteLine("You trow your {0} away. ", p1._armorPlayed.Name);
                        p1._armorPlayed = null;
                        p1.Cards.PlayCard(_carteToExchange, p1);
                        p1.PlayerState = Player.PlayerStatsEnum.PlayingObject;
                        continue;
                    }
                    else if (rep == "no")
                    {
                        p1.PlayerState = Player.PlayerStatsEnum.ChoosingObject;
                        continue;
                    }
                }
                //Changer de Helmet
                if (p1.PlayerState == Player.PlayerStatsEnum.ChangingHelmet)
                {
                    if (rep == "yes")
                    {
                        Console.WriteLine("You trow your {0} away. ", p1._helmetPlayed.Name);
                        p1._helmetPlayed = null;
                        p1.Cards.PlayCard(_carteToExchange, p1);
                        p1.PlayerState = Player.PlayerStatsEnum.PlayingObject;
                        continue;
                    }
                    else if (rep == "no")
                    {
                        p1.PlayerState = Player.PlayerStatsEnum.ChoosingObject;
                        continue;
                    }
                }
                //Changer de Shoe
                if (p1.PlayerState == Player.PlayerStatsEnum.ChangingShoe)
                {
                    if (rep == "yes")
                    {
                        Console.WriteLine("You trow your {0} away. ", p1._shoesPlayed.Name);
                        p1._shoesPlayed = null;
                        p1.Cards.PlayCard(_carteToExchange, p1); 
                        p1.PlayerState = Player.PlayerStatsEnum.PlayingObject;
                        continue;
                    }
                    else if (rep == "no")
                    {
                        p1.PlayerState = Player.PlayerStatsEnum.ChoosingObject;
                        continue;
                    }
                }
                //Play spell
                if ((p1.PlayerState == Player.PlayerStatsEnum.Drawing || p1.PlayerState == Player.PlayerStatsEnum.PlayingObject || p1.PlayerState == Player.PlayerStatsEnum.ChoosingPets) && rep == "ps")
                {
                    p1.Cards.ShowHandCards();
                    p1.LastStates = p1.PlayerState;
                    p1.PlayerState = Player.PlayerStatsEnum.ChoosingSpell;
                    continue;
                }
                //Choose spell
                if (p1.PlayerState == Player.PlayerStatsEnum.ChoosingSpell && p1.Cards.Cards.Count != 0 )
                {
                    bool hasPassed = false;
                    int leng = p1.Cards.Cards.Count;
                    for (int i = 0; i < leng; i++)
                    {
                        string card = "c" + Convert.ToString(i + 1);
                        if (leng > 0 && p1.Cards.Cards[i].PileCarte == "Spell" && rep == card)
                        {
                            PlayCardResult pcr = p1.Cards.PlayCard(p1.Cards.Cards[i], p1);
                            p1.PlayerState = p1.LastStates;
                            hasPassed = true;
                            break;
                        }
                    }
                    if(hasPassed)
                    {
                        continue;
                    }
                }
                //Choisir les Pets avant le fight
                if (p1.PlayerState == Player.PlayerStatsEnum.ChoosingPets)
                {
                    bool haspassed = false;
                    int totalPets = p1._petsPlayed.Count;
                    for (int i = 0; i < totalPets; i++)
                    {
                        string ChoosenPets = "pet" + Convert.ToString(i+1);
                        if(rep == ChoosenPets)
                        {
                            p1._petsPlayed[i].inFight = !p1._petsPlayed[i].inFight;
                            haspassed = true;
                            break;
                        }
                    }
                    if (haspassed)
                    {
                        continue;
                    }
                }
                //Choisir le type de damage
                if(p1.PlayerState == Player.PlayerStatsEnum.ChoosingDamage)
                {
                    if (rep == "s")
                    {
                        FightWithStrength = true;
                        p1.FightWithStrength = true;
                        break;
                    }
                    else if (rep == "p")
                    {
                        FightWithStrength = false;
                        p1.FightWithStrength = false;
                        break;
                    }
                }
                //Se battre contre un monstre (a faire)
                if (p1.PlayerState == Player.PlayerStatsEnum.Fighting)
                {
                    if (rep == "sc")
                    {
                        break;
                    }
                }
                //Vendre des Cartes 
                if (p1.PlayerState == Player.PlayerStatsEnum.SellingGivingCard)
                {
                    Object o1 = null;
                    bool haspassed = false;
                    for (int i = 0; i < p1.Cards.Cards.Count; i++)
                    {
                        string objectChoosen = "o" + Convert.ToString(i+1);
                        int gold = 0;
                        if (p1.Cards.Cards[i] is Object && rep == objectChoosen)
                        {
                            o1 = p1.Cards.Cards[i] as Object;
                            Carte.RarityEnum RO = o1.Rarity;
                            switch (RO)
                            {
                                case Carte.RarityEnum.Common:
                                    gold = 100;
                                    break;
                                case Carte.RarityEnum.Rare:
                                    gold = 200;
                                    break;
                                case Carte.RarityEnum.Mythical:
                                    gold = 500;
                                    break;
                                case Carte.RarityEnum.Legendary:
                                    gold = 1000;
                                    break;
                            }
                            o1.isBeingSelled = !o1.isBeingSelled;
                            if (o1.isBeingSelled)
                            {
                                p1.SellingGold += gold;
                            }
                            else
                            {
                                p1.SellingGold -= gold;
                            }
                            haspassed = true;
                            break;
                        }
                    }
                    if(haspassed)
                    {
                        continue;
                    }

                    if (rep == "sell")
                    {
                        for (int i = 0; i < p1.Cards.Cards.Count; i++)
                        {
                            if (p1.Cards.Cards[i] is Object && (p1.Cards.Cards[i] as Object).isBeingSelled)
                            {
                                p1.Cards.Cards.RemoveAt(i);
                            }
                        }
                        p1.LastStates = Player.PlayerStatsEnum.SellingGivingCard;
                        p1.PlayerState = Player.PlayerStatsEnum.ChoosingPile;
                        p1.cardToDraw = p1.SellingGold / 500;
                        Console.WriteLine("You can draw {0} cards.", p1.cardToDraw);
                        break;
                    }
                    else if (rep == "throw")
                    {
                        int total = 0;
                        for (int i = 0; i < p1.Cards.Cards.Count; i++)
                        {
                            if (p1.Cards.Cards[i] is Object && (p1.Cards.Cards[i] as Object).isBeingSelled)
                            {
                                p1.Cards.Cards.RemoveAt(i);
                                total++;
                            }
                        }
                        p1.LastStates = Player.PlayerStatsEnum.SellingGivingCard;
                        Console.WriteLine("You throw {0} cards away.", total);
                        break;
                    }
                }
                //Voir Les states d'un joueur
                if(true)
                {
                    bool hasPassed = false;
                    int NumPlayer = _listOfPlayer.Count;
                    for (int i = 0; i < NumPlayer; i++)
                    {
                        string ChoosenPlayer = "p" + Convert.ToString(i + 1);
                        if (rep == ChoosenPlayer && i+1 != p1.PlayerNumber)
                        {
                            Console.Write("States of ");
                            _listOfPlayer[i].WritePlayerStats();
                            hasPassed = true;
                            continue;
                        }
                    }
                    if ( rep == "me")
                    {
                        Console.WriteLine();
                        Console.WriteLine("My States: ");
                        p1.WritePlayerStats();
                        Console.WriteLine();
                        hasPassed = true;
                    }
                    if(hasPassed)
                    {
                        continue;
                    }
                }
                //Voir ses cartes
                if (true)
                {
                    if (rep == "ca")
                    {
                        p1.Cards.ShowHandCards();
                        continue;
                    }
                }
                //Voir la deffausse
                if (true)
                {
                    int dism = _pilesdeCartes.DeffausseMonster.Count;
                    int diso = _pilesdeCartes.DeffausseObject.Count;
                    int diss = _pilesdeCartes.DeffausseSpell.Count;
                    if ((dism != 0 || diso != 0 || diss != 0) && rep == "dis")
                    {
                        if (dism > 0)
                        {
                            Console.WriteLine("Top of the Monster Discard Stack: {0}", _pilesdeCartes.DeffausseMonster[dism-1].Name);
                        }
                        if (diso > 0)
                        {
                            Console.WriteLine("Top of the Object Discard Stack: {0}", _pilesdeCartes.DeffausseObject[diso - 1].Name);
                        }
                        if (diss > 0)
                        {
                            Console.WriteLine("Top of the Spell Discard Stack: {0}", _pilesdeCartes.DeffausseSpell[diss - 1].Name);
                        }
                        continue;
                    }
                }
                //Proposer un échange (a faire)
                //Continue
                if ((p1.PlayerState == Player.PlayerStatsEnum.PlayingObject || p1.PlayerState == Player.PlayerStatsEnum.ChoosingPets) && rep == "co")
                {
                    if (p1.PlayerState == Player.PlayerStatsEnum.SellingGivingCard)
                    {
                        for (int i = 0; i < p1.Cards.Cards.Count; i++)
                        {
                            if (p1.Cards.Cards[i] is Object)
                            {
                                (p1.Cards.Cards[i] as Object).isBeingSelled = false;
                            }
                        }
                    }
                    break;
                }
                //Go Back
                if ((p1.PlayerState == Player.PlayerStatsEnum.ChoosingObject || p1.PlayerState == Player.PlayerStatsEnum.ChoosingSpell || p1.PlayerState == Player.PlayerStatsEnum.ChoosingWeapon) && rep == "gb")
                {
                    p1.PlayerState = p1.LastStates;
                    continue;
                }
                //Commande de triche
                if(true)
                {
                    if (rep == "aa")
                    {
                        Weapon arme = new TrackingChain();
                        p1.Cards.Cards.Add(arme);
                        Console.WriteLine("You get a {0}. ", arme.Name);
                        continue;
                    }
                    if (rep == "bb")
                    {
                        p1.Cards.Cards.Add(new SmallWolf());
                        Console.WriteLine("You get a Small Wolf. ");
                        continue;
                    }
                    if (rep == "mana")
                    {
                        p1.Mana = 200;
                        continue;
                    }
                }
                Console.WriteLine("Choose a correct commande.");
            }
        }
        public void ShowOptions(Player p1)
        {
            Console.WriteLine();
            Console.WriteLine("Here are the posibility: ");
            //Drawing
            if (p1.PlayerState == Player.PlayerStatsEnum.Drawing)
            {
                Console.WriteLine(" - You can draw a card. (Draw Card: dc)");
            }
            //choos pile
            if (p1.PlayerState == Player.PlayerStatsEnum.ChoosingPile)
            {
                Console.WriteLine(" - You can choose de stack. (Object: o - Spell: s)");
            }
            //Play object
            if (p1.PlayerState == Player.PlayerStatsEnum.PlayingObject && p1.Cards.Cards.Count != 0)
            {
                bool stilobjet = false;
                for (int i = 0; i < p1.Cards.Cards.Count; i++)
                {
                    if (p1.Cards.Cards[i].PileCarte == "Object" && p1.Cards.Cards[i].Cost <= p1.Mana)
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
                int objectshow = 0;
                Console.Write(" - You can choose which Object to play : ( ");
                for (int i = 0; i < p1.Cards.Cards.Count; i++)
                {
                    if (p1.Cards.Cards[i].PileCarte == "Object" && p1.Cards.Cards[i].Cost <= p1.Mana)
                    {
                        if (objectshow != 0)
                        {
                            Console.Write(", ");
                        }
                        Console.Write("c{0}: {1}", i + 1, p1.Cards.Cards[i].Name);
                        objectshow++;
                    }
                }
                Console.WriteLine(" )");
            }
            //Changer d'arme
            if (p1.PlayerState == Player.PlayerStatsEnum.ChangingWeapon)
            {
                if (_carteToExchange is Weapon)
                {
                    int mainRestante = p1.MaxHands;
                    for (int i = 0; i < p1._weaponsPlayed.Count; i++)
                    {
                        mainRestante -= p1._weaponsPlayed[i].HandTake;
                    }
                    Weapon lplplp = _carteToExchange as Weapon;
                    if (lplplp.HandTake == p1.MaxHands)
                    {
                        Console.WriteLine(" - You had to throw all your Weapons to play the {0}: (yes or no)", _carteToExchange.Name);
                    }
                    else if (lplplp.HandTake - mainRestante > 0)
                    {
                        Console.WriteLine(" - You had to throw some Weapons for {0} hand place to play the {1}: (yes or no)", lplplp.HandTake - mainRestante, _carteToExchange.Name);
                    }
                    else { Console.WriteLine("Je n'aurais pas du passer par la Commande.ShowOptions.Changerdarme"); }
                }

            }
            //Choisir L'arme
            if (p1.PlayerState == Player.PlayerStatsEnum.ChoosingWeapon)
            {
                Console.Write(" - You can choose which Weapon to throw: ( ");
                for (int i = 0; i < p1._weaponsPlayed.Count; i++)
                {
                    if (i != 0)
                    {
                        Console.Write(", ");
                    }
                    Console.Write("w{0}: {1}", i + 1, p1._weaponsPlayed[i].Name);
                }
                Console.WriteLine(" )");
            }
            //Changer de Armor
            if (p1.PlayerState == Player.PlayerStatsEnum.ChangingArmor)
            {
                Console.WriteLine(" - You can replace your {0} by a {1}. (yes - no)", p1._armorPlayed.Name, _carteToExchange.Name);
            }
            //Changer de Helmet
            if (p1.PlayerState == Player.PlayerStatsEnum.ChangingHelmet)
            {
                Console.WriteLine(" - You can replace your {0} by a {1}. (yes - no)", p1._helmetPlayed.Name, _carteToExchange.Name);
            }
            //Changer de Shoe
            if (p1.PlayerState == Player.PlayerStatsEnum.ChangingShoe)
            {
                Console.WriteLine(" - You can replace your {0} by a {1}. (yes - no)", p1._shoesPlayed.Name, _carteToExchange.Name);
            }
            //Play spell
            if (p1.PlayerState == Player.PlayerStatsEnum.Drawing || p1.PlayerState == Player.PlayerStatsEnum.PlayingObject || p1.PlayerState == Player.PlayerStatsEnum.ChoosingPets)
            {
                bool stillspell = false;
                for (int i = 0; i < p1.Cards.Cards.Count; i++)
                {
                    if (p1.Cards.Cards[i].PileCarte == "Spell")
                    {
                        stillspell = true;
                        break;
                    }
                    if (i + 1 == p1.Cards.Cards.Count)
                    {
                        Console.WriteLine(" - You do not have Spell to play.");
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
            //Choisir les pets pour le fight
            if (p1.PlayerState == Player.PlayerStatsEnum.ChoosingPets)
            {
                int totalPets = p1._petsPlayed.Count;
                if (totalPets == 0)
                {
                    Console.WriteLine(" - You don't have Pets.");
                }
                else
                {
                    Console.Write(" - You can choose the pets you will use this fight. (write \"pet\" + num)\n     - Not in Fight: ");
                    int notInFightPets = 0;
                    for (int i = 0; i < totalPets; i++)
                    {
                        if (!p1._petsPlayed[i].inFight)
                        {
                            if (notInFightPets != 0)
                            {
                                Console.Write(", ");
                            }
                            notInFightPets++;
                            Console.Write("pet{0}: {1}", i + 1, p1._petsPlayed[i].Name);
                        }
                    }
                    Console.WriteLine();
                    Console.Write("     - In Fight: ");
                    int inFightPets = 0;
                    for (int i = 0; i < totalPets; i++)
                    {
                        if (p1._petsPlayed[i].inFight)
                        {
                            if (inFightPets != 0)
                            {
                                Console.Write(", ");
                            }
                            inFightPets++;
                            Console.Write("pet{0}: {1}", i + 1, p1._petsPlayed[i].Name);
                        }
                    }
                    Console.WriteLine();
                }
            }
            //Choisir le type de damage
            if (p1.PlayerState == Player.PlayerStatsEnum.ChoosingDamage)
            {
                Console.WriteLine(" - You can choose what Puissance you will use for the fight. (Strength: s ({0}) - Power: p ({1}))", p1.GetTotalStrength(), p1.GetTotalPower());
            }
            //Combattre
            if (p1.PlayerState == Player.PlayerStatsEnum.Fighting)
            {
                int? totalStrength = p1.GetTotalStrength();
                if (FightWithStrength)
                {
                    if (totalStrength > p1.FightMonster.Puissance)
                    {
                        Console.WriteLine(" - You are wining the fight ({0} - {1}), you can start the chrono: sc", totalStrength, p1.FightMonster.Puissance);
                    }
                    else
                    {
                        Console.WriteLine(" - You aren't wining the fight ({0} - {1}), play Cards to win the fight. ", totalStrength, p1.FightMonster.Puissance);
                    }
                }
                int? totalPower = p1.GetTotalPower();
                if (!FightWithStrength)
                {
                    if (totalPower > p1.FightMonster.Puissance)
                    {
                        Console.WriteLine(" - You are wining the fight ({0} - {1}), you can start the chrono: sc", totalPower, p1.FightMonster.Puissance);
                    }
                    else
                    {
                        Console.WriteLine(" - You aren't wining the fight ({0} - {1}), play Cards to win the fight. ", totalStrength, p1.FightMonster.Puissance);
                    }
                }
            }
            //Vendre des cartes
            if (p1.PlayerState == Player.PlayerStatsEnum.SellingGivingCard)
            {
                Console.Write(" - You can choose which Card to sell. ( ");
                for (int i = 0; i < p1.Cards.Cards.Count; i++)
                {
                    int gold = 100;
                    if (p1.Cards.Cards[i] is Object)
                    {
                        Object onparledecetobjet = p1.Cards.Cards[i] as Object;
                        if (!onparledecetobjet.isBeingSelled)
                        {
                            if (p1.Cards.Cards[i].Rarity == Carte.RarityEnum.Rare)
                            {
                                gold = 200;
                            }
                            if (p1.Cards.Cards[i].Rarity == Carte.RarityEnum.Mythical)
                            {
                                gold = 500;
                            }
                            if (p1.Cards.Cards[i].Rarity == Carte.RarityEnum.Legendary)
                            {
                                gold = 1000;
                            }
                            Console.Write("o{0}: {1} {2} gold. ", i + 1, p1.Cards.Cards[i].Name, gold);
                        }
                    }
                }
                Console.WriteLine(")");

                Console.Write(" - You can choose which Card to not sell. (");
                for (int i = 0; i < p1.Cards.Cards.Count; i++)
                {
                    int gold = 100;
                    if (p1.Cards.Cards[i] is Object)
                    {
                        Object onparledecetobjet = p1.Cards.Cards[i] as Object;
                        if (onparledecetobjet.isBeingSelled)
                        {
                            if (p1.Cards.Cards[i].Rarity == Carte.RarityEnum.Rare)
                            {
                                gold = 200;
                            }
                            if (p1.Cards.Cards[i].Rarity == Carte.RarityEnum.Mythical)
                            {
                                gold = 500;
                            }
                            if (p1.Cards.Cards[i].Rarity == Carte.RarityEnum.Legendary)
                            {
                                gold = 1000;
                            }
                            Console.Write("o{0}: {1} {2} gold. ", i + 1, p1.Cards.Cards[i].Name, gold);
                        }
                    }
                }
                Console.WriteLine(")");

                if (p1.SellingGold >= 500)
                {
                    int numcard = p1.SellingGold / 500;
                    Console.WriteLine(" - You can sell the choosen Object to draw {0} Cards. (sell) ", numcard);
                }
                else
                {
                    Console.WriteLine(" - You can throw the choosen Object away. (throw)");
                }
            }
            //Regarder les states d'un joueur
            if (true)
            {
                Console.Write(" - You can see the State of a player. ( ");
                int playerLong = _listOfPlayer.Count;
                for (int i = 0; i < playerLong; i++)
                {
                    if (i != 0)
                    {
                        Console.Write(", ");
                    }
                    if (i + 1 == p1.PlayerNumber)
                    {
                        Console.Write("me");
                        continue;
                    }
                    Console.Write("p{0}", i + 1);
                }
                Console.WriteLine(" )");
            }
            //Regarder ses cartes
            if (true)
            {
                Console.Write(" - You can see your cards. ( cards: ca ) ");
                Console.WriteLine();
            }
            //Voir la deffause
            if (true)
            {
                if (_pilesdeCartes.DeffausseMonster.Count != 0 || _pilesdeCartes.DeffausseSpell.Count != 0 || _pilesdeCartes.DeffausseObject.Count != 0)
                {
                    string? isdeffMonster = "";
                    if (_pilesdeCartes.DeffausseMonster.Count != 0)
                    {
                        isdeffMonster = "Montser";
                    }
                    string? isdeffObject = "";
                    if (_pilesdeCartes.DeffausseObject.Count != 0)
                    {
                        isdeffObject = "Object";
                    }
                    string? isdeffSpell = "";
                    if (_pilesdeCartes.DeffausseSpell.Count != 0)
                    {
                        isdeffSpell = "Spell";
                    }
                    if (isdeffSpell != "" || isdeffObject != "" || isdeffMonster != "")
                    {
                        Console.Write(" - You can see the top of the ");
                        if (isdeffMonster != "")
                        {
                            Console.Write(isdeffMonster);
                        }
                        if (isdeffObject != "")
                        {
                            if (isdeffMonster != "")
                            {
                                Console.Write(", ");
                            }
                            Console.Write(isdeffObject);
                        }
                        if (isdeffSpell != "")
                        {
                            if (isdeffObject != "")
                            {
                                Console.Write(", ");
                            }
                            Console.Write(isdeffSpell);
                        }
                        Console.Write(" Discard Stack. ( discard: dis ) ");
                    }
                    Console.WriteLine();
                }
            }
            //Continue
            if (p1.PlayerState == Player.PlayerStatsEnum.PlayingObject || p1.PlayerState == Player.PlayerStatsEnum.ChoosingPets)
            {
                Console.WriteLine(" - Continue: co");
            }
            //Go back
            if (p1.PlayerState == Player.PlayerStatsEnum.ChoosingObject || p1.PlayerState == Player.PlayerStatsEnum.ChoosingSpell || p1.PlayerState == Player.PlayerStatsEnum.ChoosingWeapon)
                {
                    Console.WriteLine(" - Go back: gb. ");
                }
        }
    }
}
