using BattleCity.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCity.Commands
{
	public class LogOutCommand : BaseCommand
	{
		private readonly AccountStore _accountStore;

		public LogOutCommand(AccountStore accountStore)
		{
			_accountStore = accountStore;
		}

		public override void Execute(object parameter)
		{
			_accountStore.Logout();
		}
	}
}
