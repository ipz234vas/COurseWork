using BattleCity.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace BattleCity.Services
{
	public static class TypeUnitService
	{
		public static TypeUnit GetTypeUnitBySymbol(char symbol)
		{
			TypeUnit type = 0;
			switch (symbol)
			{
				case GameConfiguration.BrickWallSymbol:
					type = TypeUnit.BrickWall;
					break;
				case GameConfiguration.ConcreteWallSymbol:
					type = TypeUnit.ConcreteWall;
					break;
				default:
					break;
			}
			return type;
		}
	}
}
