using System;
using System.Collections.Generic;
using System.Configuration;

namespace GrepoStats.Helper
{

    /// <summary>
    ///     Read application settings, add a new setting, and update an existing setting.
    ///     http://msdn.microsoft.com/en-us/library/system.configuration.configurationmanager.appsettings.aspx
    /// </summary>
    public static class AppSettingsHelper
    {
        /// <summary>
        /// Reads all settings.
        /// </summary>
        public static IDictionary<string, string> ReadAllSettings()
        {
            var settings = new Dictionary<string, string>();
                    
            try
            {
                var appSettings = ConfigurationManager.AppSettings;

                if (appSettings.Count == 0)
                {
                    // Log exception using log4net
                    Console.WriteLine(@"AppSettings is empty.");

                    return null;
                }
                
                foreach (var key in appSettings.AllKeys)
                {
                    settings.Add(key, appSettings[key]);                        
                }
            }
            catch (ConfigurationErrorsException)
            {
                // Log exception using log4net
                Console.WriteLine(@"Error reading app settings");

                return null;
            }

            return settings;
        }

        /// <summary>
        /// Reads the setting.
        /// </summary>
        /// <param name="key">The key.</param>
        public static string ReadSettingByKey(string key)
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                return appSettings[key] ?? "Not Found";
            }
            catch (ConfigurationErrorsException)
            {
                // Log exception using log4net
                Console.WriteLine(@"Error reading app settings");

                return null;
            }
        }

        /// <summary>
        /// Adds the update application settings.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public static bool AddUpdateAppSettings(string key, string value)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);

                return true;
            }
            catch (ConfigurationErrorsException)
            {
                // Log exception using log4net
                Console.WriteLine(@"Error writing app settings");

                return false;
            }
        }
    }
}
