using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BattleCity.Model
{
	public class LevelsListItemModel : ListItemModel
	{
		public Level Level;
		public LevelsListItemModel(Level level, string levelTitle, ICommand command) : base(levelTitle, command)
		{
			Level = level;
		}
		public LevelsListItemModel() : base()
		{ 

		}
	}
}
