using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DuelUI.CommonFrm
{
    public class BaseAnimView : UserControl
    {
        public double TargetX = 0;

        public double TargetY = 0;

        public double TargetScaleX = 1;

        public double TargetScaleY = 1;

        public double TargetAngel = 0;

        public double X
        {
            get { return (double)GetValue(XProperty); }
            set { SetValue(XProperty, value); }
        }
        public static readonly DependencyProperty XProperty =
            DependencyProperty.Register("X", typeof(double), typeof(BaseAnimView),
                new UIPropertyMetadata(0.0, XChangedCallback));

        private static void XChangedCallback(DependencyObject obj,
           DependencyPropertyChangedEventArgs args)
        {
            BaseAnimView sender = obj as BaseAnimView;
            if (sender != null)
                sender.OnXChanged();
        }

        protected virtual void OnXChanged()
        {

        }

        public double Y
        {
            get { return (double)GetValue(YProperty); }
            set { SetValue(YProperty, value); }
        }
        public static readonly DependencyProperty YProperty =
            DependencyProperty.Register("Y", typeof(double), typeof(BaseAnimView),
                new UIPropertyMetadata(0.0, YChangedCallback));

        private static void YChangedCallback(DependencyObject obj,
         DependencyPropertyChangedEventArgs args)
        {
            BaseAnimView sender = obj as BaseAnimView;
            if (sender != null)
                sender.OnYChanged();
        }

        protected virtual void OnYChanged()
        {

        }

        public double Angle
        {
            get { return (double)GetValue(AngleProperty); }
            set { SetValue(AngleProperty, value); }
        }
        public static readonly DependencyProperty AngleProperty =
            DependencyProperty.Register("Angle", typeof(double), typeof(BaseAnimView), new UIPropertyMetadata(0.0));

        public double ScaleX
        {
            get { return (double)GetValue(ScaleXProperty); }
            set { SetValue(ScaleXProperty, value); }
        }
        public static readonly DependencyProperty ScaleXProperty =
            DependencyProperty.Register("ScaleX", typeof(double), typeof(BaseAnimView), new UIPropertyMetadata(1.0));

        public double ScaleY
        {
            get { return (double)GetValue(ScaleYProperty); }
            set { SetValue(ScaleYProperty, value); }
        }
        public static readonly DependencyProperty ScaleYProperty =
            DependencyProperty.Register("ScaleY", typeof(double), typeof(BaseAnimView), new UIPropertyMetadata(1.0));

    }
}
