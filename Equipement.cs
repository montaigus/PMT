using System;

namespace PMT {
           enum TypeArme
        {
            rapide,
            normale,
            deuxMain
        }

        class Arme
        {
            public int Dmg = 1;
            //public int pcMin = 0;
            public TypeArme Type = TypeArme.rapide;
            
            public Arme(){

            }
            public Arme(int dmg, TypeArme type){
                Dmg = dmg;
                Type = type;
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

}