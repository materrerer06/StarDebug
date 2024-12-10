using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using StarBuster.GameComponents;

namespace StarBuster.Objects2D
{
    public class Hero : Object2D
    {
        public int _energy;
        private int _ShotDelay;

        public Hero(int x, int y) : base(x, y)
        {
            _hw = 20;
            _hh = 10;

            _ShotDelay = 0;
            _energy = 100;
        }

        public override void Render(Graphics g)
        {

            DrawHero(g);
            DrawFireEffect(g);
            DrawEnergy(g);
            // TODO: inne vizualne efekty, np. niewidzialność, tarcza, itp.
        }

        public void ChangeEnergy(int aValue)
        {
            _energy -= aValue;
        }
        public void AddEnergy(int aValue)
        {
            if (_energy >= 100)
            {
                aValue = 0;
            }
            if (_energy >= 80)
            {
                aValue = 100 - _energy;
                _energy += aValue;
            }
            else
            {
                _energy += aValue;
            }
        }
        public override void Update()
        {
            HandleMovement();
            HandleShooting();
            ApplyMovementLimits();
            // TODO: inne fizyczne efekty, np. przyciąganie przez magnes bossa
        }

        // Rysuje bohatera
        private void DrawHero(Graphics g)
        {
            g.FillEllipse(Brushes.DodgerBlue, new Rectangle(x - 30, y - 7, 60, 14));
            g.FillEllipse(Brushes.DodgerBlue, new Rectangle(x - 20, y - 20, 12, 40));
            g.FillEllipse(Brushes.White, new Rectangle(x - 5, y - 4, 10, 8));
        }

        // Rysuje efekt ognia za bohaterem
        private void DrawFireEffect(Graphics g)
        {
            for (int i = 0; i < 10; i++)
            {
                var px = x - _r.Next() % 20 + 4;
                var py = y + _r.Next() % 7 - 3;
                Point pt1 = new Point(x - 26, py);
                Point pt2 = new Point(px - 26, py);

                g.DrawLine(Pens.Orange, pt1, pt2);
            }
        }
        private void DrawEnergy(Graphics g)
        {
            int energy_width = _energy * 3;
            g.DrawRectangle(Pens.White, 29, 19, 302, 32);
            g.FillRectangle(Brushes.Red, 30, 20, energy_width, 30);
        }

        // Obsługuje ruch bohatera na podstawie klawiszy
        private void HandleMovement()
        {
            var keyboard = GameManager.Instance.KeySet;

            if (keyboard.Contains(Keys.Up)) y -= 5;
            else if (keyboard.Contains(Keys.Down)) y += 5;

            if (keyboard.Contains(Keys.Left)) x -= 5;
            else if (keyboard.Contains(Keys.Right)) x += 5;
        }

        // Obsługuje strzelanie i czas odnowienia dla bohatera
        private void HandleShooting()
        {
            var gm = GameManager.Instance;

            if (gm.KeySet.Contains(Keys.Space) && _ShotDelay == 0)
            {
                gm.AddObject2D(new Bullet(x + 20, y));
                _ShotDelay = 10; // Czas odnowienia strzału
            }

            if (_ShotDelay > 0) _ShotDelay--;
        }

        // Ogranicza ruch bohatera do granic ekranu gry
        private void ApplyMovementLimits()
        {
            var gm = GameManager.Instance;

            if (x < 10) x = 10;
            else if (x > gm.Width / 4) x = gm.Width / 4;

            if (y < 10) y = 10;
            else if (y > gm.Height - 10) y = gm.Height - 10;
        }
    }
}
