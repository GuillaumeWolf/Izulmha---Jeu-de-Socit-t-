using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Jeu_de_Socitété___Izulmha
{
    #region Toutes les cartes
    abstract class Carte
    {
        #region Public Properties
        //Name and Type
        public string Name;
        public string PileCarte;
        public string Categorie;
        public RarityEnum Rarity;
        //Rarity
        public enum RarityEnum { Common, Rare, Mythical, Legendary, ClassMemberShip };

        //Cost
        public int Cost { get; set; }

        #endregion
        protected Carte(string pileCarte, string categorie, string name, RarityEnum rarity)
        {
            PileCarte = pileCarte; //Object - Spell - Monster
            Categorie = categorie; // Weapon - Armor - Helmet ...
            Name = name; //Bassique Sword - Magique Orb - ...
            Rarity = rarity; // Basique - Rare - mythical - legendary

            if (Rarity == RarityEnum.Common) { Cost = 3; }
            if (Rarity == RarityEnum.Rare) { Cost = 6; }
            if (Rarity == RarityEnum.Mythical) { Cost = 10; }
            if (Rarity == RarityEnum.Legendary) { Cost = 15; }

        }


        public virtual void ShowCard()
        {
            Console.Write("A {0}: it's a {1} {2} from the pile of {3}. ", Name, Rarity, Categorie, PileCarte);
            //Console.WriteLine();
        }
        public virtual void WhenSummoned()
        {

        }

    }


    #region Class Principale

    abstract class Object : Carte
    {
        public bool isBeingSelled = false;
        SpecialCardTypeEnum specialCardTypeEnum;
        protected Object(string categorie, string name, RarityEnum rarity, SpecialCardTypeEnum specialCardTypeEnum) : base("Object", categorie, name, rarity)
        {

        }
        public override void ShowCard()
        {
            base.ShowCard();
            if(specialCardTypeEnum != SpecialCardTypeEnum.Nothing)
            {
                Console.Write("Special Type: {0}.", specialCardTypeEnum);
            }
        }
    }
    abstract class Spell : Carte
    {
        public List<String> StringListPossibleValues = new List<string>();
        public List<Object> ObjectListPossibleValues = new List<Object>();
        public List<Player> PlayerListPossibleValue = new List<Player>();

        public bool isBeingSelled = false;

        protected Spell(string categorie, string name, RarityEnum rarity) : base("Spell", categorie, name, rarity)
        {

        }

        public virtual PlayCardResult CheckCanPlay(List<Player> lp1, Player p1)
        {
            PlayCardResult result = PlayCardResult.OK;
            return result;
        }
        public virtual void CastSpell()
        {

        }
        public virtual void WritePoss()
        {

        }
    }
    abstract class Monster : Carte
    {
        protected Monster(string categorie, string name, RarityEnum rarity) : base("Monster", categorie, name, rarity)
        {

        }
        public virtual void LostConsequencesFunc(Player p1)
        {

        }
    }

    #endregion


    #region Class Secondaire
    #region Object
    abstract class Weapon : Object
    {
        //Dégats 
        public int? Strength;
        public int? Power;
        //Mana
        public int? Mana;
        //Place
        public int HandTake = 1;
        //Property
        public string Utilities;
        protected Weapon(string name, RarityEnum rarity, SpecialCardTypeEnum specialCardTypeEnum) : base("Weapon", name, rarity, specialCardTypeEnum)
        {

        }
        public override void ShowCard()
        {
            base.ShowCard();
            Console.WriteLine();
            if (Strength.HasValue)
            {
                Console.Write("Strength: {0}. ", Strength.Value);
            }
            if (Power.HasValue)
            {
                Console.Write("Power: {0}. ", Power.Value);
            }
            if (Mana.HasValue)
            {
                Console.Write("Mana: {0}. ", Mana.Value);
            }
            Console.Write("Take {0} hand. ", HandTake);
            if(Utilities != null)
            {
                Console.Write("Utilities: {0} ", Utilities);
            }
        }
    }

    abstract class Armor : Object
    {
        //Armur 
        public int? Strength;
        public int? Power;
        //Mana
        public int? Mana;
        //Property
        public string Utilities;
        protected Armor(string name, RarityEnum rarity, SpecialCardTypeEnum specialCardTypeEnum) : base("Armor", name, rarity, specialCardTypeEnum)
        {

        }
        public override void ShowCard()
        {
            base.ShowCard();
            Console.WriteLine();
            if (Strength.HasValue)
            {
                Console.Write("Strength: {0}. ", Strength.Value);
            }
            if (Power.HasValue)
            {
                Console.Write("Power: {0}. ", Power.Value);
            }
            if (Mana.HasValue)
            {
                Console.Write("Mana: {0}. ", Mana.Value);
            }
            if (Utilities != null)
            {
                Console.Write("Utilities: {0}", Utilities);
            }
        }

    }

    abstract class Helmet : Object
    {
        //Dégats 
        public int? Strength;
        public int? Power;
        //Mana
        public int? Mana;
        //Property
        public string Utilities;
        protected Helmet(string name, RarityEnum rarity, SpecialCardTypeEnum specialCardTypeEnum) : base("Helmet", name, rarity, specialCardTypeEnum)
        {

        }
        public override void ShowCard()
        {
            base.ShowCard();
            Console.WriteLine();
            if (Strength.HasValue)
            {
                Console.Write("Strength: {0}. ", Strength.Value);
            }
            if (Power.HasValue)
            {
                Console.Write("Power: {0}. ", Power.Value);
            }
            if (Mana.HasValue)
            {
                Console.Write("Mana: {0}. ", Mana.Value);
            }
            if (Utilities != null)
            {
                Console.Write("Utilities: {0} ", Utilities);
            }
        }

    }

    abstract class Shoe : Object
    {
        //Dégats 
        public int? Strength;
        public int? Power;
        //Mana
        public int? Mana;
        //Property
        public string Utilities;
        protected Shoe(string name, RarityEnum rarity, SpecialCardTypeEnum specialCardTypeEnum) : base("Shoe", name, rarity, specialCardTypeEnum)
        {

        }
        public override void ShowCard()
        {
            base.ShowCard();
            Console.WriteLine();
            if (Strength.HasValue)
            {
                Console.Write("Strength: {0}. ", Strength.Value);
            }
            if (Power.HasValue)
            {
                Console.Write("Power: {0}. ", Power.Value);
            }
            if (Mana.HasValue)
            {
                Console.Write("Mana: {0}. ", Mana.Value);
            }
            if (Utilities != null)
            {
                Console.Write("Utilities: {0} ", Utilities);
            }
        }

    }

    abstract class Amulette : Object
    {
        //Dégats 
        public int? Strength;
        public int? Power;
        //Mana
        public int? Mana;
        //Property
        public string Utilities;
        protected Amulette(string name, RarityEnum rarity, SpecialCardTypeEnum specialCardTypeEnum) : base("Amulette", name, rarity, specialCardTypeEnum)
        {

        }
        public override void ShowCard()
        {
            base.ShowCard();
            Console.WriteLine();
            if (Strength.HasValue)
            {
                Console.Write("Strength: {0}. ", Strength.Value);
            }
            if (Power.HasValue)
            {
                Console.Write("Power: {0}. ", Power.Value);
            }
            if (Mana.HasValue)
            {
                Console.Write("Mana: {0}. ", Mana.Value);
            }
            if (Utilities != null)
            {
                Console.Write("Utilities: {0} ", Utilities);
            }
        }
    }

    abstract class Pets : Object
    {
        //Dégats 
        public int Puissance;
        public int BasicPuissance;
        public int HP;
        public int BasicHP;
        //Mana
        public int? Mana;
        //Property
        public string Utilities;
        //InFight
        public bool inFight = false;
        //Dead
        public bool isDead = false;

        protected Pets(string name, RarityEnum rarity, SpecialCardTypeEnum specialCardTypeEnum) : base("Pets", name, rarity, specialCardTypeEnum)
        {

        }
        public override void ShowCard()
        {
            base.ShowCard();
            Console.WriteLine();
            Console.Write("Puissance: {0}. ", Puissance);
            Console.Write("HP: {0}. ", HP);
            if (Mana.HasValue)
            {
                Console.Write("Mana: {0}. ", Mana.Value);
            }
            if (Utilities != null)
            {
                Console.Write("Property: {0}", Utilities);
            }
        }
        public void EndOfAFight()
        {
            if ( inFight)
            {
                HP -= 1;
            }
            inFight = false;
        }

    }
    #endregion Object

    #region Spell
    abstract class Incantation : Spell
    {
        public string Utility;

        protected Incantation(string name, RarityEnum rarity) : base("Incantation", name, rarity)
        {

        }
        public override void ShowCard()
        {
            base.ShowCard();
            Console.Write("Utility: {0}", Utility);
        }
    }
    abstract class Flask : Spell
    {
        public string Utility;

        protected Flask(string name, RarityEnum rarity) : base("Flask", name, rarity)
        {

        }
        public override void ShowCard()
        {
            base.ShowCard();
            Console.Write("Utility: {0}", Utility);
        }
    }
    #endregion Spell

    #region Monster
    abstract class NormalMonster : Monster
    {
        public int Puissance;
        public int NumDropCards;
        public string Property = "";
        public string LostConsequences;

        public NormalMonster(string name, RarityEnum rarity) : base("Normal Monster", name, rarity)
        {

        }
        public override void ShowCard()
        {
            base.ShowCard();
            Console.WriteLine();
            Console.Write("Puissance: {0}. ", Puissance);
            Console.Write("Drop Card: {0}. ", NumDropCards);
            if (Property != "")
            {
                Console.Write("Property : {0}. ", Property);
            }
            Console.Write("Failure Consequences: {0}. ", LostConsequences);

        }
    }
    #endregion Monster

    #region SpecialCard

    #endregion SpecialCard

    #endregion Class Secondaire


    #region Carte Individuelle

    #region Common Card
    //Object
    #region Weapon
    //Armes
    class GreekSword : Weapon
    {
        public GreekSword() : base("Greek Sword", RarityEnum.Common, SpecialCardTypeEnum.Greek)
        {
            Strength = 1;
            Power = 1;
        }
    }
    class SimpleDagger : Weapon
    {
        public SimpleDagger() : base("Simple Dagger", RarityEnum.Common, SpecialCardTypeEnum.Nothing)
        {
            Strength = 2;
        }
    }
    class BewitchedAx : Weapon
    {
        public BewitchedAx() : base("Bewitched Ax", RarityEnum.Common, SpecialCardTypeEnum.Nothing)
        {
            Strength = 1;
            Power = 2;
        }
    }
    class MagicOrb : Weapon
    {
        public MagicOrb() : base("Magic Orb", RarityEnum.Common, SpecialCardTypeEnum.Nothing)
        {
            Power = 2;
        }
    }
    class DoubleSword : Weapon
    {
        public DoubleSword() : base("Double Sword", RarityEnum.Common, SpecialCardTypeEnum.Nothing)
        {
            Strength = 3;
            HandTake = 2;
        }
    }
    class MagicGauntlet : Weapon
    {
        public MagicGauntlet() : base("Magic Gauntlet", RarityEnum.Common, SpecialCardTypeEnum.Nothing)
        {
            Power = 3;
            HandTake = 2;
        }
    }

    //Bouclier
    class GreekShield : Weapon
    {
        public GreekShield() : base("Greek Shield", RarityEnum.Common, SpecialCardTypeEnum.Greek)
        {
            Strength = 1;
            Power = 1;
        }
    }
    class SmallShield : Weapon
    {
        public SmallShield() : base("Small Shield", RarityEnum.Common, SpecialCardTypeEnum.Nothing)
        {
            Strength = 1;
        }
    }
    class MagicShield : Weapon
    {
        public MagicShield() : base("Magic Shield", RarityEnum.Common, SpecialCardTypeEnum.Nothing)
        {
            Power = 1;
        }
    }

    #endregion Weapon

    #region Armor
    class GreekArmor : Armor
    {
        public GreekArmor() : base("Greek Armor", RarityEnum.Common, SpecialCardTypeEnum.Greek)
        {
            Strength = 1;
            Power = 1;
        }
    }
    class Chestplate : Armor
    {
        public Chestplate() : base("Chestplate", RarityEnum.Common, SpecialCardTypeEnum.Nothing)
        {
            Strength = 2;
        }
    }
    class MagicChestplate : Armor
    {
        public MagicChestplate() : base("Magic Chestplate", RarityEnum.Common, SpecialCardTypeEnum.Nothing)
        {
            Power = 2;
        }
    }
    #endregion Armor

    #region Helmet
    class GreekHelmet : Helmet
    {
        public GreekHelmet() : base("Greek Helmet", RarityEnum.Common, SpecialCardTypeEnum.Greek)
        {
            Strength = 1;
            Power = 1;
        }
    }
    class BasicHelmet : Helmet
    {
        public BasicHelmet() : base("Basic Helmet", RarityEnum.Common, SpecialCardTypeEnum.Nothing)
        {
            //Dégats 
            Strength = 2;
        }
    }
    class MagicHelmet : Helmet
    {
        public MagicHelmet() : base("Magic Helmet", RarityEnum.Common, SpecialCardTypeEnum.Nothing)
        {
            //Dégats 
            Power = 2;
        }
    }

    #endregion Helmet

    #region Shoe
    class GreekShoes : Shoe
    {
        public GreekShoes() : base("Greek Shoes", RarityEnum.Common, SpecialCardTypeEnum.Greek)
        {
            Strength = 1;
            Power = 1;
        }
    }
    class NormalShoes : Shoe
    {
        public NormalShoes() : base("Normal Shoes", RarityEnum.Common, SpecialCardTypeEnum.Nothing)
        {
            Power = 2;
        }
    }

    #endregion Shoe

    #region Amulette
    class GreekNecklace : Amulette
    {
        public GreekNecklace() : base("Greek Necklace", RarityEnum.Common, SpecialCardTypeEnum.Greek)
        {
            Strength = 1;
            Power = 1;
        }
    }
    class MagicRing : Amulette
    {
        public MagicRing() : base("Magic Ring", RarityEnum.Common, SpecialCardTypeEnum.Nothing)
        {
            Mana = 1;
        }
    }
    class StrengthCollar : Amulette
    {
        public StrengthCollar() : base("Strength Collar", RarityEnum.Common, SpecialCardTypeEnum.Nothing)
        {
            Strength = 1;
        }
    }
    #endregion Amulette

    #region Pet
    class SmallWolf : Pets
    {
        public SmallWolf() : base("Small Wolf", RarityEnum.Common, SpecialCardTypeEnum.Nothing)
        {
            BasicPuissance = 3;
            BasicHP = 1;
            Puissance = 3;
            HP = 1;
        }
    }
    class TotemOfStrength : Pets
    {
        public TotemOfStrength() : base("Totem Of Strength", RarityEnum.Common, SpecialCardTypeEnum.Nothing)
        {
            BasicPuissance = 1;
            BasicHP = 2;
            Puissance = 1;
            HP = 2;
            Utilities = "Active: +1 Strength. ";
        }
        public int GivePlayerStrength()
        {
            return 1;
        }

    }
    class GratefulTraveler : Pets
    {
        public GratefulTraveler() : base("Grateful Traveler", RarityEnum.Common, SpecialCardTypeEnum.Nothing)
        {
            BasicPuissance = 2;
            BasicHP = 2;
            Puissance = 2;
            HP = 2;
        }
    }
    #endregion Pet

    //Spell
    #region Incantation
    class AntiWeaponSpell : Incantation
    {
        public AntiWeaponSpell() : base("Anti-Weapon Spell", RarityEnum.Common)
        {
            Utility = "Destroy a Common Weapon. ";

        }
        public override PlayCardResult CheckCanPlay(List<Player> lp1, Player p1)
        {
            PlayCardResult result = PlayCardResult.CantCastSpell;
            int countweapon = 1;
            for (int i = 0; i < lp1.Count; i++)
            {
                if (lp1[i] == p1)
                {
                    continue;
                }
                for (int j = 0; j < lp1[i]._weaponsPlayed.Count; j++)
                {
                    if (lp1[i]._weaponsPlayed[j].Rarity == RarityEnum.Common)
                    {
                        result = PlayCardResult.OK;
                        string weaponsss = "w" + Convert.ToString(countweapon);
                        countweapon++;
                        StringListPossibleValues.Add(weaponsss);
                        ObjectListPossibleValues.Add(lp1[i]._weaponsPlayed[j]);
                        PlayerListPossibleValue.Add(lp1[i]);
                    }
                }
            }
            return result;
        }
        public override void CastSpell()
        {
            while (true)
            {
                Console.WriteLine("Choose the weapon to destroy. ");
                WritePoss();
                Console.Write(" --> ");
                string rep = Console.ReadLine();
                if (StringListPossibleValues.Contains(rep))
                {
                    int indexx = StringListPossibleValues.IndexOf(rep);
                    Console.WriteLine("You destroy the {0} of {1}. ", ObjectListPossibleValues[indexx].Name, PlayerListPossibleValue[indexx].PlayerName);
                    PlayerListPossibleValue[indexx]._weaponsPlayed.Remove(ObjectListPossibleValues[indexx] as Weapon);
                    StringListPossibleValues.Clear();
                    ObjectListPossibleValues.Clear();
                    PlayerListPossibleValue.Clear();
                    break;
                }
                else
                {
                    Console.WriteLine("Choose a correct target. ");
                }
            }
        }

        public override void WritePoss()
        {
            for (int i = 0; i < StringListPossibleValues.Count; i++)
            {
                Console.WriteLine(" - {0}: {1} of {2}", StringListPossibleValues[i], ObjectListPossibleValues[i].Name, PlayerListPossibleValue[i].PlayerName);
            }
        }
    }
    class AntiArmorSpell : Incantation
    {
        public AntiArmorSpell() : base("Anti-Armor Spell", RarityEnum.Common)
        {
            Utility = "Destroy a Common Armor. ";
        }
    }
    class AntiHelmetSpell : Incantation
    {
        public AntiHelmetSpell() : base("Anti-Helmet Spell", RarityEnum.Common)
        {
            Utility = "Destroy a Common Helmet. ";
        }
    }
    class AntiAmuletteSpell : Incantation
    {
        public AntiAmuletteSpell() : base("Anti-Amulette Spell", RarityEnum.Common)
        {
            Utility = "Destroy a Common Amulette. ";
        }
    }
    class DamagePetsSpell : Incantation
    {
        public DamagePetsSpell() : base("Damage Pets Spell", RarityEnum.Common)
        {
            Utility = "Deal 1 Damage to a Pets. ";
        }
    }
    class AmuletteRobbery : Incantation
    {
        public AmuletteRobbery() : base("Amulette Robbery", RarityEnum.Common)
        {
            Utility = "Steal Common Amulette. ";
        }
    }

    class MonsterBoostCommon : Incantation
    {
        public MonsterBoostCommon() : base("Monster Boost ", RarityEnum.Common)
        {
            Utility = "Give +3 to a Monster. ";
        }
    }
    #endregion Incantation

    #region Flask
    class StrengthFlask : Flask
    {
        public StrengthFlask() : base("Strength Flask", RarityEnum.Common)
        {
            Utility = "+1 Strength for this fight.";
        }
    }
    class PowerFlask : Flask
    {
        public PowerFlask() : base("Power Flask", RarityEnum.Common)
        {
            Utility = "+1 Power for this fight.";
        }
    }
    class ManaFlask : Flask
    {
        public ManaFlask() : base("Mana Flask", RarityEnum.Common)
        {
            Utility = "+1 Mana for this turn.";
        }
    }
    class HealFlask : Flask
    {
        public HealFlask() : base("Heal Flask", RarityEnum.Common)
        {
            Utility = "Heal 2 HP.";
        }
    }
    #endregion Flask

    //Monster
    #region Normal Monster
    class Slime : NormalMonster
    {
        public Slime() : base("Slime", RarityEnum.Common)
        {
            Puissance = 1;
            NumDropCards = 1;
            LostConsequences = "He disintegrates and give you a Treasure. ";
        }
    }
    class GiantTortoise : NormalMonster
    {
        public GiantTortoise() : base("Giant Tortoise", RarityEnum.Common)
        {
            Puissance = 1;
            NumDropCards = 1;
            LostConsequences = "To slow to do something. ";
        }
    }
    class GreenGobelin : NormalMonster
    {
        public GreenGobelin() : base("Green Gobelin", RarityEnum.Common)
        {
            Puissance = 2;
            NumDropCards = 1;
            LostConsequences = "He stab you in the back. You lost 1 HP. ";
        }
    }
    class GreatSandScorpion : NormalMonster
    {
        public GreatSandScorpion() : base("Great Sand Scorpion", RarityEnum.Common)
        {
            Puissance = 3;
            NumDropCards = 1;
            LostConsequences = "He tried to sting you, if you don't have an armor, lose 1 HP. ";
        }
    }
    class GraveHaunt : NormalMonster
    {
        public GraveHaunt() : base("Grave Haunt", RarityEnum.Common)
        {
            Puissance = 4;
            NumDropCards = 1;
            LostConsequences = "For your next fight you will be -3 Puissance. ";
        }
    }
    class MutantRats : NormalMonster
    {
        public MutantRats() : base("Mutant Rats", RarityEnum.Common)
        {
            Puissance = 5;
            NumDropCards = 1;
            LostConsequences = "If you had some, lose your Shoes. ";
        }
    }
    class BrigandGroup : NormalMonster
    {
        public BrigandGroup() : base("Brigand Group", RarityEnum.Common)
        {
            Puissance = 5;
            NumDropCards = 1;
            Property = "If you don't have 2 differents weapons, we have + 4. ";
            LostConsequences = "You be pillaged, you lost a random Cards from your Hand. "; 
        }
    }




    #endregion Normal Monster


    #endregion Common Card

    #region Rare Card
    //Object
    #region Weapon
    //Armes
    class VikingSword : Weapon
    {
        public VikingSword() : base("Viking Sword", RarityEnum.Rare, SpecialCardTypeEnum.Viking)
        {
            Strength = 1;
            Power = 2;
        }
    }
    class BigMagicOrb : Weapon
    {
        public BigMagicOrb() : base("Big Magic Orb", RarityEnum.Rare, SpecialCardTypeEnum.Nothing)
        {
            Power = 3;
        }
    }
    class BigSpear : Weapon
    {
        public BigSpear() : base("Big Spear", RarityEnum.Rare, SpecialCardTypeEnum.Nothing)
        {
            Strength = 5;
            HandTake = 2;
        }
    }
    class DoubleManaSword : Weapon
    {
        public DoubleManaSword() : base("Double Mana Sword", RarityEnum.Rare, SpecialCardTypeEnum.Nothing)
        {
            Power = 4;
            Mana = 1; 
            HandTake = 2;
        }
    }

    //Bouclier
    class VikingShield : Weapon
    {
        public VikingShield() : base("Viking Shield", RarityEnum.Rare, SpecialCardTypeEnum.Viking)
        {
            Strength = 1;
            Power = 2;
        }
    }
    class GiantShield : Weapon
    {
        public GiantShield() : base("Giant Shield", RarityEnum.Rare, SpecialCardTypeEnum.Nothing)
        {
            Strength = 3;
            Power = 3;
            HandTake = 2;
        }
    }

    #endregion Weapon

    #region Armor
    class VikingArmor : Armor
    {
        public VikingArmor() : base("VikingArmor", RarityEnum.Rare, SpecialCardTypeEnum.Viking)
        {
            Strength = 1;
            Power = 2;
        }
    }
    class ReinforcedChestplate : Armor
    {
        public ReinforcedChestplate() : base("Reinforced Chestplate", RarityEnum.Rare, SpecialCardTypeEnum.Nothing)
        {
            Strength = 3;
        }
    }
    class MagicArmor : Armor
    {
        public MagicArmor() : base("Magic Armor", RarityEnum.Rare, SpecialCardTypeEnum.Nothing)
        {
            Power = 3;
        }
    }
    #endregion Armor

    #region Helmet
    class VikingHelmet : Helmet
    {
        public VikingHelmet() : base("Viking Helmet", RarityEnum.Rare, SpecialCardTypeEnum.Viking)
        {
            Strength = 1;
            Power = 2;
        }
    }
    class IronHat : Helmet
    {
        public IronHat() : base("Iron Hat", RarityEnum.Rare, SpecialCardTypeEnum.Nothing)
        {
            Strength = 3;
            Power = 1;
        }
    }

    #endregion Helmet

    #region Shoe
    class VikingShoes : Shoe
    {
        public VikingShoes() : base("Viking Shoes", RarityEnum.Rare, SpecialCardTypeEnum.Viking)
        {
            Strength = 1;
            Power = 2;
        }
    }
    class SpeedShoes : Shoe
    {
        public SpeedShoes () : base("Speed Shoes", RarityEnum.Rare, SpecialCardTypeEnum.Nothing)
        {
            Mana = 2;
        }
    }
    class ArmedShoes : Shoe
    {
        public ArmedShoes() : base("Armed Shoes", RarityEnum.Rare, SpecialCardTypeEnum.Nothing)
        {
            Strength = 3;
        }
    }

    #endregion Shoe

    #region Amulette
    class WitchBracelet : Amulette
    {
        public WitchBracelet() : base("WitchBracelet", RarityEnum.Rare, SpecialCardTypeEnum.Nothing)
        {
            Power = 2;
        }
    }
    class LeaderCape : Amulette
    {
        public LeaderCape() : base("Leader Cape", RarityEnum.Rare, SpecialCardTypeEnum.Nothing)
        {
            Strength = 1;
            Power = 1;
            Mana = 1;
        }
    }
    #endregion Amulette

    #region Pet
    class VikingBear : Pets
    {
        public VikingBear() : base("Viking Bear", RarityEnum.Rare, SpecialCardTypeEnum.Viking)
        {
            BasicPuissance = 2;
            BasicHP = 1;
            Puissance = 2;
            HP = 1;
            Utilities = "When Summoned, steal a Viking Object.";
        }
        public override void WhenSummoned()
        {
            int playerTotal = Game.numberOfPlayerInThisGame;
            for (int i = 0; i < playerTotal; i++)
            {

            }
        }
    }
    class Reel : Pets
    {
        public Reel() : base("Reel", RarityEnum.Rare, SpecialCardTypeEnum.Nothing)
        {
            BasicPuissance = 4;
            BasicHP = 1;
            Puissance = 4;
            HP = 1;
            Utilities = "When I’m played, Summon a common card for free. ";
        }
    }
    class WolfPack : Pets
    {
        public WolfPack() : base("Wolf Pack", RarityEnum.Rare, SpecialCardTypeEnum.Nothing)
        {
            BasicPuissance = 2;
            BasicHP = 3;
            Puissance = 2;
            HP = 3;
            Utilities = "If you have a “Small Wolf”, my stats are 4/4. ";
        }
    }
    class WildParrot : Pets
    {
        public WildParrot() : base("Wild Parrot", RarityEnum.Rare, SpecialCardTypeEnum.Nothing)
        {
            BasicPuissance = 6;
            BasicHP = 1;
            Puissance = 6;
            HP = 1;
            Utilities = "When I die, deal 1 Damage to a random Player, even you. ";
        }
    }
    #endregion Pet

    /*
    //Spell
    #region Incantation
    class AntiWeaponSpell : Incantation
    {
        public AntiWeaponSpell() : base("Anti-Weapon Spell", RarityEnum.Common)
        {
            properties1 = "Destroy a Common Weapon. ";
        }
    }
    class AntiArmorSpell : Incantation
    {
        public AntiArmorSpell() : base("Anti-Armor Spell", RarityEnum.Common)
        {
            properties1 = "Destroy a Common Armor. ";
        }
    }
    class AntiHelmetSpell : Incantation
    {
        public AntiHelmetSpell() : base("Anti-Helmet Spell", RarityEnum.Common)
        {
            properties1 = "Destroy a Common Helmet. ";
        }
    }
    class AntiAmuletteSpell : Incantation
    {
        public AntiAmuletteSpell() : base("Anti-Amulette Spell", RarityEnum.Common)
        {
            properties1 = "Destroy a Common Amulette. ";
        }
    }
    class DamagePetsSpell : Incantation
    {
        public DamagePetsSpell() : base("Damage Pets Spell", RarityEnum.Common)
        {
            properties1 = "-1 Hp to a Pets. ";
        }
    }
    class AmuletteRobbery : Incantation
    {
        public AmuletteRobbery() : base("Amulette Robbery", RarityEnum.Common)
        {
            properties1 = "Steal Common Amulette. ";
        }
    }
    class CounterSpell : Incantation
    {
        public CounterSpell() : base("Counter Spell ", RarityEnum.Common)
        {
            properties1 = "Counter a Common Invocation. ";
        }
    }
    class MonsterBoostCommon : Incantation
    {
        public MonsterBoostCommon() : base("Monster Boost ", RarityEnum.Common)
        {
            properties1 = "Give +3 to a Monster. ";
        }
    }
    #endregion Incantation

    #region Flask
    class StrengthFlask : Flask
    {
        public StrengthFlask() : base("Strength Flask", RarityEnum.Common)
        {
            properties1 = "+1 Strength for this fight.";
        }
    }
    class PowerFlask : Flask
    {
        public PowerFlask() : base("Power Flask", RarityEnum.Common)
        {
            properties1 = "+1 Power for this fight.";
        }
    }
    class ManaFlask : Flask
    {
        public ManaFlask() : base("Mana Flask", RarityEnum.Common)
        {
            properties1 = "+1 Mana for this turn.";
        }
    }
    class HealFlask : Flask
    {
        public HealFlask() : base("Heal Flask", RarityEnum.Common)
        {
            properties1 = "Heal 2 HP.";
        }
    }
    #endregion Flask

    //Monster
    #region Normal Monster
    class Slime : NormalMonster
    {
        public Slime() : base("Slime", RarityEnum.Common)
        {
            Puissance = 1;
            NumDropCards = 1;
            LostConsequences = "He disintegrates and give you a Treasure. ";
        }
    }
    class GiantTortoise : NormalMonster
    {
        public GiantTortoise() : base("Giant Tortoise", RarityEnum.Common)
        {
            Puissance = 1;
            NumDropCards = 1;
            LostConsequences = "To slow to do something. ";
        }
    }
    class GreenGobelin : NormalMonster
    {
        public GreenGobelin() : base("Green Gobelin", RarityEnum.Common)
        {
            Puissance = 2;
            NumDropCards = 1;
            LostConsequences = "He stab you in the back. You lost 1 HP. ";
        }
    }
    class GreatSandScorpion : NormalMonster
    {
        public GreatSandScorpion() : base("Great Sand Scorpion", RarityEnum.Common)
        {
            Puissance = 3;
            NumDropCards = 1;
            LostConsequences = "He tried to sting you, if you don't have an armor, lose 1 HP. ";
        }
    }
    class GraveHaunt : NormalMonster
    {
        public GraveHaunt() : base("Grave Haunt", RarityEnum.Common)
        {
            Puissance = 4;
            NumDropCards = 1;
            LostConsequences = "For your next fight you will be -3 Puissance. ";
        }
    }
    class MutantRats : NormalMonster
    {
        public MutantRats() : base("Mutant Rats", RarityEnum.Common)
        {
            Puissance = 5;
            NumDropCards = 1;
            LostConsequences = "If you had some, lose your Shoes. ";
        }
    }
    class BrigandGroup : NormalMonster
    {
        public BrigandGroup() : base("Brigand Group", RarityEnum.Common)
        {
            Puissance = 5;
            NumDropCards = 1;
            Property = "If you don't have 2 differents weapons, we have + 4. ";
            LostConsequences = "You be pillaged, you lost a random Cards from your Hand. ";
        }
    }




    #endregion Normal Monster
    */

    #endregion Rare Card

    #region Mythical Card

    //Object
    #region Weapon
    //Armes
    class KnifeInMithril : Weapon
    {
        public KnifeInMithril() : base("Knife In Mithril", RarityEnum.Mythical, SpecialCardTypeEnum.Mithril)
        {
            Strength = 3;
        }
    }
    class GodKillerSword : Weapon
    {
        public GodKillerSword() : base("God Killer Sword", RarityEnum.Mythical, SpecialCardTypeEnum.Nothing)
        {
            Strength = 5;
        }
    }
    class MillennialMagicBook : Weapon
    {
        public MillennialMagicBook() : base("Millennial Magic Book", RarityEnum.Mythical, SpecialCardTypeEnum.Nothing)
        {
            Power = 5;
        }
    }

    //Bouclier
    class ShieldInMithril : Weapon
    {
        public ShieldInMithril() : base("ShieldInMithril", RarityEnum.Mythical, SpecialCardTypeEnum.Mithril)
        {
            Strength = 3;
        }
    }
    class BigWall : Weapon
    {
        public BigWall() : base("Big Wall", RarityEnum.Mythical, SpecialCardTypeEnum.Nothing)
        {
            Strength = 3;
            Utilities = "When Summoned: if you are Chasser, you can change the bonus in power and it take 0 Hand.";
        }
    }


    #endregion Weapon

    #region Armor
    class ChainmailInMithril : Armor
    {
        public ChainmailInMithril() : base("Chainmail In Mithril", RarityEnum.Mythical, SpecialCardTypeEnum.Mithril)
        {
            Strength = 3;
        }
    }
    class BreatplateOfSelfConfident : Armor
    {
        public BreatplateOfSelfConfident() : base("Breatplate Of Self-Confident", RarityEnum.Mythical, SpecialCardTypeEnum.Nothing)
        {
            Strength = 4;
            Utilities = "Knight: Active: When you draw a monster you can choose to draw a^nother one who will fight with the first Monster";
        }
    }
    #endregion Armor

    #region Helmet
    class HelmetInMithril : Helmet
    {
        public HelmetInMithril() : base("Helmet In Mithril", RarityEnum.Mythical, SpecialCardTypeEnum.Mithril)
        {
            Strength = 3;
        }
    }
    class MagicHat : Helmet
    {
        public MagicHat() : base("Magic Hat", RarityEnum.Mythical, SpecialCardTypeEnum.Nothing)
        {
            Power = 3;
            Utilities = "Sorcer: Active: When you draw a spell for the first time this roud, draw another one.";
        }
    }

    #endregion Helmet

    #region Shoe
    class ShoesInMithril : Shoe
    {
        public ShoesInMithril() : base("Shoes In Mithril", RarityEnum.Mythical, SpecialCardTypeEnum.Mithril)
        {
            Strength = 3;
        }
    }
    class FlyingShoes : Shoe
    {
        public FlyingShoes() : base("Flying Shoes", RarityEnum.Mythical, SpecialCardTypeEnum.Nothing)
        {
            Utilities = "If you lose a fight, don't take the Lose Consequence.";
        }
    }

    #endregion Shoe

    #region Amulette
    class BeltInMithril : Amulette
    {
        public BeltInMithril() : base("Belt In Mithril", RarityEnum.Mythical, SpecialCardTypeEnum.Mithril)
        {
            Strength = 2;
        }
    }
    class TheGraal : Amulette
    {
        public TheGraal() : base("The Graal", RarityEnum.Mythical, SpecialCardTypeEnum.Nothing)
        {
            Power = 2;
            Utilities = "Priest: +2 Power/ Heal 1 HP each EndTurn.";
        }
    }
    class PetsTotem : Amulette
    {
        public PetsTotem() : base("Pets Totem", RarityEnum.Mythical, SpecialCardTypeEnum.Nothing)
        {
            Mana = 2;
            Utilities = "When Summoned: Giave + 1 HP To your Pets. Trainer: When Summoned: Double Puissance and HP of your Pets.";
        }
    }
    #endregion Amulette

    #region Pet
    class GreatWhiteWolf : Pets
    {
        public GreatWhiteWolf() : base("Great White Wolf", RarityEnum.Mythical, SpecialCardTypeEnum.Nothing)
        {
            BasicPuissance = 4;
            BasicHP = 2;
            Puissance = 4;
            HP = 2;
            Utilities = "Trainer: When Summoned: Puissance = 6 / HP = 3.";
        }
    }
    #endregion Pet

    #endregion Mythical Card

    #region Legendary Card
    //Object
    #region Weapon
    class TrackingChain : Weapon
    {
        public TrackingChain() : base("Tracking Chain", RarityEnum.Legendary, SpecialCardTypeEnum.Nothing)
        {
            Strength = 6;
            Utilities = "Tracker: ";
        }
    }
    class ArthurLegendarySword : Weapon
    {
        public ArthurLegendarySword() : base("TArthur Legendary Sword", RarityEnum.Legendary, SpecialCardTypeEnum.Nothing)
        {
            Strength = 6;
            Utilities = "(Knight: When Summoned: You can replace your Armor, Helmet, Shoes, by every same Object in the discard";
        }
    }
    #endregion Weapon

    #region Armor
    class Exoskeleton : Armor
    {
        public Exoskeleton() : base("Exoskeleton", RarityEnum.Legendary, SpecialCardTypeEnum.Nothing)
        {
            Utilities = "Give you 2 hand Spot. Archer: When Summoned: Take the two first 1 hand Shield or the first 2 hands Shield from the Discard and summon it/them";
        }
    }
    
    #endregion Armor

    #region Helmet
    class HatofManipulation : Helmet
    {
        public HatofManipulation() : base("Hat of Manipulation", RarityEnum.Legendary, SpecialCardTypeEnum.Nothing)
        {
            Utilities = " When summoned, Summon 2 Pets from your hand (Priest: summon 5 Followers) ";
        }
    }
    #endregion Helmet

    #region Shoe
    

    #endregion Shoe

    #region Amulette
    class GhostlySaddle : Amulette
    {
        public GhostlySaddle() : base("Ghostly Saddle", RarityEnum.Legendary, SpecialCardTypeEnum.Nothing)
        {
            Utilities = "When Summoned: Choose a Pets, you give the Ghostly Saddle to it. Now he can’t die in fight. (Trainer: When Summoned: Summon a Legendary Pets and give the Ghostly Saddle to it)";
        }
    }
    class SpellHole : Amulette
    {
        public SpellHole() : base("Spell Hole", RarityEnum.Legendary, SpecialCardTypeEnum.Nothing)
        {
            Mana = 5;
            Utilities = "Sorcer: Passive: Each time you play an Object, draw a spell.";
        }
    }

    #endregion Amulette

    //Monster
    #region Normal Monster
    class HundredOfMercenary : NormalMonster
    {
        public HundredOfMercenary() : base("Hundred Of Mercenary", RarityEnum.Legendary)
        {
            Puissance = 20;
            NumDropCards = 1;
            Property = "The other can play ponly one card during the fight. ";
            LostConsequences = "Begining by the right one, all player take a Card in your Hand. ";
        }
    }
    #endregion Normal Monster

    #endregion Legendary Card


    #region Special Card

    #region Carte de class

    # region Priest
    class Follower : Pets
    {
        public Follower() : base("Follower", RarityEnum.ClassMemberShip, SpecialCardTypeEnum.Nothing)
        {
            Puissance = 2;
            HP = 1;
            inFight = true;
        }
    }
    #endregion Priest

    #region Archer
    //Arc
    class CommonBow : Weapon
    {
        public CommonBow() : base("Common Bow", RarityEnum.ClassMemberShip, SpecialCardTypeEnum.Nothing)
        {
            HandTake = 2;
        }
    }
    class RareBow : Weapon
    {
        public RareBow() : base("Rare Bow", RarityEnum.ClassMemberShip, SpecialCardTypeEnum.Nothing)
        {
            HandTake = 2;
        }
    }
    class MythicalBow : Weapon
    {
        public MythicalBow() : base("Mythical Bow", RarityEnum.ClassMemberShip, SpecialCardTypeEnum.Nothing)
        {
            HandTake = 2;
        }
    }
    class LegendaryBow : Weapon
    {
        public LegendaryBow() : base("Legendary Bow", RarityEnum.ClassMemberShip, SpecialCardTypeEnum.Nothing)
        {
            HandTake = 2;
        }
    }

    //Arbalette
    class CommonCrossBow : Weapon
    {
        public CommonCrossBow() : base("Common CrossBow", RarityEnum.ClassMemberShip, SpecialCardTypeEnum.Nothing)
        {
            HandTake = 2;
        }
    }
    class RareCrossBow : Weapon
    {
        public RareCrossBow() : base("Rare CrossBow", RarityEnum.ClassMemberShip, SpecialCardTypeEnum.Nothing)
        {
            HandTake = 2;
        }
    }
    class MythicalCrossBow : Weapon
    {
        public MythicalCrossBow() : base("Mythical CrossBow", RarityEnum.ClassMemberShip, SpecialCardTypeEnum.Nothing)
        {
            HandTake = 2;
        }
    }
    class LegendaryCrossBow : Weapon
    {
        public LegendaryCrossBow() : base("Legendary CrossBow", RarityEnum.ClassMemberShip, SpecialCardTypeEnum.Nothing)
        {
            HandTake = 2;
        }
    }

    #endregion Archer

    #region Trainer
    class MamaBear : Pets
    {
        public MamaBear() : base("Mama Bear", RarityEnum.ClassMemberShip, SpecialCardTypeEnum.Nothing)
        {
            Puissance = 1;
            HP = 1;
            Utilities = "";
        }
    }
    class WhiteDragon : Pets
    {
        public WhiteDragon() : base("White Dragon", RarityEnum.ClassMemberShip, SpecialCardTypeEnum.Nothing)
        {
            Puissance = 1;
            HP = 1;
            Utilities = "";
        }
    }
    class TheDefencer : Pets
    {
        public TheDefencer() : base("The Defencer", RarityEnum.ClassMemberShip, SpecialCardTypeEnum.Nothing)
        {
            Puissance = 1;
            HP = 1;
            Utilities = "";
        }
    }
    class SaberToothTiger : Pets
    {
        public SaberToothTiger() : base("Saber Tooth Tiger", RarityEnum.ClassMemberShip, SpecialCardTypeEnum.Nothing)
        {
            Puissance = 1;
            HP = 1;
            Utilities = "";
        }
    }
    #endregion Trainer

    #endregion Carte de class

    #region Carte de Race

    #endregion Carte de Race

    #endregion Special Card

    #endregion Carte Individuelle

    #endregion Toutes les cartes

}
