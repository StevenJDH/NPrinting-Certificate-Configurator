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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPrinting_Certificate_Configurator.Extensions
{
    /// <summary>
    /// Useful extensions to overcome some missing functionality and limitations while keeping then code clean.
    /// </summary>
    public static class ConvertEx
    {
        /// <summary>Converts an array of 8-bit unsigned integers to its equivalent string representation that is encoded with base-64 digits. A parameter specifies whether to insert line breaks in the return value.</summary>
        /// <param name="inArray">An array of 8-bit unsigned integers. </param>
        /// <param name="options">
        /// <see cref="F:System.Base64FormattingOptions.InsertLineBreaks" /> to insert a line break every 76 characters, or <see cref="F:System.Base64FormattingOptions.None" /> to not insert line breaks.</param>
        /// <returns>The string representation in base 64 of the elements in <paramref name="inArray" />.</returns>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="inArray" /> is <see langword="null" />. </exception>
        /// <exception cref="T:System.ArgumentException">
        /// <paramref name="options" /> is not a valid <see cref="T:System.Base64FormattingOptions" /> value. </exception>
        public static string ToBase64String(byte[] inArray, Base64FormattingOptions options)
        {
            return Convert.ToBase64String(inArray, options);
        }

        /// <summary>Converts an array of 8-bit unsigned integers to its equivalent string representation that is encoded with base-64 digits.</summary>
        /// <param name="inArray">An array of 8-bit unsigned integers.</param>
        /// <param name="insertLineBreaks">Insert a line break every n characters.</param>
        /// <returns>The string representation in base 64 of the elements in <paramref name="inArray" />.</returns>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="inArray" /> is <see langword="null" />. </exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="insertLineBreaks" /> is not a valid value.</exception>
        public static string ToBase64String(byte[] inArray, int insertLineBreaks)
        {
            if (insertLineBreaks < 1)
            {
                throw new ArgumentException($"{insertLineBreaks} is not a valid value.", nameof(insertLineBreaks));
            }

            var sb = new StringBuilder();
            var base64 = Convert.ToBase64String(inArray);

            // Output as Base64 with lines chopped at n characters.
            for (var i = 0; i < base64.Length; i += insertLineBreaks)
            {
                sb.AppendLine(base64.Substring(i, Math.Min(insertLineBreaks, base64.Length - i)));
            }

            return sb.ToString().TrimEnd();
        }
    }
}
