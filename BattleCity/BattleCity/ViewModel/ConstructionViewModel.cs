using BattleCity.Commands;
using BattleCity.Model;
using BattleCity.Services;
using BattleCity.Stores;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BattleCity.ViewModel
{
    public class ConstructionViewModel : BaseViewModel
    {
        private string _mapData;
        private string _enemyData;
        private string message;
        private string title;
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged(nameof(Title));
            }
        }
        public string Message
        {
            get { return message; }
            set
            {
                message = value;
                OnPropertyChanged(nameof(Message));
            }
        }
        public string MapData
        {
            get { return _mapData; }
            set
            {
                _mapData = value;
                OnPropertyChanged(nameof(MapData));
                Message = null;
            }
        }

        public string EnemyData
        {
            get { return _enemyData; }
            set
            {
                _enemyData = value;
                OnPropertyChanged(nameof(EnemyData));
                Message = null;
            }
        }
        private Account CurrentAccount;
        public ICommand CreateCommand { get; }
        public ICommand GenerateCommand { get; }
        public ICommand NavigateMenuCommand { get; }
        public ConstructionViewModel(AccountStore accountStore, NavigationStore navigationStore)
        {
            CreateCommand = new RelayCommand(Validate);
            GenerateCommand = new RelayCommand(Generate);
            NavigateMenuCommand = new NavigationCommand<MenuViewModel>(new NavigationService<MenuViewModel>(navigationStore, () => new MenuViewModel(accountStore, navigationStore)));
            CurrentAccount = accountStore.CurrentAccount;
        }

        private void Validate(object parameter)
        {
            if (CurrentAccount == null)
            {
                Message = "You need to log in to create level";
                return;
            }
            if (ValidateMapData(MapData) && ValidateEnemyData(EnemyData))
            {
                Message = "Created successful";
                LevelCreate();
            }
            else
            {
                Message = "Invalid!";
            }
        }

        private void LevelCreate()
        {
            Level level = new Level(Title, MapData, EnemyData, CurrentAccount.Id);
            LevelCreatorService.LevelCreate(level);
        }

        private void Generate(object parameter)
        {
            MapData = GenerateRandomMapData();
            EnemyData = GenerateRandomEnemyData();
        }

        private bool ValidateMapData(string mapData)
        {
            if (mapData == null) return false;
            string mapPattern = @"^[@#\$& ]{676}$";
            return Regex.IsMatch(mapData.Replace("\n", "").Replace("\r", ""), mapPattern);
        }

        private bool ValidateEnemyData(string enemyData)
        {
            if (enemyData == null) return false;
            string enemyPattern = @"^[BFP]{20}$";
            return Regex.IsMatch(enemyData, enemyPattern);
        }

        private string GenerateRandomMapData()
        {
            char[] symbols = { '@', '#', '$', ' ', '&' };
            Random random = new Random();
            char[] mapData = new char[676];
            for (int i = 0; i < 676; i++)
            {
                mapData[i] = GetRandomSymbolWithWeightedProbability(random);
            }
            return new string(mapData);
        }

        private char GetRandomSymbolWithWeightedProbability(Random random)
        {
            int value = random.Next(100); // Генеруємо число від 0 до 99
            if (value < 60) // 60% шанс
                return ' ';
            else if (value < 85) // 25% шанс (85 - 60)
                return '@';
            else if (value < 93) // 8% шанс (93 - 85)
                return '&';
            else if (value < 97) // 4% шанс (97 - 93)
                return '#';
            else // 3% шанс (100 - 97)
                return '$';
        }

        private string GenerateRandomEnemyData()
        {
            char[] symbols = { 'B', 'F', 'P' };
            Random random = new Random();
            char[] enemyData = new char[20];
            for (int i = 0; i < 20; i++)
            {
                enemyData[i] = symbols[random.Next(symbols.Length)];
            }
            return new string(enemyData);
        }
    }
}

