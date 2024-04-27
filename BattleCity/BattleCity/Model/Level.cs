using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCity.Model
{
    public class Level
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string Map { get; set; }

        public string EnemyComposition { get; set; }

        public int CreatorId { get; set; }

        public Level()
        {

        }

        public Level(string title, string map, string enemyComposition, int creatorId)
        {
            Title = title;
            Map = map;
            EnemyComposition = enemyComposition;
            CreatorId = creatorId;
        }
    }
}
