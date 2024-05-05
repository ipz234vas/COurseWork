using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCity.Model
{
	public class ApplicationDbContext : DbContext
	{
		public DbSet<Account> Accounts { get; set; }
		public DbSet<Level> Levels { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(
				"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BattleCityDB;Integrated Security=True;Pooling=False;Encrypt=True;Trust Server Certificate=False");
		}
	}
}
