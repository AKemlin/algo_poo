using System.Runtime.CompilerServices;
using System.IO;
using Projet_Algo;

namespace Projet_Algo_2023
{
    class program
    {
        static void Main(string[] args)
        {
            Console.Write("Comment Construire le plateau ? ");
            Console.WriteLine(" (Entrer le numéro correspondant)");
            Console.WriteLine("1 : Aléatoirement");
            Console.WriteLine("2 : Grâce au fichier .csv");
            int MéthodeConstru = int.Parse(Console.ReadLine());
            string cheminSave = "C:\\Users\\reymo\\OneDrive\\Bureau\\ESILV NANTES\\A2\\Algo\\Projet_Algo\\fichiers\\Sauvegarde.csv";
            if (MéthodeConstru != 1 && MéthodeConstru != 2)
            {
                while (MéthodeConstru != 1 && MéthodeConstru != 2)
                {
                    Console.WriteLine("Entrer 1 ou 2 !");
                    MéthodeConstru = int.Parse(Console.ReadLine());
                }
            }

            if (MéthodeConstru == 1)
            {
                string chemin = "C:\\Users\\reymo\\OneDrive\\Bureau\\ESILV NANTES\\A2\\Algo\\Projet_Algo\\fichiers\\Lettre.txt";
                int cote = File.ReadAllLines(chemin).Length;
                string[][] matrice = new string[cote][];
                Plateau grille = new Plateau(matrice, cote);
                grille.ToReadRandom(chemin);
                Console.WriteLine(grille.ToString());
            }

            else if (MéthodeConstru == 2)
            {
                string chemin = "C:\\Users\\reymo\\OneDrive\\Bureau\\ESILV NANTES\\A2\\Algo\\Projet_Algo\\fichiers\\Test1.csv";
                int cote = File.ReadAllLines(chemin).Length;
                string[][] matrice = new string[cote][];
                string chainematrice = matrice.ToString();
                Plateau grille = new Plateau(matrice, cote);
                grille.ToRead(chemin);
                Console.WriteLine(grille.ToString());
                string mot = "maison";
                grille.Recherche_Mot(mot);
            }

            Dictionnaire dico = new Dictionnaire("C:\\Users\\kemli\\Documents\\C#\\Projet Algo Poo\\Projet Algo Poo\\Fichier Bonus\\MotFrançais.txt");
            bool trouve = dico.RechDichoRecursif("EXEMPLE");
            Console.WriteLine(trouve ? "Mot trouvé" : "Mot non trouvé");
        }
    }
}