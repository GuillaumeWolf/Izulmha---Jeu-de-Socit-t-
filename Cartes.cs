using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Jeu_de_Socitété___Izulmha
{

    abstract class Carte
    {
        #region Public Properties
        //Name and Type
        public string Name;
        public string PileCarte;
        public string Categorie;
        public RarityEnum Rarity;
        //Rarity
        public enum RarityEnum { Common, Rare };

        //Cost
        public int Cost { get; private set; }

        
        protected Carte(string pileCarte, string categorie, string name, RarityEnum rarity)
        {
            PileCarte = pileCarte;
            Categorie = categorie;
            Name = name;
            Rarity = rarity;

            Cost = ((int)Rarity + 1) * 2;
        }

        //rarity
        public string RarityString;
        public int RarityInt;

        #endregion

        
        public virtual void ShowCard()
        {
            Console.Write("Name: {0}. Card Pile: {1}. ", Name, PileCarte);
            if (Categorie != null)
            {
                Console.Write("Categorie: {0}. ", Categorie);
            }
            Console.WriteLine("Rarity: {0}", Rarity);
        }

 
    }

    //Class Principale
    abstract class Spell : Carte
    {
        protected Spell(string categorie, string name, RarityEnum rarity) : base("Spell", categorie, name, rarity)
        {

        }

    }

    abstract class Object : Carte
    {
        protected Object(string categorie, string name, RarityEnum rarity) : base("Object", categorie, name, rarity)
        {
            
        }
    }



    //Class Secondaire
    abstract class Weapon : Object
    {
        //Dégats 
        //int
        public int? AP;
        public int? AM;


        protected Weapon(string name, RarityEnum rarity) : base("Weapon", name, rarity)
        {

        }

        public override void ShowCard()
        {
            base.ShowCard();
            if(AP.HasValue)
            {
                Console.Write("AP: {0}. ", AP.Value);
            }
            if (AM.HasValue)
            {
                Console.Write("AM: {0}. ", AM.Value);
            }
        }
    }
    /*
    abstract class Armors : Object
    {


    }

    abstract class Helmet : Object
    {
        //Second Type
        public static string Type2 = "Helmet";

    }

    abstract class Pets : Object
    {
        //Second Type
        public static string Type2 = "Pets";

    }



    */
    //Cartes Individuelles

    class BasicSword : Weapon
    {

        public BasicSword():base("Basic Sword", RarityEnum.Common )
        {
           

            //Dégats 
            //int
            AP = 2;
            AM = 0;
            
             
        }
    }

}
