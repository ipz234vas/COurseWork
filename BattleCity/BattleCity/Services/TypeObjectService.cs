using BattleCity.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace BattleCity.Services
{
	public static class TypeObjectService
	{
		public static TypeObject GetTypeUnitBySymbol(char symbol)
		{
			TypeObject type = 0;
			switch (symbol)
			{
				case GameConfiguration.BrickWallSymbol:
					type = TypeObject.BrickWall;
					break;
				case GameConfiguration.ConcreteWallSymbol:
					type = TypeObject.ConcreteWall;
					break;
				default:
					break;
			}
			return type;
		}
	}
}
