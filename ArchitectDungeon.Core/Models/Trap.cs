using System;
using System.Drawing;


namespace architectSteps
{
    public class Trap : DungeonElement
    {
        public int Damage { get; set; }
        public int ActivationRadius { get; set; }

        public Trap() { FillColor = Color.Red; }

        public Trap(int x, int y, int damage = 10, int activationRadius = 30)
            : base(x, y, Color.Red, durability: 50)
        {
            Damage = damage;
            ActivationRadius = activationRadius;
        }



        public override void Trigger()
        {
            if (!IsBroken)
            {
                Durability -= 10;
                if (Durability <= 0)
                {
                    IsBroken = true;
                    Durability = 0;
                }
            }
        }

        public bool IsWithinActivationRadius(int otherX, int otherY)
        {
            double distance = Math.Sqrt(Math.Pow(X - otherX, 2) + Math.Pow(Y - otherY, 2));
            return distance < ActivationRadius;
        }
    }
}
