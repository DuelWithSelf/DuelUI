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

namespace DuelUI.Custom
{
    public class IconMenu : Control
    {
        public string IconData
        {
            get { return (string)base.GetValue(IconDataProperty); }
            set { base.SetValue(IconDataProperty, value); }
        }
        public static readonly DependencyProperty IconDataProperty =
            DependencyProperty.Register("IconData", typeof(string), typeof(IconMenu),
                new FrameworkPropertyMetadata(""));

        public double IconWidth
        {
            get { return (double)base.GetValue(IconWidthProperty); }
            set { base.SetValue(IconWidthProperty, value); }
        }
        public static readonly DependencyProperty IconWidthProperty =
            DependencyProperty.Register("IconWidth", typeof(double), typeof(IconMenu),
                new FrameworkPropertyMetadata(60d));

        public double IconHeight
        {
            get { return (double)base.GetValue(IconHeightProperty); }
            set { base.SetValue(IconHeightProperty, value); }
        }
        public static readonly DependencyProperty IconHeightProperty =
            DependencyProperty.Register("IconHeight", typeof(double), typeof(IconMenu),
                new FrameworkPropertyMetadata(60d));

        public Brush IconBrush
        {
            get { return (Brush)base.GetValue(IconBrushProperty); }
            set { base.SetValue(IconBrushProperty, value); }
        }
        public static readonly DependencyProperty IconBrushProperty =
            DependencyProperty.Register("IconBrush", typeof(Brush), typeof(IconMenu),
                new FrameworkPropertyMetadata(Brushes.White));

        public Brush FocusBrush
        {
            get { return (Brush)base.GetValue(FocusBrushProperty); }
            set { base.SetValue(FocusBrushProperty, value); }
        }
        public static readonly DependencyProperty FocusBrushProperty =
            DependencyProperty.Register("FocusBrush", typeof(Brush), typeof(IconMenu),
                new FrameworkPropertyMetadata(Brushes.Black));

        public Brush FocusBackground
        {
            get { return (Brush)base.GetValue(FocusBackgroundProperty); }
            set { base.SetValue(FocusBackgroundProperty, value); }
        }
        public static readonly DependencyProperty FocusBackgroundProperty =
            DependencyProperty.Register("FocusBackground", typeof(Brush), typeof(IconMenu),
                new FrameworkPropertyMetadata(Brushes.Transparent));

        public bool IsSelected
        {
            get { return (bool)base.GetValue(IsSelectedProperty); }
            set { base.SetValue(IsSelectedProperty, value); }
        }
        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.Register("IsSelected", typeof(bool), typeof(IconMenu),
                new FrameworkPropertyMetadata(false));

        public bool IsShowNotice
        {
            get { return (bool)base.GetValue(IsShowNoticeProperty); }
            set { base.SetValue(IsShowNoticeProperty, value); }
        }
        public static readonly DependencyProperty IsShowNoticeProperty =
            DependencyProperty.Register("IsShowNotice", typeof(bool), typeof(IconMenu),
                new FrameworkPropertyMetadata(false));

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)base.GetValue(CornerRadiusProperty); }
            set { base.SetValue(CornerRadiusProperty, value); }
        }
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(IconMenu),
                new FrameworkPropertyMetadata(new CornerRadius(0)));

        public Thickness IconMargin
        {
            get { return (Thickness)base.GetValue(IconMarginProperty); }
            set { base.SetValue(IconMarginProperty, value); }
        }
        public static readonly DependencyProperty IconMarginProperty =
            DependencyProperty.Register("IconMargin", typeof(Thickness), typeof(IconMenu),
                new FrameworkPropertyMetadata(new Thickness(0, 0, 0, 0)));

        public Thickness NoticeMargin
        {
            get { return (Thickness)base.GetValue(NoticeMarginProperty); }
            set { base.SetValue(NoticeMarginProperty, value); }
        }
        public static readonly DependencyProperty NoticeMarginProperty =
            DependencyProperty.Register("NoticeMargin", typeof(Thickness), typeof(IconMenu),
                new FrameworkPropertyMetadata(new Thickness(0, 0, 0, 0)));

        public string MenuKey
        {
            get { return (string)base.GetValue(MenuKeyProperty); }
            set { base.SetValue(MenuKeyProperty, value); }
        }
        public static readonly DependencyProperty MenuKeyProperty =
            DependencyProperty.Register("MenuKey", typeof(string),
                typeof(IconMenu), new FrameworkPropertyMetadata(""));

        static IconMenu()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(IconMenu), new FrameworkPropertyMetadata(typeof(IconMenu)));
        }
    }
}
