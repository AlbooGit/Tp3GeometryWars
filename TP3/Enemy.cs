using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
namespace TP3
{
  //Permet aux enemies d'hériter de fonctionalités redondantes. Hérite des fonctionalités de Character.
  public class Enemy : Character
  {
    //Propriétés de Enemy
    private Random rnd = new Random();
    static bool IsSpawning
    {
      get { return isSpawning; }
    }
    static private bool isSpawning;
    /// <summary>
    /// Constructeur de la classe enemy. Instancie valeurs des données. 
    /// </summary>
    /// <param name="posX">position x enemy</param>
    /// <param name="posY">position y enemy</param>
    /// <param name="nbVertices">nombre côtés selont shape enemy</param>
    /// <param name="color">couleur de l'enemy</param>
    /// <param name="speed">vitesse de l'enemy</param>
    public Enemy(Single posX, Single posY, UInt32 nbVertices, Color color, Single speed)
       : base(posX, posY, nbVertices, color, speed)
    {
      isSpawning = true;

    }
    /// <summary>
    /// Méthode non utilisée
    /// </summary>
    /// <param name="gw">instance de GW</param>
    public virtual void Explode(GW gw)
    {

    }
    /// <summary>
    /// Permet d'updater la position des différents enemies, effectuer des tirs et gère les "AI". 
    /// </summary>
    /// <param name="gw">instance de GW</param>
    /// <param name="DeltaT">temps pour une frame</param>
    /// <returns>retourne un booléen pour si l'update est fait</returns>
    public virtual bool Update(GW gw, float DeltaT)
    {
      if (this is Square) //tourne autour de la position du héro et tir dessus.
      {
        float angle = (float)(Math.Atan2(gw.Hero.Position.Y - Position.Y, gw.Hero.Position.X - Position.X) * 180.0 / Math.PI);
        angle += 100;
        Angle = angle;
        Advance(Speed);
        angle = (float)(Math.Atan2(gw.Hero.Position.Y - Position.Y, gw.Hero.Position.X - Position.X) * 180.0 / Math.PI);
        Angle = angle;
        Fire(gw, DeltaT);
      }
      //Grêle le héro de manière immobile
      else if (this is Circle)
      {
        float angle = (float)(Math.Atan2(gw.Hero.Position.Y - Position.Y, gw.Hero.Position.X - Position.X) * 180.0 / Math.PI);
        Angle = angle;
        Fire(gw, DeltaT);
      }
      else
      {
        //Fonce sur le héro
        float angle = (float)(Math.Atan2(gw.Hero.Position.Y - Position.Y, gw.Hero.Position.X - Position.X) * 180.0 / Math.PI);
        Angle = angle;
        Advance(Speed);
      }
      return base.Update(DeltaT);
    }
  }
}
