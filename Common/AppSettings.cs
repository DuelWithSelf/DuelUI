using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuelUI.Common
{
    public class AppSettings
    {
        public const string CsfnDefaultSkin = "默认皮肤";
        public const string CfvSkinLight = "浅色皮肤";
        public const string CfvSkinDark = "深色皮肤";
        public static string GDefaultSkin = "浅色皮肤";

        public static void Init()
        {
            GDefaultSkin = DefaultSetting.GetValue("Skin", CsfnDefaultSkin, GDefaultSkin);
        }

        public static void Save()
        {
            DefaultSetting.SetValue("Skin", CsfnDefaultSkin, GDefaultSkin);
        }
    }
}
