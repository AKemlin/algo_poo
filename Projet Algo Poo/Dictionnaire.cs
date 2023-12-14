using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Projet_Algo_Poo
{
    class Dictionnaire
    {
        public string[][] lignes = new string[26][];
        public Dictionnaire(string[][] lignes)
        {
            this.lignes = lignes;
        }
        public void LectureDico()
        {
            string CheminFichier = "..//..//..//Mots_Français.txt";
            string[] lignesdico = File.ReadAllLines(CheminFichier);
            
            for (int i = 0; i < lignesdico.Length; i++)
            {
                this.lignes[i] = lignesdico[i].Split(' ');
            }
        }

        public void AfficheNbMots() // Affiche nombre de mots par lettre
        {
            Dictionary<char, int> NombreDeMots = new Dictionary<char, int>();
            for (int i = 0; i < this.lignes.Length; i++)
            {
                foreach (string ligne in this.lignes[i])
                {
                    foreach (string mot in ligne.Split(" "))
                    {
                        // S'assurer que le mot n'est pas vide
                        if (!string.IsNullOrEmpty(mot))
                        {
                            // Prendre la première lettre du mot et la convertir en majuscule
                            char lettre = char.ToUpper(mot[0]);
                            // Si la lettre est déjà dans le dictionnaire, incrémenter la valeur
                            // Sinon, ajouter la lettre avec une valeur initiale de 1
                            if (NombreDeMots.ContainsKey(lettre))
                            {
                                NombreDeMots[lettre]++;
                            }
                            else
                            {
                                NombreDeMots.Add(lettre, 1);
                            }
                        }
                    }
                }
            }
            foreach (var paire in NombreDeMots)
            {
                Console.Write($"Il y a donc pour la lettre {paire.Key} un total de {paire.Value} mots!");
                Console.Write("\n");
            }
        }

        public void AfficheDico() // Affiche le dictionnaire
        {
            for (int i = 0 ; i < this.lignes.Length ; i++)
            {
                for (int j = 0 ;j < this.lignes[i].Length ; j++)
                {
                    Console.Write(this.lignes[i][j] + " ");
                }
                Console.WriteLine();
            }
        }

        // Implémentation du TRI
        public void TRI()
        {
            for (int k  = 0; k < this.lignes.Length; k++)
            {
                for (int i = 0; i < this.lignes[k].Length - 1; i++)
                {
                    for (int j = 0; j < this.lignes[k].Length - i - 1; j++)
                    {
                        
                        if (this.lignes[k][j].CompareTo(this.lignes[k][j + 1]) > 0)
                        {
                            string tempon = this.lignes[k][j];
                            this.lignes[k][j] = this.lignes[k][j + 1];
                            this.lignes[k][j + 1] = tempon;
                        }
                    }
                }
            }
            
        }
        // Recherche dichotomique
        
        public bool RechDichoRecursif(string mot)
        {
            
            if (mot == null || mot.Length == 0)
            {
                return false;
            }
            mot = mot.ToUpper(); // Convertir le mot en majuscules
            int indexLigne = mot[0] - 'A';
            if (indexLigne < 0 || indexLigne >= this.lignes.Length)
            {
                return false;
            }
            return rechercheDichotomiqueRecursif(this.lignes[indexLigne], mot, 0, this.lignes[indexLigne].Length - 1);
        }

        public bool rechercheDichotomiqueRecursif(string[] mots, string mot, int debut, int fin)      //recherche dichotomique en récursif terminale afin de chercher un mot dans le dictionnaire. Exécuter par cette méthode permet de réduire la complexité
        {
            if (this.lignes == null || this.lignes.Length == 0)  //Les deux conditions permettent d'améliorer la complexité
            {
                return false;
            }

            if (debut > fin)
            {
                return false;
            }
            int milieu = (debut + fin) / 2;

            int compare1 = mot.CompareTo(mots[milieu]);  //Fonction compare pour comparer les mots.

            if (compare1 == 0)
            {
                return true;
            }
            if (compare1 < 0)
            {
                return rechercheDichotomiqueRecursif(mots, mot, debut, milieu - 1);
            }
            else if (compare1 > 0)
            {
                return rechercheDichotomiqueRecursif(mots, mot, milieu + 1, fin);
            }

            return false;
        }
    }
}



