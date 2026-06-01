using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace architectSteps
{
    public class StatsForm : Form
    {
        private DungeonScene _scene;
        private Label _heroStatusLabel;
        private Label _wallCountLabel;
        private Label _trapCountLabel;
        private Label _treasureCountLabel;
        private Label _inventoryLabel;
        private Label _advancedStatsLabel;
        private Timer _refreshTimer;

        public StatsForm(DungeonScene scene)
        {
            _scene = scene;
            SetupUI();
            
            _refreshTimer = new Timer();
            _refreshTimer.Interval = 100;
            _refreshTimer.Tick += (s, e) => UpdateStats();
            _refreshTimer.Start();
        }

        private void SetupUI()
        {
            this.Text = "Dungeon Statistics";
            this.Size = new Size(300, 250);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            int currentY = 20;

            _heroStatusLabel = new Label { Location = new Point(20, currentY), Size = new Size(250, 25), Font = new Font("Arial", 10, FontStyle.Bold) };
            this.Controls.Add(_heroStatusLabel);
            currentY += 40;

            _wallCountLabel = new Label { Location = new Point(20, currentY), Size = new Size(250, 25), Font = new Font("Arial", 10) };
            this.Controls.Add(_wallCountLabel);
            currentY += 30;

            _trapCountLabel = new Label { Location = new Point(20, currentY), Size = new Size(250, 25), Font = new Font("Arial", 10) };
            this.Controls.Add(_trapCountLabel);
            currentY += 30;

            _treasureCountLabel = new Label { Location = new Point(20, currentY), Size = new Size(250, 25), Font = new Font("Arial", 10) };
            this.Controls.Add(_treasureCountLabel);
            currentY += 30;

            _inventoryLabel = new Label { Location = new Point(20, currentY), Size = new Size(250, 40), Font = new Font("Arial", 10, FontStyle.Italic) };
            this.Controls.Add(_inventoryLabel);
            currentY += 45;

            _advancedStatsLabel = new Label { Location = new Point(20, currentY), Size = new Size(300, 150), Font = new Font("Arial", 9) };
            this.Controls.Add(_advancedStatsLabel);

            this.Size = new Size(350, 480);

            UpdateStats();
        }

        private void UpdateStats()
        {
            var hero = _scene.Elements.OfType<Hero>().FirstOrDefault();
            if (hero == null)
            {
                _heroStatusLabel.Text = "Hero: Not Present";
                _heroStatusLabel.ForeColor = Color.Gray;
                _inventoryLabel.Text = "Inventory: None";
            }
            else
            {
                string status = hero.IsBroken ? "Dead" : "Alive";
                _heroStatusLabel.Text = $"Hero: {status} (HP: {hero.HealthPoints})";
                _heroStatusLabel.ForeColor = hero.IsBroken ? Color.Red : Color.Green;
                string items = hero.Inventory.Count > 0 ? string.Join(", ", hero.Inventory) : "Empty";
                _inventoryLabel.Text = $"Inventory: {items}";
            }

            int walls = _scene.Elements.OfType<Wall>().Count();
            int traps = _scene.Elements.OfType<Trap>().Count();
            int brokenTraps = _scene.Elements.OfType<Trap>().Count(t => t.IsBroken);
            int chests = _scene.Elements.OfType<TreasureChest>().Count();
            int openedChests = _scene.Elements.OfType<TreasureChest>().Count(c => c.IsOpened);

            _wallCountLabel.Text = $"Walls: {walls}";
            _trapCountLabel.Text = $"Traps: {traps} ({brokenTraps} triggered)";
            _treasureCountLabel.Text = $"Treasures: {chests} ({openedChests} opened)";

            // LINQ Operations Requirement
            
            // 1. Filtering (Where) & Aggregation (Sum)
            int totalActiveDurability = _scene.Elements.Where(e => !e.IsBroken).Sum(e => e.Durability);

            // 2. Filtering (OfType) & Aggregation (Max)
            var allTraps = _scene.Elements.OfType<Trap>();
            int maxTrapDamage = allTraps.Any() ? allTraps.Max(t => t.Damage) : 0;

            // 3. Grouping (GroupBy) & Projection (Select)
            var elementCountsByType = _scene.Elements
                .GroupBy(e => e.GetType().Name)
                .Select(g => $"{g.Key}s: {g.Count()}")
                .ToList();

            // 4. Sorting (OrderByDescending) & Projection (Select)
            var topDurableItems = _scene.Elements
                .Where(e => !e.IsBroken)
                .OrderByDescending(e => e.Durability)
                .Take(3)
                .Select(e => e.GetType().Name)
                .ToList();

            string advancedText = "--- Advanced LINQ Stats ---\n";
            advancedText += $"Active Durability Sum: {totalActiveDurability}\n";
            advancedText += $"Max Trap Damage: {maxTrapDamage}\n";
            advancedText += $"Element Types:\n  " + string.Join("\n  ", elementCountsByType) + "\n";
            advancedText += $"Top Durable: " + string.Join(", ", topDurableItems);

            _advancedStatsLabel.Text = advancedText;
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _refreshTimer.Stop();
            _refreshTimer.Dispose();
            base.OnFormClosed(e);
        }
    }
}
