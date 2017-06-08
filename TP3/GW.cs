using System;
using System.IO;
using SFML;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFML.Audio;
using System.Collections.Generic;
namespace TP3
{
  public class GW
  {
    // Constantes et propriétés statiques
    public const int WIDTH = 1024;
    public const int HEIGHT = 768;
    public const uint FRAME_LIMIT = 60;
    const float DELTA_T = 1.0f / (float)FRAME_LIMIT;
    private static Random r = new Random();

    // SFML
    RenderWindow window = null;
    Font font = new Font("Data/emulogic.ttf");
    Text text = null;

    // Propriétés pour la partie
    private Hero hero;

    public Hero Hero
    {
      get { return hero; }
    }

    float totalTime = 0;
    private List<Character> characters = new List<Character> { };
    private List<Projectile> projectiles = new List<Projectile> { };
    private List<Projectile> projectilesToAdd = new List<Projectile> { };
    private List<Projectile> projectilesToRemove = new List<Projectile> { };
    private List<Star> stars = new List<Star> { };
    private List<Enemy> enemiesToAdd = new List<Enemy> { };
    private List<Enemy> enemiesToRemove = new List<Enemy> { };
    private List<Enemy> enemies = new List<Enemy> { };
    // Il en manque BEAUCOUP

    /// <summary>
    /// Permet d'effectuer la fermeture du jeu.
    /// </summary>
    /// <param name="sender">?</param>
    /// <param name="e">?</param>
    private void OnClose(object sender, EventArgs e)
    {
      RenderWindow window = (RenderWindow)sender;
      window.Close();
    }
    /// <summary>
    /// Permet d'envoyer les touches appuyées. 
    /// </summary>
    /// <param name="sender">oui</param>
    /// <param name="e">touche appuyée?</param>
    private void OnKeyPressed(object sender, KeyEventArgs e)
    {

    }
    /// <summary>
    /// Constructeur de la classe GW. Crée qu'un héro et crée plusieurs étoiles avec des positions aux hasard.
    /// </summary>
    public GW()
    {
      text = new Text("", font);
      window = new RenderWindow(new SFML.Window.VideoMode(WIDTH, HEIGHT), "GW");
      window.Closed += new EventHandler(OnClose);
      window.KeyPressed += new EventHandler<KeyEventArgs>(OnKeyPressed);
      window.SetKeyRepeatEnabled(false);
      window.SetFramerateLimit(FRAME_LIMIT);
      //Le héro
      hero = new Hero(WIDTH / 2, HEIGHT / 2);
      characters.Add(hero);

      //Étoiles
      for (int i = 0; i < 100; i++)
      {
        float positionX = r.Next(0, WIDTH);
        float positionY = r.Next(0, HEIGHT);
        stars.Add(new TP3.Star(positionX, positionY, Hero.heroSpeed));
      }
    }

    /// <summary>
    /// Permet au jeu de fonctionner. S'arrête si l'héro meurt. Joue un joli son de game over. Gère le jeu en entier.
    /// </summary>
    public void Run()
    {
      // ppoulin
      // Chargement de la StringTable. A décommenter au moment opportun
      //if( ErrorCode.OK == StringTable.GetInstance().Parse(File.ReadAllText("Data/st.txt")) )
      {
        window.SetActive();

        while (window.IsOpen)
        {
          window.Clear(Color.Black);
          window.DispatchEvents();
          if (false == Update())
          {

            SoundBuffer sound;
            sound = new SoundBuffer("Data/Game_over.wav");
            Sound gameOver = new Sound();
            gameOver.SoundBuffer = sound;
            gameOver.Play();

            break;
          }
          Draw();
          window.Display();
        }
      }
    }
    /// <summary>
    /// Enlève un certain enemy de la partie.
    /// </summary>
    /// <param name="e">l'enemy à enlever.</param>
    public void RemoveEnemy(Enemy e)
    {
      if (e is Square)
      {
        SpawnEnemies(3, true, e.Position.X, e.Position.Y);
        enemies.Remove(e);
      }
      else if (e is Circle)
      {
        SpawnEnemies(3, true, e.Position.X, e.Position.Y);
        enemies.Remove(e);
      }
      else
      {
        enemies.Remove(e);
      }
    }
    /// <summary>
    /// Permet de dessiner toutes les shapes utilisés pour la partie + le texte.
    /// </summary>
    public void Draw()
    {
      foreach (Star star in stars)
      {
        star.Draw(window);
      }
      foreach (Projectile projectile in projectiles)
      {
        projectile.Draw(window);
      }
      foreach (Character character in characters)
      {
        character.Draw(window);
      }
      foreach (Enemy enemy in enemies)
      {
        enemy.Draw(window);
      }
      // Parcourez les listes appropriées pour faire afficher les éléments demandés.



      // Affichage des statistiques. A décommenter au moment opportun
      // Temps total
      text.Position = new Vector2f(0, 10);
      text.DisplayedString = string.Format("{1} = {0,-5}", ((int)(totalTime)).ToString(), StringTable.GetInstance().GetValue(CurrentLanguage, "ID_TOTAL_TIME"));
      window.Draw(text);

      //Points de vie
      text.Position = new Vector2f(0, 50);
      text.DisplayedString = string.Format("{1} = {0,-4}", hero.Life.ToString(), StringTable.GetInstance().GetValue(CurrentLanguage, "ID_LIFE"));
      window.Draw(text);
    }

    /// <summary>
    /// Détermine si un Movable est situé à l'intérieur de la surface de jeu
    /// Peut être utilisée pour déterminer si les projectiles sont sortis du jeu
    /// ou si le héros ou un ennemi s'apprête à sortir.
    /// </summary>
    /// <param name="m">Le Movable à tester</param>
    /// <returns>true si le Movable est à l'intérieur, false sinon</returns>
    public bool Contains(Movable m)
    {
      FloatRect r = new FloatRect(0, 0, GW.WIDTH, GW.HEIGHT);
      return r.Contains(m.Position.X, m.Position.Y);
    }
    /// <summary>
    /// Fait "spawner" une quantitée décidée d'enemy. Permet de choisir s'ils sont des basic, permet aussi de chosir une position de spawn au besoin.
    /// </summary>
    /// <param name="nbEnemies">Quantitée d'enemies</param>
    /// <param name="spawnBasicEnemies">Booléen si on veut spawner des basics</param>
    /// <param name="locationX">position X possible</param>
    /// <param name="locationY">position Y possible</param>
    private void SpawnEnemies(int nbEnemies, bool spawnBasicEnemies, Single locationX, Single locationY)
    {
      if (spawnBasicEnemies)
      {
        float positionX = locationX;
        float positionY = locationY;
        enemies.Add(new BasicEnemy(positionX + 100, positionY, 0));
        enemies.Add(new BasicEnemy(positionX, positionY + 100, 0));
        enemies.Add(new BasicEnemy(positionX + 100, positionY + 100, 0));
        SoundBuffer sound;
        sound = new SoundBuffer("Data/enemy_spawn_orange.wav");
        Sound spawnOrange = new Sound();
        spawnOrange.SoundBuffer = sound;
        spawnOrange.Play();
      }
      else
      {
        for (int i = 0; i < nbEnemies; i++)
        {
          float positionX = r.Next(0, WIDTH);
          float positionY = r.Next(0, HEIGHT);
          int type = r.Next(1, 3);

          if (type == 0)
          {
            enemies.Add(new BasicEnemy(positionX, positionY, 0));
          }
          else if (type == 1)
          {
            //Spawn square
            enemies.Add(new Square(positionX, positionY, 0));
            SoundBuffer sound;
            sound = new SoundBuffer("Data/enemy_spawn_purple.wav");
            Sound spawnPurple = new Sound();
            spawnPurple.SoundBuffer = sound;
            spawnPurple.Play();
          }
          else
          {
            //spawn Circle
            enemies.Add(new Circle(positionX, positionY, 0));
            SoundBuffer sound;
            sound = new SoundBuffer("Data/enemy_spawn_blue.wav");
            Sound spawnBlue = new Sound();
            spawnBlue.SoundBuffer = sound;
            spawnBlue.Play();
          }
        }
      }
    }
    /// <summary>
    /// Permet de faire exploser la bombe. Enlève tous les enemies. Enlève une bombe à l'héro.
    /// </summary>
    public void AddBomb()
    {
      foreach (Enemy enemy in enemies)
      {
        enemiesToRemove.Add(enemy);
      }
      hero.NbBombs = hero.NbBombs - 1;
    }
    /// <summary>
    /// Ajoute un projectile à la partie.
    /// </summary>
    /// <param name="e">projectile à ajouter.</param>
    public void AddProjectile(Projectile e)
    {
      projectiles.Add(e);
    }
    /// <summary>
    /// Permet de gérer tous les changements de positions, les spawns, l'initialisation, les bombes, ce qui est à ajouter et enlever, jouer des sons, les collisions, le spawning et retourne un booléen false si le héro meurt pour mettre fin
    /// à la partie.
    /// </summary>
    /// <returns> Booléen pour si la partie continue.</returns>
    public bool Update()
    {
      // A compléter
      #region Init
      enemiesToAdd.Clear();
      enemiesToRemove.Clear();
      projectilesToAdd.Clear();
      projectilesToRemove.Clear();
      // Vidage de toutes les listes contenant les ennemis et projectiles à ajouter et enlever.
      #endregion
      #region Utilisation des bombes
      if (Keyboard.IsKeyPressed(Keyboard.Key.B) && (DateTime.Now - hero.LastFire).TotalMilliseconds >= DELTA_T * 20000)
      {
        hero.LastFire = DateTime.Now;
        hero.FireBomb(this);
      }
      // Écrire le code pertinent pour faire exploser les bombes
      #endregion
      #region Updates
      // Étoiles  
      foreach (Star star in stars)
      {
        star.Respawn();
        star.Update(-hero.Direction);
      }
      // Personnages et projectiles      
      foreach (Character character in characters)
      {
        character.Update(DELTA_T);
        if (Keyboard.IsKeyPressed(Keyboard.Key.LShift) && (DateTime.Now - character.LastFire).TotalMilliseconds >= DELTA_T * 3000)
        {
          character.Fire(this, DELTA_T);
          character.LastFire = DateTime.Now;
          SoundBuffer sound;
          sound = new SoundBuffer("Data/Fire_normal.wav");
          Sound fireNormal = new Sound();
          fireNormal.SoundBuffer = sound;
          fireNormal.Play();
        }
      }
      foreach (Projectile projectile in projectiles)
      {
        projectile.Update(DELTA_T);
        if (projectile.Position.X < 0 || projectile.Position.X > WIDTH || projectile.Position.Y < 0 || projectile.Position.Y > HEIGHT)
        {
          projectilesToRemove.Add(projectile);
        }
      }

      foreach (Enemy enemy in enemies)
      {
        enemy.Update(this, DELTA_T);

        foreach (Projectile projectile in projectiles)
        {
          if (enemy.Intersects(projectile) && projectile.Type == CharacterType.HERO)
          {
            enemiesToRemove.Add(enemy);
            projectilesToRemove.Add(projectile);
          }
          if (hero.Intersects(projectile) && projectile.Type == CharacterType.ENEMY)
          {
            projectilesToRemove.Add(projectile);
            hero.Life = hero.Life - 1;
          }
        }

        if (enemy.Intersects(hero))
        {
          enemiesToRemove.Add(enemy);
          // hero.Life = hero.Life - 50;
          hero.Life = hero.Life - 3;
        }
      }
      #endregion
      #region Gestion des collisions

      #endregion
      #region Retraits
      // Retrait des ennemis détruits et des projectiles inutiles
      foreach (Enemy enemy in enemiesToRemove)
      {
        RemoveEnemy(enemy);
        SoundBuffer sound;
        sound = new SoundBuffer("Data/enemy_explode.wav");
        Sound explode = new Sound();
        explode.SoundBuffer = sound;
        explode.Play();
      }
      foreach (Projectile projectile in projectilesToRemove)
      {
        projectiles.Remove(projectile);
      }
      #endregion
      #region Spawning des nouveaux ennemis
      // On veut avoir au minimum 5 ennemis (n'incluant pas les triangles). Il faut les ajouter ici
      int squareCounter = 0;
      int circleCounter = 0;

      foreach (Enemy enemy in enemies)
      {
        if (enemy is Square)
        {
          squareCounter++;
        }
        if (enemy is Circle)
        {
          circleCounter++;
        }
      }
      if (squareCounter + circleCounter < 10)
      {
        SpawnEnemies(8 - squareCounter - circleCounter, false, 0, 0);
      }
      #endregion
      #region Ajouts
      // Ajouts des projectiles, ennemis, etc
      #endregion
      // ppoulin 
      // A COMPLETER
      // Retourner true si le héros est en vie, false sinon.
      if (hero.Life > 0)
      {
        return true;
      }
      else
      {
        return false;
      }

    }
  }
}
