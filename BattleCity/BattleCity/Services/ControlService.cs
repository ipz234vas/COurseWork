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

		public ControlService(MoveableUnit currentPlayer, bool IsSecondPlayer = false)
		{
			CurrentPlayer = currentPlayer;

			if (IsSecondPlayer)
			{
				keyUp = GameConfiguration.KeyUp2Player;
				keyDown = GameConfiguration.KeyDown2Player;
				keyLeft = GameConfiguration.KeyLeft2Player;
				keyRight = GameConfiguration.KeyRight2Player;
			}

			timer = new DispatcherTimer();
			timer.Interval = TimeSpan.FromMilliseconds(GameConfiguration.Interval);
			timer.Tick += TickChecker;
		}
		private DispatcherTimer timer;
		public MoveableUnit CurrentPlayer;

		private bool isUpKeyPressed;
		private bool isDownKeyPressed;
		private bool isLeftKeyPressed;
		private bool isRightKeyPressed;

		private void StartTickChecker()
		{
			if (!timer.IsEnabled)
			{
				TickChecker(null, EventArgs.Empty); 
				timer.Start();
			}
		}

		private void StopTickChecker()
		{
			if (!isUpKeyPressed && !isDownKeyPressed && !isLeftKeyPressed && !isRightKeyPressed)
			{
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
			if (IsFewKeyPressed) return;
			if (isUpKeyPressed && CurrentPlayer.CanMove(MovementDirection.Up))
			{
				CurrentPlayer.Move(MovementDirection.Up);
			}
			if (isDownKeyPressed && CurrentPlayer.CanMove(MovementDirection.Down))
			{
				CurrentPlayer.Move(MovementDirection.Down);
			}
			if (isLeftKeyPressed && CurrentPlayer.CanMove(MovementDirection.Left))
			{
				CurrentPlayer.Move(MovementDirection.Left);
			}
			if (isRightKeyPressed && CurrentPlayer.CanMove(MovementDirection.Right))
			{
				CurrentPlayer.Move(MovementDirection.Right);
			}
		}
	}
}
