using BattleCity.Model.UnitModels;
using BattleCity.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCity.Model.ObjectModels
{
    public abstract class UpdatableObject : BaseObjectModel
    {
        public UpdatableObject(int id, int x, int y, int width, int height, TypeObject type) : base(id, x, y, width, height, type)
        {
        }
        public abstract void Spawn();
        public abstract void Update(object? sender, EventArgs e);
    }
}
