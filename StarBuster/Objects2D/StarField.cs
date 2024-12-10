namespace StarBuster.Objects2D
{
    public class StarField : Object2D
    {
        private int _width;
        private int _height;
        private List<Point> _stars;

        public StarField(int aCount, int aWidth, int aHeight) : base(0, 0)
        {
            _hw = 0;
            _hh = 0;

            _width = aWidth;
            _height = aHeight;
            _stars = new List<Point>();

            Random random = new Random();

            for (int i = 0; i < aCount; i++)
            {
                int x = random.Next(0, _width);
                int y = random.Next(0, _height);

                _stars.Add(new Point(x, y));
            }
        }

        public override void Render(Graphics g)
        {
            foreach (Point p in _stars)
            {
                g.FillEllipse(Brushes.White, new Rectangle(p.X - 2, p.Y - 2, 4, 4));
            }
        }

        public override void Update()
        {
            for (int i = 0; i < _stars.Count; i++)
            {
                int nx = _stars[i].X - 3;
                if (nx < 0) nx = _width - 1;
                _stars[i] = new Point(nx, _stars[i].Y);
            }
        }
    }
}
