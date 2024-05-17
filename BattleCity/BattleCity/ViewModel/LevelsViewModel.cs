using BattleCity.Commands;
using BattleCity.Model;
using BattleCity.Services;
using BattleCity.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCity.ViewModel
{
	public class LevelsViewModel : BaseViewModel
	{
		private Account CurrentAccount;
		private ObservableCollection<LevelsListItemModel> _levelsList;
		private int LevelCounts = 0;

        public ObservableCollection<LevelsListItemModel> LevelsList
		{
			get => _levelsList;
			set
			{
				_levelsList = value;
				OnPropertyChanged(nameof(LevelsList));
			}
		}
		private ObservableCollection<LevelsListItemModel> _personalLevelsList;
		public ObservableCollection<LevelsListItemModel> PersonalLevelsList
		{
			get => _personalLevelsList;
			set
			{
				_personalLevelsList = value;
				OnPropertyChanged(nameof(PersonalLevelsList));
			}
		}
		private ObservableCollection<LevelsListItemModel> _globalLevelsList;
		public ObservableCollection<LevelsListItemModel> GlobalLevelsList
		{
			get => _globalLevelsList;
			set
			{
				_globalLevelsList = value;
				OnPropertyChanged(nameof(GlobalLevelsList));
			}
		}
		public LevelsViewModel(AccountStore accountStore, NavigationStore navigationStore)
		{
			CurrentAccount = accountStore.CurrentAccount;
			GetLevelsFromDB(accountStore, navigationStore);
		}

		private async void GetLevelsFromDB(AccountStore accountStore, NavigationStore navigationStore)
		{
			await Task.Run(() => {
                using (var context = new ApplicationDbContext())
                {
                    var levels = context.Levels.ToList();

					var levelsListitems = levels.Select(Item => new LevelsListItemModel
					{
						Level = Item,
						Text = Item.Title,
						Command = new NavigationCommand<PlayerSelectionViewModel>(new NavigationService<PlayerSelectionViewModel>(navigationStore, () => new PlayerSelectionViewModel(accountStore, navigationStore, new LevelStore { CurrentLevel = Item }))),
                        IsAvailable = CurrentAccount != null ? CurrentAccount.CurrentLevel > LevelCounts++ : SessionUserStore.CurrentLevel > LevelCounts++
            });
                    this.LevelsList = new ObservableCollection<LevelsListItemModel>(levelsListitems.Where(item => item.Level.CreatorId == 1));
                    if (CurrentAccount != null)
                    {
                        this.PersonalLevelsList = new ObservableCollection<LevelsListItemModel>(levelsListitems.Where(item => item.Level.CreatorId == CurrentAccount.Id));
                        this.GlobalLevelsList = new ObservableCollection<LevelsListItemModel>(levelsListitems.Where(item => item.Level.CreatorId != 1 && item.Level.CreatorId != CurrentAccount.Id));
                    }
                }
            }); 
		}
	}
}
