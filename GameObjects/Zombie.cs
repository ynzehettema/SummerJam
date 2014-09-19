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
    class Zombie : GameObject
    {
        public float Health = 100;
        public Zombie(string textureName, Vector2 position) :
            base(textureName, position)
        {
            this.hitboxDimensions *= 0.75f;
        }

        public void Update(Player p, GameWorld g)
        {
            if(Health <= 0)
            {
                remove = true;

                HudHandler.PlayerMoney += new Random().Next(2, 5);
            }
            Vector2 oldpos = new Vector2(this.Position.X, this.Position.Y);
            Vector2 newpos = GameUtils.MoveTowards(oldpos, p.Position);

            this.Position = newpos;

            this.Rotation = (float)GameUtils.GetRotation(this.Position, p.Position) + 90;
            bool collision = false;
            foreach (GameObject x in g.getGameGrid("walls").ToGameObjectCollection("temp").ObjectList)
                if (GameObject.Intersects(this, x))
                {
                    if (x.textureName == "glasspane" )
                    {
                        ContentManager.GetSound("glass").Play();
                        x.remove = true;
                        Random r = new Random();
                        for (int i = 0; i < 25; i++)
                        {
                            GameplayState.ParticleList.Add(new Particle(this.Position, Color4.LightBlue, r));
                        }
                    }
                    if (x.textureName == "fence" || x.textureName == "fenceCorner" || x.textureName == "fenceT" || x.textureName == "door")
                    {
                        ContentManager.GetSound("door").Play();
                        x.remove = true;
                        Random r = new Random();
                        for (int i = 0; i < 25; i++)
                        {
                            GameplayState.ParticleList.Add(new Particle(this.Position, Color4.SandyBrown, r));
                        }
                    }
                    collision = true;
                    break;
                }
            if (collision)
            {
                this.Position.X = oldpos.X;
                collision = false;
                foreach (GameObject x in g.getGameGrid("walls").ToGameObjectCollection("temp").ObjectList)
                    if (GameObject.Intersects(this, x))
                    {
                        collision = true;
                        break;
                    }
                if (collision)
                {
                    this.Position.X = newpos.X;
                    this.Position.Y = oldpos.Y;
                    collision = false;
                    foreach (GameObject x in g.getGameGrid("walls").ToGameObjectCollection("temp").ObjectList)
                        if (GameObject.Intersects(this, x))
                        {
                            collision = true;
                            break;
                        }
                    if (collision)
                        this.Position = oldpos;
                }
            }
            else
                this.Position = newpos;
            if(GameUtils.GetDistance(this.Position, p.Position) < 32)
            {
                HudHandler.PlayerHealth--;
            }
        }

        public override void Draw()
        {
            base.Draw();
        }
    }
}
