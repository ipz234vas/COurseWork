using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BattleCity.Model
{
    public abstract class TankModel
    {
        public int MaxHP { get; set; }
        public int HP { get; set; }
        public int Speed { get; set; }
        public int BulletSpeed { get; set; }

        public PositionModel Position { get; set; }

        public TankModel(int maxHP, int hP, int speed, int bulletSpeed, PositionModel position)
        {
            MaxHP = maxHP;
            HP = hP;
            Speed = speed;
            BulletSpeed = bulletSpeed;
            Position = position;
        }

        public TankModel() { }

        public void Move(Vector2 Direction)
        {
            Position.XPosition += (int)Direction.X * Speed;
            Position.YPosition += (int)Direction.Y * Speed;
        }
    }
}
