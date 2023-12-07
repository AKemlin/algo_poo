Main()
{
    Dictionnaire dico = new Dictionnaire("C:\\Users\\kemli\\Documents\\C#\\Projet Algo Poo\\Projet Algo Poo\\Fichier Bonus\\MotFrançais.txt");
    bool trouve = dico.RechDichoRecursif("EXEMPLE");
    Console.WriteLine(trouve ? "Mot trouvé" : "Mot non trouvé");
}