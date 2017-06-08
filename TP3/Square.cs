using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;
namespace TP3
{
  //Classe qui sert pour instancier des enemies de type Square
  public class Square : Enemy
  {
    //Propriétés de la classe Square
    static private float basicEnemySpeed;
    static private Color enemyColor;
    private int squareSize = 20;
    /// <summary>
    /// Constructeur de la classe Square. Instancie les données importantes.
    /// </summary>    
    /// <param name="posX">position X de l'enemy</param>
    /// <param name="posY">position Y de l'enemy</param>
    /// <param name="speed">Vitesse de l'enemy</param>
    public Square(Single posX, Single posY, Single speed)
      : base(posX, posY, 4, enemyColor, basicEnemySpeed)
    {
      basicEnemySpeed = 2;
      enemyColor = Color.Magenta;

      base[0] = new Vector2f(0, 0);
      base[1] = new Vector2f(squareSize, 0);
      base[2] = new Vector2f(squareSize, squareSize);
      base[3] = new Vector2f(0, squareSize);
    }

    /// <summary>
    /// Fonction non utilisée.
    /// </summary>
    /// <param name="gw">instance de la classe GW.</param>
    public override void Explode(GW gw)
    {
      base.Explode(gw);
    }

  }
}
