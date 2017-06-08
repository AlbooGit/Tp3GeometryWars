using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TP3;

namespace TP3Test
{
  [TestClass]
  public class TestUnitaires
  {
    [TestMethod]
    public void TestMethod1()
    {
      //Test avec fichier de texte vide
      string file = ""; //texte vide
      ErrorCode code = StringTable.GetInstance().Parse(file); //Exécution de la méthode
      Assert.AreEqual(ErrorCode.BAD_FILE_FORMAT, code); //On vérifie que l'erreur mauvais format à bien été envoyé.
    }
    [TestMethod]
    public void TestMethod2()
    {
      //Test avec fichier de texte valide.
      string file = @"ID_TOTAL_TIME==>Temps total---Total time 
                     ID_LIFE ==> Vie-- - Life"; //texte valide (celui qu'on utilise).
      ErrorCode code = StringTable.GetInstance().Parse(file); //Exécution de la méthode.
      Assert.AreEqual(ErrorCode.OK, code); //On vérifie que 
    }

    [TestMethod]
    public void TestMethod3()
    {
      //Test contenu incorrect
      string file = @"ID_TOTAL_TIME>Temps total---Total time
                      ID_LIFE>Vie---Life"; //File avec contenu incorrect
      ErrorCode code = StringTable.GetInstance().Parse(file); //Exécution de la méthode.
      Assert.AreEqual(ErrorCode.BAD_FILE_FORMAT, code); //On vérifie que l'erreur mauvais format à bien été envoyé.
    }
    [TestMethod]
    public void TestMethod4()
    {
      //Test avec items manquants, on veut que l'erreur soit missing field
      string file = @"==>Temps total---Total time
                     ID_LIFE ==> -- - Life"; //File sans clé
      ErrorCode code = StringTable.GetInstance().Parse(file); //Exécution de la méthode.
      Assert.AreEqual(ErrorCode.MISSING_FIELD, code); //On vérifie que l'erreur missing field à bien été envoyé.
    }
    [TestMethod]
    public void TestMethod5()
    {
      //Test avec items manquants, on veut que l'erreur soit missing field
      string file = @"ID_TOTAL_TIME==>Temps totalTotal time 
                     ID_LIFE ==> VieLife"; //File sans tirets
      ErrorCode code = StringTable.GetInstance().Parse(file); //Exécution de la méthode.
      Assert.AreEqual(ErrorCode.MISSING_FIELD, code); //On vérifie que l'erreur missing field à bien été envoyé.
    }
    [TestMethod]
    public void TestMethod6()
    {
      //Test avec items manquants, on veut que l'erreur soit missing field
      string file = @"ID_TOTAL_TIMETemps total---Total time 
                     ID_LIFEVie-- - Life"; //File sans séparateurs
      ErrorCode code = StringTable.GetInstance().Parse(file); //Exécution de la méthode.
      Assert.AreEqual(ErrorCode.MISSING_FIELD, code); //On vérifie que l'erreur missing field à bien été envoyé.
    }


    //Tests GetValue
    [TestMethod]
    public void TestMethod7()
    {
      //Test avec un identifiant valide
      string language = "FR"; //Language valide
      string key = "ID_TOTAL_TIME"; //Une clé valide
      string testReturn = StringTable.GetValue(CurrentLanguage, "ID_TOTAL_TIME"); //Exécution de la méthode.
      Assert.AreEqual("Temps Total", testReturn); //On vérifie que le résultat est bel et bien correct.
    }
    [TestMethod]
    public void TestMethod8()
    {
      //Test avec une clé invalide
      string language = "FR"; //Language valide
      string key = "IDTOTALTIME"; //Clé invalide
      string testReturn = StringTable.GetValue(CurrentLanguage, "ID_TOTAL_TIME"); //Exécution de la méthode.
      Assert.AreNotEqual("Temps Total", testReturn); //On vérifie que le résultat de la méthode n'est pas pareille qu'une bonne exécution
    }
  }
}
