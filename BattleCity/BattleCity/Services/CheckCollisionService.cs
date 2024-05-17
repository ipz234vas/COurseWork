using BattleCity.Model.UnitModels;
using BattleCity.Types;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCity.Services
{
	public static class CheckCollisionService
	{
		public static ObservableCollection<BaseObjectModel> objects = new ObservableCollection<BaseObjectModel>();
		public static bool CheckCollision(BaseObjectModel obj1, BaseObjectModel obj2)
		{
			if (obj1.BoundingBox.IntersectsWith(obj2.BoundingBox))
				return true;
			return false;
		}
        public static bool CheckCollision(BaseObjectModel obj, Rectangle BoundingBox)
        {
            if (obj.BoundingBox.IntersectsWith(BoundingBox))
                return true;
            return false;
        }
        public static bool CheckCollision(Rectangle BoundingBox1, Rectangle BoundingBox2)
        {
            if (BoundingBox1.IntersectsWith(BoundingBox2))
                return true;
            return false;
        }
        public static bool CheckCollisionWithBorder(BaseObjectModel obj)
		{
			if (obj.X < 0 || obj.Y < 0 || obj.X + obj.Width > GameConfiguration.SceneWidth || obj.Y + obj.Height > GameConfiguration.SceneHeight) return true;
			return false;
		}
	}
}
