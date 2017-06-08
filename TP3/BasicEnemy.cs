using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Audio;
using SFML.System;
namespace TP3
{
  //Classe servant à instancier des BasicEnemy dans le jeu. Hérite d'Enemy pour les fonctionalités.
  public class BasicEnemy : Enemy
  {
    //Propriétés de la classe BasicEnemy
    static private float basicEnemySpeed;
    static private Color enemyColor;
    private Single angleToHero;
    private Single AngleToHero
    {
      get { return angleToHero; }
      set { angleToHero = value; }
    }

    /// <summary>
    /// Constructeur de la classe BasicEnemy. Initialise toutes les données importantes.
    /// </summary>
    /// <param name="posX"></param>
    /// <param name="posY"></param>
    /// <param name="angle"></param>
    public BasicEnemy(Single posX, Single posY, Single angle)
      : base(posX, posY, 4, enemyColor, basicEnemySpeed)
    {
      //initialise les données de base importantes.
      basicEnemySpeed = 3;
      enemyColor = Color.Red;
      angle = AngleToHero;
      //Mettre les points pour la forme du basic enemy.
      base[0] = new Vector2f(20, 0);
      base[1] = new Vector2f(0, -10);
      base[2] = new Vector2f(-20, 0);
      base[3] = new Vector2f(0, 10);
    }

    /// <summary>
    ///Fonctionalité non-utilisée.
    /// </summary>
    /// <param name="gw"></param>
    public override void Explode(GW gw)
    {
      base.Explode(gw);
    }

  }
}
