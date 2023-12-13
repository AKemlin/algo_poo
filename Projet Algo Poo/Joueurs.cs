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
        List<string> MotsT = new List<string>();
        int Score;

        public Joueurs(string Prenom, int Score=0)
        {
            this.Prenom = Prenom;
            this.MotsT = new List<string>();
            this.Score = 0;
        }
        public string AppelationP
        {
            get { return this.Prenom; }
            set { this.Prenom = value; }
        }
        public List<string> MotsT1
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
                MotsT.Add(mot); //C'est l'addition pour les tableaux
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
            string a = "Le Prénom est : " + this.Prenom + "\nLe Score est de : " + this.Score + "\nLe tableau des mots trouvés est : ";
            for (int i = 0; i < this.MotsT.Count; i++)
            {
                a += "\n-" + this.MotsT[i];
            }
            return a;
        }
        public void Add_Score(int val)
        {
            this.Score += val; //penser a definir val qui seras la valeur de chaque MotsT donc les mots trouvees par le joueur
        }
    }
}