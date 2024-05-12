using BattleCity.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BattleCity.View.UserControls
{
	/// <summary>
	/// Interaction logic for GameFieldView.xaml
	/// </summary>
	public partial class GameFieldView : UserControl
	{
		public GameFieldView()
		{
			InitializeComponent();
			Focusable = true;
			Loaded += (s, e) => Keyboard.Focus(this);
			DataContextChanged += GamePlayView_DataContextChanged;
		}
		private GameFieldViewModel gamePlayViewModel;

		private void GamePlayView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			if (DataContext is GamePlayViewModel gameViewModel)
			{
				// Отримати доступ до GamePlayViewModel
				GameFieldViewModel gamePlayViewModel = gameViewModel.GameFieldViewModel;
				if (gamePlayViewModel != null)
				{
					this.gamePlayViewModel = gamePlayViewModel;
				}
			}
		}
		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);

			if (e.Key == GameConfiguration.KeyDown1Player || e.Key == GameConfiguration.KeyLeft1Player ||
				e.Key == GameConfiguration.KeyRight1Player || e.Key == GameConfiguration.KeyUp1Player)
			{
				gamePlayViewModel?.Player1KeyDown(e.Key);
			}
			else if (e.Key == GameConfiguration.KeyDown2Player || e.Key == GameConfiguration.KeyLeft2Player ||
				e.Key == GameConfiguration.KeyRight2Player || e.Key == GameConfiguration.KeyUp2Player)
			{
				gamePlayViewModel?.Player2KeyDown(e.Key);
			}
		}
		protected override void OnKeyUp(KeyEventArgs e)
		{
			base.OnKeyUp(e);

			if (e.Key == GameConfiguration.KeyDown1Player || e.Key == GameConfiguration.KeyLeft1Player ||
				e.Key == GameConfiguration.KeyRight1Player || e.Key == GameConfiguration.KeyUp1Player)
			{
				gamePlayViewModel?.Player1KeyUp(e.Key);
			}
			else if (e.Key == GameConfiguration.KeyDown2Player || e.Key == GameConfiguration.KeyLeft2Player ||
				e.Key == GameConfiguration.KeyRight2Player || e.Key == GameConfiguration.KeyUp2Player)
			{
				gamePlayViewModel?.Player2KeyUp(e.Key);
			}
		}
	}
}
