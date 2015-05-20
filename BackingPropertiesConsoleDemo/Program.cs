// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="">
//   
// </copyright>
// <summary>
//   The program.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace BackingPropertiesConsoleDemo
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using BackingProperties;

    /// <summary>
    /// The program.
    /// </summary>
    internal class Program
    {
        #region Static Fields

        /// <summary>
        ///     The backing properties.
        /// </summary>
        private static readonly BackingProperties BackingProperties = new BackingProperties();

        #endregion

        #region Public Properties

        // Method Group
        /// <summary>
        ///     Gets the file count.
        /// </summary>
        public static int NovemberDays
        {
            get
            {
                return BackingProperties.GetPropertyValue(GetDaysInNovember);
            }
        }

        // Method Call
        /// <summary>
        ///     Gets the file count times ticks.
        /// </summary>
        public static long NovermberDaysTimesTicks
        {
            get
            {
                return BackingProperties.GetPropertyValue(() => Multiply(Ticks, NovemberDays));
            }
        }

        // Anonymous Method
        /// <summary>
        ///     Gets the ticks.
        /// </summary>
        public static long Ticks
        {
            get
            {
                return BackingProperties.GetPropertyValue(
                    () =>
                        {
                            var number = DateTime.Now.Ticks;
                            return number;
                        });
            }
        }

        // Constant Value
        /// <summary>
        ///     Gets the written in.
        /// </summary>
        public static string WrittenIn
        {
            get
            {
                return BackingProperties.GetPropertyValue(() => "This was written in C#");
            }
        }

        #endregion

        #region Methods

        /// <summary>
        ///     The get file count on c drive.
        /// </summary>
        /// <returns>
        ///     The <see cref="int" />.
        /// </returns>
        private static int GetDaysInNovember()
        {
            return DateTime.DaysInMonth(2015, 11);
        }

        /// <summary>
        /// The main.
        /// </summary>
        /// <param name="args">
        /// The args.
        /// </param>
        private static void Main(string[] args)
        {
            Console.WriteLine("Ticks {0}", Ticks);
            Console.WriteLine("NovemberDays {0}", NovemberDays);
            Console.WriteLine("NovermberDaysTimesTicks {0}", NovermberDaysTimesTicks);

            BackingProperties.SetPropertyValue(20, "NovemberDays");
            Console.WriteLine("We just set NovemberDays to 20, did it work?");
            Console.WriteLine("Retrieving NovemberDays With Cast To Int");
            Console.WriteLine("NovemberDays {0}", BackingProperties.GetPropertyValue<int>("NovemberDays"));
            Console.WriteLine("Retrieving NovemberDays By Accessing Property");
            Console.WriteLine("NovemberDays {0}", NovemberDays);
            Console.WriteLine("NovermberDaysTimesTicks {0}", NovermberDaysTimesTicks);

            Console.WriteLine("If I need to run the logic that usually gets NovemberDays I need remove the backing property");

            BackingProperties.Remove("NovemberDays");

            Console.WriteLine("Now the logic will fire again because the Backing Property is clear");
            Console.WriteLine("NovemberDays {0}", NovemberDays);
            Console.WriteLine("NovermberDaysTimesTicks {0}", NovermberDaysTimesTicks);

            Console.WriteLine(WrittenIn);
            Console.Read();
        }

        /// <summary>
        /// The multiply.
        /// </summary>
        /// <param name="ticks">
        /// The ticks.
        /// </param>
        /// <param name="fileCount">
        /// The file count.
        /// </param>
        /// <returns>
        /// The <see cref="long"/>.
        /// </returns>
        private static long Multiply(long ticks, int fileCount)
        {
            return ticks * fileCount;
        }

        public class Car
        {
            public string Make { get; set; }

            public string Color { get; set; }
        }

        #endregion
    }
}