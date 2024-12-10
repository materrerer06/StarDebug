namespace StarBuster.Objects2D
{
    public class Bullet : Object2D
    {
        private int _speed;

        public Bullet(int x, int y, int speed = 12) : base(x, y)
        {
            _hw = 8;
            _hh = 2;

            _speed = speed;
        }

        public override void Render(Graphics g)
        {
            g.FillRectangle(Brushes.Aquamarine, new Rectangle(x - 8, y - 1, 16, 3));
        }

        public override void Update()
        {
            x += _speed;
        }
    }
}
