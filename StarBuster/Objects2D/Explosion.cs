using StarBuster.Objects2D;
using System.Drawing;

namespace StarBuster.GameComponents
{
    public class Explosion : Object2D
    {
        private int _lifetime;
        private int _currentTime;

        public Explosion(int x, int y, int lifetime = 3) : base(x, y)
        {
            _lifetime = lifetime;
            _currentTime = 0;
        }

        public override void Render(Graphics g)
        {
            float size = 50;

            Brush brush = new SolidBrush(Color.FromArgb(255, 255, 165, 0));
            
            g.FillEllipse(brush, x - size / 2, y - size / 2, size, size);
            
        }

        public override void Update()
        {
            _currentTime++;

            if (_currentTime >= _lifetime)
            {
                GameManager.Instance.Remove(this);
            }
        }
    }
}
