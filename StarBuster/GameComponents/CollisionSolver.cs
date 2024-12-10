using StarBuster.Objects2D;
using System.Collections.Generic;

namespace StarBuster.GameComponents
{
    public class CollisionSolver
    {
        public void ResolveCollisions(List<(Object2D, Object2D)> collisions)
        {
            foreach (var (obj1, obj2) in collisions)
            {
                if (obj1 is Hero && obj2 is Enemy || obj1 is Enemy && obj2 is Hero)
                {
                    (obj1 as Hero).ChangeEnergy(20);
                    CreateExplosion(obj2.x, obj2.y); // Tworzenie eksplozji
                    GameManager.Instance.Remove(obj2);
                }
                else if (obj1 is Bullet && obj2 is Enemy || obj1 is Enemy && obj2 is Bullet)
                {
                    CreateExplosion(obj1.x, obj1.y); // Tworzenie eksplozji dla pocisku
                    GameManager.Instance.Remove(obj1);
                    GameManager.Instance.Remove(obj2);
                }
                if (obj1 is Hero && obj2 is Hunter || obj1 is Hunter && obj2 is Hero)
                {
                    (obj1 as Hero).ChangeEnergy(40);
                    CreateExplosion(obj2.x, obj2.y); 
                    GameManager.Instance.Remove(obj2);
                }
                if (obj1 is Hero && obj2 is AddLive || obj1 is AddLive && obj2 is Hero)
                {
                    (obj1 as Hero).AddEnergy(20);
                    GameManager.Instance.Remove(obj2);
                }
            }
        }

        private void CreateExplosion(int x, int y)
        {
            var explosion = new Explosion(x, y);
            GameManager.Instance.AddObject2D(explosion);
        }
    }
}
