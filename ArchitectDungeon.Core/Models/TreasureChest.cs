using System;
using System.Drawing;

namespace DungeonArchitect
{
    public class TreasureChest : DungeonElement
    {
        public string RewardItem { get; set; }
        public bool IsOpened { get; set; }
        public int ActivationRadius { get; set; }

        public TreasureChest() { FillColor = Color.Gold; }

        public TreasureChest(int x, int y, string rewardItem = "Gold", int activationRadius = 30)
            : base(x, y, Color.Gold, durability: 100)
        {
            RewardItem = rewardItem;
            IsOpened = false;
            ActivationRadius = activationRadius;
        }



        public override void Trigger()
        {
            if (!IsBroken && !IsOpened)
            {
                IsOpened = true;
                Durability = 100;
            }
        }

        public bool IsWithinActivationRadius(int otherX, int otherY)
        {
            double distance = Math.Sqrt(Math.Pow(X - otherX, 2) + Math.Pow(Y - otherY, 2));
            return distance < ActivationRadius;
        }
    }
}
