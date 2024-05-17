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
	public class ControlService
	{
		private Key keyUp = GameConfiguration.KeyUp1Player;
		private Key keyDown = GameConfiguration.KeyDown1Player;
		private Key keyLeft = GameConfiguration.KeyLeft1Player;
		private Key keyRight = GameConfiguration.KeyRight1Player;
        private DispatcherTimer timer;
		public Player CurrentPlayer;

        private bool isUpKeyPressed;
        private bool isDownKeyPressed;
        private bool isLeftKeyPressed;
        private bool isRightKeyPressed;

        public ControlService(Player currentPlayer)
		{
			CurrentPlayer = currentPlayer;

			if (CurrentPlayer.IsSecondPlayer)
			{
				keyUp = GameConfiguration.KeyUp2Player;
				keyDown = GameConfiguration.KeyDown2Player;
				keyLeft = GameConfiguration.KeyLeft2Player;
				keyRight = GameConfiguration.KeyRight2Player;
			}
			CurrentPlayer.CreateTank();

			timer = new DispatcherTimer();
			timer.Interval = TimeSpan.FromMilliseconds(GameConfiguration.MainInterval);
			timer.Tick += TickChecker;
		}
		private void StartTickChecker()
		{
			if (!timer.IsEnabled && CurrentPlayer.PlayerTank != null)
			{
				TickChecker(null, EventArgs.Empty); 
				timer.Start();
			}
		}

		private void StopTickChecker()
		{
			if (!isUpKeyPressed && !isDownKeyPressed && !isLeftKeyPressed && !isRightKeyPressed)
			{
				CurrentPlayer.PlayerTank.Stop();
				timer.Stop();
			}
		}

		public void KeyDown(Key key)
		{
			if (key == keyUp) isUpKeyPressed = true;
			else if (key == keyDown) isDownKeyPressed = true;
			else if (key == keyLeft) isLeftKeyPressed = true;
			else if (key == keyRight) isRightKeyPressed = true;

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
		private void TickChecker(object? sender, EventArgs e)
		{
			if (IsFewKeyPressed)
			{
                CurrentPlayer.PlayerTank.Stop();
                return;
			}
			if (isUpKeyPressed && CurrentPlayer.PlayerTank.CanMove(MovementDirection.Up))
			{
				CurrentPlayer.PlayerTank.Move(MovementDirection.Up);
			}
			if (isDownKeyPressed && CurrentPlayer.PlayerTank.CanMove(MovementDirection.Down))
			{
				CurrentPlayer.PlayerTank.Move(MovementDirection.Down);
			}
			if (isLeftKeyPressed && CurrentPlayer.PlayerTank.CanMove(MovementDirection.Left))
			{
				CurrentPlayer.PlayerTank.Move(MovementDirection.Left);
			}
			if (isRightKeyPressed && CurrentPlayer.PlayerTank.CanMove(MovementDirection.Right))
			{
				CurrentPlayer.PlayerTank.Move(MovementDirection.Right);
			}
		}
	}
}
