using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Projet_Algo_Poo
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void TestMethod1()
        {
            public void LectureDico()
            {
                string CheminFichier = "..//..//..//Mots_Français.txt"; // Chemin vers le fichier de dictionnaire
                string[] lignesdico = File.ReadAllLines(CheminFichier); // Lecture de toutes les lignes du fichier
                for (int i = 0; i < lignesdico.Length; i++)
                {
                    this.lignes[i] = lignesdico[i].Split(' '); // Séparation des mots dans chaque ligne
                }
            }
        }

    }
}
