using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP3
{
  public class StringTable
  {
    //Propriétés de StringTable
    private Language currentLanguage;
    private Dictionary<string, string> EN = new Dictionary<string, string> { };
    private Dictionary<string, string> FR = new Dictionary<string, string> { };
    
    public Language CurrentLanguage
    {
      get { return currentLanguage; }
      set { currentLanguage = value; }
    }

    /// <summary>
    /// Permet de retourner la valeur selon le language désiré et qu'est ce qu'on veut avoir.
    /// </summary>
    /// <param name="CurrentLanguage"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public string GetValue(Language CurrentLanguage, string value)
    {






      return "0";
    }

    /// <summary>
    /// Retourne l'instance de la classe.
    /// </summary>
    /// <returns>Retourne une instance.</returns>
    public StringTable GetInstance()
    {
      return this;
    }
  }
}
