namespace StarBuster.Objects2D
{
    public class Collectible : Object2D
    {
        public enum Kind
        {
            LifeChange,
            SpeedChange,
            DestroyAllEnemies,
            Invisibility
        }

        private Kind _kind;
        private int _change;

        public Collectible(int x, int y, int change) : base(x, y)
        {
            //hw = ?;
            //...zależy od Kind

            _change = change;
        }

        public override void Render(Graphics g)
        {
        }

        public override void Update()
        {
        }
    }
}
