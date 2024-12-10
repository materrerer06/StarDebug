using StarBuster.Objects2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarBuster.GameComponents
{
    public static class Object2DSpawner
    {
        private static Random _random = new Random();
        public static void Update(int aFrameIndex)
        { 
       int randomY = _random.Next(0, GameManager.Instance.Height); 
            if(aFrameIndex % 50 == 30)
            {
                GameManager.Instance.AddObject2D(new Enemy(GameManager.Instance.Width, randomY));
                
            }
            if (aFrameIndex % 300 == 30)
            {
                GameManager.Instance.AddObject2D(new AddLive(GameManager.Instance.Width, randomY));
            }
            if (aFrameIndex % 500 == 50) 
            {
                
                GameManager.Instance.AddObject2D(new Hunter(GameManager.Instance.Width, randomY));
            }
        }
    }
}
