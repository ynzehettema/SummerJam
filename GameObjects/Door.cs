using MetalLib.GameWorld;
using Pencil.Gaming.Graphics;
using Pencil.Gaming.MathUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Summerjam.GameObjects
{
    class Door : Tile
    {
        public bool IsOpen;
        public int rotation = 0;

        public Door(string texture, int x, int y, GameGrid g)
            : base(texture, x, y, g)
        {

        }

        public override void Update()
        {
            base.Update();

        }

        public override void Draw()
        {
            if (!IsOpen)
                base.Draw();
            else
            {
                Vector2 origin = Vector2.Zero;
                if(Rotation == 0)
                {
                    origin.Y = -16;
                }
                if (Rotation == 180)
                {
                    origin.Y = 16;
                }
                if (Rotation == 90)
                {
                    origin.X = 16;
                }
                if (Rotation == 270)
                {
                    origin.X = -16;
                }
                GL.PushMatrix();
                GL.Translate(this.Position.X - origin.X, this.Position.Y - origin.Y, 0);
                GL.Rotate(rotation,new Vector3(0,0,1));
                if (rotation < 90)
                    rotation+=5;
                GL.Translate(-this.Position.X + origin.X, -this.Position.Y + origin.Y, 0);
                base.Draw();
                GL.PopMatrix();
            }
        }
    }
}
