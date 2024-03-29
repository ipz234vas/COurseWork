using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BattleCity.Model
{
    public class BulletModel
    {
        public int Speed { get; set; }
        public Vector2 Direction { get; set; }

        public BulletModel() { }
        public BulletModel(int speed, Vector2 direction)
        {
            Speed = speed;
            Direction = direction;
        }
    }
}
