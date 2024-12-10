using StarBuster.GameComponents;
using System.Drawing;

namespace StarBuster.Objects2D
{
    public class AddLive : Object2D
    {
        int dir = 5;
        int _frame = 0;

        public AddLive(int x, int y) : base(x, y)
        {
            _hw = 25;
            _hh = 25;
        }

        public override void Render(Graphics g)
        {
            // Rysowanie tła apteczki (biały prostokąt)
            g.FillRectangle(Brushes.White, new Rectangle(x - 20, y - 20, 40, 40));

            // Pionowa część krzyża
            g.FillRectangle(Brushes.Red, new Rectangle(x - 5, y - 20, 10, 40));

            // Pozioma część krzyża
            g.FillRectangle(Brushes.Red, new Rectangle(x - 20, y - 5, 40, 10));
        }

        public override void Update()
        {
            if (y < 10 || y > GameManager.Instance.Height - 10) dir = -dir;
            x -= 6;
            y -= dir;
        }
    }
}

