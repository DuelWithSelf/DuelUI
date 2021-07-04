using DuelUI.Common;
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

namespace DuelUI.Modals.Setting
{
    /// <summary>
    /// ModelViewSettings.xaml 的交互逻辑
    /// </summary>
    public partial class ModelViewSettings : BaseModalView
    {
        public ModelViewSettings()
        {
            InitializeComponent();

            this.InitSkinSetting();
        }

        #region Change Skin

        private void InitSkinSetting()
        {
            if (AppSettings.GDefaultSkin == AppSettings.CfvSkinLight)
                this.RbtnSkinLight.IsChecked = true;
            else if (AppSettings.GDefaultSkin == AppSettings.CfvSkinDark)
                this.RbtnSkinDark.IsChecked = true;

                this.RbtnSkinLight.Checked += RbtnSkinLight_Checked;
            this.RbtnSkinDark.Checked += RbtnSkinDark_Checked;
        }

        private void RbtnSkinDark_Checked(object sender, RoutedEventArgs e)
        {
            AppSettings.GDefaultSkin = AppSettings.CfvSkinDark;
            App app = App.Current as App;
            if (app != null)
                app.UpdateSkinSetting();
            AppSettings.Save();
        }

        private void RbtnSkinLight_Checked(object sender, RoutedEventArgs e)
        {
            AppSettings.GDefaultSkin = AppSettings.CfvSkinLight;
            App app = App.Current as App;
            if (app != null)
                app.UpdateSkinSetting();
            AppSettings.Save();
        }

        #endregion
    }
}
