using DuelUI.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace DuelUI
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            this.InitNormal();
            this.InitData();
        }

        private void InitData()
        {
            AppSettings.Init();
            UpdateSkinSetting();
        }

        public void UpdateSkinSetting()
        {
            Console.WriteLine("加载主题皮肤：" + AppSettings.GDefaultSkin);
            if (AppSettings.GDefaultSkin == AppSettings.CfvSkinLight)
            {
                string sWQCivilPlatTheme = "pack://application:,,,/DuelUI;component/Styles/SkinLight.xaml";
                this.AttachResourceDictionary(sWQCivilPlatTheme);
            }
            else if (AppSettings.GDefaultSkin == AppSettings.CfvSkinDark)
            {
                string sWQCivilPlatTheme = "pack://application:,,,/DuelUI;component/Styles/SkinDark.xaml";
                this.AttachResourceDictionary(sWQCivilPlatTheme);
            }
        }

        private void AttachResourceDictionary(string sPath)
        {
            ResourceDictionary resourceDictionary = new ResourceDictionary();
            Uri uri = new Uri(sPath, UriKind.RelativeOrAbsolute);
            resourceDictionary.Source = uri;
            Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
        }

        #region Common

        private void InitNormal()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        private void App_OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            try
            {
                Console.WriteLine(e.Exception.Message);
            }
            catch { }

            e.Handled = true;
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                var exception = e.ExceptionObject as Exception;
                Console.WriteLine(exception.Message);
            }
            catch { }
        }

        #endregion



        private void App_OnExit(object sender, ExitEventArgs e)
        {
          
        }
    }
}
