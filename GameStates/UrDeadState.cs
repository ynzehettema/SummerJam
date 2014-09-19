using MetalLib;
using MetalLib.GameStructure;
using MetalLib.Pencil.Gaming;
using Pencil.Gaming;
using Pencil.Gaming.MathUtils;
using Summerjam.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Summerjam.GameStates
{
    class UrDeadState : GameState
    {
        public override void Update(GameLoop gameLoop)
        {
            FontHandler.TextList = new List<Text>();
            FontHandler.AddText(new Text("UR DEAD :]", "teehee", new Vector2(Program.Window.Width / 2, Program.Window.Height / 2), 2f, false));
            FontHandler.AddText(new Text("SPACE TO TRY AGAIN", "teehee2", new Vector2(Program.Window.Width / 2, Program.Window.Height / 2 + 50), 1f, false));
        }

        public override void Draw()
        {
            if (Input.GetState(0).Keyboard[Key.Space])
            {
                Program.MainGameLoop.ActiveGameState = new GameplayState();
                HudHandler.PlayerHealth = 100;
                HudHandler.PlayerMoney = 100;
                HudHandler.PlayerAmmo = 50;
                GameplayState.currentWave = 0;
                Player.hasMP7 = false;
                Player.hasShotgun = false;
            }

        }
    }
}
