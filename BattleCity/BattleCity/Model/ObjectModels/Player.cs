using BattleCity.Model.UnitModels;
using BattleCity.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCity.Model.ObjectModels
{
    public class Player
    {
        public Tank PlayerTank { get; set; }
        public Star Star { get; set; }
        public int TankLevel { get; set; }
        public int Lifes { get; set; }
        public bool IsSecondPlayer { get; set; }
        public event EventHandler TankCreated;
        public event EventHandler TankRemoved;
        public event EventHandler StarSpawned;
        public event EventHandler StarRemoved;

        public Player(bool is2Player = false)
        {
            IsSecondPlayer = is2Player;
        }
        public void CreateTank()
        {
            int x = IsSecondPlayer ? GameConfiguration.XFor2Player : GameConfiguration.XFor1Player;
            int y = GameConfiguration.YForPlayer;

            PlayerTank = new Tank(BaseObjectModel.NextID, x, y, GameConfiguration.TankWidth, GameConfiguration.TankHeight, TypeObject.SmallTankPlayer, 5, 5);

            Star = new Star(BaseObjectModel.NextID, x, y, GameConfiguration.TankWidth, GameConfiguration.TankHeight, TypeObject.Star, PlayerTank);
            Star.StarSpawned += StarSpawned;
            Star.StarDeleted += StarRemoved;
            Star.CurrentTank.TankCreated += TankCreated;
            Star.CurrentTank.TankDeleted += TankRemoved;
            Star.Spawn();
        }

        public void DestroyTank(object? sender, EventArgs e)
        {
            if (TankLevel > 1) TankLevel--;
            TankRemoved?.Invoke(this, EventArgs.Empty);
        }
    }
}
