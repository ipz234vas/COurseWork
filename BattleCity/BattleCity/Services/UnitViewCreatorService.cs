using BattleCity.Types;
using BattleCity.View.UnitViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace BattleCity.Services
{
	public static class UnitViewCreatorService
	{
		public static UnitView Create(int id, int x, int y, int width, int height, TypeUnit typeUnit, IDictionary<PropertiesType, object> properties)
		{
			UnitView view = null;

			switch (typeUnit)
			{
				case TypeUnit.BrickWall:
					view = new UnitView { Source = new BitmapImage(new Uri(@"../../Resources/Images/Blocks/BrickWall.png", UriKind.Relative)) };
					break;
				case TypeUnit.ConcreteWall:
					view = new UnitView { Source = new BitmapImage(new Uri(@"../../Resources/Images/Blocks/ConcreteWall.png", UriKind.Relative)) };
					break;
				case TypeUnit.SmallTankPlayer:
					view = new UnitView { Source = new BitmapImage(new Uri(@"../../Resources/Images/Tanks/Tank.png", UriKind.Relative)) };
					break;
				default:
					view = new UnitView();
					break;
			}
			view.ID = id;
			view.X = x;
			view.Y = y;
			view.Width = width;
			view.Height = height;
			view.TypeUnit = typeUnit;
			return view;
		}
	}
}
