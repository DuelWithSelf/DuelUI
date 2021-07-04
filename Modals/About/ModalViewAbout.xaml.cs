using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace DuelUI.Modals.About
{
    /// <summary>
    /// ModalViewAbout.xaml 的交互逻辑
    /// </summary>
    public partial class ModalViewAbout : BaseModalView
    {
        public ModalViewAbout()
        {
            InitializeComponent();

            this.BdrViewMall.MouseLeftButtonUp += BdrViewMall_MouseLeftButtonUp;
        }

        private void BdrViewMall_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Process proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = "https://shop173071246.taobao.com";
            proc.Start();
        }
    }
}
