using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Algo_Poo
{
    class Dictionnaire
    {
        public Dictionnaire(string cheminFichier)
        {
            string CheminFichier = "..//..//..//Mots_Français.txt";
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
            /*
            // Afficher la liste finale du dictionnaire trier
            string memoire = " ";
            foreach (string mot in lignes)
            {
                if (mot[0] == 'A')
                {
                    Console.Write(mot +" ");
                }
                else
                {
                    if (mot[0] != memoire[0])
                    {
                        Console.WriteLine();
                        Console.Write(mot+" ");
                    }
                    else
                    {
                        Console.Write(mot + " ");
                    }
                }
                memoire = mot;
            }
            Console.WriteLine();
            */

            Dictionary<char, int> NombreDeMots = new Dictionary<char, int>();
            foreach (string mot in lignes)
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
            // Afficher le contenu du dictionnaire
            foreach (var paire in NombreDeMots)
            {
                Console.Write($"Il y a donc pour la lettre {paire.Key} un total de {paire.Value} mots!");
                Console.Write("\n");
            }
        }

        // Implémentation du Quick Sort
        public void QuickSort(string[] Milieu, int left, int right)
        {
            int i = left, j = right;
            string Tampon1 = Milieu[(left + right) / 2];
            while (i <= j)
            {
                while (Milieu[i].CompareTo(Tampon1) < 0)
                {
                    i++;
                }
                while (Milieu[j].CompareTo(Tampon1) > 0)
                {
                    j--;
                }
                if (i <= j)
                {
                    // Échanger
                    string Tampon2 = Milieu[i];
                    Milieu[i] = Milieu[j];
                    Milieu[j] = Tampon2;
                    i++;
                    j--;
                }
            }
            // Appels récursifs
            if (left < j)
                QuickSort(Milieu, left, j);
            if (i < right)
                QuickSort(Milieu, i, right);
        }
        // Recherche dichotomique
        public string[] lignes;
        public bool RechDichoRecursif(string mot)
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
                int Milieu1 = gauche + (droite - gauche) / 2;
                int resultat = mots[Milieu1].CompareTo(mot);

                if (resultat == 0)
                    return true;
                if (resultat < 0)
                    gauche = Milieu1 + 1;
                else
                    droite = Milieu1 - 1;
            }
            return false;
        }
    }
}