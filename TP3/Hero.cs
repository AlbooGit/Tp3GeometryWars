using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFML.Audio;
namespace TP3
{
  //La classe pour instancier un héro, le personnage contrôlé par le joueur. Hérite des fonctionalités d'un character.
  public class Hero : Character
  {
    //Propriétés de l'héro
    static private Hero instance = null;

    //public const int LIFE_AT_BEGINNING = 100; Normalement ceci serait la vie du personnage mais pour des raisons de test, il est changé pour la vie en dessous.
    public const int LIFE_AT_BEGINNING = 10000;
    private Color HeroColor = Color.Green;
    private int nbBombs;
    private Music soundBomb;

    public bool IsAlive
    {
      get { return Life < 0; }
    }

    public int NbBombs
    {
      get { return nbBombs; }
      set { nbBombs = value; }
    }

    public int Life
    {
      get; set;
    }

    static public float heroSpeed;
    /// <summary>
    /// Permet d'updater la position du joueur selon une touche.
    /// </summary>
    /// <param name="deltaT">Quantité de temps pour une frame.</param>
    /// <returns>Retourne un booléen</returns>
    public override bool Update(float deltaT)
    {
      if(deltaT >= 0)
      {
        if(Keyboard.IsKeyPressed(Keyboard.Key.W))
        {
          Advance(heroSpeed);
        }
        if (Keyboard.IsKeyPressed(Keyboard.Key.S))
        {
          Advance(-heroSpeed);
        }
        if (Keyboard.IsKeyPressed(Keyboard.Key.A))
        {
          RotateLeft();
        }
        if (Keyboard.IsKeyPressed(Keyboard.Key.D))
        {
          RotateRight();
        }
      }
      return false;
    }
    /// <summary>
    /// Permet de tourner à gauche.
    /// </summary>
    public void RotateLeft()
    {
      Rotate(-10);
    }
    /// <summary>
    /// Permet de tourner à droite.
    /// </summary>
    public void RotateRight()
    {
      Rotate(10);
    }
    /// <summary>
    /// Constructeur de la classe héro. Permet d'instancier les valeurs des données importantes.
    /// </summary>
    /// <param name="posX">position x de l'héro</param>
    /// <param name="posY">position y de l'héro</param>
    public Hero(Single posX, Single posY)
      : base(posX, posY, 3, Color.Green, heroSpeed)
    {
      //Points pour dessiner la forme de l'héro
      base[0] = new Vector2f(20, 0);
      base[1] = new Vector2f(0, -10);
      base[2] = new Vector2f(0, 10);
      //Ses propriétés
      heroSpeed = 10;
      Life = LIFE_AT_BEGINNING;
      NbBombs = 3;
    }
    /// <summary>
    /// Permet de créer une bombe qui efface les enemy à l'écran et joue un joli son assourdissant. 
    /// </summary>
    /// <param name="gw"></param>
    public void FireBomb(GW gw)
    {
      if (nbBombs > 0)
      {
        SoundBuffer sound;
        sound = new SoundBuffer("Data/Fire_smartbomb.wav");
        Sound fireBomb = new Sound();
        fireBomb.SoundBuffer = sound;
        fireBomb.Play();
        gw.AddBomb();
      }
    }

  }
}


