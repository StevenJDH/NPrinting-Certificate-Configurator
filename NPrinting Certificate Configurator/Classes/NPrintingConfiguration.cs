/**
 * This file is part of NPrinting Certificate Configurator <https://github.com/StevenJDH/NPrinting-Certificate-Configurator>.
 * Copyright (C) 2019 Steven Jenkins De Haro.
 *
 * NPrinting Certificate Configurator is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * NPrinting Certificate Configurator is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with NPrinting Certificate Configurator.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace NPrinting_Certificate_Configurator.Classes
{
    class NPrintingConfiguration
    {
        /// <summary>
        /// Gets status updates when restarting service.
        /// </summary>
        public event EventHandler<ServiceStatusChangedEventArgs> ServiceStatusChanged;

        public struct NConfig
        {
            public string ConfigFolder;
            public string ConfigFile;
            public string SSLCertConfEntry;
            public string SSLCertFile;
            public string SSLKeyConfEntry;
            public string SSLKeyFile;
        }

        public NPrintingConfiguration()
        {
            LoadNPrintingPaths();
        }

        public NConfig NewsStand { get; private set; }
        public NConfig WebConsole { get; private set; }

        /// <summary>
        /// Loads all the path information needed to configure NPrinting with third-party certificates.
        /// </summary>
        private void LoadNPrintingPaths()
        {
            // Qlik NPrinting Server June 2017 (17.4) or later
            string programData = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            string newsStand = Path.Combine(programData, "NPrinting", "newsstandproxy");
            string webConsole = Path.Combine(programData, "NPrinting", "webconsoleproxy");
            string certFilename = "NPrinting.crt";
            string keyFilename = "NPrinting.key";

            NewsStand = new NConfig()
            {
                ConfigFolder = newsStand,
                ConfigFile = Path.Combine(newsStand, "app.conf"),
                SSLCertConfEntry = Path.Combine(newsStand, certFilename),
                SSLCertFile = Path.Combine(newsStand, certFilename),
                SSLKeyConfEntry = Path.Combine(newsStand, keyFilename),
                SSLKeyFile = Path.Combine(newsStand, keyFilename)
            };

            WebConsole = new NConfig()
            {
                ConfigFolder = webConsole,
                ConfigFile = Path.Combine(webConsole, "app.conf"),
                SSLCertConfEntry = Path.Combine(webConsole, certFilename),
                SSLCertFile = Path.Combine(webConsole, certFilename),
                SSLKeyConfEntry = Path.Combine(webConsole, keyFilename),
                SSLKeyFile = Path.Combine(webConsole, keyFilename)
            };

            if (File.Exists(NewsStand.ConfigFile) && File.Exists(WebConsole.ConfigFile))
            {
                return;
            }

            // Qlik NPrinting Server February 2017 (17.3) or older until 17.0, since 16x is 32-bit.
            string programFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
            newsStand = Path.Combine(programFiles, "proxy", "newsstandproxy", "src", "qlik.com",
                "newsstandproxy", "conf");
            webConsole = Path.Combine(programFiles, "proxy", "webconsoleproxy", "src", "qlik.com",
                "webconsoleproxy", "conf");

            NewsStand = new NConfig()
            {
                ConfigFolder = newsStand,
                ConfigFile = Path.Combine(newsStand, "app.conf"),
                SSLCertConfEntry = Path.Combine(".", "src", "qlik.com", "newsstandproxy", "conf", certFilename),
                SSLCertFile = Path.Combine(newsStand, certFilename),
                SSLKeyConfEntry = Path.Combine(".", "src", "qlik.com", "newsstandproxy", "conf", keyFilename),
                SSLKeyFile = Path.Combine(newsStand, keyFilename)
            };

            WebConsole = new NConfig()
            {
                ConfigFolder = webConsole,
                ConfigFile = Path.Combine(webConsole, "app.conf"),
                SSLCertConfEntry = Path.Combine(".", "src", "qlik.com", "webconsoleproxy", "conf", certFilename),
                SSLCertFile = Path.Combine(webConsole, certFilename),
                SSLKeyConfEntry = Path.Combine(".", "src", "qlik.com", "webconsoleproxy", "conf", keyFilename),
                SSLKeyFile = Path.Combine(webConsole, keyFilename)
            };

            if (File.Exists(NewsStand.ConfigFile) && File.Exists(WebConsole.ConfigFile))
            {
                return;
            }

            throw new FileNotFoundException("One or both configuration files for NPrinting are missing.");
        }

        /// <summary>
        /// Updates NPrinting's configuration files with the needed certificate entries for
        /// third-party certificates.
        /// </summary>
        /// <param name="configPath">Path to conf file.</param>
        /// <param name="crtPath">Path entry for public key in conf file.</param>
        /// <param name="keyPath">Path entry for private key in conf file.</param>
        public void UpdateConfig(string configPath, string crtPath, string keyPath)
        {
            string[] lines = File.ReadAllLines(configPath);

            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Contains("http.sslcert="))
                {
                    lines[i] = $"http.sslcert={crtPath}";
                }
                if (lines[i].Contains("http.sslkey="))
                {
                    lines[i] = $"http.sslkey={keyPath}";
                }
            }

            File.WriteAllLines(configPath, lines);
        }

        /// <summary>
        /// Disables third-party certificate entries in NPrinting's configuration files by 
        /// commenting them out.
        /// </summary>
        /// <param name="configPath">Path to conf file.</param>
        public void DisableConfig(string configPath)
        {
            string[] lines = File.ReadAllLines(configPath);

            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].StartsWith("http.sslcert="))
                {
                    lines[i] = $"#{lines[i]}";
                }
                if (lines[i].StartsWith("http.sslkey="))
                {
                    lines[i] = $"#{lines[i]}";
                }
            }

            File.WriteAllLines(configPath, lines);
        }

        /// <summary>
        /// Restarts NPrinting's web engine service while raising status updates for event.
        /// </summary>
        /// <returns>True if successful, and false if not.</returns>
        public bool RestartWebEngineService()
        {
            var service = new ServiceController("Qlik NPrinting Web Engine");
            TimeSpan timeout = TimeSpan.FromMinutes(1);
            var args = new ServiceStatusChangedEventArgs();

            try
            {
                if (service.Status != ServiceControllerStatus.Stopped)
                {
                    // Stop Service
                    service.Stop();
                    args.Status = "Stopping NPrinting web engine service...";
                    OnServiceStatusChanged(args);
                    service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
                }

                //Restart service
                service.Start();
                args.Status = "Starting NPrinting web engine service...";
                OnServiceStatusChanged(args);
                service.WaitForStatus(ServiceControllerStatus.Running, timeout);

                args.Status = "Finished restarting service.";
                OnServiceStatusChanged(args);
            }
            catch (InvalidOperationException)
            {
                args.Status = "Failed to restart service.";
                args.ErrorMessage = "The service was not found on this computer.";
                OnServiceStatusChanged(args);
                return false;
            }
            catch (System.ServiceProcess.TimeoutException)
            {
                args.Status = "Failed to restart service.";
                args.ErrorMessage = "The Qlik NPrinting Web Engine service timed out.";
                OnServiceStatusChanged(args);
                return false;
            }
            
            return true;
        }

        /// <summary>
        /// Event to raise for status changes when restarting service.
        /// </summary>
        /// <param name="e">Status details to pass with event-</param>
        protected virtual void OnServiceStatusChanged(ServiceStatusChangedEventArgs e)
        {
            EventHandler<ServiceStatusChangedEventArgs> handler = ServiceStatusChanged;
            handler?.Invoke(this, e);
        }
    }
}
