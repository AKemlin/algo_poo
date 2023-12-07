using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Algo_Poo
{
    class Dictionnaire
    {
        public string[] lignes;

        public Dictionnaire(string cheminFichier)
        {
            static void Main(string[] args)
            {
                string CheminFichier = "votre_fichier.txt"; // Remplacez par le chemin de votre fichier
                string[] lignes;

                using (StreamReader sr = new StreamReader(CheminFichier))
                {
                    List<string> LignesListe = new List<string>();
                    string ligne;
                    while ((ligne = sr.ReadLine()) != null)
                    {
                        // Trier chaque ligne
                        string[] mots = ligne.Split(' ');
                        QuickSort(mots, 0, mots.Length - 1);
                        foreach (string mot in mots)
                        {
                            LignesListe.Add(mot);
                        }
                    }
                    lignes = LignesListe.ToArray();
                }

                // Afficher la liste finale
                foreach (string mot in lignes)
                {
                    Console.WriteLine(mot);
                }
            }
        }

        // Implémentation du Quick Sort
        public void QuickSort(string[] elements, int left, int right)
        {
            int i = left, j = right;
            string pivot = elements[(left + right) / 2];

            while (i <= j)
            {
                while (elements[i].CompareTo(pivot) < 0)
                {
                    i++;
                }

                while (elements[j].CompareTo(pivot) > 0)
                {
                    j--;
                }

                if (i <= j)
                {
                    // Échanger
                    string tmp = elements[i];
                    elements[i] = elements[j];
                    elements[j] = tmp;

                    i++;
                    j--;
                }
            }

            // Appels récursifs
            if (left < j)
                QuickSort(elements, left, j);
            if (i < right)
                QuickSort(elements, i, right);
        }

        // Recherche dichotomique
        public bool RechercherMot(string mot)
        {
            if (string.IsNullOrEmpty(mot))
                return false;

            int indexLigne = mot[0] - 'A'; // A = 0, B = 1, etc.
            if (indexLigne < 0 || indexLigne >= lignes.Length)
                return false;

            string[] mots = lignes[indexLigne].Split(' ');
            int gauche = 0;
            int droite = mots.Length - 1;

            while (gauche <= droite)
            {
                int milieu = gauche + (droite - gauche) / 2;
                int resultat = mots[milieu].CompareTo(mot);

                if (resultat == 0)
                    return true;
                if (resultat < 0)
                    gauche = milieu + 1;
                else
                    droite = milieu - 1;
            }

            return false;
        }
    }
}