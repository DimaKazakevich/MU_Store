using System;
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

        //private async void newItem_Click(object sender, RoutedEventArgs e)
        //{
        //    var rnd = new Random();
        //    var direction = -1;

        //    while (true)
        //    {
        //        var percent = rnd.Next(0, 100);
        //        direction *= -1; // Direction changes every iteration
        //        var rotation = (int)((percent / 100d) * 45 * direction); // Max 45 degree rotation
        //        var duration = (int)(750 * (percent / 100d)); // Max 750ms rotation

        //        var da = new DoubleAnimation
        //        {
        //            To = rotation,
        //            Duration = new Duration(TimeSpan.FromMilliseconds(duration)),
        //            AutoReverse = true // makes the balloon come back to its original position
        //        };

        //        var rt = new RotateTransform();
        //        Balloon.RenderTransform = rt; // Your balloon object
        //        rt.BeginAnimation(RotateTransform.AngleProperty, da);

        //        await Task.Delay(duration * 2); // Waiting for animation to finish, not blocking the UI thread
        //    }
        //}

        public void AnimateBalloon()
        {
            var rnd = new Random();
            var direction = -1;

            var timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = new TimeSpan(0, 1, 0);
            timer.IsEnabled = true;
            timer.Tick += (o, t) => Refresh(orderUnitOfWork);
            timer.Start();

            while (Timer)
            {
                var percent = rnd.Next(0, 100);
                direction *= -1; // Direction changes every iteration
                var rotation = (int)((percent / 100d) * 360 * direction); // Max 45 degree rotation
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
        }
    }
}
