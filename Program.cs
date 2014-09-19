using MetalLib.GameStructure;
using MetalLib.Pencil.Gaming;
using Pencil.Gaming;
using Summerjam.GameStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Summerjam
{
    class Program
    {
        public static Window2D Window = new Window2D(1280, 720, true, "My house is secure! - By Metaldemon & Woellie123", 1280, 720);
        public static GameLoop MainGameLoop = new GameLoop(new MenuState());
        static void Main(string[] args)
        {
            Glfw.SwapInterval(true);
            MainGameLoop.Start(Window);
        }
    }
}
