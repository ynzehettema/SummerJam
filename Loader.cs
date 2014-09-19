using MetalLib.GameWorld;
using MetalLib.Pencil.Gaming;
using Summerjam.GameObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Summerjam
{
    class Loader
    {
        public static void LoadShizzle()
        {
            ContentManager.LoadAllTextures(AppDomain.CurrentDomain.BaseDirectory + "/data/textures/");
            ContentManager.LoadAllTextures(AppDomain.CurrentDomain.BaseDirectory + "/data/font/");
            ContentManager.LoadAllTextures(AppDomain.CurrentDomain.BaseDirectory + "/data/objects/");
            ContentManager.LoadSoundThreaded(AppDomain.CurrentDomain.BaseDirectory + "/data/sounds/music.ogg", "music");
            ContentManager.LoadSound(AppDomain.CurrentDomain.BaseDirectory + "/data/sounds/44Magnum.wav", "pistol");
            ContentManager.LoadSound(AppDomain.CurrentDomain.BaseDirectory + "/data/sounds/gunBurst.wav", "mp7");
            ContentManager.LoadSound(AppDomain.CurrentDomain.BaseDirectory + "/data/sounds/shotgun.wav", "shotgun");
            ContentManager.LoadSound(AppDomain.CurrentDomain.BaseDirectory + "/data/sounds/glass.wav", "glass");
            ContentManager.LoadSound(AppDomain.CurrentDomain.BaseDirectory + "/data/sounds/door.wav", "door");
            ContentManager.LoadSound(AppDomain.CurrentDomain.BaseDirectory + "/data/sounds/zombie.wav", "zombie");
            ContentManager.LoadSound(AppDomain.CurrentDomain.BaseDirectory + "/data/sounds/doorSound.wav", "doorSound");
            ContentManager.LoadSound(AppDomain.CurrentDomain.BaseDirectory + "/data/sounds/cashRegister.wav", "cashRegister");
            ContentManager.GetSound("glass").Gain = 0.3f;
        }

        public static bool CheckMap()
        {
            return File.Exists(AppDomain.CurrentDomain.BaseDirectory + "/data/map.summerjam");
        }

        public static GameWorld LoadMap()
        {
            GameWorld gw = new GameWorld();

            string currentLayer = string.Empty;
            using (StreamReader sr = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "/data/map.summerjam"))
            {
                string line = string.Empty;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.StartsWith("Layer"))
                    {
                        currentLayer = line.Split(' ')[1];
                        gw.AddGameGrid(39, 22, currentLayer);
                    }
                    else
                    {
                        string[] split = line.Split(' ');
                        if(split[2] != "door")
                        gw.getGameGrid(currentLayer).AddTile(new Tile(split[2], int.Parse(split[0]), int.Parse(split[1]), gw.getGameGrid(currentLayer)), int.Parse(split[0]), int.Parse(split[1]));
                        else
                            gw.getGameGrid(currentLayer).AddTile(new Door(split[2], int.Parse(split[0]), int.Parse(split[1]), gw.getGameGrid(currentLayer)), int.Parse(split[0]), int.Parse(split[1]));
                        if (split.Length > 3)
                            gw.getGameGrid(currentLayer).Grid[int.Parse(split[0]), int.Parse(split[1])].Rotation = int.Parse(split[3]);
                    }

                }
            }

            return gw;
        }
    }
}
