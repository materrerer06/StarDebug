namespace StarBuster.Objects2D
{
    public abstract class Object2D
    {
        public int x;   // pozycja na osi x obiektu
        public int y;   // pozycja na osi y obiektu

        protected int _hw; // połowa wysokości prostokąta ograniczającego (bounding box)
        protected int _hh; // połowa szerokości prostokąta ograniczającego (bounding box)
        protected Random _r;

        public Object2D(int x, int y)
        {
            this.x = x;
            this.y = y;

            _r = new Random();
        }

        public abstract void Render(Graphics g);
        public abstract void Update();

        public virtual bool Intersects(Object2D other)
        {
            return Math.Abs(this.x - other.x) < this._hw + other._hw &&
                   Math.Abs(this.y - other.y) < this._hh + other._hh;
        }

        public virtual bool IsOutOfScreen(int screenWidth, int screenHeight)
        {
            return x + _hw < 0 || x - _hw > screenWidth ||
                   y + _hh < 0 || y - _hh > screenHeight;
        }
    }
}
