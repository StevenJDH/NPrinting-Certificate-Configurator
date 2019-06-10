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
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace NPrinting_Certificate_Configurator.Extensions
{
    /// <summary>
    /// Useful extensions to overcome some missing functionality and limitations while keeping the code clean.
    /// </summary>
    public static class X509Certificate2Extensions
    {
        /// <summary>
        /// ASN.1 Universal Tag Numbers. More can be found here: https://www.obj-sys.com/asn1tutorial/node124.html
        /// </summary>
        private enum ASNTypeTag
        {
           Reserved = 0x00,
           Boolean = 0x01,
           Integer = 0x02,
           BitString = 0x03,
           OctetString = 0x04,
           NullString = 0x05,
           OID = 0x06,
           UTF8String = 0x0C,
           OIDRelative = 0x0D,
           Sequence = 0x30
        }

        /// <summary>
        /// Exports the current private key as a byte array.
        /// </summary>
        /// <param name="crt">Object owner of method.</param>
        /// <returns>Private key as byte array.</returns>
        /// <exception cref="T:System.NullReferenceException">Certificate instance is <see langword="null" />.</exception>
        /// <exception cref="T:System.CryptographicException">Certificate does not contain a private key.</exception>
        public static byte[] ExportPrivateKey(this X509Certificate2 crt)
        {
            if (!(crt.PrivateKey is RSACryptoServiceProvider csp))
            {
                throw new NullReferenceException("Certificate instance is null.");
            }
            if (csp.PublicOnly)
            {
                throw new CryptographicException("Certificate does not contain a private key.");
            }

            var parameters = csp.ExportParameters(true);

            using (var stream = new MemoryStream())
            using (var writer = new BinaryWriter(stream))
            {
                writer.Write((byte)ASNTypeTag.Sequence);

                using (var innerStream = new MemoryStream())
                using (var innerWriter = new BinaryWriter(innerStream))
                {
                    EncodeIntegerBigEndian(innerWriter, new byte[] { (byte)ASNTypeTag.Reserved }); // Version
                    EncodeIntegerBigEndian(innerWriter, parameters.Modulus);
                    EncodeIntegerBigEndian(innerWriter, parameters.Exponent);
                    EncodeIntegerBigEndian(innerWriter, parameters.D);
                    EncodeIntegerBigEndian(innerWriter, parameters.P);
                    EncodeIntegerBigEndian(innerWriter, parameters.Q);
                    EncodeIntegerBigEndian(innerWriter, parameters.DP);
                    EncodeIntegerBigEndian(innerWriter, parameters.DQ);
                    EncodeIntegerBigEndian(innerWriter, parameters.InverseQ);

                    var length = (int)innerStream.Length;
                    EncodeLength(writer, length);
                    writer.Write(innerStream.GetBuffer(), 0, length);
                }

                return stream.GetBuffer();
            }
        }

        /// <summary>
        /// Encodes integer based values using the Big-Endian convention to store byte data using the Most
        /// Significant Bit First ordering sequence for Intel-based platforms that use the Little-Endian
        /// convention.
        /// </summary>
        /// <param name="stream">Binary stream to write Big-Endian encoded bytes.</param>
        /// <param name="value">Array of bytes to encode as Big-Endian</param>
        /// <param name="forceUnsigned">Optionally force unsigned. The default is to force it.</param>
        private static void EncodeIntegerBigEndian(BinaryWriter stream, byte[] value, bool forceUnsigned = true)
        {
            stream.Write((byte)ASNTypeTag.Integer);
            var prefixZeros = value.TakeWhile(b => b == 0).Count();

            if (value.Length - prefixZeros == 0)
            {
                EncodeLength(stream, 1);
                stream.Write((byte)0);
                return;
            }

            if (forceUnsigned && value[prefixZeros] > 0x7f)
            {
                // Add a prefix zero to force unsigned if the MSB is 1
                EncodeLength(stream, value.Length - prefixZeros + 1);
                stream.Write((byte)0);
            }
            else
            {
                EncodeLength(stream, value.Length - prefixZeros);
            }

            for (var i = prefixZeros; i < value.Length; i++)
            {
                stream.Write(value[i]);
            }
        }

        /// <summary>
        /// If the integer contains fewer than 128 bytes, the Length field requires only one byte to specify
        /// the content length. If the integer is more than 127 bytes, bit 7 of the Length field is set to 1
        /// and bits 6 through 0 specify the number of additional bytes used to identify the content length.
        /// For additional information see:
        /// https://docs.microsoft.com/en-us/windows/desktop/seccertenroll/about-encoded-length-and-value-bytes
        /// </summary>
        /// <param name="stream">Binary stream to write encoded bytes.</param>
        /// <param name="length">Length of byte array.</param>
        private static void EncodeLength(BinaryWriter stream, int length)
        {
            if (length < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(length), "Length must be non-negative.");
            }

            // Short form
            if (length < 0x80)
            {
                stream.Write((byte)length);
                return;
            }

            // Long form
            var temp = length;
            var bytesRequired = 0;

            while (temp > 0)
            {
                temp >>= 8;
                bytesRequired++;
            }

            stream.Write((byte)(bytesRequired | 0x80));

            for (var i = bytesRequired - 1; i >= 0; i--)
            {
                stream.Write((byte)(length >> (8 * i) & 0xff));
            }
        }
    }
}
