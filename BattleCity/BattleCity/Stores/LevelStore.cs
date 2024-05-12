using BattleCity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCity.Stores
{
	public class LevelStore
	{
		public Level CurrentLevel { get; set; }

		public bool Is2Players { get; set; }
	}
}
