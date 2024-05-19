using BattleCity.Interfaces;
using BattleCity.Model.ObjectModels;
using BattleCity.Model.UnitModels;
using BattleCity.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace BattleCity.Services
{
    public class ControlPlayerService : IPausable
    {
        private Key keyUp = GameConfiguration.KeyUp1Player;
        private Key keyDown = GameConfiguration.KeyDown1Player;
        private Key keyLeft = GameConfiguration.KeyLeft1Player;
        private Key keyRight = GameConfiguration.KeyRight1Player;
        private Key keyShoot = GameConfiguration.KeyShoot1Player;
        private DispatcherTimer timer;
        public Player CurrentPlayer;

        private bool isUpKeyPressed;
        private bool isDownKeyPressed;
        private bool isLeftKeyPressed;
        private bool isRightKeyPressed;

        public event Action<ControlPlayerService> PlayerDied;
        public ControlPlayerService(Player currentPlayer)
        {
            CurrentPlayer = currentPlayer;
            CurrentPlayer.TankInizializated += SetProperties;
            CurrentPlayer.TankCreated += CreateTimer;
            CurrentPlayer.TankRemoved += RemoveTimer;

            if (CurrentPlayer.IsSecondPlayer)
            {
                keyUp = GameConfiguration.KeyUp2Player;
                keyDown = GameConfiguration.KeyDown2Player;
                keyLeft = GameConfiguration.KeyLeft2Player;
                keyRight = GameConfiguration.KeyRight2Player;
                keyShoot = GameConfiguration.KeyShoot2Player;
            }
            CurrentPlayer.PlayerDied += OnPlayerDied;
            CurrentPlayer.CreateTank();
        }

        private void SetProperties(object? sender, EventArgs e)
        {
            CurrentPlayer.Tank.Properties[PropertiesType.MovementDirection] = MovementDirection.Up;
            CurrentPlayer.Tank.Properties[PropertiesType.IsSecondPlayer] = CurrentPlayer.IsSecondPlayer;
            CurrentPlayer.Tank.Properties[PropertiesType.IsImmortal] = true;
        }

        private void OnPlayerDied()
        {
            PlayerDied?.Invoke(this);
        }

        private void StartTickChecker()
        {
            if (IsPaused == false && timer?.IsEnabled == false && CurrentPlayer.Tank != null)
            {
                TickChecker(null, EventArgs.Empty);
                timer?.Start();
            }
        }

        private void StopTickChecker()
        {
            if (IsPaused || (!isUpKeyPressed && !isDownKeyPressed && !isLeftKeyPressed && !isRightKeyPressed))
            {
                CurrentPlayer.Tank.Stop();
                timer?.Stop();
            }
        }
        private void CreateTimer(object? sender, EventArgs e)
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(GameConfiguration.MainInterval);
            timer.Tick += TickChecker;
        }
        private void RemoveTimer(object? sender, EventArgs e)
        {
            timer?.Stop();
            timer = null;
        }
        public void KeyDown(Key key)
        {
            if (key == keyUp) isUpKeyPressed = true;
            else if (key == keyDown) isDownKeyPressed = true;
            else if (key == keyLeft) isLeftKeyPressed = true;
            else if (key == keyRight) isRightKeyPressed = true;
            else if (key == keyShoot) CurrentPlayer.Tank.TryFire();

            StartTickChecker();
        }
        public void KeyUp(Key key)
        {
            if (key == keyUp) isUpKeyPressed = false;
            else if (key == keyDown) isDownKeyPressed = false;
            else if (key == keyLeft) isLeftKeyPressed = false;
            else if (key == keyRight) isRightKeyPressed = false;

            StopTickChecker();
        }
        private bool IsFewKeyPressed => (isUpKeyPressed && isDownKeyPressed) || (isUpKeyPressed && isLeftKeyPressed) || (isUpKeyPressed && isRightKeyPressed) ||
                                        (isDownKeyPressed && isLeftKeyPressed) || (isDownKeyPressed && isRightKeyPressed) || (isLeftKeyPressed && isRightKeyPressed);

        private bool IsPaused { get; set; } = false;

        private void TickChecker(object? sender, EventArgs e)
        {
            if (IsFewKeyPressed)
            {
                CurrentPlayer.Tank.Stop();
                return;
            }
            if (isUpKeyPressed && CurrentPlayer.Tank.CanMove(MovementDirection.Up))
            {
                CurrentPlayer.Tank.Move(MovementDirection.Up);
            }
            if (isDownKeyPressed && CurrentPlayer.Tank.CanMove(MovementDirection.Down))
            {
                CurrentPlayer.Tank.Move(MovementDirection.Down);
            }
            if (isLeftKeyPressed && CurrentPlayer.Tank.CanMove(MovementDirection.Left))
            {
                CurrentPlayer.Tank.Move(MovementDirection.Left);
            }
            if (isRightKeyPressed && CurrentPlayer.Tank.CanMove(MovementDirection.Right))
            {
                CurrentPlayer.Tank.Move(MovementDirection.Right);
            }
        }

        public void Pause()
        {
            IsPaused = true;
            StopTickChecker();
        }

        public void Resume()
        {
            IsPaused = false;
        }
    }
}
