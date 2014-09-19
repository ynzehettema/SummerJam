using MetalLib.GameStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MetalLib.GameWorld;
using Pencil.Gaming.Graphics;
using Pencil.Gaming.MathUtils;
using MetalLib.Pencil.Gaming;
using Summerjam.GameObjects;
using MetalLib;
using System.Threading;
using Pencil.Gaming;

namespace Summerjam.GameStates
{
    class GameplayState : GameState
    {
        public static Vector2 CameraPos = new Vector2(0, 0);
        public static int ScreenShakeDuration = 0;
        public GameWorld GameWorld;
        public Player Player;
        public List<Zombie> ZombieList = new List<Zombie>();
        public List<Projectile> ProjectileList = new List<Projectile>();
        public static List<Particle> ParticleList = new List<Particle>();
        private int GridWidth, GridHeight;

        public int zombiesSpawned = 0;
        private List<int> waveThreshold = new List<int>() { 25, 50, 100, 175, 250, 99999999 };
        public static int currentWave = 0;

        public static bool InShop = false;
        private ShopState shopState;

        public GameplayState()
        {
            //Loader.LoadShizzle();
            if (Loader.CheckMap())
                GameWorld = Loader.LoadMap();



            GridWidth = 39;
            GridHeight = 22;

            Player = new Player("Player", new Vector2(Program.Window.Width / 2 - 150, Program.Window.Height / 2 - 60));

            GameWorld.getGameGrid("walls").collision = true;
            HudHandler.Init();
            FontHandler.TextList = new List<Text>();

        }
        private int spawntimer = 60;
        private bool musicPlaying = true;
        public override void Update(GameLoop gameLoop)
        {
            if (!musicPlaying)
            {
                if (ContentManager.GetSound("music") != null)
                {
                    ContentManager.GetSound("music").Play();
                    ContentManager.GetSound("music").Looping = true;
                    musicPlaying = true;
                }
            }
            if (!InShop)
            {
               
                if (ScreenShakeDuration > 0)
                {
                    int intensity = 15;
                    int speed = 15;
                    Random r = new Random();
                    CameraPos = GameUtils.MoveTowards(CameraPos, new Vector2(r.Next(0, intensity) - intensity / 2, r.Next(0, intensity) - intensity / 2), speed);
                    ScreenShakeDuration--;
                }
                else
                {
                    if (GameUtils.GetDistance(CameraPos, Vector2.Zero) > 1)
                        CameraPos = GameUtils.MoveTowards(CameraPos, new Vector2(0, 0), 1f);
                    else
                        CameraPos = Vector2.Zero;
                }
                spawntimer--;
                if (spawntimer <= 0 && zombiesSpawned < waveThreshold[currentWave])
                {
                    List<GameObject> woot = GameWorld.getGameGrid("ground").ToGameObjectCollection("temp").ObjectList.Where(x => x.textureName == "grass").ToList();
                    int r = new Random().Next(0, woot.Count);
                    if (GameWorld.getGameGrid("walls").Grid[((Tile)woot[r]).X, ((Tile)woot[r]).Y] == null)
                    {
                        ZombieList.Add(new Zombie("zombie", new Vector2((((Tile)woot[r]).X * 32) + 8, (((Tile)woot[r]).Y * 32) + 8)));
                        ContentManager.GetSound("zombie").Play();
                        zombiesSpawned++;
                    }
                    if (currentWave == 0)
                    {
                        spawntimer = new Random().Next(80, 250);
                    }
                    else
                        if(currentWave == 1)
                        {
                            spawntimer = new Random().Next(80, 175);
                        }
                        else
                           if(currentWave == 2)
                           {
                               spawntimer = new Random().Next(50, 150);
                           }
                           else
                               if(currentWave == 3)
                               {
                                   spawntimer = new Random().Next(45, 125);
                               }
                               else
                                   if(currentWave == 4)
                                   {
                                       spawntimer = new Random().Next(30, 100);
                                   }
                }
                if (zombiesSpawned >= waveThreshold[currentWave] && ZombieList.Count == 0)
                {
                    InShop = true;
                    shopState = new ShopState(this);
                }
                Player.Rotation = (float)GameUtils.GetRotation(Player.Position, Input.GetState(0).MousePosition - new Vector2((Program.Window.Width / 2) - ((GridWidth * 16) - 16), (Program.Window.Height / 2) - ((GridHeight * 16) - 16))) + 90;
                for (int i = 0; i < 3; i++)
                {
                    Vector2 newpos = Player.Position, oldpos = Player.Position;
                    if (Input.GetState(0).Keyboard['W'])
                    {
                        if (newpos.Y > 8)
                            newpos.Y--;
                    }
                    if (Input.GetState(0).Keyboard['S'])
                    {
                        if (newpos.Y < Program.Window.Height - 40)
                            newpos.Y++;
                    }
                    if (Input.GetState(0).Keyboard['A'])
                    {
                        if (newpos.X > 8)
                            newpos.X--;
                    }
                    if (Input.GetState(0).Keyboard['D'])
                    {
                        if (newpos.X < 1272)
                            newpos.X++;
                    }
                    Player.Position = newpos;
                    bool collision = false;
                    foreach (GameObject x in GameWorld.getGameGrid("walls").ToGameObjectCollection("temp").ObjectList)
                        if (GameObject.Intersects(Player, x))
                        {
                            if (x.textureName == "door")
                            {
                                if (((Door)x).IsOpen)
                                    continue;
                                else
                                {
                                    ((Door)x).IsOpen = true;
                                    ContentManager.GetSound("doorSound").Play();
                                }
                            }
                            collision = true;
                            break;
                        }
                    if (collision)
                    {
                        Player.Position.X = oldpos.X;
                        collision = false;
                        foreach (GameObject x in GameWorld.getGameGrid("walls").ToGameObjectCollection("temp").ObjectList)
                            if (GameObject.Intersects(Player, x))
                            {
                                if (x.textureName == "door")
                                {
                                    if (((Door)x).IsOpen)
                                        continue;
                                    else
                                    {
                                        ((Door)x).IsOpen = true;
                                        ContentManager.GetSound("doorSound").Play();
                                    }
                                }
                                collision = true;
                                break;
                            }
                        if (collision)
                        {
                            Player.Position.X = newpos.X;
                            Player.Position.Y = oldpos.Y;
                            collision = false;
                            foreach (GameObject x in GameWorld.getGameGrid("walls").ToGameObjectCollection("temp").ObjectList)
                                if (GameObject.Intersects(Player, x))
                                {
                                    if (x.textureName == "door")
                                    {
                                        if (((Door)x).IsOpen)
                                            continue;
                                        else
                                        {
                                            ((Door)x).IsOpen = true;
                                            ContentManager.GetSound("doorSound").Play();
                                        }
                                    }
                                    collision = true;
                                    break;
                                }
                            if (collision)
                            {
                                Player.Position = oldpos;
                            }
                        }
                    }
                }
                Player.Update(ref ProjectileList);
                ProjectileList = ProjectileList.Where(x => !x.remove).ToList();
                ProjectileList.ForEach(x => x.Update(GameWorld, ZombieList, ref ParticleList));
                ParticleList = ParticleList.Where(x => !x.remove).ToList();
                ParticleList.ForEach(x => x.Update(0.25f));
                ZombieList = ZombieList.Where(x => !x.remove).ToList();
                ZombieList.ForEach(x => x.Update(Player, GameWorld));
                for (int x = 0; x < GridWidth; x++)
                    for (int y = 0; y < GridHeight; y++)
                    {
                        if (GameWorld.getGameGrid("walls").Grid[x, y] != null)
                            if (GameWorld.getGameGrid("walls").Grid[x, y].remove)
                            {
                                GameWorld.getGameGrid("walls").Grid[x, y] = null;
                            }
                    }
                FontHandler.AddText(new Text("WAVE: " + (currentWave + 1) + " - Zombies: " + zombiesSpawned + " / " + waveThreshold[currentWave], "wave", new Vector2(Program.Window.Width / 2, 20), 1f, false));
            }
            else
            {
                shopState.Update(gameLoop);
            }
        }

        public override void Draw()
        {
            if (HudHandler.PlayerHealth <= 0)
            {
                Program.MainGameLoop.ActiveGameState = new UrDeadState();
            }
            GL.PushMatrix();
            GL.Translate(((Program.Window.Width / 2) - ((GridWidth * 16) - 16)) + CameraPos.X, ((Program.Window.Height / 2) - ((GridHeight * 16) - 16)) + CameraPos.Y, 0);
            GameWorld.Draw("ground");
            Player.Draw();
            ZombieList.ForEach(x => x.Draw());
            ProjectileList.ForEach(x => x.Draw());
            ParticleList.ForEach(x => x.Draw());
            GameWorld.Draw("walls");

            GL.PopMatrix();
            if (!InShop)
                HudHandler.Draw();
            else
                shopState.Draw();
        }
    }
}
