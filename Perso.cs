using System;

namespace PMT
{

    public class Perso
    {
        public string Nom = "Unknown";
        public int Pv = default;
        public int Pc = default;
        public int Initiative = 1;

        // protected Perso(string nom, int pv, int pc, int init)
        // {
        //     Nom = nom;
        //     Pv = pv;
        //     Pc=pc;
        //     Initiative = init;
        // }

        public virtual int Dmg { get; }

        public bool passeDArme(Perso adversaire)
        {
            int attaque = this.Pc + Misc.lanceDes6();
            int defense = adversaire.Pc + Misc.lanceDes6();

            if (attaque >= defense)
            {
                Console.WriteLine("ça touche !");
                adversaire.Pv -= Dmg;
                return true;
            }
            else
            {
                Console.WriteLine("Esquive...");
                return false;
            }
        }

        public virtual bool checkPersoMort()
        {
            Console.WriteLine("Ceci est un bug");
            return false;
        }
    }

    class Joueur : Perso
    {
        public int Pm = 2;

        public Joueur(string nom, int pv = 6, int pc = 4, int init = 2)
        {
            Nom = nom;
            Pc = pc;
            Pv = pv;
            Initiative = init;
        }
        public Equipement equipement = new Equipement();
        public List<Sort> deckSorts = new List<Sort>();

        public new int Dmg
        {
            get => equipement.arme.Dmg;
        }

        public void actionJoueur(List<Monstre> monstres)
        {
            Console.WriteLine("Choisissez votre action");
            foreach (int i in Enum.GetValues(typeof(Actions)))
            {
                Console.WriteLine($"{i}. {Enum.GetName(typeof(Actions), i)}");
            }

            int reponse = 0;

            do
            {
                reponse = Misc.readInt();
            } while (!checkReponseAction(reponse));

            Console.Clear();

            switch (reponse)
            {
                case 1:
                    attaquer(monstres);
                    break;

                case 2:
                    lancerSort(monstres);
                    break;

                case 3:
                    utiliserObjet(monstres);
                    //objetUtilise = true;
                    break;

                default:
                    Misc.erreur();
                    break;
            }
            return;
        }

        private bool checkReponseAction(int reponse)
        {

            foreach (int i in Enum.GetValues(typeof(Actions)))
            {
                if (reponse == i)
                {
                    if (i == (int)Actions.Sort && this.deckSorts.Count == 0)
                    {
                        Console.WriteLine("Vous n'avez aucun sorts.");
                        return false;
                    }
                    return true;
                }
            }

            Console.WriteLine("Ce n'est pas une réponse acceptable, réessayez.");

            return false;

        }


        private void utiliserObjet(List<Monstre> monstres)
        {
            Console.WriteLine("J'utilise un objet.");
        }

        private void lancerSort(List<Monstre> monstres)
        {
            Console.WriteLine("J'utilise un sort.");
        }

        private void attaquer(List<Monstre> monstres)
        {
            Console.WriteLine("Qui voulez vous attaquer ?");
            int i = 1;
            foreach (Monstre monstre in monstres)
            {
                Console.WriteLine($"{i}. {monstre.Nom}");
                i++;
            }

            int reponse = 0;
            do
            {
                reponse = Misc.readInt();
            } while (!checkReponseAttaque(reponse, monstres));

            int index = reponse - 1;

            if (passeDArme(monstres[index]))
            {
                if (monstres[index].checkPersoMort())
                {
                    monstres.RemoveAt(index);
                }
            }

            Misc.PressAnyKey();

            return;

        }

        private static bool checkReponseAttaque(int reponse, List<Monstre> monstres)
        {
            bool result = !(reponse == 0 || reponse > monstres.Count());
            if (!result) Console.WriteLine("Ce n'est pas une réponse acceptable. Réessayez.");
            return result;
        }
        public override bool checkPersoMort()
        {
            if (Pv <= 0)
            {
                Console.WriteLine("Vous êtes mort.\nVous allez quitter le jeu.");
                Misc.PressAnyKey();
                Environment.Exit(0);
                return true;
            }
            return false;
        }

        enum Actions
        {
            Attaque = 1,
            Sort = 2,
            Objet = 3
        }


    }

    class Monstre : Perso
    {
        public int _dmg;
        public Monstre(string nom, int pv, int pc, int init, int dmg)
        {
            Nom = nom;
            Pv = pv;
            Pc = pc;
            Initiative = init;
            _dmg = dmg;
        }
        //private bool isEnemy = true;
        //public bool isMort = false;
        public new int Dmg
        {
            get => _dmg;
            set => _dmg = value;
        }

        public override bool checkPersoMort()
        {
            if (Pv <= 0)
            {
                mortDuMonstre();
                return true;

            }
            else
            {
                Console.WriteLine($"{Nom} : {Pv} PV");
                return false;
            }
        }

        public void mortDuMonstre()
        {
            Console.WriteLine($"{this.Nom} est mort.");
        }

        public void attaqueMonstre(Joueur joueur)
        {
            Console.WriteLine(this.Nom + " vous attaque.");

            if (passeDArme(joueur))
            {
                if (!joueur.checkPersoMort())
                {
                    Console.WriteLine($"Il vous reste {joueur.Pv} PV.");
                }
            }

            Console.WriteLine();
            return;

        }


    }


}