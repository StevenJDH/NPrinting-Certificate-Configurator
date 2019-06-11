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

using NPrinting_Certificate_Configurator.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NPrinting_Certificate_Configurator
{
    public partial class FrmMain : Form
    {
        private readonly NPrintingConfiguration _np;

        public FrmMain()
        {
            InitializeComponent();

            try
            {
                _np = new NPrintingConfiguration();
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show($"{ex.Message} Please ensure that NPrinting 17.0 or newer is installed.",
                    Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                Process.GetCurrentProcess().Kill();
            }

            _np.ServiceStatusChanged += Np_ServiceStatusChanged;
        }

        private void BtnBrowse_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "PKCS#12 certificate file (*.pfx)|*.pfx";
            openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtFile.Text = openFileDialog1.FileName;
                toolStripServiceStatus.Text = "Idle";
            }
        }

        private void ValidateFields()
        {
            btnConfigure.Enabled = (String.IsNullOrWhiteSpace(txtFile.Text) == false && 
                String.IsNullOrWhiteSpace(txtPassword.Text) == false);
        }

        private void TxtFile_TextChanged(object sender, EventArgs e)
        {
            ValidateFields();
        }

        private void TxtPassword_TextChanged(object sender, EventArgs e)
        {
            ValidateFields();
        }

        private async void BtnConfigure_Click(object sender, EventArgs e)
        {
            try
            {
                btnConfigure.Enabled = false;
                Properties.Settings.Default.NotProcessing = false;
                toolStripServiceStatus.Text = "Initializing...";

                var cert = new RSACryptoService(pfxFile: txtFile.Text, password: txtPassword.Text.Trim());

                if (cert.HasPrivateKey == false)
                {
                    toolStripServiceStatus.Text = "Failed to configure NPrinting. No private key present.";

                    MessageBox.Show("Error: The provided certificate does not have a private key.",
                        Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);

                    txtFile.Text = "";
                    txtPassword.Text = "";
                    btnConfigure.Enabled = true;
                    Properties.Settings.Default.NotProcessing = true;
                    return;
                }

                if (chkBackup.Checked)
                {
                    toolStripServiceStatus.Text = "Creating configuration backups...";

                    File.Copy(_np.NewsStand.ConfigFile,
                        $"{_np.NewsStand.ConfigFile}.bak", overwrite: true);
                    File.Copy(_np.WebConsole.ConfigFile,
                        $"{_np.WebConsole.ConfigFile}.bak", overwrite: true);
                }

                toolStripServiceStatus.Text = "Converting and installing certificate...";

                cert.SavePublicKeyPem(_np.NewsStand.SSLCertFile);
                cert.SavePrivateKeyPem(_np.NewsStand.SSLKeyFile);
                cert.SavePublicKeyPem(_np.WebConsole.SSLCertFile);
                cert.SavePrivateKeyPem(_np.WebConsole.SSLKeyFile);

                toolStripServiceStatus.Text = "Updating NPrinting configuration files...";

                _np.UpdateConfig(_np.NewsStand.ConfigFile, _np.NewsStand.SSLCertConfEntry,
                    _np.NewsStand.SSLKeyConfEntry);
                _np.UpdateConfig(_np.WebConsole.ConfigFile, _np.WebConsole.SSLCertConfEntry,
                    _np.WebConsole.SSLKeyConfEntry);

                toolStripServiceStatus.Text = "NPrinting was successfully configured.";

                if (MessageBox.Show("Configuration of NPrinting was completed successfully. " +
                                    "Do you want to restart the Qlik NPrinting Web Engine service?",
                        Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    await RestartServiceAsync();
                }
            }
            catch (CryptographicException)
            {
                toolStripServiceStatus.Text = "Failed to configure NPrinting. Invalid certificate password provided.";

                MessageBox.Show("Error: The password provided for the certificate is incorrect.",
                    Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException)
            {
                toolStripServiceStatus.Text = "Failed to configure NPrinting.";

                MessageBox.Show($"Error: {ex.Message}",
                    Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Properties.Settings.Default.NotProcessing = true;
            btnConfigure.Enabled = true;
            txtPassword.Text = "";
            txtPassword.Focus();
        }

        private async Task RestartServiceAsync()
        {
            if (await _np.RestartWebEngineServiceAsync())
            {
                MessageBox.Show("The Qlik NPrinting Web Engine service was restarted successfully.",
                    Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Failed to restart the Qlik NPrinting Web Engine service. " +
                                "Please see status below for more information.",
                    Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void Np_ServiceStatusChanged(object sender, ServiceStatusChangedEventArgs e)
        {
            toolStripServiceStatus.Text = e.Status;

            if (e.ErrorMessage != null)
            {
                toolStripServiceStatus.Text += $" {e.ErrorMessage}";
            }
        }

        private void MnuExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void MnuRestartService_Click(object sender, EventArgs e)
        {
            btnConfigure.Enabled = false;
            Properties.Settings.Default.NotProcessing = false;

            await RestartServiceAsync();

            Properties.Settings.Default.NotProcessing = true;
            ValidateFields();
        }

        private void MnuAbout_Click(object sender, EventArgs e)
        {
            using (FrmAbout frm = new FrmAbout())
            {
                frm.ShowDialog();
            }
        }

        private void MnuDisableConfig_Click(object sender, EventArgs e)
        {
            btnConfigure.Enabled = false;
            Properties.Settings.Default.NotProcessing = false;

            try
            {
                _np.DisableConfig(_np.NewsStand.ConfigFile);
                _np.DisableConfig(_np.WebConsole.ConfigFile);

                MessageBox.Show("Configuration for third-party certificates was disabled successfully.",
                    Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show($"Error: {ex.Message}",
                    Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Properties.Settings.Default.NotProcessing = true;
            ValidateFields();
        }
        private void MnuRestoreBackup_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to restore your configuration backups? " +
                                "This will also remove the third-party certificate.",
                    Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            btnConfigure.Enabled = false;
            Properties.Settings.Default.NotProcessing = false;

            try
            {
                string backupFile = $"{_np.NewsStand.ConfigFile}.bak";
                string publicKey = _np.NewsStand.SSLCertFile;
                string privateKey = _np.NewsStand.SSLKeyFile;

                if (File.Exists(backupFile))
                {
                    File.Copy(backupFile, _np.NewsStand.ConfigFile, overwrite: true);
                    File.Delete(backupFile);
                }
                if (File.Exists(publicKey)) { File.Delete(publicKey); }
                if (File.Exists(privateKey)) { File.Delete(privateKey); }

                backupFile = $"{_np.WebConsole.ConfigFile}.bak";
                publicKey = _np.WebConsole.SSLCertFile;
                privateKey = _np.WebConsole.SSLKeyFile;

                if (File.Exists(backupFile))
                {
                    File.Copy(backupFile, _np.WebConsole.ConfigFile, overwrite: true);
                    File.Delete(backupFile);
                }
                if (File.Exists(publicKey)) { File.Delete(publicKey); }
                if (File.Exists(privateKey)) { File.Delete(privateKey); }

                MessageBox.Show("All backups have been restored, and the certificates were removed.",
                    Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}",
                    Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Properties.Settings.Default.NotProcessing = true;
            ValidateFields();
        }

        private void MnuDoc_Click(object sender, EventArgs e)
        {
            OpenWebsite("https://help.qlik.com/en-US/nprinting/Content/NPrinting/" +
                        "DeployingQVNprinting/Installing-ssl-certificates.htm");
        }

        private void MnuGitHub_Click(object sender, EventArgs e)
        {
            OpenWebsite("https://github.com/StevenJDH/NPrinting-Certificate-Configurator");
        }

        private void MnuFaq_Click(object sender, EventArgs e)
        {
            OpenWebsite("https://github.com/StevenJDH/NPrinting-Certificate-Configurator/wiki/FAQ");
        }

        private void MnuCheckUpdates_Click(object sender, EventArgs e)
        {
            OpenWebsite("https://github.com/StevenJDH/NPrinting-Certificate-Configurator/releases/latest");
        }

        /// <summary>
        /// Sends a URL to the operating system to have it open in the default web browser.
        /// </summary>
        /// <param name="url">URL of website to open.</param>
        public static void OpenWebsite(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch (Exception) {/* Consuming exceptions */ }
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Properties.Settings.Default.NotProcessing == false)
            {
                e.Cancel = true;
            }
        }

        private void TxtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && btnConfigure.Enabled)
            {
                e.SuppressKeyPress = true;
                BtnConfigure_Click(this, EventArgs.Empty);
            }
        }
    }
}
