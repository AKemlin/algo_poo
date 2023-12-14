using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Algo_Poo
{
    public class Jeu
    {
        static void Main(string[] args)
        {
            // Définition de la couleur du texte dans la console
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Bienvenue sur le jeu de mots glissés de Paul et Amaury !");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.WriteLine();
            // Demande à l'utilisateur s'il souhaite connaître l'histoire du jeu
            Console.WriteLine("Voulez vous connaitre notre histoire ? (oui/non)");
            string answer1 = Console.ReadLine();
            while (answer1 != "oui" && answer1 != "non" && answer1 != null)
            {
                Console.WriteLine("Voulez vous connaitre notre histoire ? (oui/non)");
                answer1 = Console.ReadLine();
            }
            // Affichage de l'histoire si l'utilisateur répond "oui"
            if (answer1 == "oui")
            {
                // [Insérez ici l'histoire de Paul et Amaury, déjà fournie]
            }
            // Initialisation des composants du jeu
            Plateau grille;
            string[][] lignes = new string[26][]; // Tableau pour les lignes du dictionnaire
            Dictionnaire dico = new Dictionnaire(lignes); // Création d'un dictionnaire
            dico.LectureDico(); // Lecture et chargement du dictionnaire
            // Tri alphabétique de chaque ligne du dictionnaire
            for (int i = 0; i < 26; i++)
            {
                dico.QuickSort(lignes[i], 0, lignes[i].Length - 1);
            }
            // Demande à l'utilisateur s'il souhaite afficher le dictionnaire
            Console.WriteLine();
            Console.WriteLine("Voulez vous afficher le dictionnaire ? (oui/non)");
            string answer2 = Console.ReadLine();
            while (answer2 != "oui" && answer2 != "non" && answer2 != null)
            {
                Console.WriteLine("Voulez vous afficher le dictionnaire ? (oui/non)");
                answer2 = Console.ReadLine();
            }
            if (answer2 == "oui")
            {
                dico.AfficheDico(); // Affichage du dictionnaire
            }
            // Demande à l'utilisateur s'il souhaite voir le nombre de mots par lettre
            Console.WriteLine();
            Console.WriteLine("Voulez vous afficher le nombre de mots par lettre ? (oui/non)");
            string answer3 = Console.ReadLine();
            while (answer3 != "oui" && answer3 != "non" && answer3 != null)
            {
                Console.WriteLine("Voulez vous afficher le nombre de mots par lettre ? (oui/non)");
                answer3 = Console.ReadLine();
            }
            if (answer3 == "oui")
            {
                dico.AfficheNbMots(); // Affichage du nombre de mots par lettre
            }
            Console.WriteLine(" ");
            // Demande et création des joueurs
            Console.Write("Nom du ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("joueur 1");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(" ?");
            Joueurs joueur1 = new Joueurs(Console.ReadLine());
            Console.WriteLine();
            Console.Write("Nom du ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("joueur 2");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(" ?");
            Joueurs joueur2 = new Joueurs(Console.ReadLine());
            Console.WriteLine();
            // Paramétrage de la durée du tour et de la partie
            Console.WriteLine("Combien de temps voulez vous que le tour dure ? (en secondes)");
            TimeSpan tempspartour = new TimeSpan(0, 0, Convert.ToInt32(Console.ReadLine()));
            Console.WriteLine();
            Console.WriteLine("Combien de temps voulez vous que dure la partie ? (en secondes)");
            TimeSpan TempsPartie = new TimeSpan(0, 0, Convert.ToInt32(Console.ReadLine()));
            Console.WriteLine();
            // Sélection de la méthode de construction du plateau
            Console.Write("Comment voulez vous construire le plateau ? ");
            Console.WriteLine(" (Entrer le numéro correspondant)");
            Console.WriteLine("1 : Aléatoirement");
            Console.WriteLine("2 : Grâce au fichier .csv");
            string MéthodeConstru = (Console.ReadLine());
            string cheminSave = "../../../Sauvegarde.csv"; // Chemin de sauvegarde
            // Validation de la méthode de construction du plateau
            if (MéthodeConstru != "1" && MéthodeConstru != "2")
            {
                while (MéthodeConstru != "1" && MéthodeConstru != "2" && MéthodeConstru != null)
                {
                    Console.WriteLine("Entrer 1 ou 2 !");
                    MéthodeConstru = Console.ReadLine();
                }
            }
            if (MéthodeConstru == "1")
            {
                string chemin = "../../../Lettre.txt"; // Chemin vers le fichier contenant les lettres
                Console.WriteLine("Quelle dimension du plateau ? (max = 10)");
                int cote = int.Parse(Console.ReadLine()); // Lecture de la dimension du plateau
                string[][] matrice = new string[cote][]; // Création d'une matrice pour le plateau
                grille = new Plateau(matrice, cote); // Initialisation du plateau
                grille.ToReadRandom(chemin); // Lecture aléatoire pour remplir le plateau
                Console.WriteLine(grille.ToString()); // Affichage du plateau
                // Demande si l'utilisateur souhaite sauvegarder le plateau
                Console.WriteLine("Voulez-vous sauvegarder le plateau dans un fichier exterieur ? (oui ou non)");
                string Save = Console.ReadLine().ToLower();
                while (Save != "oui" && Save != "non" && Save != null)
                {
                    Console.WriteLine("Voulez-vous sauvegarder le plateau dans un fichier exterieur ? (oui ou non)");
                    Save = Console.ReadLine();
                }
                if (Save == "oui")
                {
                    grille.ToFile(cheminSave); // Sauvegarde du plateau dans un fichier
                }
            }
            else
            {
                Console.WriteLine();
                string chemin = "../../../Test1.csv"; // Chemin vers le fichier .csv
                int cote = File.ReadAllLines(chemin).Length; // Détermination de la dimension du plateau
                string[][] matrice = new string[cote][]; // Création d'une matrice pour le plateau
                grille = new Plateau(matrice, cote); // Initialisation du plateau
                grille.ToRead(chemin); // Lecture du fichier .csv pour remplir le plateau
                Console.WriteLine(grille.ToString()); // Affichage du plateau
            }
            Console.WriteLine("Appuyez sur une touche pour commencer");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine(grille.ToString());
            DateTime date3 = DateTime.Now; // Début du timer pour la partie
            // Boucle principale du jeu
            while (DateTime.Now - date3 < TempsPartie)
            {
                DateTime date1 = DateTime.Now; // Début du timer pour le tour
                while (DateTime.Now - date1 < tempspartour)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write(joueur1.AppelationP);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(" A toi de jouer : ");
                    string mot1 = Console.ReadLine();
                    if (DateTime.Now - date1 > tempspartour)
                    {
                        Console.WriteLine("Temps du tour écoulé !");
                        break;
                    }
                    bool presence = true;
                    bool dicto = dico.RechDichoRecursif(mot1); // Vérifie si le mot est dans le dictionnaire
                    if (!dicto)
                    {
                        Console.WriteLine("Le mot entré n'est pas dans le dictionnaire !");
                        presence = false;
                    }
                    if (presence)
                    {
                        Console.WriteLine("Le mot entré est dans le dictionnaire");
                        (presence, grille.Matrice) = grille.Recherche_Mot(mot1); // Recherche du mot dans le plateau
                        if (presence)
                        {
                            Console.WriteLine("Le mot est dans le plateau");
                            grille.Maj_Plateau(); // Mise à jour du plateau
                            // Lecture du fichier lettre.txt pour le calcul du score
                            string cheminscore = "../../../Lettre.txt";
                            string[] ligneslettres = File.ReadAllLines(cheminscore);
                            string[][] transition = new string[ligneslettres.Length][];
                            string[][] informations = new string[ligneslettres.Length][];
                            for (int i = 0; i < ligneslettres.Length; i++)
                            {
                                transition[i] = ligneslettres[i].Split(';');
                                informations[i] = transition[i][0].Split(',');
                            }
                            string motmaj = mot1.ToUpper();
                            char[] lettres = motmaj.ToCharArray();
                            int score = 0;
                            for (int i = 0; i < lettres.Length; i++)
                            {
                                int ligneinfo = lettres[i] - 'A';
                                score += Convert.ToInt32(informations[ligneinfo][2]);
                            }
                            bool vrai1 = joueur1.Contient(mot1);
                            if (!vrai1)
                            {
                                joueur1.Add_Mot(mot1); // Ajoute le mot trouvé
                                joueur1.Add_Score(score); // Ajoute le score correspondant
                            }
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Le mot n'est pas dans le plateau");
                        }
                    }
                }
                Console.Write("Fin du tour de ");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(joueur1.AppelationP);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
                grille.Maj_Plateau();
                Console.WriteLine(grille.ToString());
                DateTime date2 = DateTime.Now; // Début du timer pour le tour du joueur 2
                while (DateTime.Now - date2 < tempspartour) // Boucle pour le tour du joueur 2
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(joueur2.AppelationP); // Affiche le nom du joueur 2
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(" A toi de jouer : ");
                    string mot2 = Console.ReadLine(); // Lecture du mot entré par le joueur 2
                    if (DateTime.Now - date2 > tempspartour) // Vérifie si le temps imparti est écoulé
                    {
                        Console.WriteLine("Temps du tour écoulé !");
                        break;
                    }
                    bool presence = true; // Variable pour vérifier la présence du mot
                    bool dicto = dico.RechDichoRecursif(mot2); // Vérifie si le mot est dans le dictionnaire
                    if (!dicto) // Si le mot n'est pas dans le dictionnaire
                    {
                        Console.WriteLine("Le mot entré n'est pas dans le dictionnaire !");
                        presence = false; // Indique que le mot n'est pas valide
                    }
                    if (presence) // Si le mot est valide
                    {
                        Console.WriteLine("Le mot entré est dans le dictionnaire");
                        (presence, grille.Matrice) = grille.Recherche_Mot(mot2); // Recherche du mot dans le plateau
                        if (presence) // Si le mot est trouvé dans le plateau
                        {
                            Console.WriteLine("Le mot est dans le plateau");
                            grille.Maj_Plateau(); // Mise à jour du plateau après avoir trouvé le mot
                            // Lecture du fichier lettre.txt pour calculer le score du mot
                            string cheminscore = "../../../Lettre.txt";
                            string[] ligneslettres = File.ReadAllLines(cheminscore);
                            string[][] transition = new string[ligneslettres.Length][];
                            string[][] informations = new string[ligneslettres.Length][];
                            for (int i = 0; i < ligneslettres.Length; i++)
                            {
                                transition[i] = ligneslettres[i].Split(';');
                                informations[i] = transition[i][0].Split(',');
                            }
                            string motmaj = mot2.ToUpper(); // Convertit le mot en majuscules
                            char[] lettres = motmaj.ToCharArray(); // Découpe le mot en lettres
                            int score = 0;
                            for (int i = 0; i < lettres.Length; i++) // Calcule le score du mot
                            {
                                int ligneinfo = lettres[i] - 'A';
                                score += Convert.ToInt32(informations[ligneinfo][2]);
                            }
                            bool vrai2 = joueur2.Contient(mot2); // Vérifie si le joueur a déjà trouvé ce mot
                            if (!vrai2) // Si le joueur n'a pas encore trouvé ce mot
                            {
                                joueur2.Add_Mot(mot2); // Ajoute le mot à la liste des mots trouvés du joueur
                                joueur2.Add_Score(score); // Ajoute le score du mot au score total du joueur
                            }
                            break; // Termine le tour du joueur
                        }
                        else // Si le mot n'est pas dans le plateau
                        {
                            Console.WriteLine("Le mot n'est pas dans le plateau");
                        }
                    }
                }
                // Affichage de la fin du tour du joueur 2
                Console.Write("Fin du tour de ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(joueur2.AppelationP); // Affiche le nom du joueur 2
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
                grille.Maj_Plateau(); // Mise à jour du plateau après le tour du joueur 2
                Console.WriteLine(grille.ToString()); // Affichage de l'état actuel du plateau
                Console.WriteLine();
            }
            // Affichage de la fin de la partie une fois le temps imparti écoulé
            Console.WriteLine("Fin de la partie ! (temps de la partie écoulé)");
            Console.WriteLine();
            Console.WriteLine("Appuyez sur une touche pour afficher les résultats");
            Console.ReadKey();
            Console.Clear();
            // Affichage du récapitulatif de la partie
            Console.WriteLine();
            Console.WriteLine("Récap de la partie :");
            Console.WriteLine();
            Console.Write("Vainqueur : ");
            joueur1.Vainqueur(joueur2); // Détermine et affiche le vainqueur de la partie
            Console.WriteLine();
            Console.WriteLine();
            // Affichage des résultats du joueur 1
            joueur1.AffichePrenom1(); // Affiche le prénom du joueur 1 avec une couleur spécifique
            Console.WriteLine(joueur1.toString()); // Affiche le score et les mots trouvés par le joueur 1
            Console.WriteLine();
            // Affichage des résultats du joueur 2
            joueur2.AffichePrenom2(); // Affiche le prénom du joueur 2 avec une couleur spécifique
            Console.WriteLine(joueur2.toString()); // Affiche le score et les mots trouvés par le joueur 2
            Console.WriteLine();
            Console.WriteLine();
            // Fin du programme
            Console.WriteLine("Appuyez sur une touche pour fermer");
            Console.ReadKey();
        }
    }
}