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
    public class Star : UpdatableObject, IPausable
    {
        public Tank CurrentTank;
        public DispatcherTimer Timer;
        public event EventHandler StarSpawned;
        public event EventHandler StarDeleted;
        public Star(int id, int x, int y, int width, int height, Tank currentTank, TypeObject type = TypeObject.Star) : base(id, x, y, width, height, type)
        {
            CurrentTank = currentTank;
            Timer = new DispatcherTimer();
            Timer.Tick += Update;
            Timer.Interval = TimeSpan.FromSeconds(GameConfiguration.SpawnIntervalInSeconds);
        }

        public override void Spawn()
        {
            Timer.Start();
            StarSpawned?.Invoke(this, EventArgs.Empty);
        }
        public override void Update(object? sender, EventArgs e)
        {
            Timer.Stop();
            Delete();
        }
        public void Delete()
        {
            StarDeleted?.Invoke(this, EventArgs.Empty);
            CurrentTank.Create();
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
