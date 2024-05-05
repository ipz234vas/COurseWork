using BattleCity.Services;
using BattleCity.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCity.Model
{
	public class Account
	{
		public int Id { get; set; }
		public string Username { get; set; }
		public string PasswordHash { get; set; }
		public int? CurrentLevel { get; set; }
		public ICollection<Level> Levels { get; set; }

		public Account()
		{

		}
		public Account(string username, string password, int? currentLevel)
		{
			Username = username;
			PasswordHash = PasswordHashService.HashPassword(password);
			CurrentLevel = currentLevel;
		}
		public Account(int id, string username, int? currentLevel)
		{
			Id = id;
			Username = username;
			CurrentLevel = currentLevel;
		}
	}
}
