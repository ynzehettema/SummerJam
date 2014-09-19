using MetalLib;
using MetalLib.GameStructure;
using MetalLib.GameWorld;
using MetalLib.Pencil.Gaming;
using Pencil.Gaming.Graphics;
using Pencil.Gaming.MathUtils;
using Summerjam.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Summerjam.GameStates
{
    class ShopState : GameState
    {
        private GameObject shop;
        private List<GameObject> boxes = new List<GameObject>();
        private GameplayState game;
        public ShopState(GameplayState game)
        {
            this.game = game;
            shop = new GameObject("shop", new Vector2(Program.Window.Width / 2, Program.Window.Height / 2));
            boxes.Add(new GameObject(Color4.Red, new Vector2(140, 390)));
            boxes.Add(new GameObject(Color4.Red, new Vector2(140 + 248, 390)));
            boxes.Add(new GameObject(Color4.Red, new Vector2(140 + (248 * 2), 390)));
            boxes.Add(new GameObject(Color4.Red, new Vector2(140 + (248 * 3), 390)));
            boxes.Add(new GameObject(Color4.Red, new Vector2(140 + (248 * 4), 390)));
            
            boxes.ForEach(x => {
                x.hitboxDimensions = new Vector2(200, 225);
                x.Width = 200;
                x.Height = 225;
            });
            boxes.Add(new GameObject(Color4.Red, new Vector2(1060, 670)));
            boxes[boxes.Count - 1].hitboxDimensions = new Vector2(390, 85);
            boxes[boxes.Count - 1].Width = 390;
            boxes[boxes.Count - 1].Height = 85;
            FontHandler.TextList = new List<Text>();
            
        }

        public override void Update(GameLoop gameLoop)
        {
            FontHandler.AddText(new Text("150", "0", new Vector2(150, 480), 1f, false));
            FontHandler.AddText(new Text("150", "1", new Vector2(150 + 248, 480), 1f, false));
            FontHandler.AddText(new Text("50", "2", new Vector2(150 + (248 * 2), 480), 1f, false));
            FontHandler.AddText(new Text("50", "3", new Vector2(150 + (248 * 3), 480), 1f, false));
            FontHandler.AddText(new Text("125", "4", new Vector2(150 + (248 * 4), 480), 1f, false));
            FontHandler.AddText(new Text("TO SPEND: " + HudHandler.PlayerMoney , "money", new Vector2(Program.Window.Width / 2, 600), 1f, false));

            

            if (Input.GetState(0).Mouse.LeftButton && !Input.GetState(1).Mouse.LeftButton)
            {
                int i = 0;
                foreach (GameObject g in boxes)
                {
                    if(GameObject.Contains(Input.GetState(0).MousePosition, g))
                    {
                        if(i == 0 && !Player.hasMP7 && HudHandler.PlayerMoney >= 150)
                        {
                            Player.hasMP7 = true;
                            HudHandler.PlayerMoney -= 150;
                            ContentManager.GetSound("cashRegister").Play();
                        }
                        if (i == 1 && !Player.hasShotgun && HudHandler.PlayerMoney >= 150)
                        {
                            Player.hasShotgun = true;
                            HudHandler.PlayerMoney -= 150;
                            ContentManager.GetSound("cashRegister").Play();
                        }
                        if (i == 2 && HudHandler.PlayerMoney >= 50)
                        {
                            HudHandler.PlayerAmmo += 100;
                            HudHandler.PlayerMoney -= 50;
                            ContentManager.GetSound("cashRegister").Play();
                        }
                        if (i == 3 && HudHandler.PlayerMoney >= 50)
                        {
                            HudHandler.PlayerHealth = 100;
                            HudHandler.PlayerMoney -= 50;
                            ContentManager.GetSound("cashRegister").Play();
                        }
                        if (i == 4 && HudHandler.PlayerMoney >= 125)
                        {
                            HudHandler.PlayerHealth = 250;
                            HudHandler.PlayerMoney -= 125;
                            ContentManager.GetSound("cashRegister").Play();
                        }
                        if(i == 5)
                        {
                            FontHandler.TextList = new List<Text>();
                            GameplayState.InShop = false;
                            GameplayState.currentWave++;
                            game.GameWorld = Loader.LoadMap();
                            game.zombiesSpawned = 0;
                        }
                    }
                    i++;
                }
            }
        }

        public override void Draw()
        {
            shop.Draw();
            //boxes.ForEach(x => x.Draw());
        }
    }
}
