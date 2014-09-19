using MetalLib;
using MetalLib.GameWorld;
using MetalLib.Pencil.Gaming;
using Pencil.Gaming.Graphics;
using Pencil.Gaming.MathUtils;
using Summerjam.GameStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Summerjam.GameObjects
{
    class Projectile : GameObject
    {
        private float speed = 9001;
        private float damage = -20;
        int frames = 0;
        public Projectile(string textureName, Vector2 position, float speed, float damage, Vector2 target)
            : base(textureName, position)
        {
            this.Rotation = (float)GameUtils.GetRotation(position, target) + 90;
            this.speed = speed;
            this.damage = damage;
            
        }
        public void Update(GameWorld gw, List<Zombie> zl, ref List<Particle> pl)
        {
            frames++;
            if (frames > 120)
                remove = true;
            base.Update();
            Position = GameUtils.MoveAlongAngle(this.Rotation - 90, this.Position, speed);
            foreach (GameObject x in gw.getGameGrid("walls").ToGameObjectCollection("temp").ObjectList)
            {
                if (x.textureName == "door" && ((Door)x).IsOpen)
                    continue;
                if (GameObject.Contains(this.Position, x))
                {
                    remove = true;
                    Random r = new Random();
                    for (int i = 0; i < 12; i++)
                    {
                        pl.Add(new Particle(this.Position, Color4.DarkGray, r));
                    }
                    if (x.textureName == "glasspane")
                    {
                        ContentManager.GetSound("glass").Play();
                        x.remove = true;
                        for (int i = 0; i < 25; i++)
                        {
                            GameplayState.ParticleList.Add(new Particle(this.Position, Color4.LightBlue, r));
                        }
                    }
                    break;
                }
            }

            foreach (Zombie z in zl)
            {
                if (GameObject.Contains(this.Position, z))
                {
                    z.Health -= damage;
                    if (new Random().Next(0, 50) > speed)
                        remove = true;
                    Random r = new Random();
                    for (int i = 0; i < 25; i++)
                    {
                        pl.Add(new Particle(this.Position, Color4.Red, r));
                    }
                    break;
                }
            }
        }

        public override void Draw()
        {
            base.Draw();
        }
    }
}
