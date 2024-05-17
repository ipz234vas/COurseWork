using BattleCity.Model;
using BattleCity.Model.UnitModels;
using BattleCity.Services;
using BattleCity.Stores;
using BattleCity.Types;
using BattleCity.View.UnitViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace BattleCity.ViewModel
{
	public class GameFieldViewModel : BaseViewModel
	{
		public ObservableCollection<UnitView> UnitViews { get; } = new ObservableCollection<UnitView>();
		public ObservableCollection<Unit> UnitModels { get; } = new ObservableCollection<Unit>();

		private DispatcherTimer timer;
		public ControlService ControlFor1Player;
		public ControlService ControlFor2Player;

		public GameFieldViewModel(LevelStore levelStore)
		{
			UnitModels.CollectionChanged += UnitsModel_CollectionChanged;

			Level _level = levelStore.CurrentLevel;
			GenerateMap(_level.MapData);

			MoveableUnit player1 = new MoveableUnit(Unit.NextID, 8 * GameConfiguration.UnitWidth, 24 * GameConfiguration.UnitHeight, GameConfiguration.TankWidth, GameConfiguration.TankHeight, TypeUnit.SmallTankPlayer, 5);
			player1.PropertyChanged += Update;
			UnitModels.Add(player1);
			RemoveUnitsOnPosition(player1);
			ControlFor1Player = new ControlService(player1);
			if (levelStore.Is2Players)
			{
				MoveableUnit player2 = new MoveableUnit(Unit.NextID, 16 * GameConfiguration.UnitWidth, 24 * GameConfiguration.UnitHeight, GameConfiguration.TankWidth, GameConfiguration.TankHeight, TypeUnit.SmallTankPlayer, 5);
				player2.PropertyChanged += Update;
				UnitModels.Add(player2);
				RemoveUnitsOnPosition(player2);
				ControlFor2Player = new ControlService(player2, true);
			}
			CheckCollisionService.units = UnitModels;

			timer = new DispatcherTimer();
			timer.Interval = TimeSpan.FromMilliseconds(GameConfiguration.Interval);
		}
		#region Control

		public void Player1KeyDown(Key key)
		{
			ControlFor1Player.KeyDown(key);
		}
		public void Player1KeyUp(Key key)
		{
			ControlFor1Player.KeyUp(key);
		}
		public void Player2KeyDown(Key key)
		{
			ControlFor2Player?.KeyDown(key);
		}
		public void Player2KeyUp(Key key)
		{
			ControlFor2Player?.KeyUp(key);
		}

		#endregion

		private void UnitsModel_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
		{
			if (e.Action == NotifyCollectionChangedAction.Add)
			{
				foreach (Unit unit in e.NewItems)
				{
					// Створюємо відображення UnitView та прив'язуємо його до моделі
					UnitViews.Add(UnitViewCreatorService.Create(unit.ID, unit.X, unit.Y, unit.Width, unit.Height, unit.Type, unit.Properties));
				}
			}
			else if (e.Action == NotifyCollectionChangedAction.Remove)
			{
				foreach (Unit unit in e.OldItems)
				{
					// Знаходимо відповідне відображення та видаляємо його
					UnitView viewToRemove = UnitViews.FirstOrDefault(v => v.ID == unit.ID);
					if (viewToRemove != null)
					{
						UnitViews.Remove(viewToRemove);
					}
				}
			}
		}
		public void GenerateMap(string mapData)
		{
			int index = 0;
			for (int i = 0; i < 26; i++)
			{
				for (int j = 0; j < 26; j++)
				{
					if (mapData[index] != ' ')
					{
						Unit unit = new Unit(Unit.NextID, j * GameConfiguration.UnitWidth, i * GameConfiguration.UnitHeight,
							GameConfiguration.UnitWidth, GameConfiguration.UnitHeight, TypeUnitService.GetTypeUnitBySymbol(mapData[index]));
						unit.PropertyChanged += Update;
						UnitModels.Add(unit);
					}
					index++;
				}
			}
		}
		public void RemoveUnitsOnPosition(Unit currentUnit)
		{
			List<Unit> unitsForRemove = new List<Unit>();
			foreach (Unit unit in UnitModels)
			{
				if (CheckCollisionService.CheckCollision(unit, currentUnit) && unit != currentUnit) unitsForRemove.Add(unit);
			}
			foreach (Unit unit in unitsForRemove)
			{
				UnitModels.Remove(unit);
			}
			unitsForRemove.Clear();
		}
		public void Update(int id, PropertiesType prop, object value)
		{
			UnitViews.FirstOrDefault(item => item.ID == id)?.Update(prop, value);
		}
	}
}
