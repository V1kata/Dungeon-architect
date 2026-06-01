using System;
using System.Drawing;
using System.Text.Json.Serialization;

namespace DungeonArchitect
{
    public abstract class DungeonElement
    {
        public Guid Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        
        [JsonIgnore]
        public Color FillColor { get; set; }
        
        public string ColorName 
        { 
            get => FillColor.IsNamedColor ? FillColor.Name : FillColor.Name; 
            set => FillColor = Color.FromName(value); 
        }

        public int Durability { get; set; }
        public bool IsBroken { get; set; }

        protected DungeonElement() { Id = Guid.NewGuid(); FillColor = Color.Gray; }

        protected DungeonElement(int x, int y, Color fillColor, int durability = 100)
        {
            Id = Guid.NewGuid();
            X = x;
            Y = y;
            FillColor = fillColor;
            Durability = durability;
            IsBroken = false;
        }


        public abstract void Trigger();

        public virtual void Move(int dx, int dy)
        {
            X += dx;
            Y += dy;
        }

        public virtual Rectangle GetBounds()
        {
            return new Rectangle(X - 25, Y - 25, 50, 50);
        }
    }
}
