using BattleCity.Model.UnitModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCity.Services
{
	public static class CheckCollisionService
	{
		public static ObservableCollection<Unit> units = new ObservableCollection<Unit>();
		public static bool CheckCollision(Unit obj1, Unit obj2)
		{
			if (obj1.BoundingBox.IntersectsWith(obj2.BoundingBox))
				return true;
			return false;
		}

		public static bool CheckCollisionWithBorder(Unit obj)
		{
			if (obj.X < 0 || obj.Y < 0 || obj.X + obj.Width > GameConfiguration.SceneWidth || obj.Y + obj.Height > GameConfiguration.SceneHeight) return true;
			return false;
		}
	}
}
