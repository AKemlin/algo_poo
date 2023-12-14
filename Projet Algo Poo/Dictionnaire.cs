using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Algo_Poo
{
    class Dictionnaire
    {
        // Tableau de tableaux de chaînes pour stocker les mots classés par première lettre
        public string[][] lignes = new string[26][];
        // Constructeur pour initialiser les lignes du dictionnaire avec des données fournies
        public Dictionnaire(string[][] lignes)
        {
            this.lignes = lignes;
        }
        // Méthode pour lire et charger le dictionnaire à partir d'un fichier texte
        public void LectureDico()
        {
            string CheminFichier = "..//..//..//Mots_Français.txt"; // Chemin vers le fichier de dictionnaire
            string[] lignesdico = File.ReadAllLines(CheminFichier); // Lecture de toutes les lignes du fichier
            for (int i = 0; i < lignesdico.Length; i++)
            {
                this.lignes[i] = lignesdico[i].Split(' '); // Séparation des mots dans chaque ligne
            }
        }
        // Méthode pour afficher le nombre de mots par lettre
        public void AfficheNbMots()
        {
            Dictionary<char, int> NombreDeMots = new Dictionary<char, int>(); // Dictionnaire pour stocker le nombre de mots par lettre
            for (int i = 0; i < this.lignes.Length; i++)
            {
                foreach (string ligne in this.lignes[i])
                {
                    foreach (string mot in ligne.Split(" "))
                    {
                        if (!string.IsNullOrEmpty(mot)) // Vérifie que le mot n'est pas vide
                        {
                            char lettre = char.ToUpper(mot[0]); // Première lettre du mot en majuscule
                            if (NombreDeMots.ContainsKey(lettre))
                            {
                                NombreDeMots[lettre]++; // Incrémente le compteur pour cette lettre
                            }
                            else
                            {
                                NombreDeMots.Add(lettre, 1); // Ajoute la lettre avec un compteur initialisé à 1
                            }
                        }
                    }
                }
            }
            // Affichage du nombre de mots par lettre
            foreach (var paire in NombreDeMots)
            {
                Console.Write($"Il y a donc pour la lettre {paire.Key} un total de {paire.Value} mots!");
                Console.Write("\n");
            }
        }
        // Méthode pour afficher tout le dictionnaire
        public void AfficheDico()
        {
            for (int i = 0; i < this.lignes.Length; i++)
            {
                for (int j = 0; j < this.lignes[i].Length; j++)
                {
                    Console.Write(this.lignes[i][j] + " "); // Affiche chaque mot
                }
                Console.WriteLine(); // Nouvelle ligne après chaque lettre
            }
        }
        // Méthode pour trier les mots dans l'ordre alphabétique en utilisant le tri rapide
        public void QuickSort(string[] tab, int debut, int fin)
        {
            int i = debut;
            int j = fin;
            string Tampon1 = tab[(debut + fin) / 2]; // Élément pivot pour le tri
            while (i <= j)
            {
                while (tab[i].CompareTo(Tampon1) < 0) { i++; }
                while (tab[j].CompareTo(Tampon1) > 0) { j--; }
                if (i <= j)
                {
                    // Échange les éléments
                    string Tampon2 = tab[i];
                    tab[i] = tab[j];
                    tab[j] = Tampon2;
                    i++;
                    j--;
                }
            }
            // Appels récursifs pour continuer le tri dans les sous-sections
            if (debut < j) QuickSort(tab, debut, j);
            if (i < fin) QuickSort(tab, i, fin);
        }
        // Recherche dichotomique
        public bool RechDichoRecursif(string mot)
        {
            if (mot == null || mot.Length == 0)
            {
                return false; // Retourne faux si le mot est vide ou nul
            }
            mot = mot.ToUpper(); // Convertit le mot en majuscules
            int indexLigne = mot[0] - 'A'; // Calcule l'index basé sur la première lettre du mot
            if (indexLigne < 0 || indexLigne >= this.lignes.Length)
            {
                return false; // Retourne faux si l'index est hors des limites
            }
            return rechercheDichotomiqueRecursif(this.lignes[indexLigne], mot, 0, this.lignes[indexLigne].Length - 1);
        }
        public bool rechercheDichotomiqueRecursif(string[] mots, string mot, int debut, int fin)
        {
            if (this.lignes == null || this.lignes.Length == 0)
            {
                return false; // Retourne faux si les lignes ne sont pas initialisées ou vides
            }
            if (debut > fin)
            {
                return false; // Retourne faux si le début de la recherche est après la fin (cas non trouvé)
            }
            int milieu = (debut + fin) / 2; // Calcule l'index du milieu
            int compare1 = mot.CompareTo(mots[milieu]); // Compare le mot recherché avec le mot du milieu
            if (compare1 == 0)
            {
                return true; // Retourne vrai si le mot est trouvé
            }
            if (compare1 < 0)
            {
                return rechercheDichotomiqueRecursif(mots, mot, debut, milieu - 1); // Continue la recherche dans la moitié inférieure
            }
            else if (compare1 > 0)
            {
                return rechercheDichotomiqueRecursif(mots, mot, milieu + 1, fin); // Continue la recherche dans la moitié supérieure
            }
            return false; // Retourne faux si le mot n'est pas trouvé
        }
    }
}
