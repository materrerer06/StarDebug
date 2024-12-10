using StarBuster.GameComponents;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace StarBuster.Objects2D
{
    public class Enemy : Object2D
    {
        private int _direction = 5;  // Kierunek ruchu w pionie
        private double _angle;       // Kąt rotacji
        private int _frame = 0;      // Zmienna do animacji

        // Konstruktor inicjujący wymiary obiektu
        public Enemy(int x, int y) : base(x, y)
        {
            _hw = 25;  // Szerokość
            _hh = 15;  // Wysokość
            _angle = 0;
        }

        public override void Render(Graphics g)
        {
            // Ustawiamy tryb antyaliasing dla płynniejszego rysowania
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Rysowanie ciała z gradientem
            DrawBody(g);

            // Rysowanie głowy z efektem świetlnym
            DrawHead(g);

            // Rysowanie promieni (projektów) z efektem rozbłysku
            DrawProjectiles(g);
        }

        private void DrawBody(Graphics g)
        {
            // Użycie gradientu wypełniającego ciało
            using (LinearGradientBrush brush = new LinearGradientBrush(
                new Point(x - 30, y - 10), new Point(x + 30, y + 10),
                Color.Silver, Color.Gray))
            {
                g.FillEllipse(brush, new Rectangle(x - 30, y - 10, 60, 20));
            }
        }

        private void DrawHead(Graphics g)
        {
            // Głowa z efektem świetlnym (jasnoniebieska z cieniem)
            using (PathGradientBrush headBrush = new PathGradientBrush(
                new Point[] { new Point(x - 15, y - 25), new Point(x + 15, y - 25),
                             new Point(x, y - 5) }))
            {
                headBrush.CenterColor = Color.LightBlue;
                headBrush.SurroundColors = new Color[] { Color.DarkBlue };
                g.FillEllipse(headBrush, new Rectangle(x - 15, y - 25, 30, 20));
            }
        }

        private void DrawProjectiles(Graphics g)
        {
            // Rysowanie promieni wokół obiektu z lekkim rozbłyskiem
            for (int i = 0; i < 6; i++)
            {
                // Obliczanie kąta i pozycji każdego promienia
                double angle = 2.0 * Math.PI * i / 6.0;
                double px = x + 30.0 * Math.Cos(angle);
                double py = y + 15.0 * Math.Sin(angle);

                // Rysowanie promieni (żółte kółka z efektem rozbłysku)
                using (Brush glowBrush = new SolidBrush(Color.FromArgb(255, 255, 150, 50)))
                {
                    g.FillEllipse(glowBrush, (int)(px - 5), (int)(py - 5), 10, 10);
                }

                // Dodanie promienia z efektem połysku
                using (Pen shinePen = new Pen(Color.FromArgb(255, 255, 220, 120), 2))
                {
                    g.DrawEllipse(shinePen, (int)(px - 6), (int)(py - 6), 12, 12);
                }
            }
        }

        public override void Update()
        {
            // Zwiększanie kąta w celu rotacji obiektu
            _angle += 0.1;

            // Zmiana kierunku ruchu w pionie, jeżeli dotarł do granic ekranu
            if (y < 10 || y > GameManager.Instance.Height - 10)
                _direction = -_direction;

            // Ruch obiektu w lewo i w pionie
            x -= 6;   // Ruch w poziomie
            y += _direction;  // Ruch w pionie z określonym kierunkiem
        }
    }
}
