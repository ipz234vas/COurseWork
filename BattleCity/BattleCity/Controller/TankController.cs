using BattleCity.Model;
using BattleCity.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCity.Controller
{
    class TankController
    {
        public TankModel Tank;
        public TankView TankView;

        public TankController(TankModel tank, TankView tankView)
        {
            Tank = tank;
            TankView = tankView;
            Tank.Position._position += TankView.InPosition;
        }
    }
}
