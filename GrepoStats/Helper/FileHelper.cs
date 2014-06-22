using System;
using System.IO;
using GrepoStats.Utils;

namespace GrepoStats.Helper
{
    public static class FileHelper
    {
        public static bool IsFileOld(string latestFile, string previousFile)
        {
            var latestModificationDate = File.GetLastWriteTime(latestFile);
            var previousModificationDate = File.GetLastWriteTime(previousFile);
            
            var difference = latestModificationDate - previousModificationDate;

            var actualDaysDifference = difference.Days;
            var actualHoursDifference = difference.Hours;
            var actualMinutesDifference = difference.Minutes;

            // TODO : To optimize performances
            // If others settings exists in App.Config beside of (Days, Hours, and Minutes)
            // => Call AppSettingsManager.ReadSetting(string key) instead
            var settings = AppSettingsHelper.ReadAllSettings();

            var fixedDays = 0;
            var fixedHours = 0;
            var fixedMinutes = 0;

            foreach (var setting in settings)
            {
                switch (setting.Key)
                {
                    case Constants.DaysSetting:
                        fixedDays = Convert.ToInt32(setting.Value);
                        break;
                    
                    case Constants.HoursSetting:
                        fixedHours = Convert.ToInt32(setting.Value);
                        break;
                    
                    case Constants.MinutesSetting:
                        fixedMinutes = Convert.ToInt32(setting.Value);
                        break;
                }
            }

            if (actualDaysDifference > fixedDays)
            {
                return true;
            }
            
            if(actualDaysDifference == fixedDays && actualHoursDifference > fixedHours)
            {
                return true;
            }
            
            if (actualDaysDifference == fixedDays && actualHoursDifference == fixedHours && actualMinutesDifference >= fixedMinutes)
            {
                return true;
            }

            return false;
        }
    }
}
