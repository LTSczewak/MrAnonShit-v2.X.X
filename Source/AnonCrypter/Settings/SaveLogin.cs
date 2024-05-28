using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.Win32;

namespace AnonCrypter.Settings
{
    public class SaveLogin
    {
        private const string AppName = "AnonCrypter";
        private const string RegistryPath = @"Software\" + AppName;
        private const string LicenseKeyName = "license";

        public static void WriteLicenseKey(string licenseKey)
        {
            using (RegistryKey key = Registry.CurrentUser.CreateSubKey(RegistryPath))
            {
                key.SetValue(LicenseKeyName, licenseKey);
            }
        }

        public static string ReadLicenseKey()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(RegistryPath))
            {
                if (key != null)
                {
                    return (string)key.GetValue(LicenseKeyName);
                }
                else
                {
                    return null;
                }
            }
        }

        public static bool CheckLicenseKeyExists()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(RegistryPath))
            {
                if (key != null && key.GetValue(LicenseKeyName) != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
