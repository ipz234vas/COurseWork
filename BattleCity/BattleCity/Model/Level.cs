using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCity.Model
{
    public class Level
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string MapData { get; set; }

        public string EnemyComposition { get; set; }
		[ForeignKey("Creator")]
		public int CreatorId { get; set; }
        public Account Creator { get; set; }

        public Level()
        {

        }

        public Level(string title, string map, string enemyComposition, int creatorId)
        {
            Title = title;
            MapData = map;
            EnemyComposition = enemyComposition;
            CreatorId = creatorId;
        }
    }
}
