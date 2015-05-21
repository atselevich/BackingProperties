// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Extensions.cs" company="">
//   
// </copyright>
// <summary>
//   The extensions.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace BackingProperties.Extensions
{
    using System;
    using System.Globalization;
    using System.Linq;

    /// <summary>
    /// The extensions.
    /// </summary>
    public static class Extensions
    {
        #region Public Methods and Operators

        /// <summary>
        /// Converts as int.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <param name="defaultValue">
        /// The default value.
        /// </param>
        /// <returns>
        /// The int value of string or default int value
        /// </returns>
        public static int ConvertAsInt(this string input, int defaultValue = default(int))
        {
            if (input.IsValidInteger())
            {
                var temp = Convert.ToInt64(input, CultureInfo.InvariantCulture);
                if (temp <= int.MaxValue && temp >= int.MinValue)
                {
                    return (int)temp;
                }
            }

            return defaultValue;
        }

        /// <summary>
        /// The is valid integer.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool IsValidInteger(this string input)
        {
            // long.MaxValue.ToString() = "9223372036854775807"
            // "9223372036854775807".Length = 19
            if (!string.IsNullOrEmpty(input) && input.Length <= 19)
            {
                if (input.Length == 1)
                {
                    return char.IsDigit(input[0]);
                }

                return (char.IsDigit(input[0]) || input[0] == '+' || input[0] == '-') && IsDigits(input.Substring(1));
            }

            return false;
        }

        #endregion

        #region Methods

        /// <summary>
        /// This is supplementary function
        /// </summary>
        /// <param name="input">
        /// string containing the data to validate.
        /// </param>
        /// <returns>
        /// <c>true</c> if the string is digits only; otherwise, <c>false</c>.
        /// </returns>
        internal static bool IsDigits(string input)
        {
            // Assume that input not null or empty!
            return input.All(char.IsDigit);
        }

        #endregion
    }
}