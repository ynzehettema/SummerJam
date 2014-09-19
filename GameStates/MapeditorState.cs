using MetalLib;
using MetalLib.GameStructure;
using MetalLib.GameWorld;
using MetalLib.Pencil.Gaming;
using Pencil.Gaming;
using Pencil.Gaming.Graphics;
using Pencil.Gaming.MathUtils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Summerjam.GameStates
{
    class MapeditorState : GameState
    {
        public GameWorld GameWorld;
        private int gridWidth, gridHeight;

        private string currentEditLayer = "ground";
        private float currentRotation = 0;
        private int editTileX, editTileY;
        private List<string> textureList = new List<string>() { "grass", "floor", "floorGrassCorner", "floorGrassCornerFlip", "grassFloor", "grassFloorCorner", "grassFloorVertical", "grassFloorCornerVertical", "path", "pathCorner", "wall", "wallcorner", "wallCornerFlip", "wallT", "wallInside", "wallInsideCorner", "wallInsideT", "door", "glasspane", "fence", "fenceCorner", "fenceT" };
        private int currentTexture = 0;

        public MapeditorState()
        {
            Loader.LoadShizzle();
            gridWidth = 39;
            gridHeight = 22;
            editTileX = editTileY = 0;
            if (Loader.CheckMap())
                GameWorld = Loader.LoadMap();
            else
            {
                GameWorld = new GameWorld();
                GameWorld.AddGameGrid(new GameGrid(gridWidth, gridHeight, "ground"));
                GameWorld.AddGameGrid(new GameGrid(gridWidth, gridHeight, "walls"));
            }
            GameWorld.AddGameGrid(new GameGrid(gridWidth, gridHeight, "editlayer"));

            for (int x = 0; x < gridWidth; x++)
                for (int y = 0; y < gridHeight; y++)
                    GameWorld.getGameGrid("editlayer").AddTile("temp", x, y);
        }

        public override void Update(GameLoop gameLoop)
        {
            for (int x = 0; x < gridWidth; x++)
                for (int y = 0; y < gridHeight; y++)
                    GameWorld.getGameGrid("editlayer").AddTile("temp", x, y);
            GameWorld.getGameGrid("editlayer").AddTile(textureList[currentTexture], editTileX, editTileY);
            GameWorld.getGameGrid("editlayer").Grid[editTileX, editTileY].Rotation = currentRotation;
            if (Input.GetState(0).Keyboard[Key.Up] && !Input.GetState(1).Keyboard[Key.Up])
            {
                if (editTileY > 0)
                    editTileY--;
            }
            if (Input.GetState(0).Keyboard[Key.Down] && !Input.GetState(1).Keyboard[Key.Down])
            {
                if (editTileY < gridHeight - 1)
                    editTileY++;
            }
            if (Input.GetState(0).Keyboard[Key.Left] && !Input.GetState(1).Keyboard[Key.Left])
            {
                if (editTileX > 0)
                    editTileX--;
            }
            if (Input.GetState(0).Keyboard[Key.Right] && !Input.GetState(1).Keyboard[Key.Right])
            {
                if (editTileX < gridWidth - 1)
                    editTileX++;
            }
            if (Input.GetState(0).Keyboard[Key.Space])//&& !Input.GetState(1).Keyboard[Key.Space])
            {
                GameWorld.getGameGrid(currentEditLayer).AddTile(textureList[currentTexture], editTileX, editTileY);
                GameWorld.getGameGrid(currentEditLayer).Grid[editTileX, editTileY].Rotation = currentRotation;
            }
            if (Input.GetState(0).Keyboard[Key.Backspace])//&& !Input.GetState(1).Keyboard[Key.Backspace])
            {
                GameWorld.getGameGrid(currentEditLayer).Grid[editTileX, editTileY] = null;
            }
            if (Input.GetState(0).Keyboard['S'] && !Input.GetState(1).Keyboard['S'])
            {
                using (StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "/data/map.summerjam"))
                {
                    GameWorld.GetGrids().ForEach(x =>
                        {
                            if (x.Name != "editlayer")
                            {
                                sw.WriteLine("Layer " + x.Name);
                                for (int i = 0; i < x.Grid.GetLength(0); i++)
                                {
                                    for (int j = 0; j < x.Grid.GetLength(1); j++)
                                    {
                                        if (x.Grid[i, j] != null)
                                            sw.WriteLine(i + " " + j + " " + x.Grid[i, j].Texture.Name + " " + (int)x.Grid[i, j].Rotation);
                                    }
                                }
                            }
                        });
                }
                Console.WriteLine("Map saved!");
            }
            if (Input.GetState(0).Keyboard['['] && !Input.GetState(1).Keyboard['['])
            {
                if (currentTexture > 0) currentTexture--;
            }
            if (Input.GetState(0).Keyboard[']'] && !Input.GetState(1).Keyboard[']'])
            {
                if (currentTexture < textureList.Count - 1) currentTexture++;
            }
            if (Input.GetState(0).Keyboard['L'] && !Input.GetState(1).Keyboard['L'])
            {
                currentEditLayer = currentEditLayer == "ground" ? "walls" : "ground";
                Console.WriteLine("layer: " + currentEditLayer);
            }
            if (Input.GetState(0).Keyboard['R'] && !Input.GetState(1).Keyboard['R'])
            {
                currentRotation += 90;
                currentRotation %= 360;
            }
        }

        public override void Draw()
        {


            GL.PushMatrix();
            GL.Translate((Program.Window.Width / 2) - ((gridWidth * 16) - 16), (Program.Window.Height / 2) - ((gridHeight * 16) - 16), 0);

            GameWorld.Draw("ground");
            GameWorld.Draw("walls");
            GameWorld.Draw("editlayer");
            GL.PopMatrix();
            GL.PushMatrix();
            GL.Color4(Color4.White);
            GL.Translate(Program.Window.Width / 2, Program.Window.Height / 2, 0);
            GL.PointSize(5f);
            GL.Begin(BeginMode.Points);
            GL.Vertex2(0, 0);
            GL.End();
            GL.PopMatrix();
        }
    }
}
