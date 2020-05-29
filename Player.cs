using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Jeu_de_Socitété___Izulmha
{
    class Player
    {
        #region public properties
        //Name/class/race
        public string PlayerName;
        public Classe PlayerClass;
        public int ClassLevel = 1;
        public Races PlayerRace;
        public int RaceLevel = 1;
        public int PlayerNumber;
        List<Player> _listofPlayer;
        //Stats
            //Vie
        public int BasicHP = 8;
        public int HP;
            //Physique
        public int BasicStrength = 1;
        public int Strength;
            //Magique
        public int BasicPower = 1;
        public int Power;
            //Mana
        public int ManaGemme = 2;
        public int Mana;

        //Objets
        public int MaxHands = 2;
        public List<Weapon> _weaponsPlayed = new List<Weapon>();
        public Armor _armorPlayed = null;
        public Helmet _helmetPlayed = null;
        public Shoe _shoesPlayed = null;
        public List<Amulette> _amulettePlayed = new List<Amulette>();
        public List<Pets> _petsPlayed = new List<Pets>();

        // Cartes
        public PlayerHand Cards = new PlayerHand();
        public int maxCardInHand = 10;

        //Vente
        public int SellingGold = 0;
        public int cardToDraw = 0;

        //Etat
        public enum PlayerStatesEnum 
        { 
            NotIsTurn, 
            //1
            Drawing, 
            ChoosingPile, 
            //2
            PlayingObject, 
            ChoosingObject, 
            ChoosingSpell, 
            ChangingWeapon, 
            ChoosingWeapon, 
            ChangingArmor, 
            ChangingHelmet, 
            ChangingShoe,
            //3
            ChoosingPets,
            ChoosingDamage,
            Fighting,
            //4
            SellingGivingCard,
            //Autre
            ChoosingPlayer,
            ChoosingObjectPlayer
        }
        public PlayerStatesEnum PlayerState;
        public PlayerStatesEnum LastStates;

        //Fight
        public bool FightWithStrength = true;
        public NormalMonster FightMonster = null;

        #endregion public properties


        public Player(string nameInput, Classe classofplayer, Races raceofplayer, List<Player> listofPlayer)
        {
            PlayerName = nameInput;
            PlayerClass = classofplayer;
            PlayerRace = raceofplayer;
            HP = BasicHP;
            Strength = BasicStrength + PlayerClass.StrengthBonus;
            Power = BasicPower + PlayerClass.PowerBonus ;
            Mana = ManaGemme + PlayerClass.ManaBonus;

            _listofPlayer = listofPlayer;
        }

        //Crée un joueur
        public static Player CreatNewRandomPlayer(List<Player> listofPlayer)
        {
            //Name
            Console.Write("Enter your name: \n --> ");
            string rep1 = Console.ReadLine();
            //string rep1 = ChooseName();
            //Class
            Classe classofplayer = null;
            int n = Aleatoire.RandomInt(Classe.ClassPoss.Count);
            classofplayer = Classe.ClassPoss[n];
            Classe.ClassPoss.RemoveAt(n);

            // todo remove
            classofplayer = new Archer();

            //Race
            Races raceofplayer = null;
            n = Aleatoire.RandomInt(Races.RacePoss.Count);
            raceofplayer = Races.RacePoss[n];
            Races.RacePoss.RemoveAt(n);

            //Instancie le Player
            Player p1 = new Player(rep1, classofplayer, raceofplayer, listofPlayer);
            return p1;
        }
        private static void CreatNewPlayer()
        {

        }

        private static string ChooseName()
        {
            while (true)
            {
                Console.Write("Enter your name: \n --> ");
                string rep1 = Console.ReadLine();
                //Check si correct
                Console.WriteLine("Are you sure {0} is your name ? (yes)", rep1);
                Console.Write("--> ");
                string rep2 = Console.ReadLine();
                if (rep2 == "yes")
                {
                    return rep1;
                }
                else
                {
                    continue;
                }
            }
        }

        internal void Play(PilesdeCarte pilesdeCartes, Commande C)
        {
            ManaGemme += 1;
            Mana = ManaGemme;
            Console.WriteLine("You get 1 Mana Gemme. And the other are fill. You got {0} Mana.", ManaGemme);
            //1
            Console.Write("\n1. Draw Card");
            PlayerState = PlayerStatesEnum.Drawing;
            C.AllCommande(this);
            C.AllCommande(this);


            //2
            Console.Write("\n2. Play Card");
            PlayerState = PlayerStatesEnum.PlayingObject;
            C.AllCommande(this);


            //3
            Console.Write("\n3. Fight a random Monster. ");
            Monster m1 =  Cards.DrawMonster(pilesdeCartes);
            m1 = PlayerClass.ApplayAbilityDrawMonster(this, m1, pilesdeCartes);

            if (m1 is NormalMonster)
            {
                FightMonster = m1 as NormalMonster;
            }
            else
            {
                //Consigne pour le boss
            }
            Console.Write("You are facing ", FightMonster.Name);
            FightMonster.ShowCard();

            PlayerState = PlayerStatesEnum.ChoosingDamage;
            C.AllCommande(this); 
            PlayerState = PlayerStatesEnum.ChoosingPets;
            C.AllCommande(this);
            PlayerState = PlayerStatesEnum.Fighting;
            PlayerClass.ApplyAbilityStartFight(this);
            C.AllCommande(this);

            //Pets
            for (int i = 0; i < _petsPlayed.Count; )
            {
                _petsPlayed[i].EndOfAFight();
                if (_petsPlayed[i].HP <= 0)
                {
                    Console.WriteLine("Your {0} is dead. ", _petsPlayed[i].Name);
                    _petsPlayed.RemoveAt(i);
                    continue;
                }
                i++;
            }

            //4
            Console.Write("\n4. Selling Object (per 500 gold draw a Card).");
            do
            {
                PlayerState = PlayerStatesEnum.SellingGivingCard;
                C.AllCommande(this);
                while (cardToDraw > 0)
                {
                    C.AllCommande(this);
                    cardToDraw--;
                }
                if (Cards.Cards.Count > maxCardInHand)
                {
                    Console.WriteLine("You have to much Cards. Sell them, or throw some.");
                }
                SellingGold = 0;
            } while (Cards.Cards.Count > maxCardInHand);

            //5
            PlayerState = PlayerStatesEnum.NotIsTurn;

        }



        //Methode du player en jeu
        public void WritePlayerDescritpion()
        {
            Console.WriteLine("{0} the {1} {2}. ", PlayerName, PlayerClass.Name, PlayerRace.Name);
        }
        public void WritePlayerStats()
        {
            int x = 1;
            x += 2;
            if (PlayerState != PlayerStatesEnum.Fighting)
            {
                Console.WriteLine("{0} the {1} {2}:", PlayerName, PlayerClass.Name, PlayerRace.Name);
                int? totalStrength = Strength;
                int? totalPower = Power;
                int? totalMana = Mana;
                int? totalManaGemme = ManaGemme;
                int longWeapon = _weaponsPlayed.Count;
                for (int i = 0; i < longWeapon; i++)
                {
                    if (_weaponsPlayed[i].Mana.HasValue)
                    {
                        totalMana += _weaponsPlayed[i].Mana;
                        totalManaGemme += _weaponsPlayed[i].Mana;
                    }
                    Console.Write("Weapon {0}: {1}. ", i + 1, _weaponsPlayed[i].Name);
                }
                if (longWeapon == 0) { Console.Write("No Weapons. "); }

                if (_armorPlayed != null)
                {
                    if (_armorPlayed.Mana.HasValue)
                    {
                        totalMana += _armorPlayed.Mana;
                        totalManaGemme += _armorPlayed.Mana;
                    }
                    Console.Write("Armor: {0}. ", _armorPlayed.Name);
                }
                else { Console.Write("No Armor. "); }

                if (_helmetPlayed != null)
                {
                    if (_helmetPlayed.Mana.HasValue)
                    {
                        totalMana += _helmetPlayed.Mana;
                        totalManaGemme += _helmetPlayed.Mana;
                    }
                    Console.Write("Helmet: {0}. ", _helmetPlayed.Name);
                }
                else { Console.Write("No Helmet. "); }

                int longAmulette = _amulettePlayed.Count;
                for (int i = 0; i < longAmulette; i++)
                {
                    if (_amulettePlayed[i].Mana.HasValue)
                    {
                        totalMana += _amulettePlayed[i].Mana;
                        totalManaGemme += _amulettePlayed[i].Mana;
                    }
                    Console.Write("Amulette {0}: {1}. ", i + 1, _amulettePlayed[i].Name);
                }
                if (longAmulette == 0) { Console.Write("No Amulette. "); }

                int longPets = _petsPlayed.Count;
                for (int i = 0; i < longPets; i++)
                {
                    if (_petsPlayed[i].Mana.HasValue)
                    {
                        totalMana += _petsPlayed[i].Mana;
                        totalManaGemme += _petsPlayed[i].Mana;
                    }
                    Console.Write("Pets {0}: {1}. ", i + 1, _petsPlayed[i].Name);
                }
                if (longPets == 0) { Console.Write("No Pets. "); }

                Console.WriteLine();
                if (PlayerState != PlayerStatesEnum.Fighting)
                {
                    Console.WriteLine("Strength: {0}, Power: {1}, Mana: {2}/{3}.", GetTotalPower(), GetTotalPower(), totalMana, totalManaGemme);
                }
                else if (FightWithStrength)
                {
                    Console.WriteLine("Puissance: {0}.", GetTotalPower());
                }
                else
                {
                    Console.WriteLine("Puissance: {0}.", GetTotalPower());
                }
            }
        }
        public int? GetTotalStrength()
        {
            int? totalStrength = Strength;
            int longWeapon = _weaponsPlayed.Count;
            for (int i = 0; i < longWeapon; i++)
            {
                if (_weaponsPlayed[i].Strength.HasValue)
                {
                    totalStrength += _weaponsPlayed[i].Strength;
                }
            }

            if (_armorPlayed != null)
            {
                if (_armorPlayed.Strength.HasValue)
                {
                    totalStrength += _armorPlayed.Strength;
                }
            }

            if (_helmetPlayed != null)
            {
                if (_helmetPlayed.Strength.HasValue)
                {
                    totalStrength += _helmetPlayed.Strength;
                }
            }

            int longAmulette = _amulettePlayed.Count;
            for (int i = 0; i < longAmulette; i++)
            {
                if (_amulettePlayed[i].Strength.HasValue)
                {
                    totalStrength += _amulettePlayed[i].Strength;
                }
            }

            int longPets = _petsPlayed.Count;
            for (int i = 0; i < longPets; i++)
            {
                if (_petsPlayed[i].inFight)
                {
                    totalStrength += _petsPlayed[i].Puissance;
                }
                if (_petsPlayed[i] is TotemOfStrength)
                {
                    totalStrength += (_petsPlayed[i] as TotemOfStrength).GivePlayerStrength();
                }
            }
            totalStrength += PlayerClass.ApplyAbilityCountStrength(this);
            return totalStrength;
        }
        public int? GetTotalPower()
        {
            int? totalPower = Power;
            int longWeapon = _weaponsPlayed.Count;
            for (int i = 0; i < longWeapon; i++)
            {
                if (_weaponsPlayed[i].Power.HasValue)
                {
                    totalPower += _weaponsPlayed[i].Power;
                }
            }

            if (_armorPlayed != null)
            {
                if (_armorPlayed.Power.HasValue)
                {
                    totalPower += _armorPlayed.Power;
                }
            }

            if (_helmetPlayed != null)
            {
                if (_helmetPlayed.Power.HasValue)
                {
                    totalPower += _helmetPlayed.Power;
                }
            }

            int longAmulette = _amulettePlayed.Count;
            for (int i = 0; i < longAmulette; i++)
            {
                if (_amulettePlayed[i].Power.HasValue)
                {
                    totalPower += _amulettePlayed[i].Power;
                }
            }

            int longPets = _petsPlayed.Count;
            for (int i = 0; i < longPets; i++)
            {
                if (_petsPlayed[i].inFight)
                {
                    totalPower += _petsPlayed[i].Puissance;
                }
            }
            return totalPower;
        }

        //Ajoute des Cartes
        public PlayCardResult PlayWeapon(Weapon w1)
        {
            int totalHand = _weaponsPlayed.Select(x => x.HandTake).Sum() + w1.HandTake;
            if (totalHand > MaxHands)
            {
                return PlayCardResult.NoEnoughHand;
            }
            _weaponsPlayed.Add(w1);
            return PlayCardResult.OK;
        }
        public PlayCardResult PlayArmor(Armor a1)
        {
            if (_armorPlayed != null)
            {
                return PlayCardResult.NoEnoughBody;
            }
            _armorPlayed = a1;
            return PlayCardResult.OK;
        }
        public PlayCardResult PlayHelmet(Helmet h1)
        {
            if (_helmetPlayed != null)
            {
                return PlayCardResult.NoEnoughHead;
            }
            _helmetPlayed = h1;
            return PlayCardResult.OK;
        }
        public PlayCardResult PlayShoe (Shoe s1)
        {
            if (_shoesPlayed != null)
            {
                return PlayCardResult.NoEnoughFeet;
            }
        _shoesPlayed = s1;
            return PlayCardResult.OK;
        }
        public void PlayAmulette(Amulette a1)
        {
            _amulettePlayed.Add(a1);
        }
        public void PlayPets(Pets p1)
        {
            _petsPlayed.Add(p1);
        }


        //Joue les Spell
        public PlayCardResult PlaySpell(Spell s1)
        {
            PlayCardResult result = PlayCardResult.OK;
            result = s1.CheckCanPlay(_listofPlayer);
            if (result == PlayCardResult.CantCastSpell)
            {
                return result;
            }
            else
            {
                s1.CastSpell();
                return result;
            }
        }

    }
}
