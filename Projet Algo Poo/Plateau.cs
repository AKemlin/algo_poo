using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using System.Security.Cryptography;

namespace Projet_Algo_Poo
{
    public class Plateau
    {
        public string[][] Matrice; // Matrice représentant le plateau de jeu
        public int Cote; // Taille du côté du plateau (nombre de cases par côté)

        // Constructeur pour initialiser la matrice et la taille du plateau
        public Plateau(string[][] Matrice, int Cote)
        {
            this.Matrice = Matrice; // Initialisation de la matrice du plateau
            this.Cote = Cote; // Initialisation de la taille du plateau
        }

        // Méthode pour sauvegarder le plateau dans un fichier externe
        public void ToFile(string chemin)
        {
            try
            {
                using (StreamWriter Sauvegarde = new StreamWriter(chemin))
                {
                    for (int i = 0; i < this.Cote; i++)
                    {
                        for (int j = 0; j < this.Cote; j++)
                        {
                            Sauvegarde.Write(this.Matrice[i][j]); // Écriture de chaque élément du plateau dans le fichier
                            if (j < this.Cote - 1)
                            {
                                Sauvegarde.Write(","); // Séparation des éléments par des virgules
                            }
                        }
                        Sauvegarde.WriteLine(); // Nouvelle ligne pour chaque ligne du plateau
                    }
                }
                Console.WriteLine("Plateau Sauvegardé"); // Confirmation de la sauvegarde
                Console.WriteLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("Le plateau n'a pas pu être sauvegardé"); // Gestion des erreurs de sauvegarde
                Console.WriteLine();
            }
        }

        // Méthode pour créer le plateau à partir d'un fichier
        public void ToRead(string chemin)
        {
            string[] lignes = File.ReadAllLines(chemin); // Lecture des lignes du fichier
            for (int i = 0; i < this.Cote; i++)
            {
                this.Matrice[i] = lignes[i].Split(';'); // Remplissage du plateau à partir du fichier
            }
        }

        // Méthode pour créer le plateau de manière aléatoire
        public void ToReadRandom(string chemin)
        {
            string[] lignes = File.ReadAllLines(chemin); // Lecture des lignes du fichier Lettre.txt
            string[][] transition = new string[lignes.Length][];
            string[][] informations = new string[lignes.Length][];

            // Traitement des informations de chaque ligne
            for (int i = 0; i < lignes.Length; i++)
            {
                transition[i] = lignes[i].Split(';');
                informations[i] = transition[i][0].Split(',');
            }

            // Initialisation de la matrice du plateau
            for (int i = 0; i < this.Cote; i++)
            {
                this.Matrice[i] = new string[this.Cote];
            }

            int[] compteur = new int[26]; // Compteur pour chaque lettre
            Random r = new Random(); // Générateur de nombres aléatoires

            // Remplissage aléatoire du plateau en respectant les contraintes
            for (int i = 0; i < this.Cote; i++)
            {
                for (int j = 0; j < this.Cote; j++)
                {
                    int element = r.Next(0, 26); // Sélection aléatoire d'une lettre
                    while (compteur[element] == int.Parse(informations[element][1]))
                    {
                        element = r.Next(0, 26); // Sélection d'une autre lettre si le quota est atteint
                    }
                    this.Matrice[i][j] = informations[element][0].ToLower(); // Ajout de la lettre au plateau
                    compteur[element]++; // Incrémentation du compteur pour cette lettre
                }
            }
        }
        public string ToString()
        {
            string chainematrice = null; // Chaîne de caractères pour stocker la représentation du plateau
            for (int i = 0; i < this.Cote; i++) // Parcours de chaque ligne du plateau
            {
                for (int j = 0; j < this.Cote; j++) // Parcours de chaque colonne du plateau
                {
                    chainematrice += this.Matrice[i][j] + "  "; // Ajout de chaque élément du plateau à la chaîne
                }
                chainematrice += "\n"; // Ajout d'un saut de ligne après chaque ligne du plateau
            }
            return chainematrice; // Retour de la représentation du plateau sous forme de chaîne
        }
        public (bool, string[][]) Recherche_Mot(string mot)
        {
            char[] lettres = mot.ToCharArray(); // Convertit le mot en un tableau de caractères
            Stack<int[]> indices = new Stack<int[]>(); // Pile pour stocker les indices des lettres trouvées
            List<int[]> interdits = new List<int[]>(); // Liste pour stocker les indices interdits (déjà parcourus)
            int[] indicelettres = new int[2]; // Tableau pour stocker l'indice courant
            int comptelettre = 1; // Compteur pour le nombre de lettres trouvées
            bool p = false; // Variable pour vérifier si la première lettre est trouvée

            // Recherche de la première lettre du mot dans la dernière ligne du plateau
            for (int i = 0; i < this.Cote; i++)
            {
                if (this.Matrice[this.Cote - 1][i] == Convert.ToString(lettres[0]))
                {
                    int[] indtransi = new int[2] { this.Cote - 1, i };
                    indices.Push(indtransi); // Ajout de l'indice de la lettre trouvée à la pile
                    p = true; // Indique que la première lettre a été trouvée
                }
            }

            // Si la première lettre est trouvée, continue la recherche du reste du mot
            if (p)
            {
                (bool present, Stack<int[]> indicesfinal) = Recherche(lettres, indices, interdits, comptelettre);
                if (present)
                {
                    int taille = indicesfinal.Count;
                    for (int i = 0; i < taille; i++)
                    {
                        int[] indicesup = indicesfinal.Pop(); // Récupère les indices des lettres trouvées
                        this.Matrice[indicesup[0]][indicesup[1]] = " "; // Efface la lettre du plateau
                    }
                    return (true, this.Matrice); // Retourne vrai si le mot est trouvé, avec le plateau mis à jour
                }
            }
            return (false, this.Matrice); // Retourne faux si le mot n'est pas trouvé
        }
        public (bool, Stack<int[]>) Recherche(char[] lettres, Stack<int[]> indices, List<int[]> interdits,int comptelettre) // Recherche
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
            for (int k = 0; k < 10; k++)
            {
                for (int i = 0; i < this.Cote - 1; i++)
                {
                    for (int j = 0; j < this.Cote; j++)
                    {
                        if (this.Matrice[i + 1][j] == " ")
                        {
                            this.Matrice[i + 1][j] = this.Matrice[i][j];
                            this.Matrice[i][j] = " ";
                        }
                    }
                }
            }
        }
    }
}
