using BattleCity.Interfaces;
using BattleCity.Model;
using BattleCity.Model.ObjectModels;
using BattleCity.Model.UnitModels;
using BattleCity.Services;
using BattleCity.Stores;
using BattleCity.Types;
using BattleCity.View.ObjectViews;
using BattleCity.View.UnitViews;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace BattleCity.ViewModel
{
    public class GameFieldViewModel : BaseViewModel
    {
        public ObservableCollection<BaseObjectView> ObjectViews { get; } = new ObservableCollection<BaseObjectView>();
        public ObservableCollection<BaseObjectModel> ObjectModels { get; } = new ObservableCollection<BaseObjectModel>();

        public ControlPlayerService ControlFor1Player;
        public ControlPlayerService ControlFor2Player;
        public ControlEnemiesService ControlEnemies;
        public ControlBulletsService ControlBullets;
        public Action<bool> OnGameOver;
        public Action PausedChanged;
        public GameFieldViewModel(LevelStore levelStore)
        {
            ObjectModels.CollectionChanged += ObjectModels_CollectionChanged;
            CheckCollisionService.objects = ObjectModels;

            Level _level = levelStore.CurrentLevel;
            GenerateMap(_level.MapData);

            InitializePlayerControl(levelStore);
            if (levelStore.Is2Players)
            {
                InitializePlayerControl(levelStore, true);
            }

            ControlEnemies = new ControlEnemiesService();
            ControlEnemies.EnemyCreated += OnEnemyCreated;
            ControlEnemies.AllEnemiesDestroyed += GameWon;
            AddPausableObject(ControlEnemies);
            ControlEnemies.Initialize(_level.EnemyComposition);

            ControlBullets = new ControlBulletsService();
            OnBulletCreated += ControlBullets.AddBullet;
            AddPausableObject(ControlBullets);
        }

        #region Control

        public void Player1KeyDown(Key key)
        {
            ControlFor1Player?.KeyDown(key);
        }
        public void Player1KeyUp(Key key)
        {
            ControlFor1Player?.KeyUp(key);
        }
        public void Player2KeyDown(Key key)
        {
            ControlFor2Player?.KeyDown(key);
        }
        public void Player2KeyUp(Key key)
        {
            ControlFor2Player?.KeyUp(key);
        }
        private void InitializePlayerControl(LevelStore levelStore, bool isSecondPlayer = false)
        {
            Player player = new Player(isSecondPlayer);
            EventsSubcriptions(player);

            ControlPlayerService controlService = new ControlPlayerService(player);
            controlService.PlayerDied += RemoveControlService;
            AddPausableObject(controlService);
            if (isSecondPlayer)
            {
                ControlFor2Player = controlService;
            }
            else
            {
                ControlFor1Player = controlService;
            }
        }

        private void RemoveControlService(ControlPlayerService service)
        {
            RemoveServiceSubscriptions(service);
            if (ControlFor1Player == service)
                ControlFor1Player = null;
            else if (ControlFor2Player == service)
                ControlFor2Player = null;
            if (ControlFor1Player == null && ControlFor2Player == null) GameLose();
        }

        private void RemoveServiceSubscriptions(ControlPlayerService service)
        {
            service.PlayerDied -= RemoveControlService;
            RemovePausableObject(service);
        }

        private void OnEnemyCreated(object? sender, Enemy enemy)
        {
            EventsSubcriptions(enemy);
        }
        private void EventsSubcriptions(Owner owner)
        {
            owner.TankCreated += AddObject;
            owner.TankRemoved += RemoveObject;
            owner.StarSpawned += AddObject;
            owner.StarRemoved += RemoveObject;
            owner.BulletCreated += AddBullet;
            owner.BulletRemoved += RemoveObject;
        }
        private event Action<Bullet> OnBulletCreated;
        #endregion
        #region ConnectModelsWithViews
        private void ObjectModels_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (BaseObjectModel _object in e.NewItems)
                {
                    // Створюємо відображення UnitView та прив'язуємо його до моделі
                    _object.PropertyChanged += Update;
                    _object.Remove += RemoveObject;
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
        #endregion
        #region Map
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
                        if (_object.Type == TypeObject.BrickWall) _object.Properties[PropertiesType.TypeBrickWall] = TypeBrickWall.Whole;
                        ObjectModels.Add(_object);
                    }
                    index++;
                }
            }
            RemoveObjectsFromMap();
            Eagle eagle = new Eagle(BaseObjectModel.NextID, GameConfiguration.XForEagle, GameConfiguration.YForEagle, GameConfiguration.EagleWidth, GameConfiguration.EagleHeight);
            eagle.EagleDestroyed += GameLose;
            eagle.BigExplosionSpawned += AddObject;
            eagle.BigExplosionDeleted += RemoveObject;
            ObjectModels.Add(eagle);
        }
        public void RemoveObjectsFromMap()
        {
            Rectangle[] BoundingBoxForObjects = new Rectangle[6];
            BoundingBoxForObjects[0] = new Rectangle(GameConfiguration.XFor1Enemy, GameConfiguration.YForEnemy, GameConfiguration.TankWidth, GameConfiguration.TankHeight);
            BoundingBoxForObjects[1] = new Rectangle(GameConfiguration.XFor2Enemy, GameConfiguration.YForEnemy, GameConfiguration.TankWidth, GameConfiguration.TankHeight);
            BoundingBoxForObjects[2] = new Rectangle(GameConfiguration.XFor3Enemy, GameConfiguration.YForEnemy, GameConfiguration.TankWidth, GameConfiguration.TankHeight);
            BoundingBoxForObjects[3] = new Rectangle(GameConfiguration.XFor1Player, GameConfiguration.YForPlayer, GameConfiguration.TankWidth, GameConfiguration.TankHeight);
            BoundingBoxForObjects[4] = new Rectangle(GameConfiguration.XFor2Player, GameConfiguration.YForPlayer, GameConfiguration.TankWidth, GameConfiguration.TankHeight);
            BoundingBoxForObjects[5] = new Rectangle(GameConfiguration.XForEagle, GameConfiguration.YForEagle, GameConfiguration.EagleWidth, GameConfiguration.EagleHeight);
            foreach (Rectangle rectangle in BoundingBoxForObjects)
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
        #endregion
        #region ObjectActions
        public void Update(int id, PropertiesType prop, object value)
        {
            ObjectViews.FirstOrDefault(item => item.ID == id)?.Update(prop, value);
        }
        private void AddBullet(object? sender, EventArgs e)
        {
            if (sender is Bullet bullet)
                OnBulletCreated?.Invoke(bullet);
            AddObject(sender, e);
        }
        private void AddObject(object? sender, EventArgs e)
        {
            if (sender is BaseObjectModel _object)
            {
                ObjectModels.Add(_object);
            }
            if(sender is Star star)
            {
                AddPausableObject(star);
                star.StarDeleted += RemovePausableObject;
            }
        }
        private void RemoveObject(object? sender, EventArgs e)
        {
            if (sender is BaseObjectModel _object)
            {
                ObjectModels.Remove(_object);
            }
        }
        #endregion
        #region Pause
        public bool IsPaused { get; private set; }
        public List<IPausable> pausableObjects { get; } = new List<IPausable> { };
        public event Action PauseRequested;
        public event Action ResumeRequested;
        public void AddPausableObject(IPausable pausable)
        {
            pausableObjects.Add(pausable);
            PauseRequested += pausable.Pause;
            ResumeRequested += pausable.Resume;
        }
        public void RemovePausableObject(object? sender, EventArgs e)
        {
            if (sender is IPausable pausable) RemovePausableObject(pausable);
        }
        public void RemovePausableObject(IPausable pausable)
        {
            pausableObjects.Remove(pausable);
            PauseRequested -= pausable.Pause;
            ResumeRequested -= pausable.Resume;
        }

        public void Pause(bool needChanged = true)
        {
            if (!IsPaused) PauseRequested?.Invoke();
            else ResumeRequested?.Invoke();
            IsPaused = !IsPaused;
            if(needChanged)PausedChanged?.Invoke();
        }
        #endregion
        private void GameWon()
        {
            Pause(false);
            OnGameOver.Invoke(true);
        }
        private void GameLose()
        {
            Pause(false);
            OnGameOver.Invoke(false);
        }
    }
}