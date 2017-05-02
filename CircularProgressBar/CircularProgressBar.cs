using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace CircularProgressBarApp
{
    public partial class CircularProgressBar : ProgressBar
    {
        public CircularProgressBar()
        {
            this.ValueChanged += CircularProgressBar_ValueChanged;
            this.SizeChanged += CircularProgressBar_SizeChanged;

        }
        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            if (e.Property == CircularProgressBar.RadiusProperty)
            {
                Width = Height = Radius * 2;
            }
            base.OnPropertyChanged(e);
        }
        void CircularProgressBar_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.Radius = Math.Min(ActualWidth, ActualHeight) / 2;
        }
        void CircularProgressBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

            CircularProgressBar bar = sender as CircularProgressBar;
            double currentAngle = bar.Angle;
            double targetAngle = e.NewValue / bar.Maximum * 359.999;
            double duration = Math.Abs(currentAngle - targetAngle) / 359.999 * 500;
            DoubleAnimation anim = new DoubleAnimation(currentAngle, targetAngle, TimeSpan.FromMilliseconds(duration > 0 ? duration : 10));
            bar.BeginAnimation(CircularProgressBar.AngleProperty, anim, HandoffBehavior.Compose);
        }

        public double Angle
        {
            get { return (double)GetValue(AngleProperty); }
            set { SetValue(AngleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Angle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AngleProperty =
            DependencyProperty.Register("Angle", typeof(double), typeof(CircularProgressBar), new PropertyMetadata(0.0));

        public double StrokeThickness
        {
            get { return (double)GetValue(StrokeThicknessProperty); }
            set { SetValue(StrokeThicknessProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StrokeThickness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StrokeThicknessProperty =
            DependencyProperty.Register("StrokeThickness", typeof(double), typeof(CircularProgressBar), new PropertyMetadata(10.0));



        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(CircularProgressBar), new PropertyMetadata("%0.00"));



        public double Radius
        {
            get { return (double)GetValue(RadiusProperty); }
            set { SetValue(RadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Radius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RadiusProperty =
            DependencyProperty.Register("Radius", typeof(double), typeof(CircularProgressBar), new PropertyMetadata(50.0));



        public double InnerRadius
        {
            get { return (double)GetValue(InnerRadiusProperty); }
            set { SetValue(InnerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for InnerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InnerRadiusProperty =
            DependencyProperty.Register("InnerRadius", typeof(double), typeof(CircularProgressBar), new PropertyMetadata(40.0));


    }
}
