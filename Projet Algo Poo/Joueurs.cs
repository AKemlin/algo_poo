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
    {   //creation de chaque attribut de la class joueurs
        string Prenom;
        string[] MotsT = new string[1];
        int Score;

        public Joueurs(string Prenom, string[] MotsT=null, int Score=0)
        {
            this.Prenom = Prenom;
            this.MotsT = MotsT;
            this.Score = 0;
        }
        public string AppelationP
        {
            get { return this.Prenom; }
            set { this.Prenom = value; }
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
                MotsT.Append(mot); //C'est l'addition pour les tableaux
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
            return "Le Prénom est : " + this.Prenom + "\nLe Score est de : " + this.Score + "\nLe tableau des mots trouvés est : " + this.MotsT;
        }
        public void Add_Score(int val)
        {
            this.Score += val; //penser a definir val qui seras la valeur de chaque MotsT donc les mots trouvees par le joueur
        }
    }
}