using System;
using System.Collections.Generic;

namespace PMT
{
    public class combatSimple {
        
public static void Main(string[] args) {

Perso monPerso = creationPerso();
List<Arme> stockArme = new List<Arme>();


console.WriteLine("Bienvenue " + monPerso.nom + " dans Porte Monstre Trésor !");
console.WriteLine("Vous entrez dans une nouvelle pièce.");

Monstre[] monstreDeLaPiece = popMonstre();

if (monstreDeLaPiece.count > 0)
{
declarationCombat(monstreDeLaPiece);

}


}

Perso creationPerso() {
    Perso monPerso = new Perso("Montaigus",100);
    Arme hachette = new Arme(dmg:3, type:TypeArme.normale);
    Sort eclair = new Sort(dmg:2, coutMana:1);

    monPerso.equipement.arme = hachette;
    monPerso.deckSorts.add(eclair);
    monPerso.deckSorts.add(eclair);
    return monPerso;
}

Monstre[] popMonstre() {

    Monstre[] monstres = new Monstre[2]();

    monstres[0] = new Monstre("Zombie", 20, 4, 2, 1);
    monstres[1] = new Monstre("Zombie", 20, 4, 2, 1);

    return monstres;
}

private static void declarationCombat (Monstre[] monstreDeLaPiece)
{
    string texte = "Vous tombez sur";
    foreach (Monstre monstre in monstreDeLaPiece)
    {
        texte += " un " + monstre.nom + " et";
        
    }

    texte += " engagez le combat.";

    console.WriteLine(texte);
} 

private static void deroulementCombat (Monstre[] monstreDeLaPiece, Perso monPerso){
    do {
        afficherPV(monstreDeLaPiece);
        actionPerso(monPerso, monstreDeLaPiece);

        foreach (Monstre monstre in monstreDeLaPiece) {
            if (monstre.isMort ==false) attaqueMonstre(monstre, monPerso);
        }

    } while (!verifTousMorts(monstreDeLaPiece) && perso.pv != 0)
}

private void attaqueMonstre(Monstre monstre, Perso perso)
{
    console.WriteLine(monstre.nom + " vous attaque.");
    int defensePerso = perso.pc + lanceDes6();
    int attaqueMonstre = monstres.pc + lanceDes6();

    if (attaqueMonstre >= defensePerso) {
        console.WriteLine("Attaque réussie, vous prenez une torgnole !");
        perso.pv -= monstre.dmg;
        console.WriteLine("Il vous reste " + perso.pv + " PV.");
    }
    else {
        console.WriteLine("Vous esquivez.");
    }
    return;

}

private void afficherPV(Monstre[] monstres) {
    console.WriteLine("Encore en combat :")
    foreach (Monstre monstre in monstres) {
        if (monstre.isMort == true) next;
        console.WriteLine(monstre.nom + " : " + monstre.pv + " PV");        
    }
}

private void actionPerso(Perso perso, Monstre[] monstres)
{
    bool objetUtilise = false;

    console.WriteLine("Choisissez votre action");
    console.WriteLine("1. Attaquer");
    if (perso.deckSorts.count!=0) console.WriteLine("2. Lancer un sort");
    console.WriteLine("3. Utiliser un objet");

    int reponse;
    //capture de la réponse

    switch (reponse){
        case 1:
        attaquer(perso, monstres);
        break;

        case 2:
        lancerSort(perso, monstre);
        break;

        case 3:
        utiliserObjet(perso, monstre);
        objetUtilise = true;
        break;
        
        default:    
        erreur();    
        break;
    } 
    return;
}

private void attaquer(Perso perso, Monstre[] monstres)
{
    console.WriteLine("Qui voulez vous attaquer ?")
    int i = 1;
    foreach (Monstre monstre in monstres) {
        console.WriteLine(i + ". " + monstre.nom);
        i++;
    }

    int reponse;//capturer la réponse;
    if (reponse > monstres.count()) {
        erreur();
        return;
    }

    int attaquePerso = perso.pc + lanceDes6();
    int defenseMonstre = monstres[reponse-1].pc + lanceDes6();

    if (attaquePerso >= defenseMonstre) {
        console.WriteLine("Attaque réussie !");
        monstres[reponse - 1].pv -= perso.equipement.arme.dmg;
        console.WriteLine(monstres[reponse - 1].nom + " : " + monstres[reponse - 1].pv + " PV");
    }
    else {
        console.WriteLine("Attaque ratée...");
    }

    return;

} 

private int lanceDes6() {
    return Math.hasard(6);
}

private void erreur(){
console.WriteLine("Vous êtez un gros con, vous venez de perdre votre tour !");
return;
}



private bool verifTousMorts (Monstres[] monstres){
    foreach (Monstre monstre in monstres) {
        if monstre.isMort == false ;
        return false;
    }
    return true;
}

private void verifMonstreVivant(Monstre monstre, bool tousMorts) {
        if (monstre.pv==0) 
        {
            console.WriteLine ("vous venez de tuer " + monstre.nom);
            monstre.isMort = true;
        }
        return;
}

Dictionnary<int,string> ActionsPossibles = new Dictionnary<int,string>();
ActionsPossibles.add(1,"Attaquer");
ActionsPossibles.add(2, "Lancer un sort");
ActionsPossibles.add(3, "Utiliser un objet");


class Monstre {
    public string nom;
    public int pv;
    public int pc;
    public int dmg;
    public int initiative;
    //private bool isEnemy = true;
    private bool isMort = false;
}

class Sort {
    public int dmg;
    public int coutMana;
}

enum TypeArme
{
    rapide,
    normale,
    deuxMain
}

class Arme {
    public int dmg = 1;
    public int pcMin = 0;
    public string type = TypeArme.rapide;
}

class Tenue {
    public int pcDefBonus = 0;
    public int pcAttBonus = 0;
    public int pmBonus = 0;
}

class Equipement {
    public Arme arme = new Arme();
    public Tenue tenue = new Tenue();
}



class Perso {
    public string nom = "vous n'avez pas rentré de nom";
    public int pv=6;
    public int pc=4;
    public int initiative=2;
    public int pm=2;
    //public bool isEnemy = false;
    public Equipement equipement = new Equipement();
    public List<Sort> deckSorts = new List<Sort>();    
}

}
}