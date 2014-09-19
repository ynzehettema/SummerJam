using MetalLib;
using Pencil.Gaming;
using Pencil.Gaming.Graphics;
using Pencil.Gaming.MathUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Summerjam.GameObjects
{
    class Particle
    {
        private Vector2 position;
        private Color4 color;
        private float angle;
        private float speed;

        private float maxTime;
        private float time = 0f;
        private float size = 0f;

        public bool remove;

        public Particle(Vector2 position, Color4 color, Random random)
        {
            this.position = position;
            this.color = color;
            this.angle = (float)random.NextDouble() * 360;
            this.speed = 2f + (float)random.NextDouble() * 5f;
            size = 1f + (float)random.NextDouble() * 2.5f;
            maxTime = (float)Glfw.GetTime() * random.Next(10, 25);

        }
        public Particle(Vector2 position, Color4 color, float perpAngle, Random random)
        {
            this.position = position;
            this.color = color;
            float spread = 45f;
            this.angle = (perpAngle - 180 - spread) + (float)random.NextDouble() * spread * 2;
            this.speed = 2f + (float)random.NextDouble() * 5f;
            size = 1f + (float)random.NextDouble() * 2.5f;
            maxTime = (float)Glfw.GetTime() * random.Next(10, 25);
        }

        public void Update(float delta)
        {
            position = GameUtils.MoveAlongAngle(angle, position, speed);
            time += delta / 10;
            remove = time >= maxTime;
        }

        public void Draw()
        {
            GL.Color4(color);

            GL.PointSize(size);

            GL.Begin(BeginMode.Points);

            GL.Vertex2(position.X, position.Y);

            GL.End();
        }
    }
}
