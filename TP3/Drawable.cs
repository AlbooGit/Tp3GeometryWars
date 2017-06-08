using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;

namespace TP3
{
  //Super classe pour tout ce qui est dessiné sauf les étoiles.
  public abstract class Drawable
  {
    ConvexShape shape = null;
    public Vector2f Position { get; set;}
    public Color Color { get { return shape.FillColor; } set { shape.FillColor = value; } }
    public virtual float Angle
    {
      get { return shape.Rotation; }
      set { shape.Rotation =value ; }
    }
    /// <summary>
    /// Constructeur de la super classe. Contient les fonctionalités communes à tous les sous classes.
    /// </summary>
    /// <param name="posX">position x du drawable</param>
    /// <param name="posY">position y du drawable</param>
    /// <param name="nbVertices">nombre côtés du drawable</param>
    /// <param name="color">couleur du drawable</param>
    protected Drawable(float posX, float posY, uint nbVertices, Color color)
    {
      Position = new Vector2f(posX, posY);
      shape = new ConvexShape(nbVertices);
      shape.FillColor = color;
      Angle = 0;
    }
    
    public Vector2f this[uint index]
    {
      get { return shape.GetPoint(index); }
      set { shape.SetPoint(index, value); }
    }
    /// <summary>
    /// Dessine le shape sur la fenêtre du jeu.
    /// </summary>
    /// <param name="window"> fenêtre du jeu</param>
    public virtual void Draw(RenderWindow window)
    {
      shape.Position = Position;      
      window.Draw(shape);
    }

    /// <summary>
    /// Retourne la boîte englobante associée à la forme.  Utilisée pour les collisions.
    /// </summary>
    public FloatRect BoundingBox
    {
      get { return shape.GetGlobalBounds(); }
    }

    /// <summary>
    /// Vérifie si deux éléments affichés s'entrecoupent
    /// </summary>
    /// <param name="m">Le second élément avec lequel vérifier la collision</param>
    /// <returns>true s'il y a collision, false sinon.</returns>
    public bool Intersects(Drawable m)
    {
      FloatRect r = m.BoundingBox;
      r.Left = m.Position.X;
      r.Top = m.Position.Y;
      return BoundingBox.Intersects(r);
    }

    /// <summary>
    /// Vérifie si l'objet contient le point où se trouve l'élément reçu en paramètre
    /// </summary>
    /// <param name="m">L'élément dont il faut vérifier la position</param>
    /// <returns>true si la boîte englobante de l'objet courant contient le point (la position) où se
    /// trouve l'objet reçu en paramètre</returns>
    public bool Contains(Drawable m)
    {
      return BoundingBox.Contains(m.Position.X, m.Position.Y);
    }
  }
}
