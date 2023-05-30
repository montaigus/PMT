using System;

namespace PMT 
{
    public static class Misc
    {


        public static void PressAnyKey()
        {
            Console.WriteLine("...\nAppuyez sur une touche pour continuer.");
            Console.ReadKey(true);
            Console.Clear();
        }
        
        public static int lanceDes6()
        {
            Random rand = new Random();
            return rand.Next(1,7);
        }

        public static void erreur()
        {
            Console.WriteLine("Vous êtez un gros con, vous venez de perdre votre tour !");
            return;
        }

        public static int readInt()
        {
            int reponse=0;
            bool success = false;
            
            do 
            {
                success = int.TryParse(Console.ReadLine(), out reponse);
                if (!success) {Console.WriteLine("Ce n'est pas une réponse. Réessayez.");}
            } while (!success);
            return reponse;
        }


    }
}