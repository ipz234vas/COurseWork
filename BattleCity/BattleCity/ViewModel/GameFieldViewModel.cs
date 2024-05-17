using BattleCity.Model;
using BattleCity.Model.ObjectModels;
using BattleCity.Model.UnitModels;
using BattleCity.Services;
using BattleCity.Stores;
using BattleCity.Types;
using BattleCity.View.ObjectViews;
using BattleCity.View.UnitViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace BattleCity.ViewModel
{
	public class GameFieldViewModel : BaseViewModel
	{
		public ObservableCollection<BaseObjectView> ObjectViews { get; } = new ObservableCollection<BaseObjectView>();
		public ObservableCollection<BaseObjectModel> ObjectModels { get; } = new ObservableCollection<BaseObjectModel>();

		private DispatcherTimer timer;
		public ControlService ControlFor1Player;
		public ControlService ControlFor2Player;

		public GameFieldViewModel(LevelStore levelStore)
		{
			ObjectModels.CollectionChanged += ObjectModels_CollectionChanged;
            CheckCollisionService.objects = ObjectModels;

            Level _level = levelStore.CurrentLevel;
			GenerateMap(_level.MapData);

			Player player1 = new Player();
			player1.TankCreated += AddObject;
            player1.StarSpawned += AddObject;
            player1.StarRemoved += RemoveObject;
            ControlFor1Player = new ControlService(player1);
            if (levelStore.Is2Players)
			{
				Player player2 = new Player(true);
				player2.TankCreated += AddObject;
                player2.StarSpawned += AddObject;
                player2.StarRemoved += RemoveObject;
                ControlFor2Player = new ControlService(player2);
            }

			timer = new DispatcherTimer();
			timer.Interval = TimeSpan.FromMilliseconds(GameConfiguration.MainInterval);
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

		private void ObjectModels_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
		{
			if (e.Action == NotifyCollectionChangedAction.Add)
			{
				foreach (BaseObjectModel _object in e.NewItems)
				{
					// Створюємо відображення UnitView та прив'язуємо його до моделі
					_object.PropertyChanged += Update;
					ObjectViews.Add(ObjectViewCreatorService.Create(_object.ID, _object.X, _object.Y, _object.Width, _object.Height, _object.Type, _object.Properties));
				}
			}
			else if (e.Action == NotifyCollectionChangedAction.Remove)
			{
				foreach (BaseObjectModel _object in e.OldItems)
				{
					// Знаходимо відповідне відображення та видаляємо його
					BaseObjectView viewToRemove = ObjectViews.FirstOrDefault(v => v.ID == _object.ID);
					if (viewToRemove != null)
					{
						if (viewToRemove is AnimatedObjectView animatedObjectView) animatedObjectView.Dispose();
						ObjectViews.Remove(viewToRemove);
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
					if (mapData[index] != GameConfiguration.EmptySpaceSymbol)
					{
						BaseObjectModel _object = new BaseObjectModel(BaseObjectModel.NextID, j * GameConfiguration.ObjectWidth, i * GameConfiguration.ObjectHeight,
							GameConfiguration.ObjectWidth, GameConfiguration.ObjectHeight, TypeObjectService.GetTypeUnitBySymbol(mapData[index]));
						ObjectModels.Add(_object);
					}
					index++;
				}
			}
			RemoveObjectsFromMap();
		}
		public void RemoveObjectsFromMap()
		{
			Rectangle[] BoundingBoxForTanks = new Rectangle[5];
			BoundingBoxForTanks[0] = new Rectangle(GameConfiguration.XFor1Enemy, GameConfiguration.YForEnemy, GameConfiguration.TankWidth, GameConfiguration.TankHeight);
			BoundingBoxForTanks[1] = new Rectangle(GameConfiguration.XFor2Enemy, GameConfiguration.YForEnemy, GameConfiguration.TankWidth, GameConfiguration.TankHeight);
			BoundingBoxForTanks[2] = new Rectangle(GameConfiguration.XFor3Enemy, GameConfiguration.YForEnemy, GameConfiguration.TankWidth, GameConfiguration.TankHeight);
			BoundingBoxForTanks[3] = new Rectangle(GameConfiguration.XFor1Player, GameConfiguration.YForPlayer, GameConfiguration.TankWidth, GameConfiguration.TankHeight);
			BoundingBoxForTanks[4] = new Rectangle(GameConfiguration.XFor2Player, GameConfiguration.YForPlayer, GameConfiguration.TankWidth, GameConfiguration.TankHeight);
			foreach(Rectangle rectangle in BoundingBoxForTanks)
			{
				RemoveObjectsOnPosition(rectangle);
			}
		}
		public void RemoveObjectsOnPosition(Rectangle boundingBox)
		{
			List<BaseObjectModel> objectsForRemove = new List<BaseObjectModel>();
			foreach (BaseObjectModel _object in ObjectModels)
			{
				if (CheckCollisionService.CheckCollision(_object, boundingBox)) 
				{
                    objectsForRemove.Add(_object);
                }
			}
			foreach (BaseObjectModel unit in objectsForRemove)
			{
				ObjectModels.Remove(unit);
			}
			objectsForRemove.Clear();
		}
		public void Update(int id, PropertiesType prop, object value)
		{
			ObjectViews.FirstOrDefault(item => item.ID == id)?.Update(prop, value);
		}
        private void AddObject(object? sender, EventArgs e)
        {
            if (sender is BaseObjectModel _object)
            {
                ObjectModels.Add(_object);
            }
        }
        private void RemoveObject(object? sender, EventArgs e)
        {
            if (sender is BaseObjectModel _object)
            {
                ObjectModels.Remove(_object);
            }
        }
    }
}