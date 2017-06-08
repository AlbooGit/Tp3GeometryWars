using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace TP3
{
  //Classe d'une étoile qui sera instancié et dessiné. Essentiellement le background.
  public class Star
  {
    //Propriétés pour la classe star
    private RectangleShape shape = null;
    public const int STAR_WIDTH = 4;
    public const int STAR_HEIGHT = 4;
    private Color[] color = new Color[] { Color.Cyan, Color.Green, Color.White, Color.Red, Color.Yellow, Color.Magenta };
    private int colorType;
    static private Random rnd = new Random();
    
    public Single Speed
    {
      get;
      set;
    }

    public Single PositionX
    {
      get;
      set;
    }

    public Single PositionY
    {
      get;
      set;
    }
    /// <summary>
    /// Constructeur de la classe star qui permet d'instancier les valeurs importantes.
    /// </summary>
    /// <param name="posX">position X de l'étoile</param>
    /// <param name="posY">position Y de l'étoile</param>
    /// <param name="speed">vitesse de l'étoile</param>
    public Star(Single posX, Single posY, Single speed)
    {
      //Valeurs importantes pour une Star
      shape = new RectangleShape(new Vector2f(STAR_WIDTH,STAR_HEIGHT));
      shape.Position = new Vector2f(posX,posY);
      Speed = speed;
      colorType = rnd.Next(0, color.Length);
      shape.FillColor = color[colorType];
    }
    /// <summary>
    /// Permet de dessiner la forme de l'étoile dans la fenêtre.
    /// </summary>
    /// <param name="window">La fenêtre de jeu.</param>
    public void Draw(RenderWindow window)
    {
      window.Draw(shape);
    }
    /// <summary>
    /// Permet d'updater les informations relatives à l'étoile. La position change selon la direction de l'héro.
    /// </summary>
    /// <param name="direction">Direction de l'héro.</param>
    /// <returns></returns>
    public bool Update(Vector2f direction)
    {
      shape.Position += direction;
      Respawn();
      PositionX = shape.Position.X;
      PositionY = shape.Position.Y;
      return true;
    }
    /// <summary>
    /// Permet aux étoiles de réapparaître s'ils disparaissent de l'écran.
    /// </summary>
    public void Respawn()
    {
      //Selon le bord qu'ils sortent, ils apparaissent à celui qui est opposé.
      if (PositionX < 0)
        shape.Position = new Vector2f(GW.WIDTH, PositionY); ;
      if (PositionX > GW.WIDTH)
        shape.Position = new Vector2f(0, PositionY);
      if (PositionY < 0)
       shape.Position = new Vector2f(PositionX, GW.HEIGHT); 
      if (PositionY > GW.HEIGHT)
       shape.Position = new Vector2f(PositionX, 0);
    }
  }
}
