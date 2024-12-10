using StarBuster.GameComponents;
using System;
using System.Drawing;

namespace StarBuster.Objects2D
{
    public class Hunter : Enemy
    {
        // Konstruktor z ustawieniem szerokości i wysokości
        public Hunter(int x, int y) : base(x, y)
        {
            _hw = 25;  // Szerokość
            _hh = 25;  // Wysokość
        }

        public override void Render(Graphics g)
        {
            // Ustawiamy tryb antyaliasing dla lepszej jakości rysowania
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Rysowanie ciała Huntera (główna elipsa)
            g.FillEllipse(Brushes.Red, new Rectangle(x - 30, y - 10, 60, 20));

            // Rysowanie głowy (mniejsza elipsa na górze)
            g.FillEllipse(Brushes.LightBlue, new Rectangle(x - 15, y - 25, 30, 20));

            // Rysowanie promieni (6 żółtych kółek wokół Huntera)
            DrawProjectiles(g);
        }

        // Metoda rysująca promienie wokół Huntera
        private void DrawProjectiles(Graphics g)
        {
            for (int i = 0; i < 6; i++)
            {
                // Obliczanie kąta w zależności od iteracji
                double angle = 2.0 * Math.PI * i / 6.0;

                // Obliczanie pozycji promieni na podstawie kąta
                double px = x + 30.0 * Math.Cos(angle);
                double py = y + 15.0 * Math.Sin(angle);

                // Rysowanie promienia (żółte kółko)
                g.FillEllipse(Brushes.Yellow, (int)(px - 5), (int)(py - 5), 10, 10);
            }
        }
    }
}
