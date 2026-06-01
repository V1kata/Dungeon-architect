using System.Drawing;
using System.Drawing.Drawing2D;
using architectSteps;

namespace architectSteps
{
    public class DungeonRenderer
    {
        public void DrawScene(Graphics g, DungeonScene scene)
        {
            foreach (var element in scene.Elements)
            {
                DrawElement(g, element);
            }
        }

        public void DrawElement(Graphics g, DungeonElement element)
        {
            if (element is Hero hero)
            {
                Brush brush = new SolidBrush(hero.IsBroken ? Color.Gray : hero.FillColor);
                g.FillEllipse(brush, hero.X - 15, hero.Y - 15, 30, 30);
                Pen pen = new Pen(Color.DarkGreen, 2);
                g.DrawEllipse(pen, hero.X - 15, hero.Y - 15, 30, 30);
                if (hero.HealthPoints > 0)
                {
                    string healthText = $"{hero.HealthPoints}";
                    Font font = new Font("Arial", 8);
                    g.DrawString(healthText, font, Brushes.White, hero.X - 10, hero.Y - 5);
                }
                brush.Dispose();
                pen.Dispose();
            }
            else if (element is Trap trap)
            {
                Brush brush = new SolidBrush(trap.IsBroken ? Color.DarkGray : trap.FillColor);
                g.FillRectangle(brush, trap.X - 15, trap.Y - 15, 30, 30);
                Pen pen = new Pen(Color.DarkRed, 2);
                g.DrawRectangle(pen, trap.X - 15, trap.Y - 15, 30, 30);
                if (!trap.IsBroken)
                {
                    Pen radiusPen = new Pen(Color.Orange, 1) { DashStyle = DashStyle.Dash };
                    g.DrawEllipse(radiusPen, trap.X - trap.ActivationRadius, trap.Y - trap.ActivationRadius, trap.ActivationRadius * 2, trap.ActivationRadius * 2);
                    radiusPen.Dispose();
                }
                brush.Dispose();
                pen.Dispose();
            }
            else if (element is TreasureChest chest)
            {
                Brush brush = new SolidBrush(chest.IsOpened ? Color.DarkGoldenrod : chest.FillColor);
                g.FillRectangle(brush, chest.X - 15, chest.Y - 15, 30, 30);
                Pen pen = new Pen(Color.DarkGoldenrod, 2);
                g.DrawRectangle(pen, chest.X - 15, chest.Y - 15, 30, 30);
                Pen lidPen = new Pen(Color.Yellow, 2);
                if (chest.IsOpened)
                {
                    g.DrawLine(lidPen, chest.X - 15, chest.Y - 8, chest.X + 15, chest.Y - 8);
                }
                else
                {
                    g.DrawLine(lidPen, chest.X - 15, chest.Y - 12, chest.X, chest.Y - 20);
                    g.DrawLine(lidPen, chest.X + 15, chest.Y - 12, chest.X, chest.Y - 20);
                }
                brush.Dispose();
                pen.Dispose();
                lidPen.Dispose();
            }
            else if (element is Wall wall)
            {
                Brush brush = new SolidBrush(wall.IsBroken ? Color.DarkGray : wall.FillColor);
                g.FillRectangle(brush, wall.X - wall.Width / 2, wall.Y - wall.Height / 2, wall.Width, wall.Height);
                Pen brickPen = new Pen(Color.Black, 1);
                for (int i = 0; i <= wall.Height; i += 10)
                    g.DrawLine(brickPen, wall.X - wall.Width / 2, wall.Y - wall.Height / 2 + i, wall.X + wall.Width / 2, wall.Y - wall.Height / 2 + i);
                for (int i = 0; i <= wall.Width; i += 10)
                    g.DrawLine(brickPen, wall.X - wall.Width / 2 + i, wall.Y - wall.Height / 2, wall.X - wall.Width / 2 + i, wall.Y + wall.Height / 2);
                Pen outlinePen = new Pen(Color.Black, 2);
                g.DrawRectangle(outlinePen, wall.X - wall.Width / 2, wall.Y - wall.Height / 2, wall.Width, wall.Height);
                brush.Dispose();
                brickPen.Dispose();
                outlinePen.Dispose();
            }
        }
    }
}
