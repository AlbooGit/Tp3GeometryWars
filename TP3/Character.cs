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
  //Class qui est supérieur à Héro et Enemy. Sert à effectuer des fonctionalités communes. Hérite de Movable.
  public class Character : Movable
  {
    //Propriétés de la classe Character.
    private Music firesound;
    private DateTime lastfire;
    protected Double fireDelay;

    public DateTime LastFire
    {
      get { return lastfire; }
      set { lastfire = value; }
    }

    public Double FireDelay
    {
      get { return fireDelay; }
    }

    public CharacterType Type
    {
      get;
      set;
    }

    /// <summary>
    /// Constructeur de la class Character. Permet d'instancier des informations importantes selon le Character.
    /// </summary>
    /// <param name="posX">position x du personnage</param>
    /// <param name="posY">position y du personnage</param>
    /// <param name="nbVertices">nombres de côtés dépendament de la forme</param>
    /// <param name="color">couleur du personnage</param>
    /// <param name="speed">vitesse du personnage</param>
    protected Character(Single posX, Single posY, UInt32 nbVertices, Color color, Single speed)
      : base(posX, posY, nbVertices, color, speed)
    {
      //Détermine quel type de personnage selon les vertices
      if (nbVertices == 3)
      {
        Type = CharacterType.HERO;
        fireDelay = 100;
      }
      else
      {
        Type = CharacterType.ENEMY;
        fireDelay = 500;
      }
      //initialiser le lastfire à 0.
      lastfire = DateTime.Now;
    }

    /// <summary>
    /// Permet aux différents characters de se déplacer dans le jeu. Empêchent ces personnages de sortir de la fenêtre.
    /// </summary>
    /// <param name="nbPixels"></param>
    protected override void Advance(Single nbPixels) //Pour empêcher les vaisseaux de sortir j'avais beaucoup trop de problèmes. Antony Langevin m'a aidé pour cela. 
    {
      base.Advance(nbPixels);
      float x = Math.Min(Math.Max(Position.X, this[1].X), GW.WIDTH - this[1].X);
      float y = Math.Min(Math.Max(Position.Y, this[1].X), GW.HEIGHT - this[1].X);
      Position = new Vector2f(x, y);
    }
    /// <summary>
    /// Permets aux personnages qui sont capables de de créer des projectiles d'en tirer avec un certain délai selon le personnage.
    /// </summary>
    /// <param name="gw">instance de la classe GW</param>
    /// <param name="deltaT">Le temps de une "frame".</param>
    public void Fire(GW gw, Single deltaT)
    {
      if (this is Hero) //Délai contrôlé dans GW.
      {
        gw.AddProjectile(new Projectile(Type, Position.X, Position.Y, 4, Color, Angle));
      }
      else if (this is Square) //Plus grand délai pour un square
      {
        if ((DateTime.Now - LastFire).TotalMilliseconds >= deltaT * 30000)
        {
          gw.AddProjectile(new Projectile(Type, Position.X, Position.Y, 4, Color, Angle));
          LastFire = DateTime.Now;
        }
      }
      else
      {
        if ((DateTime.Now - LastFire).TotalMilliseconds >= deltaT * 8000) //Plus petit délai pour un Circle
        {
          gw.AddProjectile(new Projectile(Type, Position.X, Position.Y, 4, Color, Angle));
          LastFire = DateTime.Now;
        }
      }
    }
    /// <summary>
    /// Vérifie si un movable contient un autre movable.
    /// </summary>
    /// <param name="m">instance du movable qu'on veut vérifier.</param>
    /// <returns>Retourne un true s'il est contenu.</returns>
    public bool Contains(Movable m)
    {
      return false;
    }


  }
}
