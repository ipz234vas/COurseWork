using BattleCity.Interfaces;
using BattleCity.Model.ObjectModels;
using BattleCity.Types;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace BattleCity.Services
{
    public class ControlEnemiesService : IPausable
    {
        private readonly List<Enemy> enemies = new List<Enemy>();
        public readonly DispatcherTimer timer;
        private readonly Random random = new Random();
        private int spawnIteration = 0;
        private string enemyTypes;
        private int currentEnemyIndex = 0;

        public event EventHandler<Enemy>? EnemyCreated;
        public event Action AllEnemiesDestroyed; 

        public ControlEnemiesService()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(GameConfiguration.MainInterval);
            timer.Tick += UpdateEnemies;
        }

        public void Initialize(string enemyTypes)
        {
            this.enemyTypes = enemyTypes;
            timer.Start();
        }


        private bool IsCollision(Rectangle boundingBox)
        {
            foreach (var model in CheckCollisionService.objects)
            {
                if (model.Type == TypeObject.Forest) continue;
                Rectangle objectBoundingBox = new Rectangle(model.X, model.Y, model.Width, model.Height);
                if (CheckCollisionService.CheckCollision(objectBoundingBox, boundingBox))
                {
                    return true;
                }
            }
            return false;
        }

        private int GetNextX()
        {
            int x = 0;
            switch (spawnIteration)
            {
                case 0:
                    x = GameConfiguration.XFor1Enemy;
                    break;
                case 1:
                    x = GameConfiguration.XFor2Enemy;
                    break;
                case 2:
                    x = GameConfiguration.XFor3Enemy;
                    break;
            }
            spawnIteration = (spawnIteration + 1) % 3;
            return x;
        }

        private void UpdateEnemies(object? sender, EventArgs e)
        {
            if (enemies.Count < 3)
            {
                SpawnEnemy();
            }

            foreach (var enemy in enemies)
            {
                if (enemy.Star != null) continue;
                MoveEnemy(enemy);
                EnemyShoot(enemy);
            }
        }
        private void SpawnEnemy()
        {
            if (enemies.Count >= 3 || currentEnemyIndex >= 19) return;

            int spawnChance = random.Next(100);
            if (spawnChance <= 30 * (enemies.Count + 1)) return;

            int y = GameConfiguration.YForEnemy;
            int x = GetNextX();

            // Пошук вільного місця для спавна
            Rectangle boundingBox = new Rectangle(x, y, GameConfiguration.TankWidth, GameConfiguration.TankHeight);
            int maxAttempts = 3; // Максимальна кількість спроб знайти вільне місце

            for (int i = 0; i < maxAttempts; i++)
            {
                if (!IsCollision(boundingBox))
                {
                    break;
                }
                x = GetNextX();
                boundingBox = new Rectangle(x, y, GameConfiguration.TankWidth, GameConfiguration.TankHeight);
            }

            // Якщо після всіх спроб місце не знайдено, не створюємо ворога
            if (IsCollision(boundingBox))
            {
                return;
            }
            char enemyType = enemyTypes[currentEnemyIndex];
            currentEnemyIndex++;
            Enemy enemy = new Enemy();
            EnemyCreated?.Invoke(this, enemy);  // Сповіщення про створення ворога
            enemies.Add(enemy);
            enemy.TankRemoved += OnEnemyRemoved;
            enemy.CreateTank(x, y, TypeObjectService.GetEnemyType(enemyType));
            enemy.Tank.Properties[PropertiesType.MovementDirection] = MovementDirection.Down;
        }
        private void MoveEnemy(Enemy enemy)
        {
            if (enemy.Tank == null) return;

            MovementDirection direction = (MovementDirection)enemy.Tank.Properties[PropertiesType.MovementDirection];
            if (enemy.Tank.CanMove(direction))
            {
                enemy.Tank.Move(direction);
            }
            else if (random.Next(100) < 25) // 25% chance to change direction
            {
                MovementDirection newDirection;
                for (int i = 1; i < 4; i++)
                {
                    newDirection = GetNextDirection((MovementDirection)enemy.Tank.Properties[PropertiesType.MovementDirection]);
                    if (enemy.Tank.CanMove(newDirection))
                    {
                        enemy.Tank.Properties[PropertiesType.MovementDirection] = newDirection;
                        break;
                    }
                }
                return;
            }

            if (random.Next(100) < 2) // 2% chance to change direction
            {
                enemy.Tank.Properties[PropertiesType.MovementDirection] = GetNextDirection((MovementDirection)enemy.Tank.Properties[PropertiesType.MovementDirection]);
            }
        }
        private void EnemyShoot(Enemy enemy)
        {
            if (enemy.Tank == null) return;
            int shootChance = random.Next(100);
            if (shootChance <= 90) return;
            enemy.Tank.TryFire();
        }

        private MovementDirection GetNextDirection(MovementDirection currentDirection)
        {
            Random random = new Random();
            int randomNumber = random.Next(1, 4);
            switch (randomNumber)
            {
                case 1:
                    return (MovementDirection)(((int)currentDirection + 1) % 4);
                case 2:
                    return (MovementDirection)(((int)currentDirection + 2) % 4);
                case 3:
                    return (MovementDirection)(((int)currentDirection + 3) % 4);
            }
            return currentDirection;
        }

        private void OnEnemyRemoved(object? sender, EventArgs e)
        {
            if (sender is Tank tank)
            {
                Enemy enemy = enemies.FirstOrDefault(enemy => enemy.Tank.ID == tank.ID);
                if(enemy != null) enemies.Remove(enemy);
                //if (currentEnemyIndex >= 19 && enemies.Count == 0) AllEnemiesDestroyed?.Invoke();
            }
        }

        public void Pause()
        {
            timer.Stop();
            foreach (var enemy in enemies)
            {
                enemy.Tank.Stop();
            }
        }

        public void Resume()
        {
            timer.Start();
        }
    }
}
