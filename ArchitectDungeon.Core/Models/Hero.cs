using System;
using System.Drawing;

namespace architectSteps
{
    public class Hero : DungeonElement
    {
        public int HealthPoints { get; set; }
        public float Speed { get; set; }
        public System.Collections.Generic.List<string> Inventory { get; set; } = new System.Collections.Generic.List<string>();

        public Hero() { FillColor = Color.Green; }

        public Hero(int x, int y, int healthPoints = 100, float speed = 1.0f)
            : base(x, y, Color.Green, durability: 100)
        {
            HealthPoints = healthPoints;
            Speed = speed;
        }



        public override void Trigger() { }

        public override void Move(int dx, int dy)
        {
            int scaledDx = (int)(dx * Speed);
            int scaledDy = (int)(dy * Speed);
            base.Move(scaledDx, scaledDy);
        }

        public void TakeDamage(int damage)
        {
            if (!IsBroken && HealthPoints > 0)
            {
                HealthPoints = Math.Max(0, HealthPoints - damage);
                if (HealthPoints == 0) IsBroken = true;
            }
        }

        public void CollectLoot(string item)
        {
            Inventory.Add(item);
            System.Diagnostics.Debug.WriteLine($"Hero collected: {item}");
        }
    }
}
