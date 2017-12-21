using Config;

namespace Utilities
{
    public static class ConfigUtils
    {
        /// <summary>
        /// Gets application setting or returns a default value on nay exception
        /// </summary>
        /// <param name="appSettingName"></param>
        /// <param name="defaultValue"></param>
        /// <returns>App setting value</returns>
        public static string GetAppSetting(string appSettingName, string defaultValue)
        {
            var appValue = defaultValue;

            try
            {
                if (!appSettingName.IsNullEmpty())
                {
                    appValue = typeof(AppConstants).GetProperty(appSettingName)?.GetValue(null) as string ??
                               defaultValue;
                }
            }
            catch
            {
                // Empty - default value
            }

            return appValue;
        }

        /// <summary>
        /// Gets application setting and returns as Int32
        /// </summary>
        /// <param name="appSettingame"></param>
        /// <param name="defaultValue"></param>
        /// <returns>App Setting Value</returns>
        public static int GetAppSettingInt32(string appSettingame, int defaultValue)
        {
            return GetAppSetting(appSettingame, defaultValue.ToString()).ToInt32();
        }
    }
}
