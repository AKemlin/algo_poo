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

        public int score
        {
            get { return Score; }
            set { this.Score = value; }
        }

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
            string a =":\nScore : " + this.Score + "\nMots trouvés : ";
            for (int i = 0; i < this.MotsT.Count; i++)
            {
                a += "\n-" + this.MotsT[i];
            }
            return a;
        }

        public void AffichePrenom1()
        {
            
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(this.Prenom);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void AffichePrenom2()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(this.Prenom);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public void Add_Score(int val) // Ajoute le score du mot au score total du joueur
        {
            this.Score += val; //penser a definir val qui seras la valeur de chaque MotsT donc les mots trouvees par le joueur
        }

        public void Vainqueur(Joueurs joueur2)
        {
            if (this.score > joueur2.score)
            {
                this.AffichePrenom1();
            }
            else if (this.score < joueur2.score)
            {
                joueur2.AffichePrenom2();
            }
            else if (this.score < joueur2.score)
            {
                Console.WriteLine("Egalité !");
            }
        }
    }
}