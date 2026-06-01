using System;
using System.Drawing;

namespace architectSteps
{
    public class Wall : DungeonElement
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public Wall() { FillColor = Color.Gray; }

        public Wall(int x, int y, int width = 50, int height = 50)
            : base(x, y, Color.Gray, durability: 1000)
        {
            Width = width;
            Height = height;
        }



        public override void Trigger() { }

        public override Rectangle GetBounds()
        {
            return new Rectangle(X - Width / 2, Y - Height / 2, Width, Height);
        }
    }
}
