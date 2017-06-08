using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;

namespace TP3
{
  //Classe qui permet aux sous classes d'hériter plusieurs fonctionalités associés à une entitée qui bouge.
  public class Movable : Drawable
  {
    public override float Angle
    {
      get
      {
        return base.Angle;
      }

      set
      {
        base.Angle = value;
        Direction = new Vector2f((float)Math.Cos(Math.PI * Angle / 180.0f), (float)Math.Sin(Math.PI * Angle / 180.0f));
      }
    }
    public float Size { get { return Math.Max(BoundingBox.Height, BoundingBox.Width); } }


    public Vector2f Direction { get; set; }
    public virtual bool IsAlive { get; protected set; }

    public float Speed { get; set; }
    /// <summary>
    /// Constructeur de la classe movable. Instancie les valeurs importantes des données.
    /// </summary>
    /// <param name="posX">position X d'un movable</param>
    /// <param name="posY">position Y d'un movable</param>
    /// <param name="nbVertices">nombre de côtés du shape d'un movable</param>
    /// <param name="color">couleur d'un movable</param>
    /// <param name="speed">son speed</param>
    public Movable(float posX, float posY, uint nbVertices, Color color, float speed)
      : base(posX, posY, nbVertices, color)
    {
      Angle = 0;
      IsAlive = true;
      Speed = speed;
    }
    /// <summary>
    /// Permet à l'angle de rotationner selon le degré indiqué.
    /// </summary>
    /// <param name="angleInDegrees">degrés pour la rotation</param>
    protected void Rotate(float angleInDegrees)
    {
      Angle = Angle + angleInDegrees;
    }
    /// <summary>
    /// Permet à un movable d'avancer selon une direction et une quantitée de pixels.
    /// </summary>
    /// <param name="nbPixels"> Quantité de pixels pour bouger. </param>
    protected virtual void Advance(float nbPixels)
    {
      Position = Position + Direction * nbPixels;
    }
    /// <summary>
    /// Permet à un movable de s'updater.
    /// </summary>
    /// <param name="DeltaT">Temps pour une frame.</param>
    /// <returns></returns>
    public virtual bool Update(float DeltaT)
    {
      return true; 
    }
  }
}
