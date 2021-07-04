using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DuelUI.Common
{
    public class IniFileHelper
    {
        #region Fields And Consts

        public string IniCfgFile;
        private int iSingle = 255 * 10;
        private int iSection = 65535 * 10;
        private string sErrorTooSmallBuffer = "buffer size too small";

        #endregion

        #region 声明读写ini文件的api函数

        [DllImport("kernel32", EntryPoint = "GetPrivateProfileString")]
        private static extern int GetPrivateProfileString(
            string lpAppName,
            string lpKeyName,
            string lpDefault,
            StringBuilder lpReturnedString,
            int nSize,
            string lpFileName);

        [DllImport("kernel32", EntryPoint = "WritePrivateProfileString")]
        private static extern bool WritePrivateProfileString(
            string lpAppName,
            string lpKeyName,
            string lpString,
            string lpFileName);

        [DllImport("kernel32", EntryPoint = "GetPrivateProfileSection")]
        private static extern int GetPrivateProfileSection(
            string lpAppName,
            byte[] lpReturnedString,
            int nSize,
            string lpFileName);

        [DllImport("kernel32", EntryPoint = "WritePrivateProfileSection")]
        private static extern bool WritePrivateProfileSection(
          string lpAppName,
          string lpString,
          string lpFileName);

        [DllImport("kernel32", EntryPoint = "WritePrivateProfileSection")]
        private static extern bool WritePrivateProfileSection(
          string lpAppName,
          byte[] lpString,
          string lpFileName);

        [DllImport("kernel32", EntryPoint = "GetPrivateProfileSectionNames")]
        private static extern int GetPrivateProfileSectionNames(
            byte[] lpszReturnBuffer,
            int nSize,
            string lpFileName);

        #endregion

        #region 对外接口

        public string DoGetValue(string section, string key)
        {
            StringBuilder sBuilder = new StringBuilder(iSingle);
            int result = GetPrivateProfileString(section, key, "", sBuilder, iSingle, this.IniCfgFile);
            if (result == iSingle - 1)
            {
                throw new Exception(sErrorTooSmallBuffer);
            }

            return sBuilder.ToString();
        }

        public bool DoSetValue(string section, string key, string value)
        {
            return WritePrivateProfileString(section, key, value, this.IniCfgFile);
        }

        public Hashtable GetSection(string section)
        {
            Hashtable item = new Hashtable();
            string key, value;
            byte[] buffer = new byte[iSection];
            StringBuilder sBuilder;
            string[] pair;

            int bufLen = GetPrivateProfileSection(section, buffer, buffer.GetUpperBound(0), this.IniCfgFile);
            string sSection = Encoding.Default.GetString(buffer);

            if (bufLen > 0)
            {
                sBuilder = new StringBuilder();
                for (int i = 0; i < bufLen; i++)
                {
                    if (sSection[i] != 0)
                    {
                        sBuilder.Append(sSection[i]);
                    }
                    else if (sBuilder.Length > 0)
                    {
                        pair = sBuilder.ToString().Split('=');

                        if (pair.Length > 1)
                        {
                            key = pair[0].Trim();
                            value = pair[1].Trim();

                            item.Add(key, value);
                        }

                        sBuilder = new StringBuilder();
                    }
                }
            }

            return item;
        }

        public List<string> GetINISection(String section)
        {
            List<string> items = new List<string>();
            byte[] buffer = new byte[iSection];
            int bufLen = GetPrivateProfileSection(section, buffer, buffer.GetUpperBound(0), this.IniCfgFile);
            if (bufLen > 0)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < bufLen; i++)
                {
                    if (buffer[i] != 0)
                    {
                        sb.Append((char)buffer[i]);
                    }
                    else if (sb.Length > 0)
                    {
                        items.Add(sb.ToString());
                        sb = new StringBuilder();
                    }
                }
            }

            return items;
        }

        public bool SetSection(string section, Hashtable hSection)
        {
            StringBuilder sBuilder = new StringBuilder();

            int i = 0;
            foreach (DictionaryEntry de in hSection)
            {
                sBuilder.Append(de.Key + "=" + de.Value);

                if (i < hSection.Count - 1)
                    sBuilder.Append("\r\n");

                i++;
            }

            return WritePrivateProfileSection(section, sBuilder.ToString(), this.IniCfgFile);
        }

        public bool SetSection2(string section, Hashtable hSection)
        {
            byte[] buffer = new byte[iSection];

            int index = 0;

            foreach (DictionaryEntry de in hSection)
            {
                foreach (byte bVaule in de.Key.ToString())
                {
                    buffer[index] = bVaule;
                    index++;
                }

                buffer[index] = 61;
                index++;

                foreach (byte bVaule in de.Value.ToString())
                {
                    buffer[index] = bVaule;
                    index++;
                }

                buffer[index] = 0;
                index++;
            }

            return WritePrivateProfileSection(section, buffer, this.IniCfgFile);
        }

        public List<string> GetSectionNames()
        {
            List<string> items = new List<string>();
            byte[] buffer = new byte[iSingle];
            int bufLen = GetPrivateProfileSectionNames(buffer, buffer.GetUpperBound(0), this.IniCfgFile);
            if (bufLen > 0)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < bufLen; i++)
                {
                    if (buffer[i] != 0)
                    {
                        sb.Append((char)buffer[i]);
                    }
                    else if (sb.Length > 0)
                    {
                        items.Add(sb.ToString());
                        sb = new StringBuilder();
                    }
                }
            }

            return items;
        }

        #endregion
    }

    public class DefaultSetting
    {
        private static IniFileHelper GlobalIniFile;

        public static string DefaultIniFile = AppDomain.CurrentDomain.BaseDirectory + "Settings.ini";

        public static void Init(string sIniFile)
        {
            DefaultIniFile = sIniFile;
            if (GlobalIniFile == null)
                GlobalIniFile = new IniFileHelper();
            GlobalIniFile.IniCfgFile = DefaultIniFile;
        }

        private static bool IsAccess()
        {
            if (GlobalIniFile == null)
            {
                GlobalIniFile = new IniFileHelper();
                GlobalIniFile.IniCfgFile = DefaultIniFile;
            }

            try
            {
                string sPath = System.IO.Path.GetDirectoryName(DefaultIniFile);
                if (!Directory.Exists(sPath))
                    Directory.CreateDirectory(sPath);

                if (!File.Exists(DefaultIniFile))
                    File.Create(DefaultIniFile).Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

            return true;
        }

        public static void SetValue(string sSectionName, string sPropertyName, string sValue)
        {
            if (IsAccess())
            {
                GlobalIniFile.DoSetValue(sSectionName, sPropertyName, sValue);
            }
        }

        public static string GetValue(string sSectionName, string sPropertyName, string sDefaultValue = "")
        {
            try
            {
                if (IsAccess())
                {
                    Hashtable hsConfig = GlobalIniFile.GetSection(sSectionName);
                    if (hsConfig != null)
                    {
                        if (hsConfig.ContainsKey(sPropertyName))
                            return hsConfig[sPropertyName].ToString();
                        else
                            SetValue(sSectionName, sPropertyName, sDefaultValue);
                    }
                }
            }
            catch { }
            return sDefaultValue;
        }

        public static Hashtable GetSection(string sSectionName)
        {
            try
            {
                if (IsAccess())
                {
                    Hashtable hsConfig = GlobalIniFile.GetSection(sSectionName);
                    return hsConfig;
                }
            }
            catch { }
            return null;
        }
    }
}
