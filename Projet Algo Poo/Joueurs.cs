System;
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
    {       //creation de chaque attribut de la class joueurs
        string Prenom;
        string Nom;
        string[] MotsT = new string[1];
        int Score;
        DateTime Timer;

        public Joueurs(string Prenom, string Nom, string[] MotsT, int Score, DateTime Timer)
        {
            this.Prenom = Prenom;
            this.Nom = Nom;
            this.MotsT = MotsT;
            this.Score = 0;
            this.Timer = Timer;
        }
        public string AppelationP
        {
            get { return this.Prenom; }
            set { this.Prenom = value; }
        }
        public string AppelationN
        {
            get { return this.Nom; }
            set { this.Nom = value; }
        }
        public string[] MotsT1
        {
            get { return this.MotsT; }
            set { this.MotsT = value; }
        }
        string MotARajouter = "exemple";// Cree un mot a rajouter pour changer cette variable
        public void Add_Mot(string mot)
        {
            // Ajoute le mot à la liste s'il n'est pas déjà présent
            if (!Contient(mot))
            {
                MotsT.Append(mot); //C'est l'addition pour les tableaus
            }
        }
        public bool Contient(string mot)
        {
            bool MotTrouver = false; // Réinitialise à chaque appel
            foreach (var tabMot in MotsT)
            {
                if (tabMot == mot) // Comparaison avec le mot passé en argument
                {
                    MotTrouver = true;
                    break; // Arrête la boucle si le mot est trouvé
                }
            }
            return MotTrouver;
        }

        public string toString()
        {
            return "Le Prénom est : " + this.Prenom + "\nLe Nom est : " + this.Nom + "\nLe Score est de : " + this.Score + "\nLe tableau des mots trouvés est : " + this.MotsT;
        }
        public void Add_Score(int val)
        {
            this.Score += val; //penser a definir val qui seras la valeur de chaque MotsT donc les mots trouvees par le joueur
        }
        /*
        #region Timers
        public class GameTimer
        {
            public Timer timer;
            public GameTimer()
            {
                // Création du timer pour 3 heures et 30 minutes
                timer = new Timer(12600000); // Temps en millisecondes (3h30min = 3*60*60*1000 + 30*60*1000)
                timer.AutoReset = false; // Pour que le timer ne se répète pas
            }
            public void Start()
            {
                timer.Start(); // Démarrage du timer
            }
            public void tempsMax()
            {
                Console.WriteLine("Stop, le joueur a dépassé le temps !");
                // Ajoutez ici toute autre logique nécessaire après l'expiration du temps
            }
        }
        #endregion Timers
        */
    }
}