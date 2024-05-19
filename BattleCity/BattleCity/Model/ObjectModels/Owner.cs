using BattleCity.Model.UnitModels;
using BattleCity.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCity.Model.ObjectModels
{
    public abstract class Owner
    {
        public Tank Tank { get; protected set; }
        public Star Star { get; protected set; }

        public event EventHandler TankCreated;
        public event EventHandler TankInizializated;
        public event EventHandler TankRemoved;
        public event EventHandler BulletCreated;
        public event EventHandler BulletRemoved;
        public event EventHandler StarSpawned;
        public event EventHandler StarRemoved;

        public virtual void CreateTank(int x, int y, TypeObject tankType)
        {
            Tank = new Tank(BaseObjectModel.NextID, x, y, GameConfiguration.TankWidth, GameConfiguration.TankHeight, tankType);
            TankInizializated?.Invoke(this, EventArgs.Empty);
            Star = new Star(BaseObjectModel.NextID, x, y, GameConfiguration.TankWidth, GameConfiguration.TankHeight, Tank);

            Star.StarSpawned += StarSpawned;
            Star.StarDeleted += StarRemoved;
            Star.StarDeleted += OnStarRemoved;
            Tank.TankCreated += TankCreated;
            Tank.TankDeleted += TankRemoved;
            Tank.BulletCreated += BulletCreated;
            Tank.BulletDeleted += BulletRemoved;

            Star.Spawn();
        }

        public void DestroyTank()
        {
            if (Tank != null)
            {
                TankRemoved?.Invoke(this, EventArgs.Empty);
            }
        }
        public void OnStarRemoved(object? sender, EventArgs e)
        {
            Star = null;
        }
    }

}