using BattleCity.Model;
using BattleCity.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCity.Services
{
    public static class LevelCreatorService
    {
        public static void LevelCreate(Level level)
        {
            using(var context = new ApplicationDbContext())
            {
                context.Add(level);
                context.SaveChanges();
            }
        }
    }
}
