using System;
using System.Collections.Generic;

namespace PMT
{
    public class combatSimple
    {

        static void Main(string[] args)
        {

            //var newCombat = new combatSimple();
            Perso monPerso = creationPerso();
            List<Arme> stockArme = new List<Arme>();


            Console.WriteLine($"Bienvenue {monPerso.Nom} dans Porte Monstre Trésor !");
            Misc.PressAnyKey();
            Console.WriteLine("Vous entrez dans une nouvelle pièce.");

            List<Monstre> monstreDeLaPiece = popMonstre();

            if (monstreDeLaPiece.Count > 0)
            {
                declarationCombat(monstreDeLaPiece);
                deroulementCombat(monstreDeLaPiece, monPerso);
            }
            else
            {
                Console.WriteLine("La pièce est vide.");
            }



        }

        static Perso creationPerso()
        {
            
            Perso monPerso = new Perso("Montaigus");
            monPerso.Pv = 100;
            Arme hachette = new Arme(3, TypeArme.normale);
            Arme poing = new Arme();
            Sort eclair = new Sort(2, 1);

            monPerso.equipement.arme = hachette;
            monPerso.deckSorts.Add(eclair);
            monPerso.deckSorts.Add(eclair);
            return monPerso;
        }

        static List<Monstre> popMonstre()
        {
            List<Monstre> monstres = new List<Monstre>();

            monstres.Add(new Monstre("Zombie", 20, 4, 2, 1));
            monstres.Add(new Monstre("Zombie", 20, 4, 2, 1));

            return monstres;
        }

        private static void declarationCombat(List<Monstre> monstreDeLaPiece)
        {
            string texte = "Vous tombez sur";
            foreach (Monstre monstre in monstreDeLaPiece)
            {
                texte += $" un {monstre.Nom} et";

            }

            texte += " engagez le combat.";

            Console.WriteLine(texte);
            Misc.PressAnyKey();
        }

        private static void deroulementCombat(List<Monstre> monstreDeLaPiece, Perso monPerso)
        {
            do
            {
                afficherPV(monstreDeLaPiece);
                monPerso.actionPerso(monstreDeLaPiece);

                foreach (Monstre monstre in monstreDeLaPiece)
                {
                    monstre.attaqueMonstre(monPerso);
                }
                Misc.PressAnyKey();

            } while (monstreDeLaPiece.Count>0 && monPerso.Pv > 0);
        }

        private static void afficherPV(List<Monstre> monstres)
        {
            Console.WriteLine("Encore en combat :");
            foreach (Monstre monstre in monstres)
            {
                Console.WriteLine($"{monstre.Nom} : {monstre.Pv} PV");
            }
        }       
 

    }
    
}