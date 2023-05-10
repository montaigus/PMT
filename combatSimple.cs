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


            Console.WriteLine("Bienvenue " + monPerso.nom + " dans Porte Monstre Trésor !");
            Console.WriteLine("Vous entrez dans une nouvelle pièce.");

            List<Monstre> monstreDeLaPiece = popMonstre();

            if (monstreDeLaPiece.Count > 0)
            {
                declarationCombat(monstreDeLaPiece);
            }


        }

        static Perso creationPerso()
        {
            Perso monPerso = new Perso("Montaigus");
            monPerso.pv = 100;
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
                texte += " un " + monstre.nom + " et";

            }

            texte += " engagez le combat.";

            Console.WriteLine(texte);
        }

        private static void deroulementCombat(List<Monstre> monstreDeLaPiece, Perso monPerso)
        {
            do
            {
                afficherPV(monstreDeLaPiece);
                actionPerso(monPerso, monstreDeLaPiece);

                foreach (Monstre monstre in monstreDeLaPiece)
                {
                    if (monstre.isMort == false) attaqueMonstre(monstre, monPerso);
                }

            } while (!verifTousMorts(monstreDeLaPiece) && monPerso.pv != 0);
        }

        private static void attaqueMonstre(Monstre monstre, Perso perso)
        {
            Console.WriteLine(monstre.nom + " vous attaque.");
            int defensePerso = perso.pc + lanceDes6();
            int attaqueMonstre = monstre.pc + lanceDes6();

            if (attaqueMonstre >= defensePerso)
            {
                Console.WriteLine("Attaque réussie, vous prenez une torgnole !");
                perso.pv -= monstre.dmg;
                Console.WriteLine("Il vous reste " + perso.pv + " PV.");
            }
            else
            {
                Console.WriteLine("Vous esquivez.");
            }
            return;

        }

        private static void afficherPV(List<Monstre> monstres)
        {
            Console.WriteLine("Encore en combat :");
            foreach (Monstre monstre in monstres)
            {
                if (monstre.isMort == true) { continue; }
                Console.WriteLine(monstre.nom + " : " + monstre.pv + " PV");
            }
        }

        private static void actionPerso(Perso perso, List<Monstre> monstres)
        {
            bool objetUtilise = false;

            Console.WriteLine("Choisissez votre action");
            Console.WriteLine("1. Attaquer");
            if (perso.deckSorts.Count != 0) Console.WriteLine("2. Lancer un sort");
            Console.WriteLine("3. Utiliser un objet");

            int reponse=0;
            //capture de la réponse

            switch (reponse)
            {
                case 1:
                    attaquer(perso, monstres);
                    break;

                case 2:
                    lancerSort(perso, monstres);
                    break;

                case 3:
                    utiliserObjet(perso, monstres);
                    objetUtilise = true;
                    break;

                default:
                    erreur();
                    break;
            }
            return;
        }

        private static void utiliserObjet(Perso perso, List<Monstre> monstres)
        {
            throw new NotImplementedException();
        }

        private static void lancerSort(Perso perso, List<Monstre> monstres)
        {
            throw new NotImplementedException();
        }

        private static void attaquer(Perso perso, List<Monstre> monstres)
        {
            Console.WriteLine("Qui voulez vous attaquer ?");
            int i = 1;
            foreach (Monstre monstre in monstres)
            {
                Console.WriteLine(i + ". " + monstre.nom);
                i++;
            }

            int reponse=0;//capturer la réponse;
            if (i == 0 || reponse > monstres.Count())
            {
                erreur();
                return;
            }

            int attaquePerso = perso.pc + lanceDes6();
            int defenseMonstre = monstres[reponse - 1].pc + lanceDes6();

            if (attaquePerso >= defenseMonstre)
            {
                Console.WriteLine("Attaque réussie !");
                monstres[reponse - 1].pv -= perso.equipement.arme.dmg;
                Console.WriteLine(monstres[reponse - 1].nom + " : " + monstres[reponse - 1].pv + " PV");
            }
            else
            {
                Console.WriteLine("Attaque ratée...");
            }

            return;

        }

        private static int lanceDes6()
        {
            Random rand = new Random();
            return rand.Next(1,7);
        }

        private static void erreur()
        {
            Console.WriteLine("Vous êtez un gros con, vous venez de perdre votre tour !");
            return;
        }



        private static bool verifTousMorts(List<Monstre> monstres)
        {
            foreach (Monstre monstre in monstres)
            {
                if (monstre.isMort == false)
                    return false;
            }
            return true;
        }

        private void verifMonstreVivant(Monstre monstre, bool tousMorts)
        {
            if (monstre.pv == 0)
            {
                Console.WriteLine("vous venez de tuer " + monstre.nom);
                monstre.isMort = true;
            }
            return;
        }


        class Monstre
        {
            public string nom;
            public int pv;
            public int pc;
            public int dmg;
            public int initiative;
            //private bool isEnemy = true;
            public bool isMort = false;

            public Monstre(string nom, int pv, int pc, int dmg, int initiative)
            {
                this.nom = nom;
                this.pv = pv;
                this.pc = pc;
                this.dmg = dmg;
                this.initiative = initiative;

            }

        }

        class Sort
        {
            public int dmg;
            public int coutMana;

            public Sort(int DMG, int CM)
            {
                this.dmg = DMG;
                this.coutMana = CM;
            }

        }

        enum TypeArme
        {
            rapide,
            normale,
            deuxMain
        }

        class Arme
        {
            public int dmg = 1;
            //public int pcMin = 0;
            public TypeArme type = TypeArme.rapide;
            
            public Arme(){

            }
            public Arme(int dmg, TypeArme type){
                this.dmg = dmg;
                this.type = type;
            }

        }

        class Tenue
        {
            public int pcDefBonus = 0;
            public int pcAttBonus = 0;
            public int pmBonus = 0;
        }

        class Equipement
        {
            public Arme arme = new Arme();
            public Tenue tenue = new Tenue();
        }



        class Perso
        {
            public string nom = "vous n'avez pas rentré de nom";
            public int pv = 6;
            public int pc = 4;
            public int initiative = 2;
            public int pm = 2;
            //public bool isEnemy = false;
            public Perso(string nom)
            {
                this.nom = nom;
            }
            public Equipement equipement = new Equipement();
            public List<Sort> deckSorts = new List<Sort>();
        }

    }
}