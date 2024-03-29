using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCity.Model
{
    public class EnemyTankModel : TankModel
    {
        public int Points { get; set; }

        public EnemyTankModel() { }

        public EnemyTankModel(int points, int maxHP, int hP, int speed, int bulletSpeed, PositionModel position) : base(maxHP, hP, speed, bulletSpeed, position)
        { 
            Points = points;
        }
    }
}
