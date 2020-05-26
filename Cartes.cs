using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices.ComTypes;
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
        public enum RarityEnum { Common, Rare, Mythical, Legendary };

        //Cost
        public int Cost { get; private set; }

        #endregion
        protected Carte(string pileCarte, string categorie, string name, RarityEnum rarity)
        {
            PileCarte = pileCarte; //Object - Spell - Monster
            Categorie = categorie; // Weapon - Armor - Helmet ...
            Name = name; //Bassique Sword - Magique Orb - ...
            Rarity = rarity; // Basique - Rare - mythical - legendary

            Cost = ((int)Rarity + 1) * 2; // Coût d'invocation
            if (Rarity == RarityEnum.Legendary) { Cost = 10; }

        }


        public virtual void ShowCard()
        {
            Console.Write("A {0}: it's a {1} {2} from the pile of {3}. ", Name, Rarity, Categorie, PileCarte);
            //Console.WriteLine();
        }


    }


    #region Class Principale

    abstract class Object : Carte
    {
        protected Object(string categorie, string name, RarityEnum rarity) : base("Object", categorie, name, rarity)
        {

        }
    }
    abstract class Spell : Carte
    {
        protected Spell(string categorie, string name, RarityEnum rarity) : base("Spell", categorie, name, rarity)
        {

        }

    }

    abstract class Monster : Carte
    {
        protected Monster(string categorie, string name, RarityEnum rarity) : base("Monster", categorie, name, rarity)
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
        public string? Utilities;
        protected Weapon(string name, RarityEnum rarity) : base("Weapon", name, rarity)
        {

        }
        public override void ShowCard()
        {
            base.ShowCard();
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
        public string? Utilities;
        protected Armor(string name, RarityEnum rarity) : base("Armor", name, rarity)
        {

        }
        public override void ShowCard()
        {
            base.ShowCard();
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
        public string? Utilities;
        protected Helmet(string name, RarityEnum rarity) : base("Helmet", name, rarity)
        {

        }
        public override void ShowCard()
        {
            base.ShowCard();
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
        public string? Utilities;
        protected Shoe(string name, RarityEnum rarity) : base("Shoe", name, rarity)
        {

        }
        public override void ShowCard()
        {
            base.ShowCard();
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
        public string? Utilities;
        protected Amulette(string name, RarityEnum rarity) : base("Helmet", name, rarity)
        {

        }
        public override void ShowCard()
        {
            base.ShowCard();
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
        }
    }

    abstract class Pets : Object
    {
        //Dégats 
        public int Puissance;
        public int HP;
        //Mana
        public int? Mana;
        //Property
        public string Utilities = "";
        //InFight
        public bool inFight = false;

        protected Pets(string name, RarityEnum rarity) : base("Pets", name, rarity)
        {

        }
        public override void ShowCard()
        {
            base.ShowCard();
            Console.Write("Strength: {0}. ", Puissance);
            if (Mana.HasValue)
            {
                Console.Write("Mana: {0}. ", Mana.Value);
            }
            if (Utilities != "")
            {
                Console.Write("Property; {0}. ", Utilities);
            }
        }

    }
    #endregion Object

    #region Spell
    abstract class Incantation : Spell
    {
        public string? properties1;

        protected Incantation(string name, RarityEnum rarity) : base("Incantation", name, rarity)
        {

        }
        public override void ShowCard()
        {
            base.ShowCard();
            Console.Write("Power: {0}", properties1);
        }
    }
    abstract class Flask : Spell
    {
        public string? properties1;

        protected Flask(string name, RarityEnum rarity) : base("Flask", name, rarity)
        {

        }
        public override void ShowCard()
        {
            base.ShowCard();
            Console.Write("Effect: {0}", properties1);
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

    #endregion Class Secondaire


    #region Cartes Individuelles

    #region Common Card
    //Object
    #region Weapon
    //Armes
    class BasicSword : Weapon
    {
        public BasicSword() : base("Basic Sword", RarityEnum.Common)
        {
            //Dégats 
            Strength = 2;
        }
    }
    class MagicOrb : Weapon
    {
        public MagicOrb() : base("Magic Orb", RarityEnum.Common)
        {
            //Dégats 
            Power = 2;
        }
    }
    class DoubleSword : Weapon
    {
        public DoubleSword() : base("Double Sword", RarityEnum.Common)
        {
            //Dégats 
            Strength = 3;
            HandTake = 2;
        }
    }
    class MagicGauntlet : Weapon
    {
        public MagicGauntlet() : base("Magic Gauntlet", RarityEnum.Common)
        {
            //Dégats 
            Power = 3;
            HandTake = 2;
        }
    }

    //Bouclier
    class SmallShield : Weapon
    {
        public SmallShield() : base("Small Shield", RarityEnum.Common)
        {
            //Dégats 
            Strength = 1;
        }
    }
    class MagicShield : Weapon
    {
        public MagicShield() : base("Magic Shield", RarityEnum.Common)
        {
            //Dégats 
            Power = 1;
        }
    }

    #endregion Weapon

    #region Armor
    class Chestplate : Armor
    {
        public Chestplate() : base("Chestplate", RarityEnum.Common)
        {
            //Dégats 
            Strength = 2;
        }
    }
    class MagicChestplate : Armor
    {
        public MagicChestplate() : base("Magic Chestplate", RarityEnum.Common)
        {
            //Dégats 
            Power = 2;
        }
    }
    #endregion Armor

    #region Helmet
    class BasicHelmet : Armor
    {
        public BasicHelmet() : base("Basic Helmet", RarityEnum.Common)
        {
            //Dégats 
            Strength = 2;
        }
    }
    class MagicHelmet : Armor
    {
        public MagicHelmet() : base("Magic Helmet", RarityEnum.Common)
        {
            //Dégats 
            Power = 2;
        }
    }

    #endregion Helmet

    #region Shoe
    class NormalShoes : Armor
    {
        public NormalShoes() : base("Normal Shoes", RarityEnum.Common)
        {
            //Dégats 
            Power = 1;
        }
    }

    #endregion Shoe

    #region Amulette
    class MagicRing : Amulette
    {
        public MagicRing() : base("Magic Ring", RarityEnum.Common)
        {
            //Mana
            Mana = 1;
        }
    }
    class StrengthCollar : Amulette
    {
        public StrengthCollar() : base("Strength Collar", RarityEnum.Common)
        {
            //Dégats
            Strength = 1;
        }
    }
    #endregion Amulette

    #region Pet
    class SmallWolf : Pets
    {
        public SmallWolf() : base("Small Wolf", RarityEnum.Common)
        {
            Puissance = 3;
            HP = 1;
        }
    }
    class TotemOfStrength : Pets
    {
        public TotemOfStrength() : base("Totem Of Strength", RarityEnum.Common)
        {
            Puissance = 1;
            HP = 2;
            Utilities = "Give +1 Strength to his master. ";
        }
    }
    class GratefulTraveler : Pets
    {
        public GratefulTraveler() : base("Grateful Traveler", RarityEnum.Common)
        {
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


    #endregion Common Card

    #region Rare Card
    //Object
    #region Weapon
    //Armes
    class LongSword : Weapon
    {
        public LongSword() : base("Long Sword", RarityEnum.Rare)
        {
            //Dégats 
            Strength = 3;
        }
    }
    class BigMagicOrb : Weapon
    {
        public BigMagicOrb() : base("Big Magic Orb", RarityEnum.Rare)
        {
            //Dégats 
            Power = 2;
        }
    }
    class BigSpear : Weapon
    {
        public BigSpear() : base("Big Spear", RarityEnum.Rare)
        {
            //Dégats 
            Strength = 5;
            HandTake = 2;
        }
    }
    class DoubleManaSword : Weapon
    {
        public DoubleManaSword() : base("Double Mana Sword", RarityEnum.Rare)
        {
            //Dégats 
            Power = 4;
            Mana = 1; 
            HandTake = 2;
        }
    }

    //Bouclier
    class IronShield : Weapon
    {
        public IronShield() : base("Small Shield", RarityEnum.Rare)
        {
            //Dégats 
            Strength = 2;
            Power = 1;
        }
    }
    class GiantShield : Weapon
    {
        public GiantShield() : base("Giant Shield", RarityEnum.Rare)
        {
            //Dégats 
            Strength = 3;
            Power = 3;
            HandTake = 2;
        }
    }

    #endregion Weapon

    #region Armor
    class ReinforcedChestplate : Armor
    {
        public ReinforcedChestplate() : base("Reinforced Chestplate", RarityEnum.Rare)
        {
            //Dégats 
            Strength = 3;
        }
    }
    class MagicArmor : Armor
    {
        public MagicArmor() : base("Magic Armor", RarityEnum.Rare)
        {
            //Dégats 
            Power = 3;
        }
    }
    #endregion Armor

    #region Helmet
    class VikingHelmet : Armor
    {
        public VikingHelmet() : base("Viking Helmet", RarityEnum.Rare)
        {
            //Dégats 
            Strength = 2;
            Mana = 1;
        }
    }
    class IronHat : Armor
    {
        public IronHat() : base("Iron Hat", RarityEnum.Rare)
        {
            //Dégats 
            Strength = 1;
            Power = 2;
        }
    }

    #endregion Helmet

    #region Shoe
    class SpeedShoes : Armor
    {
        public SpeedShoes () : base("Speed Shoes", RarityEnum.Rare)
        {
            //Dégats 
            Power = 1;
        }
    }
    class ArmedShoes : Armor
    {
        public ArmedShoes() : base("Armed Shoes", RarityEnum.Rare)
        {
            //Dégats 
            Strength = 3;
        }
    }

    #endregion Shoe

    #region Amulette
    class WitchBracelet : Amulette
    {
        public WitchBracelet() : base("WitchBracelet", RarityEnum.Rare)
        {
            //Mana
            Power = 2;
        }
    }
    class LeaderCape : Amulette
    {
        public LeaderCape() : base("Leader Cape", RarityEnum.Rare)
        {
            //Dégats
            Strength = 1;
            Power = 1;
            Mana = 1;
        }
    }
    #endregion Amulette

    #region Pet
    class Reel : Pets
    {
        public Reel() : base("Reel", RarityEnum.Rare)
        {
            Puissance = 4;
            HP = 1;
            Utilities = "When I’m played, Summon a common card for free. ";
        }
    }
    class WolfPack : Pets
    {
        public WolfPack() : base("Wolf Pack", RarityEnum.Rare)
        {
            Puissance = 2;
            HP = 3;
            Utilities = "If you have a “Small Wolf”, my stats are 4/4. ";
        }
    }
    class WildParrot : Pets
    {
        public WildParrot() : base("Wild Parrot", RarityEnum.Rare)
        {
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

    #region Legendary Card

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


    #endregion Cartes Individuelles

    #endregion Toutes les cartes

}
