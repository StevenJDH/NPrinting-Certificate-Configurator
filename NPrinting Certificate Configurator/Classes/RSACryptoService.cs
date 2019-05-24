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

using NPrinting_Certificate_Configurator.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace NPrinting_Certificate_Configurator.Classes
{
    class RSACryptoService
    {
        private readonly X509Certificate2 _certificate;

        public RSACryptoService(string pfxFile, string password)
        {
            // Load your certificate from file
            _certificate = new X509Certificate2(pfxFile, password,
                X509KeyStorageFlags.Exportable | X509KeyStorageFlags.PersistKeySet);

            HasPrivateKey = _certificate.HasPrivateKey;
            SerialNumber = _certificate.SerialNumber;
            Thumbprint = _certificate.Thumbprint;
            Subject = _certificate.Subject;
            Issuer = _certificate.Issuer;
        }

        /// <summary>
        /// Gets a value that indicates whether an System.Security.Cryptography.X509Certificates.X509Certificate2
        ///  object contains a private key.
        /// </summary>
        public bool HasPrivateKey { get; private set; }

        /// <summary>
        /// Gets the serial number of a certificate.
        /// </summary>
        public string SerialNumber { get; set; }

        /// <summary>
        /// Gets the thumbprint of a certificate.
        /// </summary>
        public string Thumbprint { get; set; }

        /// <summary>
        /// Gets the subject distinguished name from the certificate.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Gets the name of the certificate authority that issued the X.509v3 certificate.
        /// </summary>
        public string Issuer { get; set; }

        public string GetPublicKey()
        {
            var sb = new StringBuilder();

            //sb.AppendLine("Bag Attributes");
            //sb.AppendLine($"    Serial: {certificate.SerialNumber}");
            //sb.AppendLine($"    Thumbprint: {certificate.Thumbprint}");
            //sb.AppendLine($"subject={certificate.Subject}");
            //sb.AppendLine($"issuer={certificate.Issuer}");

            sb.AppendLine("-----BEGIN CERTIFICATE-----");
            sb.AppendLine(ConvertEx.ToBase64String(_certificate.Export(X509ContentType.Cert), 64));
            sb.Append("-----END CERTIFICATE-----");

            return sb.ToString();
        }

        public string GetPrivateKey()
        {
            var sb = new StringBuilder();

            sb.AppendLine("-----BEGIN RSA PRIVATE KEY-----");
            sb.AppendLine(ConvertEx.ToBase64String(_certificate.ExportPrivateKey(), 64));
            sb.Append("-----END RSA PRIVATE KEY-----");

            return sb.ToString();
        }

        public void SavePublicKeyPem(string path)
        {
            File.WriteAllText(path, GetPublicKey());
        }

        public void SavePrivateKeyPem(string path)
        {
            File.WriteAllText(path, GetPrivateKey());
        }
    }
}
