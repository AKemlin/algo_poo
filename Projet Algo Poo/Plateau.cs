using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.CompilerServices;
using System.Diagnostics;

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

        public void ToFile(string chemin)  // Sauvegarde du plateau sur un fichier externe(Sauvegarde.csv)
        {
            try
            {
                using (StreamWriter Sauvegarde = new StreamWriter(chemin))
                {
                    for (int i = 0; i < this.Cote; i++)
                    {
                        for (int j = 0; j < this.Cote; j++)
                        {
                            Sauvegarde.Write(this.Matrice[i][j]);
                            if (j < this.Cote - 1)
                            {
                                Sauvegarde.Write(",");
                            }
                        }
                        Sauvegarde.WriteLine();
                    }
                }
                Console.WriteLine("Plateau Sauvegardé");
            }
            catch (Exception e)
            {
                Console.WriteLine("Le plateau n'a pas pu être sauvegardé");
            }
            
        }

        public void ToRead(string chemin)  // Création du plateau a partir du fichier Test1.csv
        {
            string[] lignes = File.ReadAllLines(chemin);
            for (int i = 0; i < this.Cote; i++)
            {
                this.Matrice[i] = lignes[i].Split(';');
            }
        }

        public void ToReadRandom(string chemin)  // Création du plateau aléatoirement en respectant les contraintes du fichier Lettre.txt
        {
           
            string[] lignes = File.ReadAllLines(chemin);
            string[][] transition = new string[lignes.Length][];
            string[][] informations = new string[lignes.Length][];
            for (int i = 0; i < lignes.Length; i++)
            {
                transition[i] = lignes[i].Split(';');
                informations[i] = transition[i][0].Split(',');
            }
            for (int i = 0;i < this.Cote; i++)
            {
                this.Matrice[i] = new string[this.Cote];
                
            }
            
            int[] compteur = new int[26];
            Random r = new Random();
            for (int i = 0; i < this.Cote ; i++)
            {
                
                for (int j = 0; j < this.Cote; j++)
                {
                    int element = r.Next(0, 26);
                    while (compteur[element] == int.Parse(informations[element][1]))
                    {
                        element = r.Next(0, 26);
                    }
                    this.Matrice[i][j] = informations[element][0].ToLower();
                    compteur[element]++;
                }
                    
            }
        }


        public string ToString()  // Retourne une chaine de caractères qui permet d'afficher le plateau
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


        public string[][] Recherche_Mot(string mot)  // recherche si le mot entré est present dans le plateau selon les règles 
        {
            char[] lettres = mot.ToCharArray();
            Stack<int[]> indices = new Stack<int[]>();
            List<int[]> interdits = new List<int[]>();
            int[] indicelettres = new int[2];
            
            bool p = false;
            int comptelettre = 1;
            for (int i = 0; i < this.Cote; i++)
            {
                if (Convert.ToChar(this.Matrice[this.Cote - 1][i]) == lettres[0])
                {
                    p = true;
                    indicelettres[0] = i;
                    indicelettres[1] = this.Cote - 1;
                    indices.Push(indicelettres);
                }

            }
            if (p == false)
            {
                Console.WriteLine("Le mot écrit n'est pas présent dans le plateau (Le mot doit commencer à partir du bas du plateau)");
                return this.Matrice;
            }
            //while ()
            //{

            //}
            return this.Matrice;
            

        }

        public bool Recherche(char[] lettres, Stack<int[]> indices, List<int[]> interdits,int comptelettre, bool f)
        {
            if (f == true )
            {
                return false;
            }

            return true;
        }

        public (bool, Stack<int[]>, int) PresenceGauche (char[] lettres, Stack<int[]> indices, Stack<int[]> interdits, int comptelettre)
        {
            int[] ind = indices.Peek();
            int[] newind = new int[2];
            if (ind[1] == 0)
            {
                return (false,indices,comptelettre);
            }
            else if (Convert.ToChar(this.Matrice[ind[0]][ind[1]-1]) == lettres[comptelettre])
            {
                newind[0] = ind[0];
                newind[1] = ind[1]-1;
                indices.Push(newind);
                return (true, indices, comptelettre++);
            }
            return (false,indices,comptelettre);
        }
        
        public (bool, Stack<int[]>, int) PresenceDroite(char[] lettres, Stack<int[]> indices, int comptelettre)
        {
            int[] ind = indices.Peek();
            int[] newind = new int[2];
            if (ind[1] == this.Cote-1)
            {
                return (false, indices, comptelettre);
            }
            else if (Convert.ToChar(this.Matrice[ind[0]][ind[1] + 1]) == lettres[comptelettre])
            {
                newind[0] = ind[0];
                newind[1] = ind[1] + 1;
                indices.Push(newind);
                return (true, indices, comptelettre++);
            }
            return (false, indices, comptelettre);
        }

        public (bool, Stack<int[]>, int) PresenceHaut(char[] lettres, Stack<int[]> indices, int comptelettre)
        {
            int[] ind = indices.Peek();
            int[] newind = new int[2];
            if (ind[0] == 0)
            {
                return (false, indices, comptelettre);
            }
            else if (Convert.ToChar(this.Matrice[ind[0] - 1][ind[1]]) == lettres[comptelettre])
            {
                newind[0] = ind[0] - 1;
                newind[1] = ind[1];
                indices.Push(newind);
                return (true, indices, comptelettre++);
            }
            return (false, indices, comptelettre);
        }

        public (bool, Stack<int[]>, int) PresenceHautGauche(char[] lettres, Stack<int[]> indices, int comptelettre)
        {
            int[] ind = indices.Peek();
            int[] newind = new int[2];
            if (ind[0] == 0 || ind[1] == 0)
            {
                return (false, indices, comptelettre);
            }
            else if (Convert.ToChar(this.Matrice[ind[0] - 1][ind[1] - 1]) == lettres[comptelettre])
            {
                newind[0] = ind[0] - 1;
                newind[1] = ind[1] - 1;
                indices.Push(newind);
                return (true, indices, comptelettre++);
            }
            return (false, indices, comptelettre);
        }

        public (bool, Stack<int[]>, int) PresenceHautDroite(char[] lettres, Stack<int[]> indices, int comptelettre)
        {
            int[] ind = indices.Peek();
            int[] newind = new int[2];
            if (ind[0] == 0 || ind[1] == this.Cote-1)
            {
                return (false, indices, comptelettre);
            }
            else if (Convert.ToChar(this.Matrice[ind[0] - 1][ind[1] + 1]) == lettres[comptelettre])
            {
                newind[0] = ind[0] - 1;
                newind[1] = ind[1] + 1;
                indices.Push(newind);
                return (true, indices, comptelettre++);
            }
            return (false, indices, comptelettre);
        }


        public void Maj_Plateau()  // Met a jour le plateau pour continuer le jeu.
        {
            for (int i = this.Cote; i >= 0; i++)
            {
                for (int j = 0; j < this.Cote; j++)
                {
                    if (i == 0)
                    {
                        break;
                    }
                    else if (this.Matrice[i - 1][j] == null)
                    {
                        break;
                    }
                    else if (this.Matrice[i][j] == null)
                    {
                        this.Matrice[i][j] = this.Matrice[i - 1][j];
                    }
                }
            }
        }


    }
}
