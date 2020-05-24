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
            Console.Write("Power: {0}.", properties1);
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

    //Bouclier
    class SmallShield : Weapon
    {
        public SmallShield() : base("Small Shield", RarityEnum.Common)
        {
            //Dégats 
            Strength = 1;
        }
    }
    class SmallMagicShield : Weapon
    {
        public SmallMagicShield() : base("Small Magic Shield", RarityEnum.Common)
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
            Strength = 1;
            Power = 1;
        }
    }

    #endregion Armor

    #region Helmet
    class BasicHelmet : Armor
    {
        public BasicHelmet() : base("Basic Helmet", RarityEnum.Common)
        {
            //Dégats 
            Strength = 1;
        }
    }
    class BasicMagicHelmet : Armor
    {
        public BasicMagicHelmet() : base("Basic Magic Helmet", RarityEnum.Common)
        {
            //Dégats 
            Power = 1;
        }
    }

    #endregion Helmet

    #region Amulette
    class MagicRing : Amulette
    {
        public MagicRing() : base("Magic Ring", RarityEnum.Common)
        {
            //Dégats
            Power = 1;
            Mana = 1;
        }
    }
    #endregion Amulette

    //Spell
    #region Incantation
    class Enchantement : Incantation
    {
        public Enchantement() : base ("Enchantement", RarityEnum.Common)
        {
            properties1 = "Give +1 Strength to a Weapon.";
        }
    }
    class MagicEnchantement : Incantation
    {

        public MagicEnchantement() : base("Magic Enchantement", RarityEnum.Common)
        {
            properties1 = "Give +1 Power to a Weapon.";
        }
    }
    #endregion Incantation

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
