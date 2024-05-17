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

		public static int ObjectWidth = 20;
		public static int ObjectHeight = 20;

		public static int TankWidth = 40;
		public static int TankHeight = 40;

		public static int SceneWidth = 26 * ObjectWidth;
		public static int SceneHeight = 26 * ObjectHeight;

		public static int UserControlWidth = 31 * ObjectWidth;
		public static int UserControHeight = 28 * ObjectHeight;

		public static int MainInterval = 20;
		public static int SpawnIntervalInSeconds = 1;

		public static int Volume = 50;

		public static int XFor1Enemy = 0;
        public static int XFor2Enemy = 12 * ObjectWidth;
        public static int XFor3Enemy = 24 * ObjectWidth;
        public static int YForEnemy = 0;
        public static int XFor1Player = 8 * ObjectWidth;
        public static int XFor2Player = 16 * ObjectWidth;
        public static int YForPlayer = 24 * ObjectHeight;


        public static HashSet<Key> UsedKeys { get; } = new HashSet<Key>();
        public static Key KeyUp1Player = Key.W;
		public static Key KeyDown1Player = Key.S;
		public static Key KeyLeft1Player = Key.A;
		public static Key KeyRight1Player = Key.D;
		public static Key KeyUp2Player = Key.Up;
		public static Key KeyDown2Player = Key.Down;
		public static Key KeyLeft2Player = Key.Left;
		public static Key KeyRight2Player = Key.Right;
		public static Key KeyShoot1Player = Key.Space;
		public static Key KeyShoot2Player = Key.Enter;
		static GameConfiguration()
		{
            UsedKeys.Add(KeyUp1Player);
            UsedKeys.Add(KeyUp2Player);
            UsedKeys.Add(KeyDown1Player);
            UsedKeys.Add(KeyDown2Player);
            UsedKeys.Add(KeyLeft1Player);
            UsedKeys.Add(KeyLeft2Player);
            UsedKeys.Add(KeyRight1Player);
            UsedKeys.Add(KeyRight2Player);
			UsedKeys.Add(KeyShoot1Player);
			UsedKeys.Add(KeyShoot2Player);
        }
	}
}
