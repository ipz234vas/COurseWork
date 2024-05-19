using BattleCity.Model.UnitModels;
using BattleCity.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCity.Model.ObjectModels
{
    public class Player : Owner
    {
        public int TankLevel { get; set; } = GameConfiguration.BasePlayerTankLevel;
        public int Lifes { get; set; } = GameConfiguration.BasePlayerTankLifes;
        public bool IsSecondPlayer { get; set; }
        public event Action PlayerDied;

        public Player(bool is2Player = false)
        {
            IsSecondPlayer = is2Player;
        }
        public void CreateTank()
        {
            int x = IsSecondPlayer ? GameConfiguration.XFor2Player : GameConfiguration.XFor1Player;
            int y = GameConfiguration.YForPlayer;

            base.CreateTank(x, y, TypeObject.SmallTankPlayer);
            Tank.TankDeleted += DestroyTank;
        }

        public new void DestroyTank(object? sender, EventArgs e)
        {
            if (TankLevel > 1) TankLevel--;
            base.DestroyTank();
            Lifes--;
            if (Lifes <= 0) PlayerDied?.Invoke();
            else CreateTank();
        }
    }
}
