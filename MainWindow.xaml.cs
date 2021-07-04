using DuelUI.Common;
using DuelUI.CommonFrm;
using DuelUI.Modals;
using DuelUI.Modals.About;
using DuelUI.Modals.Message;
using DuelUI.Modals.Setting;
using DuelUI.Modals.User;
using DuelUI.Modules;
using DuelUI.Modules.Home;
using DuelUI.Modules.Model;
using DuelUI.Modules.Project;
using DuelUI.Modules.Store;
using DuelUI.Modules.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DuelUI
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
            this.LoadUserProtrait();
            this.AttachWindowEventHandlers();
            this.AttachNavMenuEventHanders();
            this.AttachModalViewEventHandlers();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.Loaded -= MainWindow_Loaded;

            this.CreatePageByNavMenuKey("首页");
        }

        #region Modal Views

        private void AttachModalViewEventHandlers()
        {
            this.MenuSetting.MouseLeftButtonDown += MenuSetting_MouseLeftButtonDown;
            this.MenuEmail.MouseLeftButtonDown += MenuEmail_MouseLeftButtonDown;
            this.RectPortrait.MouseLeftButtonDown += RectPortrait_MouseLeftButtonDown;
            this.MenuAbout.MouseLeftButtonDown += MenuAbout_MouseLeftButtonDown;
        }

        private void MenuAbout_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.GdContent.Children.Clear();
            ModalViewAbout modelView = new ModalViewAbout();
            modelView.OnReturn += ModelView_OnReturn;
            this.GdContent.Children.Add(modelView);
            e.Handled = true;
        }

        private void RectPortrait_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.GdContent.Children.Clear();
            ModalViewUser modelView = new ModalViewUser();
            modelView.OnReturn += ModelView_OnReturn;
            this.GdContent.Children.Add(modelView);
            e.Handled = true;
        }

        private void MenuEmail_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.GdContent.Children.Clear();
            ModalViewMessage modelView = new ModalViewMessage();
            modelView.OnReturn += ModelView_OnReturn;
            this.GdContent.Children.Add(modelView);
            e.Handled = true;
        }

        private void MenuSetting_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.GdContent.Children.Clear();
            ModelViewSettings modelView = new ModelViewSettings();
            modelView.OnReturn += ModelView_OnReturn;
            this.GdContent.Children.Add(modelView);
            e.Handled = true;
        }

        private void ModelView_OnReturn(BaseModalView sender)
        {
            this.GdContent.Children.Clear();
        }

        #endregion

        #region Window Events

        private void AttachWindowEventHandlers()
        {
            this.BdrHeader.MouseDown += BdrHeader_MouseDown;
            this.MenuMaximize.MouseLeftButtonDown += MenuMaximize_MouseLeftButtonDown;
            this.MenuMinimize.MouseLeftButtonDown += MenuMinimize_MouseLeftButtonDown;
            this.MenuShutdown.MouseLeftButtonDown += MenuShutdown_MouseLeftButtonDown;
        }

        private void MenuShutdown_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AppEvents.ExecuteBeforeShutDown();
            Application.Current.Shutdown();
            e.Handled = true;
        }

        private void MenuMinimize_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
            e.Handled = true;
        }

        private void MenuMaximize_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.ResizeWindow();
            e.Handled = true;
        }

        private void BdrHeader_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 1)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                    DragMove();
            }
            //else if (e.ClickCount == 2)
            //{
            //    this.ResizeWindow();
            //}
        }

        private void ResizeWindow()
        {
            if (this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Maximized;
                string sData = this.TryFindResource("PathData.WindowRestore") as string;
                this.MenuMaximize.IconData = sData;
            }
            else
            {
                this.WindowState = WindowState.Normal;
                string sData = this.TryFindResource("PathData.WindowMaximize") as string;
                this.MenuMaximize.IconData = sData;
            }
        }

        #endregion

        #region Protrait

        private void LoadUserProtrait()
        {
            string sFile = AppDomain.CurrentDomain.BaseDirectory + "Datas/Portrait.jpg";
            if (File.Exists(sFile))
            {
                BitmapImage bitmap = new BitmapImage(new Uri(sFile));
                bitmap.DecodePixelWidth = 36;
                bitmap.DecodePixelHeight = 36;
                ImageBrush brush = new ImageBrush(bitmap);
                this.RectPortrait.Fill = brush;
            }
        }

        #endregion

        #region Navigate Page

        private void AttachNavMenuEventHanders()
        {
            for(int i= 0; i< this.CvNavMenus.Children.Count; i++)
            {
                if(this.CvNavMenus.Children[i] is NavMenu)
                {
                    NavMenu navMenu = this.CvNavMenus.Children[i] as NavMenu;
                    navMenu.MouseLeftButtonDown += NavMenu_MouseLeftButtonDown;
                }
            }
        }

        private void CancelSelectNavMenus()
        {
            for (int i = 0; i < this.CvNavMenus.Children.Count; i++)
            {
                if (this.CvNavMenus.Children[i] is NavMenu)
                {
                    NavMenu navMenu = this.CvNavMenus.Children[i] as NavMenu;
                    navMenu.IsSelected = false;
                }
            }
        }

        private void NavMenu_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavMenu navMenu = sender as NavMenu;
            if (navMenu.IsSelected)
                return;
            this.CancelSelectNavMenus();
            navMenu.IsSelected = true;
            this.FocusToNavMenu(navMenu);
            e.Handled = true;
        }

        private DoubleAnimation AnimNavSelector;

        private void FocusToNavMenu(NavMenu navMenu)
        {
            double dTargetX = navMenu.X + navMenu.Width / 2d - this.NavSelector.Width / 2d;
            AnimNavSelector = new DoubleAnimation(dTargetX, TimeSpan.FromSeconds(0.2));
            this.NavSelector.BeginAnimation(NavSelector.XProperty, AnimNavSelector);

            this.CreatePageByNavMenuKey(navMenu.MenuKey);
        }

        private Hashtable HsPages;

        private void CreatePageByNavMenuKey(string sMenuKey)
        {
            if (this.HsPages == null)
                this.HsPages = new Hashtable();

            if (this.HsPages.Contains(sMenuKey))
            {
                BaseModuleView moduleView = this.HsPages[sMenuKey] as BaseModuleView;
                this.GdMain.Children.Clear();
                this.GdMain.Children.Add(moduleView);
            }
            else
            {
                BaseModuleView moduleView = null;
                if (sMenuKey == "首页")
                    moduleView = new ModuleViewHome();
                else if (sMenuKey == "我的项目")
                    moduleView = new ModuleViewProject();
                else if (sMenuKey == "工具箱")
                    moduleView = new ModuleViewTools();
                else if (sMenuKey == "户型")
                    moduleView = new ModuleViewModel();
                else if (sMenuKey == "商城")
                    moduleView = new ModuleViewStore();

                this.GdMain.Children.Clear();
                this.GdMain.Children.Add(moduleView);
                this.HsPages.Add(sMenuKey, moduleView);
            }
        }

        #endregion
    }
}
