using BattleCity.Model.UnitModels;
using BattleCity.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCity.Model.ObjectModels
{
    public class Eagle : BaseObjectModel
    {
        public event Action EagleDestroyed;
        public event EventHandler BigExplosionSpawned;
        public event EventHandler BigExplosionDeleted;
        public BigExplosion bigExplosion;
        public Eagle(int id, int x, int y, int width, int height, TypeObject type = TypeObject.Eagle) : base(id, x, y, width, height, type) 
        {
            Properties[PropertiesType.Detonation] = false;
            bigExplosion = new BigExplosion(BaseObjectModel.NextID, x, y, width, height);
            bigExplosion.BigExplosionSpawned += OnBigExplosionSpawned;
            bigExplosion.BigExplosionDeleted += OnBigExplosionDeleted;
        }
        public void Destroyed()
        {
            Properties[PropertiesType.Detonation] = true;
            EagleDestroyed?.Invoke();
            bigExplosion.Spawn();
        }
        private void OnBigExplosionSpawned(object? sender, EventArgs e)
        {
            BigExplosionSpawned?.Invoke(bigExplosion, e);
        }

        private void OnBigExplosionDeleted(object? sender, EventArgs e)
        {
            BigExplosionDeleted?.Invoke(bigExplosion, e);
        }
    }
}
