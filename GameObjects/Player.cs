using MetalLib;
using MetalLib.GameWorld;
using Pencil.Gaming.MathUtils;
using Summerjam.GameObjects.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Summerjam.GameObjects
{
    class Player : GameObject
    {
        public Weapon Weapon;
        public static int FireDelay = 20;
        public static bool hasShotgun = true;
        public static bool hasMP7 = true;
        public Player(string texturename, Vector2 position)
            : base(texturename, position)
        {
            Weapon = new Pistol("pistol", position);
        }

        public void Update(ref List<Projectile> pl)
        {
            if (FireDelay > 0)
                FireDelay--;
            Weapon.Position = this.Position;
            Weapon.Rotation = this.Rotation;
            if (FireDelay == 0 && Input.GetState(0).Mouse.LeftButton && (!Input.GetState(1).Mouse.LeftButton || Weapon.textureName == "mp7"))
                Weapon.Fire(Input.GetState(0).MousePosition - new Vector2(8, 8), ref pl);
            base.Update();
            if (Input.GetState(0).Mouse.ScrollWheel > Input.GetState(1).Mouse.ScrollWheel)
            {
                if (Weapon.textureName == "shotgun")
                {
                    Weapon = new Pistol("pistol", this.Position);
                    HudHandler.Weapon.textureName = "pistolhud";
                    return;
                }
                if (Weapon.textureName == "mp7" && hasShotgun)
                {
                    Weapon = new Shotgun("shotgun", this.Position);
                    HudHandler.Weapon.textureName = "shotgunhud";
                    return;
                }
                if (Weapon.textureName == "pistol" && hasMP7)
                {
                    Weapon = new MP7("mp7", this.Position);
                    HudHandler.Weapon.textureName = "riflehud";
                    return;
                }
            }
            if (Input.GetState(0).Mouse.ScrollWheel < Input.GetState(1).Mouse.ScrollWheel)
            {
                if (Weapon.textureName == "shotgun" && hasMP7)
                {
                    Weapon = new MP7("mp7", this.Position);
                    HudHandler.Weapon.textureName = "riflehud";
                    return;
                }
                if (Weapon.textureName == "mp7")
                {
                    Weapon = new Pistol("pistol", this.Position);
                    HudHandler.Weapon.textureName = "pistolhud";
                    return;
                }
                if (Weapon.textureName == "pistol" && hasShotgun)
                {
                    Weapon = new Shotgun("shotgun", this.Position);
                    HudHandler.Weapon.textureName = "shotgunhud";
                    return;
                }
            }
        }

        public override void Draw()
        {
            Weapon.Draw();
            base.Draw();
        }
    }
}
