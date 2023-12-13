using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using System.Security.Cryptography;

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


        public (bool,string[][]) Recherche_Mot(string mot)  // recherche si le mot entré est present dans le plateau selon les règles 
        {
            char[] lettres = mot.ToCharArray();
            
            Stack<int[]> indices = new Stack<int[]>();
            List<int[]> interdits = new List<int[]>();
            int[] indicelettres = new int[2];
            int comptelettre = 1;
            bool p = false;
            for (int i = 0; i < this.Cote; i++)
            {
                if (this.Matrice[this.Cote - 1][i] == Convert.ToString(lettres[0]))
                {
                    int[] indtransi = new int [2] {this.Cote-1,i};
                    indices.Push (indtransi);
                    p = true;
                    
                }
            }
            if (p == true)
            {
                (bool present, Stack<int[]> indicesfinal) = Recherche(lettres, indices, interdits, comptelettre);
                
                if (present == true)
                {
                    int taille = indicesfinal.Count;
                    for (int i = 0; i < taille; i++)
                    {
                        int[] indicesup = indicesfinal.Pop();
                        
                        this.Matrice[indicesup[0]][indicesup[1]] = " ";
                        
                        
                    }
                    return (true, this.Matrice);
                }
            }
            
            return (false, this.Matrice);


        }

        public (bool, Stack<int[]>) Recherche(char[] lettres, Stack<int[]> indices, List<int[]> interdits,int comptelettre)
        {
            
            if (comptelettre >= lettres.Length)
            {
                return (true,indices);
            }

            else if (comptelettre == 1 && PresenceGauche(lettres,indices,interdits,comptelettre) == false && PresenceDroite(lettres, indices, interdits, comptelettre) == false && PresenceHaut(lettres, indices, interdits, comptelettre) == false && PresenceHautGauche(lettres, indices, interdits, comptelettre) == false && PresenceHautDroite(lettres, indices, interdits, comptelettre) == false)
            {
                return (false,indices);
            }

            else if (comptelettre > 1 && PresenceGauche(lettres, indices, interdits, comptelettre) == false && PresenceDroite(lettres, indices, interdits, comptelettre) == false && PresenceHaut(lettres, indices, interdits, comptelettre) == false && PresenceHautGauche(lettres, indices, interdits, comptelettre) == false && PresenceHautDroite(lettres, indices, interdits, comptelettre) == false)
            {
                int[] transiinterdits = indices.Pop();
                interdits.Add(transiinterdits);
                comptelettre--;
                return Recherche(lettres, indices, interdits, comptelettre);
            }

            else if (PresenceGauche(lettres, indices, interdits, comptelettre) == true)
            {
                int[] ind = indices.Peek();
                int[] newind = new int[2];
                newind[0] = ind[0];
                newind[1] = ind[1] - 1;
                indices.Push(newind);
                comptelettre++;
                return Recherche(lettres,indices,interdits,comptelettre);
            }

            else if (PresenceHautGauche(lettres, indices, interdits, comptelettre) == true)
            {
                int[] ind = indices.Peek();
                int[] newind = new int[2];
                newind[0] = ind[0] - 1;
                newind[1] = ind[1] - 1;
                indices.Push(newind);
                comptelettre++;
                return Recherche(lettres, indices, interdits, comptelettre);
            }

            else if (PresenceHaut(lettres, indices, interdits, comptelettre) == true)
            {
                int[] ind = indices.Peek();
                int[] newind = new int[2];
                newind[0] = ind[0] - 1;
                newind[1] = ind[1];
                indices.Push(newind);
                comptelettre++;
                return Recherche(lettres, indices, interdits, comptelettre);
            }

            else if (PresenceHautDroite(lettres, indices, interdits, comptelettre) == true)
            {
                int[] ind = indices.Peek();
                int[] newind = new int[2];
                newind[0] = ind[0] - 1;
                newind[1] = ind[1] + 1;
                indices.Push(newind);
                comptelettre++;
                return Recherche(lettres, indices, interdits, comptelettre);
            }

            else if (PresenceDroite(lettres, indices, interdits, comptelettre) == true)
            {
                int[] ind = indices.Peek();
                int[] newind = new int[2];
                newind[0] = ind[0];
                newind[1] = ind[1] + 1;
                indices.Push(newind);
                comptelettre++;
                return Recherche(lettres, indices, interdits, comptelettre);
            }
            return (true, indices);
        }

        public bool PresenceGauche (char[] lettres, Stack<int[]> indices, List<int[]> interdits, int comptelettre)
        {
            int[] ind = indices.Peek();
            
            if (ind[1] == 0)
            {
                return false;
            }
            
            for (int i = 0; i < interdits.Count; i++)
            {
                if (interdits[i] == ind)
                {
                    return false;
                }
            }
            
            if (this.Matrice[ind[0]][ind[1]-1] == Convert.ToString(lettres[comptelettre]))
            {
                
                return true;
            }
            return false;
        }
        
        public bool PresenceDroite(char[] lettres, Stack<int[]> indices, List<int[]> interdits, int comptelettre)
        {
            int[] ind = indices.Peek();
            
            for (int i = 0; i < interdits.Count; i++)
            {
                if (interdits[i] == ind)
                {
                    return false;
                }
            }
            
            if (ind[1] == this.Cote-1)
            {
                return false;
            }
            
            else if (this.Matrice[ind[0]][ind[1] + 1] == Convert.ToString(lettres[comptelettre]))
            {

                
                return true;
            }
            return false;
        }

        public bool PresenceHaut(char[] lettres, Stack<int[]> indices, List<int[]> interdits, int comptelettre)
        {
            int[] ind = indices.Peek();
            
            if (ind[0] == 0)
            {
                return false;
            }

            for (int i = 0; i < interdits.Count; i++)
            {
                if (interdits[i] == ind)
                {
                    return false;
                }
            }

            if (this.Matrice[ind[0] - 1][ind[1]] == Convert.ToString(lettres[comptelettre]))
            {
                
                return true;
            }
            return false;
        }

        public bool PresenceHautGauche(char[] lettres, Stack<int[]> indices, List<int[]> interdits, int comptelettre)
        {
            int[] ind = indices.Peek();
            
            if (ind[0] == 0 || ind[1] == 0)
            {
                return false;
            }

            for (int i = 0; i < interdits.Count; i++)
            {
                if (interdits[i] == ind)
                {
                    return false;
                }
            }

            if (this.Matrice[ind[0] - 1][ind[1] - 1] == Convert.ToString(lettres[comptelettre]))
            {
                
                return true;
            }
            return false;
        }

        public bool PresenceHautDroite(char[] lettres, Stack<int[]> indices, List<int[]> interdits, int comptelettre)
        {
            int[] ind = indices.Peek();
            
            if (ind[0] == 0 || ind[1] == this.Cote-1)
            {
                return false;
            }

            for (int i = 0; i < interdits.Count; i++)
            {
                if (interdits[i] == ind)
                {
                    return false;
                }
            }

            if (this.Matrice[ind[0] - 1][ind[1] + 1] == Convert.ToString(lettres[comptelettre]))
            {
                
                return true;
            }
            return false;
        }


        public void Maj_Plateau()  // Met a jour le plateau pour continuer le jeu.
        {
            
            for (int k = 0 ; k < 100; k++)
            {
                for (int i = this.Cote - 1; i >= 0; i--)
                {
                    for (int j = 0; j < this.Cote - 1; j++)
                    {
                        if (i == 0)
                        {
                            break;
                        }
                        else if (this.Matrice[i - 1][j] == " ")
                        {
                            break;
                        }
                        else if (this.Matrice[i][j] == " ")
                        {
                            this.Matrice[i][j] = this.Matrice[i - 1][j];
                            this.Matrice[i - 1][j] = " ";
                        }
                    }
                }
            }
            
        }
    }
}
