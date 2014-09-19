using MetalLib;
using MetalLib.GameStructure;
using MetalLib.GameWorld;
using MetalLib.Pencil.Gaming;
using Pencil.Gaming.Graphics;
using Pencil.Gaming.MathUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Summerjam.GameStates
{
    class MenuState : GameState
    {
        private GameObject button1;
        private GameObject button1Outline;
        private GameObject button2;
        private GameObject button2Outline;

        public MenuState()
        {
            Loader.LoadShizzle();
            button1 = new GameObject(Color4.Blue, new Vector2(Program.Window.Width / 2, 350));
            button1.Width = 350;
            button1.Height = 40;
            button1.hitboxDimensions = new Vector2(button1.Width, button1.Height);
            button1Outline = new GameObject(Color4.DarkBlue, new Vector2(Program.Window.Width / 2, 350));
            button1Outline.Width = 360;
            button1Outline.Height = 45;
            button2 = new GameObject(Color4.Green, new Vector2(Program.Window.Width / 2, 420));
            button2.Width = 200;
            button2.Height = 40;
            button2.hitboxDimensions = new Vector2(button2.Width, button2.Height);
            button2Outline = new GameObject(Color4.DarkGreen, new Vector2(Program.Window.Width / 2, 420));
            button2Outline.Width = 210;
            button2Outline.Height = 50;
        }

        public override void Update(GameLoop gameLoop)
        {
            FontHandler.AddText(new Text("My house is secure", "menu1", new Vector2(Program.Window.Width / 2, 100), 2f, false));
            FontHandler.AddText(new Text("start game", "menu2", new Vector2(Program.Window.Width / 2, 350), 1f, false));
            FontHandler.AddText(new Text("exit", "menu3", new Vector2(Program.Window.Width / 2, 420), 1f, false));

            if (Input.GetState(0).Mouse.LeftButton)
            {
                if (GameObject.Contains(Input.GetState(0).MousePosition, button1))
                {
                    button1.color = Color4.DarkBlue;
                    button1Outline.color = Color4.Blue;
                }
                else if (GameObject.Contains(Input.GetState(0).MousePosition, button2))
                {
                    button2.color = Color4.DarkGreen;
                    button2Outline.color = Color4.Green;
                }
            }
            else
            {
                button1.color = Color4.Blue;
                button1Outline.color = Color4.DarkBlue;
                button2.color = Color4.Green;
                button2Outline.color = Color4.DarkGreen;
            }
        }

        public override void Draw()
        {

            button1Outline.Draw();
            button1.Draw();
            button2Outline.Draw();
            button2.Draw();
            if (button1.color == Color4.DarkBlue)
            {
                Program.MainGameLoop.ActiveGameState = new GameplayState();
            }
            if(button1.color == Color4.DarkGreen)
            {
                Program.MainGameLoop.Run = false;
                Program.Window.Close();
                Environment.Exit(0);
            }
        }
    }
}
