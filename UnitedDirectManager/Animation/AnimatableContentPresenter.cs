using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace UnitedDirectManager.Animation
{
    [TemplatePart(Name = "PART_Animation", Type = typeof(Storyboard))]
    public class AnimatableContentPresenter : Control
    {
        static AnimatableContentPresenter()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(AnimatableContentPresenter),
                new FrameworkPropertyMetadata(typeof(AnimatableContentPresenter)));
        }

        Storyboard animation; // текущая анимация
        bool isAnimationRunning = false;

        #region dp object Content, on change OnContentChanged
        public object Content
        {
            get { return (object)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register(
                "Content", typeof(object), typeof(AnimatableContentPresenter),
                new PropertyMetadata(OnContentChangedStatic));

        static void OnContentChangedStatic(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var self = (AnimatableContentPresenter)d;
            self.OnContentChanged(e.OldValue, e.NewValue);
        }
        #endregion

        #region dp object PreviousContent
        public object PreviousContent
        {
            get { return (object)GetValue(PreviousContentProperty); }
            set { SetValue(PreviousContentProperty, value); }
        }

        public static readonly DependencyProperty PreviousContentProperty =
            DependencyProperty.Register(
                "PreviousContent", typeof(object), typeof(AnimatableContentPresenter));
        #endregion

        // когда Content поменяется...
        void OnContentChanged(object oldContent, object newContent)
        {
            if (isAnimationRunning)
                animation?.Stop();

            // ... запомним старый Content в PreviousContent
            PreviousContent = oldContent;

            // и перезапустим анимацию
            if (animation != null)
            {
                animation.Begin();
                isAnimationRunning = true;
            }
        }

        // при появлении шаблона, вычитаем из него анимацию
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (animation != null)
                animation.Completed -= OnAnimationCompleted;

            if (isAnimationRunning)
            {
                // TODO: начать новую анимацию там, где предыдущая завершилась?
                animation?.Stop();
            }

            animation = (Storyboard)Template.FindName("PART_Animation", this);

            if (animation != null) // подпишемся на завершение анимации
                animation.Completed += OnAnimationCompleted;
        }

        // по окончанию анимации...
        private void OnAnimationCompleted(object sender, EventArgs e)
        {
            // выбросим старый контент
            PreviousContent = null;
            // сбросим эффект анимации
            animation.Remove();
            isAnimationRunning = false;
        }
    }
}
