using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BattleCity
{
	public static class GameConfiguration
	{
		public const char BrickWallSymbol = '@';
		public const char ConcreteWallSymbol = '#';
		public const char EmptySpaceSymbol = ' ';

		public static int UnitWidth = 20;
		public static int UnitHeight = 20;

		public static int TankWidth = 40;
		public static int TankHeight = 40;

		public static int SceneWidth = 26 * UnitWidth;
		public static int SceneHeight = 26 * UnitHeight;

		public static int UserControlWidth = 31 * UnitWidth;
		public static int UserControHeight = 28 * UnitHeight;

		public static int Interval = 20;

		public static Key KeyUp1Player = Key.W;
		public static Key KeyDown1Player = Key.S;
		public static Key KeyLeft1Player = Key.A;
		public static Key KeyRight1Player = Key.D;
		public static Key KeyUp2Player = Key.Up;
		public static Key KeyDown2Player = Key.Down;
		public static Key KeyLeft2Player = Key.Left;
		public static Key KeyRight2Player = Key.Right;
	}
}
