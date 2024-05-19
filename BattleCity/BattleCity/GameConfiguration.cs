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
		public const char WaterSymbol = '$';
		public const char IceSymbol = '&';
		public const char EmptySpaceSymbol = ' ';

		public static int ObjectWidth = 20;
		public static int ObjectHeight = 20;

		public static int TankWidth = 40;
		public static int TankHeight = 40;

        public static int EagleWidth = 40;
        public static int EagleHeight = 40;

        public static int BulletWidth = 6;
		public static int BulletHeight = 8;

		public static int SceneWidth = 26 * ObjectWidth;
		public static int SceneHeight = 26 * ObjectHeight;

		public static int UserControlWidth = 31 * ObjectWidth;
		public static int UserControHeight = 28 * ObjectHeight;

		public static int MainInterval = 20;
		public static int SpawnIntervalInSeconds = 1;

		public static int SlowTankSpeed = 2;
		public static int NormalTankSpeed = 4;
		public static int FastTankSpeed = 5;
		public static int SlowBulletSpeed = 4;
		public static int NormalBulletSpeed = 8;
		public static int FastBulletSpeed = 12;

        public static int BasePlayerTankLevel = 1;
        public static int BasePlayerTankLifes = 3;

        public static int XFor1Enemy = 0;
        public static int XFor2Enemy = 12 * ObjectWidth;
        public static int XFor3Enemy = 24 * ObjectWidth;
        public static int YForEnemy = 0;
        public static int XFor1Player = 8 * ObjectWidth;
        public static int XFor2Player = 16 * ObjectWidth;
        public static int YForPlayer = 24 * ObjectHeight;
        public static int XForEagle = 12 * ObjectHeight;
        public static int YForEagle = 24 * ObjectHeight;

        public static int TimeDetonation = 6;

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
		public static Key KeyPause = Key.Escape;
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
			UsedKeys.Add(KeyPause);
        }
	}
}
