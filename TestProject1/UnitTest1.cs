using Projet_Algo_Poo;

namespace TestProject1
{
    public class Tests
    {

        [Test]
        public void TestMethodeAddScore()
        {
            Joueurs j = new Joueurs("Momo");
            j.Add_Score(10);
            Assert.AreEqual(j.score, 10);
        }

        [Test]
        public void TestMethodeAddMot()
        {
            List<string> MotsTest = new List<string>() { "Test" };
            Joueurs j1 = new Joueurs("Momo");
            j1.Add_Mot("Test");
            Assert.AreEqual(j1.MotsT1, MotsTest);
        }

        [Test]
        public void TestMethodetoString()
        {
            Joueurs j2 = new Joueurs("Momo");
            j2.Add_Score(10);
            j2.Add_Mot("Test");
            string b = ":\nScore : " + j2.score + "\nMots trouvés : \n-" + j2.MotsT1[0];
            Assert.AreEqual(j2.toString(), b);
        }

        [Test]
        public void TestMethodeContient()
        {
            Joueurs j = new Joueurs("Momo");
            j.Add_Mot("test");
            Assert.IsTrue(j.Contient("test"));
        }

    }
}