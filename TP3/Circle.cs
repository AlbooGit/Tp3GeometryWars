using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
namespace TP3
{
  public class Circle : Enemy
  {
    //Propriétés
    static private float basicEnemySpeed;
    static private Color enemyColor;
    private Single angleToHero;
    static private UInt32 nbSides;
    
    private Single AngleToHero
    {
      get { return angleToHero; }
      set { angleToHero = value; }
    }

    public UInt32 NbSides
    {
      get { return nbSides; }
      set { nbSides = value; }
    }
   
    /// <summary>
    /// Constructeur de la classe Circle. Permet d'instancier 
    /// </summary>
    /// <param name="posX"></param>
    /// <param name="posY"></param>
    /// <param name="speed"></param>
    public Circle(Single posX, Single posY, Single speed)
      : base(posX, posY, 10, enemyColor, basicEnemySpeed)
    {
      //Données importante pour le cercle
      basicEnemySpeed = speed;
      enemyColor = Color.Cyan;
      NbSides = 10;
      float radius = 25;

      //Set les points avec formule fourni 
      for (int i = 0; i < NbSides; i++)
      {
        float x = radius * (float)Math.Cos((2 * Math.PI / NbSides) * i);
        float y = radius * (float)Math.Sin((2 * Math.PI / NbSides) * i);
        this[(uint)i] = new Vector2f(x, y);
      }

    }
    
    public override void Explode(GW gw)
    {
      base.Explode(gw);
    }
  }
}
