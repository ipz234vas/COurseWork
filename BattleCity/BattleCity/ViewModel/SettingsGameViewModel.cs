using BattleCity.Commands;
using BattleCity.Services;
using BattleCity.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BattleCity.ViewModel
{
    public class SettingsGameViewModel : BaseViewModel
    {
        public ObservableCollection<Key> AvailableKeys { get; }
        private ObservableCollection<Key> GetAvailableKeys()
        {
            var keys = new ObservableCollection<Key>()
            {
                Key.Enter, Key.Escape, Key.Space, Key.LeftShift, Key.RightShift, Key.LeftAlt, Key.RightAlt, Key.LeftCtrl, Key.RightCtrl, Key.Left, Key.Right, Key.Up, Key.Down
            };
            for (int i = 34; i <= 69; i++)
            {
                keys.Add((Key)i);
            }
            return keys;
        }
        private string errorMessage;
        public string ErrorMessage
        {
            get => errorMessage;
            set
            {
                errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }
        public Key KeyUp1Player
        {
            get => GameConfiguration.KeyUp1Player;
            set { KeyUpdate(value, ref GameConfiguration.KeyUp1Player, nameof(KeyUp1Player)); }
        }

        public Key KeyUp2Player
        {
            get => GameConfiguration.KeyUp2Player;
            set { KeyUpdate(value, ref GameConfiguration.KeyUp2Player, nameof(KeyUp2Player)); }
        }

        public Key KeyDown1Player
        {
            get => GameConfiguration.KeyDown1Player;
            set { KeyUpdate(value, ref GameConfiguration.KeyDown1Player, nameof(KeyDown1Player)); }
        }

        public Key KeyDown2Player
        {
            get => GameConfiguration.KeyDown2Player;
            set { KeyUpdate(value, ref GameConfiguration.KeyDown2Player, nameof(KeyDown2Player)); }
        }

        public Key KeyLeft1Player
        {
            get => GameConfiguration.KeyLeft1Player;
            set { KeyUpdate(value, ref GameConfiguration.KeyLeft1Player, nameof(KeyLeft1Player)); }
        }

        public Key KeyLeft2Player
        {
            get => GameConfiguration.KeyLeft2Player;
            set { KeyUpdate(value, ref GameConfiguration.KeyLeft2Player, nameof(KeyLeft2Player)); }
        }

        public Key KeyRight1Player
        {
            get => GameConfiguration.KeyRight1Player;
            set { KeyUpdate(value, ref GameConfiguration.KeyRight1Player, nameof(KeyRight1Player)); }
        }

        public Key KeyRight2Player
        {
            get => GameConfiguration.KeyRight2Player;
            set { KeyUpdate(value, ref GameConfiguration.KeyRight2Player, nameof(KeyRight2Player)); }
        }

        public Key KeyShoot1Player
        {
            get => GameConfiguration.KeyShoot1Player;
            set { KeyUpdate(value, ref GameConfiguration.KeyShoot1Player, nameof(KeyShoot1Player)); }
        }

        public Key KeyShoot2Player
        {
            get => GameConfiguration.KeyShoot2Player;
            set { KeyUpdate(value, ref GameConfiguration.KeyShoot2Player, nameof(KeyShoot2Player)); }
        }
        private bool IsValidKey(Key key)
        {
            return !GameConfiguration.UsedKeys.Contains(key);
        }
        private void KeyUpdate(Key newKey, ref Key currentKey, string propertyName)
        {
            if (IsValidKey(newKey))
            {
                UsedKeysUpdate(newKey, currentKey);
                currentKey = newKey;
                CheckErrorsAndUpdate(propertyName);
            }
            else if (newKey == currentKey) CheckErrorsAndUpdate();
            else ErrorMessage = "This key is already used";
        }
        private void CheckErrorsAndUpdate(string propertyName = null)
        {
            if (ErrorMessage != null)
            {
                KeysChanged();
                ErrorMessage = null;
            }
            else if (propertyName != null) OnPropertyChanged(propertyName);
        }
        private void KeysChanged()
        {
            OnPropertyChanged(nameof(KeyUp1Player));
            OnPropertyChanged(nameof(KeyUp2Player));
            OnPropertyChanged(nameof(KeyDown1Player));
            OnPropertyChanged(nameof(KeyDown2Player));
            OnPropertyChanged(nameof(KeyLeft1Player));
            OnPropertyChanged(nameof(KeyLeft2Player));
            OnPropertyChanged(nameof(KeyRight1Player));
            OnPropertyChanged(nameof(KeyRight2Player));
        }

        private void UsedKeysUpdate(Key newKey, Key currentKey)
        {
            GameConfiguration.UsedKeys.Remove(currentKey);
            GameConfiguration.UsedKeys.Add(newKey);
        }

        public ICommand NavigateSettingsMenuCommand { get; }

        public SettingsGameViewModel(AccountStore accountStore, NavigationStore navigationStore, NavigationStore settingsNavigationStore)
        {
            NavigateSettingsMenuCommand = new NavigationCommand<SettingsMenuViewModel>(
                new NavigationService<SettingsMenuViewModel>(
                    settingsNavigationStore,
                    () => new SettingsMenuViewModel(accountStore, navigationStore, settingsNavigationStore)
                )
            );
            AvailableKeys = GetAvailableKeys();
        }
    }
}
