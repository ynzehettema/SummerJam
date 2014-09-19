using MetalLib.GameWorld;
using MetalLib.Pencil.Gaming;
using Pencil.Gaming.MathUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Summerjam
{
    class HudHandler
    {
        private static GameObject Hud;
        public static GameObject Weapon;
        public static int PlayerHealth = 100;
        public static int PlayerMoney = 100;
        public static int PlayerAmmo = 500;
        public static void Init()
        {
            Hud = new GameObject("hud", new Vector2(44, 76));
            Weapon = new GameObject("pistolhud", new Vector2(44, 35.5f));
        }
        public static void Draw()
        {
            FontHandler.AddText(new Text(PlayerAmmo.ToString(), "ammo", new Vector2(35, 85), 0.4f, true));
            FontHandler.AddText(new Text(PlayerHealth.ToString(), "health", new Vector2(35, 110), 0.5f, true));
            FontHandler.AddText(new Text(PlayerMoney.ToString(), "monies", new Vector2(35, 143.5f), 0.5f, true));
            Weapon.Draw();
            Hud.Draw();
        }
    }
}
