using BattleCity.Interfaces;
using BattleCity.Model.ObjectModels;
using BattleCity.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace BattleCity.Services
{
    public class ControlBulletsService : IPausable
    {
        private readonly List<Bullet> bullets = new List<Bullet>();
        private readonly DispatcherTimer timer;

        public ControlBulletsService()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(GameConfiguration.MainInterval);
            timer.Tick += UpdateBullets;
            timer.Start();
        }

        public void AddBullet(Bullet bullet)
        {
            bullets.Add(bullet);
            bullet.BulletDestroyed += OnBulletDestroyed;
        }

        private void UpdateBullets(object? sender, EventArgs e)
        {
            for (int i = bullets.Count - 1; i >= 0; i--)
            {
                var bullet = bullets[i];
                bullet.Move((MovementDirection)bullet.Properties[PropertiesType.MovementDirection]);
            }
        }

        private void OnBulletDestroyed(object? sender, EventArgs e)
        {
            if (sender is Bullet bullet)
                bullets.Remove(bullet);
        }

        public void Pause()
        {
            timer.Stop();
        }

        public void Resume()
        {
            timer.Start();
        }
    }
}
