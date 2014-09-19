using MetalLib.Pencil.Gaming;
using Pencil.Gaming.MathUtils;
using Summerjam.GameStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Summerjam.GameObjects.Weapons
{
    class Pistol : Weapon
    {
        public Pistol(string textureName, Vector2 position) :
            base(textureName, position) { }
        public override void Fire(Vector2 target, ref List<Projectile> pl)
        {
            Random r = new Random();
            int spread = 5;
            int displacement = 5;
            for (int i = 0; i < 1; i++)
            {
                pl.Add(new Projectile("Particle", this.Position+ new Vector2(r.Next(0, displacement) - displacement / 2, r.Next(0, displacement) - displacement / 2), 15, 40, target + new Vector2(r.Next(0, spread) - spread / 2, r.Next(0, spread) - spread / 2)));
                GameplayState.ScreenShakeDuration = 5;
                ContentManager.GetSound("pistol").Play();
            }
            Player.FireDelay = 10;
        }
    }
}
