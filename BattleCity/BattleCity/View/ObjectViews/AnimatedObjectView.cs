using BattleCity.View.UnitViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Media;
using System.Windows.Threading;

namespace BattleCity.View.ObjectViews
{
    public class AnimatedObjectView : BaseObjectView
    {
        private DispatcherTimer timer = new DispatcherTimer();
        private int frame;
        private ImageSource[] imagesSources;
        private bool isAnimated = false;

        public AnimatedObjectView(int updateInterval, ImageSource[] _imagesSources, bool animationStart = false)
        {
            ImagesSources = _imagesSources;
            Frame = _imagesSources.Length;
            timer.Interval = TimeSpan.FromMilliseconds(GameConfiguration.MainInterval * updateInterval);
            timer.Tick += TimerTick;
            if (animationStart) AnimationStart();
        }

        protected ImageSource[] ImagesSources
        {
            get { return imagesSources; }
            set
            {
                imagesSources = value;
                Source = imagesSources[frame];
            }
        }
        public int Frame { get; set; }
        protected void AnimationStop()
        {
            isAnimated = false;
            timer.Stop();
        }
        public void AnimationStart()
        {
            isAnimated = true;
            timer.Start();
        }
        protected virtual void UpdateSource()
        {
            this.Source = imagesSources[frame];
        }

        private void TimerTick(object? sender, EventArgs e)
        {
            frame = frame >= Frame - 1 ? 0 : frame + 1;
            UpdateSource();
        }
        public virtual void Dispose()
        {
            timer.Stop();
            timer.Tick -= TimerTick;
        }

        public void Pause(bool isPause)
        {
            if (isPause && isAnimated) timer.Stop();
            else if (!isPause && isAnimated) timer.Start();
        }
    }
}
