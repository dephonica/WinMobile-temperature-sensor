using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace GSMTemperature
{
    /// <summary>
    /// Provides access to configuration files for client applications on the .NET Compact Framework.
    /// </summary>

    public static class ConfigurationManager
    {
        #region Private Members
        private static Dictionary<string, string> appSettings = new Dictionary<string, string>();
        private static string configFile;
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets configuration settings in the appSettings section.
        /// </summary>
        public static Dictionary<string, string> AppSettings
        {
            get
            {
                return appSettings;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Static constructor.
        /// </summary>
        static ConfigurationManager()
        {
            // Determine the location of the config file
            ConfigurationManager.configFile = String.Format("{0}.config", System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);

            // Ensure configuration file exists
            if (File.Exists(ConfigurationManager.configFile))
            {
                try
                {
                    using (var configStream = File.OpenRead(ConfigurationManager.configFile))
                    {
                        var bytesBuffer = new byte[configStream.Length];
                        configStream.Read(bytesBuffer, 0, (int)configStream.Length);

                        var configText = Encoding.Unicode.GetString(bytesBuffer, 0, bytesBuffer.Length);

                        var configLines = configText.Split('\n');

                        foreach (var line in configLines)
                        {
                            var lineItems = line.Replace("\r", "").Replace("\n", "").Split('|');
                            if (lineItems.Length > 1)
                            {
                                appSettings.Add(lineItems[0], lineItems[1]);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    // ignore
                }
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Saves changes made to the configuration settings.
        /// </summary>
        public static void Save()
        {
            try
            {
                using (var configStream = File.OpenWrite(ConfigurationManager.configFile))
                {
                    var configString = new StringBuilder();

                    foreach (var configKey in appSettings.Keys)
                    {
                        var configLine = configKey.ToString() + "|" + appSettings[configKey.ToString()].ToString();
                        configString.AppendLine(configLine);
                    }

                    var configBytes = Encoding.Unicode.GetBytes(configString.ToString());
                    configStream.Write(configBytes, 0, configBytes.Length);
                }
            }
            catch
            {
                // ignore
            }
        }
        #endregion
    }
}
