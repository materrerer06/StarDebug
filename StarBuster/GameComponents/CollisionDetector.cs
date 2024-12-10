using System.Collections.Generic;
using StarBuster.Objects2D;

namespace StarBuster.GameComponents
{
    public class CollisionDetector
    {
        private List<Object2D> _objects;

        public CollisionDetector(List<Object2D> objects)
        {
            _objects = objects;
        }

        // Metoda wykrywająca kolizje i zwracająca listę par kolidujących obiektów
        public List<(Object2D, Object2D)> DetectCollisions()
        {
            var collisions = new List<(Object2D, Object2D)>();

            for (int i = 0; i < _objects.Count; i++)
            {
                for (int j = i + 1; j < _objects.Count; j++)
                {
                    if (_objects[i].Intersects(_objects[j]))
                    {
                        collisions.Add((_objects[i], _objects[j]));
                    }
                }
            }

            return collisions;
        }
    }
}
