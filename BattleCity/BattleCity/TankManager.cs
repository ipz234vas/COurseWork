using BattleCity.Controller;
using BattleCity.Model;
using BattleCity.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BattleCity
{
    public class TankManager
    {
        private Grid _parentPanel;
        public TankManager(Grid parentPanel)
        {
            _parentPanel = parentPanel;
            SpawnEnemyTank();
        }

        protected void SpawnTank(TankModel newTank, TankView newTankView)
        {
            TankController newTankController = new TankController(newTank, newTankView);
        }

        public void SpawnEnemyTank()
        {
            TankModel newEnemy = new EnemyTankModel(200, 20, 10, 30, 20, new PositionModel(1000, 200));

            EnemyTankView newEnemyTankView = new EnemyTankView(_parentPanel);

            SpawnTank(newEnemy, newEnemyTankView);
        }

    }
}
