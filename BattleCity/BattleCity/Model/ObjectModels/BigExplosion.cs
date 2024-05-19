using BattleCity.Interfaces;
using BattleCity.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace BattleCity.Model.ObjectModels
{
    public class BigExplosion : UpdatableObject, IPausable
    {
        public DispatcherTimer Timer;
        public event EventHandler BigExplosionSpawned;
        public event EventHandler BigExplosionDeleted;
        public BigExplosion(int id, int x, int y, int width, int height, TypeObject type = TypeObject.BigExplosion) : base(id, x, y, width, height, type)
        {
            Timer = new DispatcherTimer();
            Timer.Tick += Update;
            Timer.Interval = TimeSpan.FromSeconds(GameConfiguration.SpawnIntervalInSeconds);
        }

        public override void Spawn()
        {
            Timer.Start();
            BigExplosionSpawned?.Invoke(this, EventArgs.Empty);
        }

        public override void Update(object? sender, EventArgs e)
        {
            Timer.Stop();
            Delete();
        }
        public void Delete()
        {
            BigExplosionDeleted?.Invoke(this, EventArgs.Empty);
        }
        public void Pause()
        {
            Timer.Stop();
        }
        public void Resume()
        {
            Timer.Start();
        }
    }
}
