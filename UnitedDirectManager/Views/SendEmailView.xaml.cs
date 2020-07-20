using System;
using System.Diagnostics;
using System.Threading;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace UnitedDirectManager.Views
{
    /// <summary>
    /// Interaction logic for SendEmailView.xaml
    /// </summary>
    public partial class SendEmailView : UserControl, IRightSideView
    {
        public SendEmailView()
        {
            InitializeComponent();
        }

        private void newItem_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var thread = new Thread(this.AnimateBalloon);
            thread.Start();
        }

        public void AnimateBalloon()
        {
            var rnd = new Random();
            var direction = -1;

            Stopwatch s = new Stopwatch();
            s.Start();
            while (s.Elapsed < TimeSpan.FromSeconds(1))
            {
                var percent = rnd.Next(0, 100);
                direction *= -1; // Direction changes every iteration
                var rotation = (int)((percent / 100d) * 90 * direction); // Max 45 degree rotation
                var duration = (int)(750 * (percent / 100d)); // Max 750ms rotation

                Balloon.Dispatcher.BeginInvoke(
                    (Action)(() =>
                    {
                        var da = new DoubleAnimation
                        {
                            To = rotation,
                            Duration = new Duration(TimeSpan.FromMilliseconds(duration)),
                            AutoReverse = true // makes the balloon come back to its original position
                        };

                        var rt = new RotateTransform();
                        Balloon.RenderTransform = rt; // Your balloon object
                        rt.BeginAnimation(RotateTransform.AngleProperty, da);
                    }));

                Thread.Sleep(duration * 2);
            }

            s.Stop();
            s.Reset();
        }
    }
}
