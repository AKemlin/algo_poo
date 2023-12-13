using Projet_Algo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Projet_Algo_Poo
{
    public class Jeu
    {

        /*static void Main(string[] args) {
            List<string> lignes = new List<string> { };
            Dictionnaire dico = new Dictionnaire(lignes);
        }*/

        static void Main(string[] args)
        {
            //Joueurs amaury = new Joueurs("Amaury");
            //amaury.Add_Mot("Chaussette");
            //amaury.Add_Score(15);
            /*Console.WriteLine("Voulez vous connaitre notre histoire ? (oui/non)");
            string answer3 = Console.ReadLine();
            while (answer3 != "oui" && answer3 != "non")
            {
                Console.WriteLine("Etes vous sûr ? (oui/non)");
                answer3 = Console.ReadLine();
            }
            if (answer3 == "oui")
            {
                Console.WriteLine("Il était une fois deux amis passionnés de programmation, Paul et Amaury. Ils partageaient un amour particulier pour les jeux de mots et les défis algorithmiques. Un jour, en jouant au Scrabble, une idée leur vint : créer un jeu similaire, mais qui serait informatisé et aurait ses propres règles uniques.");
                Console.WriteLine("Paul, expert en logique et structuration de données, prit en charge le développement du cœur du jeu en C#. Amaury, avec son esprit créatif et sa compréhension approfondie des algorithmes, proposa des idées innovantes pour rendre le jeu plus stimulant et engageant. Ils commencèrent par définir les règles de leur jeu. ");
                Console.WriteLine("Tout comme le Scrabble, il s'agirait de former des mots sur un plateau de jeu, mais avec des twists algorithmiques. Par exemple, certains mots déclencheraient des événements spéciaux ou rapporteraient des points bonus selon leur complexité algorithmique.");
                Console.WriteLine("Ensuite, Amaury se concentra sur la programmation des fonctionnalités de base. Il utilisa des structures de données complexes pour gérer le dictionnaire des mots et vérifier la validité des mots formés. Il implémenta également un système de points sophistiqué qui récompenserait les joueurs pour l'utilisation de concepts algorithmiques avancés.");
                Console.WriteLine("Pendant ce temps, Paul travaillait sur l'interface utilisateur. Il conçut un plateau de jeu clair et agréable à l'œil, avec des animations fluides pour les mouvements des lettres et les événements spéciaux. Il veilla aussi à ce que le jeu soit accessible et facile à comprendre pour les joueurs de tous niveaux.");
                Console.WriteLine("Après plusieurs semaine de travail acharné, le jeu, qu'ils nommèrent \"AlgoScrabble\", était prêt. Ils le testèrent avec leurs amis et furent ravis de voir que leur création était non seulement amusante, mais aussi éducative. Les joueurs pouvaient améliorer leur vocabulaire tout en apprenant des concepts d'algorithmique.");
                Console.WriteLine("Le jeu rencontra un succès inattendu dans la communauté des développeurs et des amateurs de jeux de mots. Paul et Amaury parlèrent de leur expérience unique dans la création d'un jeu combinant programmation et divertissement. AlgoScrabble devint un exemple inspirant de la manière dont la passion pour la programmation et le jeu peut mener à des créations innovantes et enrichissantes.");
            }*/
            Plateau grille;
            List<string> lignes = new List<string> { };
            Dictionnaire dico = new Dictionnaire(lignes);
            dico.tout();
            Console.WriteLine(" ");
            Console.WriteLine("Nom du joueur 1 ? ");
            Joueurs joueur1 = new Joueurs(Console.ReadLine());
            Console.WriteLine("Nom du joueur 2 ? ");
            Joueurs joueur2 = new Joueurs(Console.ReadLine());
            Console.WriteLine("Combien de temps voulez vous que le tour dure ?");
            TimeSpan tempspartour = new TimeSpan(0,0,Convert.ToInt32(Console.ReadLine()));
            Console.WriteLine("Combien de temps voulez vous que dure la partie ?");
            TimeSpan TempsPartie = new TimeSpan(0, 0, Convert.ToInt32(Console.ReadLine()));
            Console.Write("Comment Construire le plateau ? ");
            Console.WriteLine(" (Entrer le numéro correspondant)");
            Console.WriteLine("1 : Aléatoirement");
            Console.WriteLine("2 : Grâce au fichier .csv");
            string MéthodeConstru = (Console.ReadLine());
            string cheminSave = "../../../Sauvegarde.csv";
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
                string chemin = "../../../Lettre.txt";
                Console.WriteLine("Quelle dimension du plateau ?");
                int cote = int.Parse(Console.ReadLine());
                string[][] matrice = new string[cote][];
                grille = new Plateau(matrice, cote);
                grille.ToReadRandom(chemin);
                Console.WriteLine(grille.ToString());
                Console.WriteLine("Voulez-vous sauvegarder le plateau dans un fichier exterieur ? (oui ou non)");
                string Save = Console.ReadLine().ToLower();
                if (Save == "oui")
                {
                    grille.ToFile(cheminSave);
                }
            }
            else
            {
                Console.WriteLine();
                string chemin = "../../../Test1.csv";
                int cote = File.ReadAllLines(chemin).Length;
                string[][] matrice = new string[cote][];
                grille = new Plateau(matrice, cote);
                grille.ToRead(chemin);
                Console.WriteLine(grille.ToString());
            }


            DateTime date3 = DateTime.Now;
            while (DateTime.Now - date3 < TempsPartie)
            {
                DateTime date1 = DateTime.Now;
                while (DateTime.Now - date1 < tempspartour)
                {

                    Console.WriteLine(joueur1.AppelationP + " veuillez rentrez votre mot : ");
                    string mot1 = Console.ReadLine();
                    mot1.ToUpper();
                    
                    bool dicto = dico.rechercheDichotomiqueRecursif(mot1, lignes.Count - 1, 0);
                    while (dicto == false)
                    {
                        Console.WriteLine($"dicto = {dicto}");
                        Console.WriteLine("Le mot entrer n'est pas dans le dictionnaire ! ");
                        Console.WriteLine(joueur1.AppelationP + " veuillez rentrez votre mot : ");
                        mot1 = Console.ReadLine();
                        mot1.ToUpper();
                        
                        dicto = dico.rechercheDichotomiqueRecursif(mot1, lignes.Count-1, 0);
                        
                    }
                    
                    (bool presence, grille.Matrice) = grille.Recherche_Mot(mot1);
                    if (presence == true)
                    {
                        Console.WriteLine("Le mot est dans le plateau");
                        grille.Maj_Plateau();
                        grille.Maj_Plateau();
                        grille.Maj_Plateau();
                        grille.Maj_Plateau();
                        grille.Maj_Plateau();
                        grille.Maj_Plateau();
                        grille.Maj_Plateau();
                        grille.Maj_Plateau();
                        Console.WriteLine(grille.ToString());

                        // lire fichier lettre.txt et ajouter le score du mot au joueur
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

                        if (vrai1 == false)
                        {
                            joueur1.Add_Mot(mot1);
                            joueur1.Add_Score(score);
                        }
                        break;
                    }

                    else if (presence == false)
                    {
                        Console.WriteLine("Le mot n'est pas dans le plateau");
                    }
                }
                Console.WriteLine("Fin du tour de " + joueur1.AppelationP);



                DateTime date2 = DateTime.Now;
                
                while (DateTime.Now - date2 < tempspartour)
                {
                    Console.WriteLine(joueur2.AppelationP + " veuillez rentrez votre mot : ");
                    string mot2 = Console.ReadLine();
                    mot2.ToUpper();
                    /*
                    bool dicto = dico.RechDichoRecursif(mot1);
                    while (dicto == false)
                    {
                        Console.WriteLine("Le mot entrer n'est pas dans le dictionnaire ! ");
                        Console.WriteLine(joueur1.AppelationP + " veuillez rentrez votre mot : ");
                        mot1 = Console.ReadLine();
                        dicto = dico.RechDichoRecursif(mot1);
                    }
                    */
                    (bool presence, grille.Matrice) = grille.Recherche_Mot(mot2);
                    if (presence == true)
                    {
                        Console.WriteLine("Le mot est dans le plateau");
                        grille.Maj_Plateau();
                        grille.Maj_Plateau();
                        grille.Maj_Plateau();
                        grille.Maj_Plateau();
                        grille.Maj_Plateau();
                        grille.Maj_Plateau();
                        grille.Maj_Plateau();
                        grille.Maj_Plateau();
                        Console.WriteLine(grille.ToString());

                        // lire fichier lettre.txt et ajouter le score du mot au joueur
                        string cheminscore = "../../../Lettre.txt";
                        string[] ligneslettres = File.ReadAllLines(cheminscore);
                        string[][] transition = new string[ligneslettres.Length][];
                        string[][] informations = new string[ligneslettres.Length][];

                        for (int i = 0; i < ligneslettres.Length; i++)
                        {
                            transition[i] = ligneslettres[i].Split(';');
                            informations[i] = transition[i][0].Split(',');
                        }

                        string motmaj = mot2.ToUpper();
                        char[] lettres = motmaj.ToCharArray();
                        int score = 0;

                        for (int i = 0; i < lettres.Length; i++)
                        {
                            int ligneinfo = lettres[i] - 'A';
                            score += Convert.ToInt32(informations[ligneinfo][2]);
                        }

                        bool vrai2 = joueur2.Contient(mot2);

                        if (vrai2 == false)
                        {
                            joueur2.Add_Mot(mot2);
                            joueur2.Add_Score(score);
                        }
                        break;
                    }

                    else if (presence == false)
                    {
                        Console.WriteLine("Le mot n'est pas dans le plateau");
                    }
                }
                Console.WriteLine("Fin du tour de "+joueur2.AppelationP);
                
            }
            Console.WriteLine();
            Console.WriteLine("Fin de la partie!");
            Console.WriteLine(joueur1.toString());
            Console.WriteLine();
            Console.WriteLine(joueur2.toString());
            Console.ReadKey();
        }
        
    }
}