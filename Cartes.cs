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
        }


        public virtual void ShowCard()
        {
            Console.Write("A {0}: it's a {1} {2} from the pile of {3}. ", Name, Rarity, Categorie, PileCarte );
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
        public int HandTake;
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
            if(Mana.HasValue)
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
        public int? Strength;
        public int? Power;
        //Mana
        public int? Mana;


        protected Pets(string name, RarityEnum rarity) : base("Pets", name, rarity)
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
        public int? Puissance;
        public string? Property;

        public NormalMonster(string name, RarityEnum rarity) : base("Normal Monster", name, rarity)
        {

        }
        public override void ShowCard()
        {
            base.ShowCard();
            if (Puissance.HasValue)
            {
                Console.Write("Puissance: {0}. ", Puissance.Value);
            }
            if (Property != "")
            {
                Console.Write("Property : {0}. ", Property);
            }

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
            Power = 3;
            HandTake = 2;
        }
    }
    class MagicGauntlet : Weapon
    {
        public MagicGauntlet() : base("Magic Gauntlet", RarityEnum.Common)
        {
            //Dégats 
            Power = 2;
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
    class SmallArmor : Armor
    {
        public SmallArmor() : base("Small Armor", RarityEnum.Common)
        {
            //Dégats 
            Strength = 2;
        }
    }
    class MagicArmor : Armor
    {
        public MagicArmor() : base("Magic Armor", RarityEnum.Common)
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
    class AntiPetsSpell : Incantation
    {
        public AntiPetsSpell() : base("Anti-Pets Spell ", RarityEnum.Common)
        {
            properties1 = "Destroy a Common Pets. ";
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
            properties1 = "Destroy a Common Pets. ";
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
    class Gobelin : NormalMonster
    {
        public Gobelin() : base("Gobelin", RarityEnum.Common)
        {
            Puissance = 2;
        }
    }
    #endregion Normal Monster


    #endregion Common Card



    #endregion Cartes Individuelles

    #endregion Toutes les cartes

}
