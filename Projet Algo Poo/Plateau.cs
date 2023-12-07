using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.CompilerServices;

namespace Projet_Algo
{
    public class Plateau
    {
        public string[][] Matrice;
        public int Cote;
        public Plateau(string[][] Matrice, int Cote)
        {
            this.Matrice = Matrice;
            this.Cote = Cote;
        }

        public void ToFile(string chemin)
        {
            string[] lignes = new string[] { };
            for (int i = 0; i < this.Cote; i++)
            {
                for (int j = 0; j < this.Cote; j++)
                {
                    lignes.Append($"{this.Matrice[i][j]}");
                }
                lignes.Append("");
            }
            File.WriteAllLines(chemin, lignes);

        }

        public void ToRead(string chemin)
        {
            string[] lignes = File.ReadAllLines(chemin);
            for (int i = 0; i < this.Cote; i++)
            {
                this.Matrice[i] = lignes[i].Split(';');
            }
        }

        public void ToReadRandom(string chemin)
        {
            string[] lignes = File.ReadAllLines(chemin);
            string[][] transition = new string[lignes.Length][];
            string[][] informations = new string[lignes.Length][];
            for (int i = 0; i < lignes.Length; i++)
            {
                transition[i] = lignes[i].Split(';');
                informations[i] = transition[i][0].Split(',');

            }

            int[] compteur = new int[26];
            for (int i = 0; i < 26; i++)
            {
                compteur[i] = Convert.ToInt32(informations[i][1]);

            }
            Random r = new Random();

            for (int i = 0; i < this.Cote; i++)
            {
                for (int j = 0; j < this.Cote; j++)
                {
                    int element = r.Next(0, 26);
                    while (compteur[element] == 0)
                    {
                        element = r.Next(0, 26);
                    }

                    this.Matrice[i][j] = informations[element][0].ToLower();
                    compteur[element]--;
                }
            }
        }


        public string ToString()
        {
            string chainematrice = null;
            for (int i = 0; i < this.Cote; i++)
            {
                for (int j = 0; j < this.Cote; j++)
                {
                    chainematrice += this.Matrice[i][j] + "  ";
                }
                chainematrice += "\n";
            }

            return chainematrice;
        }


        public string[][] Recherche_Mot(string mot)
        {
            char[] lettres = mot.ToCharArray();
            int[][] indicelettres = new int[lettres.Length][];
            bool p = false;
            for (int i = 0; i < this.Cote; i++)
            {
                if (Convert.ToChar(this.Matrice[this.Cote - 1][i]) == lettres[0])
                {
                    p = true;
                    indicelettres[0][0] = i;
                    indicelettres[0][1] = this.Cote - 1;
                }

            }
            if (p == false)
            {
                Console.WriteLine("Le mot écrit n'est pas présent dans le plateau (Le mot doit commencer à partir du bas du plateau)");
                return this.Matrice;
            }
            int comptelettre = 1;

            while (p != false)
            {
                if (Convert.ToChar(this.Matrice[indicelettres[comptelettre][0]]) == lettres[comptelettre])
                {

                }
                return this.Matrice;
            }
            return this.Matrice;

        }
    }
}
