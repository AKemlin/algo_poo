using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using static System.Formats.Asn1.AsnWriter;

namespace Projet_Algo_Poo
{
    public class Joueurs
    {
        // Déclaration des attributs de la classe Joueurs
        string Prenom; // Stocke le prénom du joueur
        List<string> MotsT = new List<string>(); // Liste pour stocker les mots trouvés par le joueur
        int Score; // Score du joueur
        // Constructeur de la classe
        public Joueurs(string Prenom, int Score = 0)
        {
            this.Prenom = Prenom; // Initialisation du prénom
            this.MotsT = new List<string>(); // Initialisation de la liste des mots trouvés
            this.Score = 0; // Initialisation du score
        }
        // Propriété pour accéder et modifier le prénom
        public string AppelationP
        {
            get { return this.Prenom; }
            set { this.Prenom = value; }
        }
        // Propriété pour accéder et modifier la liste des mots trouvés
        public List<string> MotsT1
        {
            get { return this.MotsT; }
            set { this.MotsT = value; }
        }
        // Propriété pour accéder et modifier le score
        public int score
        {
            get { return Score; }
            set { this.Score = value; }
        }
        // Méthode pour ajouter un mot à la liste des mots trouvés
        public void Add_Mot(string mot)
        {
            // Ajoute le mot à la liste s'il n'est pas déjà présent
            if (!Contient(mot))
            {
                MotsT.Add(mot); // Ajout du mot à la liste
            }
        }
        // Méthode pour vérifier si un mot est déjà dans la liste des mots trouvés
        public bool Contient(string mot)
        {
            bool MotTrouver = false; // Initialise la variable de contrôle
            foreach (var tabMot in MotsT)
            {
                if (tabMot == mot) // Vérifie si le mot est déjà présent
                {
                    MotTrouver = true;
                    break; // Arrête la boucle si le mot est trouvé
                }
            }
            return MotTrouver; // Retourne le résultat de la recherche
        }
        // Méthode pour convertir les informations du joueur en chaîne de caractères
        public string toString()
        {
            string a = ":\nScore : " + this.Score + "\nMots trouvés : ";
            for (int i = 0; i < this.MotsT.Count; i++)
            {
                a += "\n-" + this.MotsT[i]; // Ajoute chaque mot trouvé à la chaîne
            }
            return a; // Retourne la chaîne de caractères complète
        }
        // Méthode pour afficher le prénom avec une couleur spécifique
        public void AffichePrenom1()
        {
            Console.ForegroundColor = ConsoleColor.Blue; // Change la couleur en bleu
            Console.Write(this.Prenom); // Affiche le prénom
            Console.ForegroundColor = ConsoleColor.White; // Réinitialise la couleur
        }
        // Méthode pour afficher le prénom avec une autre couleur
        public void AffichePrenom2()
        {
            Console.ForegroundColor = ConsoleColor.Red; // Change la couleur en rouge
            Console.Write(this.Prenom); // Affiche le prénom
            Console.ForegroundColor = ConsoleColor.White; // Réinitialise la couleur
        }
        // Méthode pour ajouter des points au score
        public void Add_Score(int val)
        {
            this.Score += val; // Ajoute la valeur spécifiée au score actuel
        }
        // Méthode pour déterminer et afficher le vainqueur entre deux joueurs
        public void Vainqueur(Joueurs joueur2)
        {
            if (this.score > joueur2.score)
            {
                this.AffichePrenom1(); // Affiche le prénom du joueur actuel si son score est plus élevé
            }
            else if (this.score < joueur2.score)
            {
                joueur2.AffichePrenom2(); // Affiche le prénom du joueur2 si son score est plus élevé
            }
            else
            {
                Console.WriteLine("Egalité !"); // Affiche un message d'égalité si les scores sont identiques
            }
        }
    }
}