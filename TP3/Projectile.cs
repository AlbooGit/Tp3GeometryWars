using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
namespace TP3
{
  //Classe qui permet une instance d'un projectile dans le jeu. Hérite des propriétés de movable.
  public class Projectile : Movable
  {
    //Propriétés pour les projectiles
    public CharacterType Type { get; set; }
    private CharacterType type;
    private static Single projectileSpeed = 20.0f;
    private Vector2f position;
    /// <summary>
    /// Retourne quelle couleur sera le projectile selon le type de character.
    /// </summary>
    /// <param name="Type">Le type de character du character.</param>
    /// <returns>Couleur selon le type</returns>
    private Color GetColor(CharacterType Type)
    {
      if (Type == CharacterType.HERO)
        return Color.Blue;
      else
        return Color.Red;
    }
    /// <summary>
    /// Constructeur de la classe projectile. Permet d'instancier les valeurs des données importantes.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="posX"></param>
    /// <param name="posY"></param>
    /// <param name="nbVertices"></param>
    /// <param name="color"></param>
    /// <param name="angle"></param>
    public Projectile(CharacterType type, Single posX, Single posY, UInt32 nbVertices, Color color, Single angle)
      : base (posX, posY, nbVertices, color, projectileSpeed)
    {
      //Valeurs données importantes
      Type = type;
      color = GetColor(Type);
      Angle = angle;
      //Points pour dessiner le shape.
      base[0] = new Vector2f(0, 0);
      base[1] = new Vector2f(3, 0);
      base[2] = new Vector2f(3, 3);
      base[3] = new Vector2f(0, 3);
    }
    /// <summary>
    /// Permet au projectile d'updater sa position.
    /// </summary>
    /// <param name="DeltaT">Quantité de temps pour une frame.</param>
    /// <returns>booléen selon le résultat de l'update</returns>
    public override bool Update(float DeltaT)
    {
      Advance(projectileSpeed);
      return base.Update(DeltaT);
    }

  }
}
