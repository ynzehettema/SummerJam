using MetalLib.Pencil.Gaming;
using Pencil.Gaming.MathUtils;
using Summerjam.GameStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Summerjam.GameObjects.Weapons
{
    class MP7 : Weapon
    {
        public MP7(string textureName, Vector2 position) :
            base(textureName, position) { }
        public override void Fire(Vector2 target, ref List<Projectile> pl)
        {
            if (HudHandler.PlayerAmmo > 0)
            {
                Random r = new Random();
                int spread = 20;
                int displacement = 5;
                for (int i = 0; i < 1; i++)
                {
                    pl.Add(new Projectile("Particle", this.Position + new Vector2(r.Next(0, displacement) - displacement / 2, r.Next(0, displacement) - displacement / 2), 20, 20, target + new Vector2(r.Next(0, spread) - spread / 2, r.Next(0, spread) - spread / 2)));
                    GameplayState.ScreenShakeDuration = 3;
                    ContentManager.GetSound("mp7").Play();
                }
                HudHandler.PlayerAmmo--;
                Player.FireDelay = 5;
            }
        }
    }
}
