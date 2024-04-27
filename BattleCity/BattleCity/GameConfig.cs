using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCity
{
	public class GameConfig
	{
		static GameConfig()
		{
			TitleHeight = 20;
			TitleWidth = 20;
			WindowHeight = TitleHeight * 28;
			WindowWidth = TitleWidth * 31;
		}

		public static int TitleHeight { get; private set; }
		public static int TitleWidth { get; private set; }
		public static int WindowHeight { get; private set; }
		public static int WindowWidth { get; private set; }
	}
}
