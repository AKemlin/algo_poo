using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Algo_Poo
{
    public class Joueurs
    {
        string Prenom;
        string Nom;
        string[] MotsT;
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

    }
}
