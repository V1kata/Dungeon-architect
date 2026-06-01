using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace architectSteps
{
    public class DungeonScene
    {
        private List<DungeonElement> _elements = new List<DungeonElement>();

        public IReadOnlyList<DungeonElement> Elements => _elements.AsReadOnly();

        public void AddElement(DungeonElement element)
        {
            _elements.Add(element);
        }

        public void RemoveElement(DungeonElement element)
        {
            _elements.Remove(element);
        }



        public DungeonElement GetElementAtPosition(int x, int y)
        {
            return _elements.FirstOrDefault(e => e.GetBounds().Contains(x, y));
        }

        public bool TryMoveHero(Hero hero, int dx, int dy)
        {
            if (hero == null || hero.IsBroken) return false;
            
            int scaledDx = (int)(dx * hero.Speed);
            int scaledDy = (int)(dy * hero.Speed);
            Rectangle proposedBounds = new Rectangle(hero.X + scaledDx - 25, hero.Y + scaledDy - 25, 50, 50);

            var walls = _elements.OfType<Wall>();
            foreach (var wall in walls)
            {
                if (wall.GetBounds().IntersectsWith(proposedBounds))
                {
                    return false;
                }
            }

            hero.X += scaledDx;
            hero.Y += scaledDy;
            return true;
        }

        public void UpdateProximityEngine(Hero hero)
        {
            if (hero == null || hero.IsBroken) return;

            var traps = _elements.OfType<Trap>().Where(t => !t.IsBroken).ToList();
            foreach (var trap in traps)
            {
                if (trap.IsWithinActivationRadius(hero.X, hero.Y))
                {
                    trap.Trigger();
                    hero.TakeDamage(trap.Damage);
                }
            }

            var chests = _elements.OfType<TreasureChest>().Where(c => !c.IsOpened).ToList();
            foreach (var chest in chests)
            {
                if (chest.IsWithinActivationRadius(hero.X, hero.Y))
                {
                    chest.Trigger();
                    hero.CollectLoot(chest.RewardItem);
                }
            }
        }
    }
}
