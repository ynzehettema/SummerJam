using MetalLib.GameWorld;
using Pencil.Gaming.MathUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Summerjam.GameObjects.Weapons
{
    class Weapon : GameObject
    {
        public Weapon(string textureName, Vector2 position) :
            base(textureName, position) { }

        public virtual void Fire(Vector2 target, ref List<Projectile> pl)
        {

        }
    }
}
