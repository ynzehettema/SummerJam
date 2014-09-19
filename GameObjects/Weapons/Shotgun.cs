using MetalLib.Pencil.Gaming;
using Pencil.Gaming.MathUtils;
using Summerjam.GameStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Summerjam.GameObjects.Weapons
{
    class Shotgun : Weapon
    {
        public Shotgun(string textureName, Vector2 position) :
            base(textureName, position) { }
        public override void Fire(Vector2 target, ref List<Projectile> pl)
        {
            if (HudHandler.PlayerAmmo >= 5)
            {
                HudHandler.PlayerAmmo -= 5;

                Random r = new Random();
                int spread = 50;
                int displacement = 20;
                for (int i = 0; i < 10; i++)
                {
                    pl.Add(new Projectile("Particle", this.Position + new Vector2(r.Next(0, displacement) - displacement / 2, r.Next(0, displacement) - displacement / 2), 25, 50, target + new Vector2(r.Next(0, spread) - spread / 2, r.Next(0, spread) - spread / 2)));
                }
                ContentManager.GetSound("shotgun").Play();
                GameplayState.ScreenShakeDuration = 5;
                Player.FireDelay = 70;
            }
            
        }
    }
}
