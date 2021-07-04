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

namespace DuelUI.CommonFrm
{
    /// <summary>
    /// NavMenu.xaml 的交互逻辑
    /// </summary>
    public partial class NavMenu : BaseAnimView
    {
        public string MenuName
        {
            get { return (string)base.GetValue(MenuNameProperty); }
            set { base.SetValue(MenuNameProperty, value); }
        }
        public static readonly DependencyProperty MenuNameProperty =
            DependencyProperty.Register("MenuName", typeof(string),
                typeof(NavMenu), new FrameworkPropertyMetadata(""));

        public bool IsSelected
        {
            get { return (bool)base.GetValue(IsSelectedProperty); }
            set { base.SetValue(IsSelectedProperty, value); }
        }
        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.Register("IsSelected", typeof(bool), typeof(NavMenu),
                new FrameworkPropertyMetadata(false));

        public Brush TitleForeground
        {
            get { return (Brush)base.GetValue(TitleForegroundProperty); }
            set { base.SetValue(TitleForegroundProperty, value); }
        }
        public static readonly DependencyProperty TitleForegroundProperty =
            DependencyProperty.Register("TitleForeground", typeof(Brush), typeof(NavMenu),
                new FrameworkPropertyMetadata(Brushes.Black));

        public string MenuKey
        {
            get { return (string)base.GetValue(MenuKeyProperty); }
            set { base.SetValue(MenuKeyProperty, value); }
        }
        public static readonly DependencyProperty MenuKeyProperty =
            DependencyProperty.Register("MenuKey", typeof(string),
                typeof(NavMenu), new FrameworkPropertyMetadata(""));

        public NavMenu()
        {
            InitializeComponent();
            this.DataContext = this;
        }
    }
}
