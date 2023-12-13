using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Algo_Poo
{
    class Dictionnaire
    {
        public List<string> lignes = new List<string> { };
        public Dictionnaire(List<string> lignes)
        {
            this.lignes = lignes;
            //this.tout();
            //Console.WriteLine(this.lignes[5]);
        }
        public void tout()
        {
            string CheminFichier = "..//..//..//Mots_Français.txt";
            using (StreamReader sr = new StreamReader(CheminFichier))
            {
                List<string> LignesListe = new List<string>();
                string ligne;
                int compteLigne = 0;
                while ((ligne = sr.ReadLine()) != null)
                {
                    // Trier chaque ligne
                    string[] mots = ligne.Split(' ');
                    QuickSort(mots, 0, mots.Length - 1);
                    string ligneStr = String.Join(" ", mots);
                    LignesListe.Add(ligneStr);
                }
                this.lignes = LignesListe;
            }
            
            // Afficher la liste finale du dictionnaire trier
            Console.WriteLine();
            Console.WriteLine("Voulez vous afficher le dictionnaire ? (oui/non)");
            string answer = Console.ReadLine();
            while(answer != "oui" && answer != "non")
            {
                Console.WriteLine("Etes vous sûr ? (oui/non)");
                answer = Console.ReadLine();
            }
            if(answer == "oui")
            {
                string memoire = " ";
                foreach (string mot in this.lignes)
                {
                    if (mot[0] == 'A')
                    {
                        Console.Write(mot + " ");
                    }
                    else
                    {
                        if (mot[0] != memoire[0])
                        {
                            Console.WriteLine();
                            Console.Write(mot + " ");
                        }
                        else
                        {
                            Console.Write(mot + " ");
                        }
                    }
                    memoire = mot;
                }
                Console.WriteLine();
            }
            

            Dictionary<char, int> NombreDeMots = new Dictionary<char, int>();
            foreach (string ligne in this.lignes)
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

            // iteration sur les lettres 
            Console.WriteLine();
            Console.WriteLine("Voulez vous afficher le nombre de mots par lettre ? (oui/non)");
            string answer1 = Console.ReadLine();
            while (answer1 != "oui" && answer1 != "non")
            {
                Console.WriteLine("Etes vous sûr ? (oui/non)");
                answer1 = Console.ReadLine();
            }
            if (answer1 == "oui")
            {
                // Afficher le contenu du dictionnaire
                foreach (var paire in NombreDeMots)
                {
                    Console.Write($"Il y a donc pour la lettre {paire.Key} un total de {paire.Value} mots!");
                    Console.Write("\n");
                }
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
        
        public bool RechDichoRecursif(string mot)
        {
            if (string.IsNullOrEmpty(mot))
            {
                return false;
            }
            mot = mot.ToUpper(); // Convertir le mot en majuscules
            int indexLigne = mot[0] - 'A';
            if (indexLigne < 0 || indexLigne >= lignes.Count)
            {
                return false;
            }
            return rechercheDichotomiqueRecursif( mot, 0, this.lignes.Count - 1);
        }
        
        public bool rechercheDichotomiqueRecursif(string mot, int debut, int fin)      //recherche dichotomique en récursif terminale afin de chercher un mot dans le dictionnaire. Exécuter par cette méthode permet de réduire la complexité
        {
            if (this.lignes == null || this.lignes.Count == 0)  //Les deux conditions permettent d'améliorer la complexité
            {
                return false;
            }
             
            if (fin > debut)
            {
                return false;
            }
            int milieu = (debut + fin) / 2;

            int compare1 = string.Compare(mot, this.lignes[milieu]);  //Fonction compare pour comparer les mots.
            if (debut - fin == 1)
            {
                return this.lignes[fin] == mot || this.lignes[debut] == mot;    //Fin - début == 1 permet de ne pas se retrouver avec un stackOverFlow
            }
            if (fin < debut)    //Comparaison afin de vérifier si la condition de récursion est respectée.
            {
                if (compare1 == 0)
                {
                    return true;
                }
                if (compare1 < 0)
                {
                    return rechercheDichotomiqueRecursif(mot, debut, milieu - 1);
                }
                else if (compare1 > 0)
                {
                    return rechercheDichotomiqueRecursif(mot, milieu - 1, fin);
                }
            }
            return false;
        }
    }
}



