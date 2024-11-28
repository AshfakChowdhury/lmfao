/*


This is the candlestick file.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Namespace for the project, grouping related classes together
namespace Project_2
{
    // Class representing a financial candlestick with open, high, low, close prices, volume, and date
    internal class CandleStick // Declaring the "CandleStick" class as internal, meaning it's accessible only within this assembly.
    {
        /// <summary>
        /// Gets or sets the opening price of the candlestick.
        /// </summary>
        public Decimal open { get; set; } // Property for the opening price of the candlestick, with getter and setter.

        /// <summary>
        /// Gets or sets the highest price of the candlestick.
        /// </summary>
        public Decimal high { get; set; } // Property for the highest price of the candlestick, with getter and setter.

        /// <summary>
        /// Gets or sets the lowest price of the candlestick.
        /// </summary>
        public Decimal low { get; set; } // Property for the lowest price of the candlestick, with getter and setter.

        /// <summary>
        /// Gets or sets the closing price of the candlestick.
        /// </summary>
        public Decimal close { get; set; } // Property for the closing price of the candlestick, with getter and setter.

        /// <summary>
        /// Gets or sets the trading volume for the candlestick.
        /// </summary>
        public long volume { get; set; } // Property for the trading volume of the candlestick, with getter and setter.

        /// <summary>
        /// Gets or sets the date of the candlestick.
        /// </summary>
        public DateTime date { get; set; } // Property for the date of the candlestick, with getter and setter.

        /// <summary>
        /// Initializes a new instance of the CandleStick class with default values.
        /// </summary>
        public CandleStick() { } // Default constructor that initializes an instance of CandleStick with default values.

        /// <summary>
        /// Initializes a new instance of the CandleStick class with specified values.
        /// </summary>
        /// <param name="date">The date of the candlestick.</param>
        /// <param name="open">The opening price (default is 0).</param>
        /// <param name="high">The highest price (default is 0).</param>
        /// <param name="low">The lowest price (default is 0).</param>
        /// <param name="close">The closing price (default is 0).</param>
        /// <param name="volume">The trading volume (default is 0).</param>
        public CandleStick(DateTime date, decimal open = 0, decimal high = 0, decimal low = 0, decimal close = 0, long volume = 0)
        {
            // Assign the parameter value to the date property
            this.date = date;

            // Assign the parameter value to the open property
            this.open = open;

            // Assign the parameter value to the high property
            this.high = high;

            // Assign the parameter value to the low property
            this.low = low;

            // Assign the parameter value to the close property
            this.close = close;

            // Assign the parameter value to the volume property
            this.volume = volume;
        }

        /// <summary>
        /// Initializes a new instance of the CandleStick class from a CSV row string.
        /// </summary>
        /// <param name="rowofData">A string containing comma-separated values for the candlestick data.</param>
        public CandleStick(String rowofData)
        {
            // Split the input CSV string into an array of substrings using a comma as the delimiter
            string[] subs = rowofData.Split(',');

            // Declare a temporary DateTime variable to parse the date
            DateTime tempDate;
            // Try to parse the first substring as a DateTime and assign it to the date property if successful
            if (DateTime.TryParse(subs[0], out tempDate))
            {
                date = tempDate; // Assign the parsed date to the date property
            }

            // Declare a temporary Decimal variable to parse numeric values
            Decimal temp;
            // Try to parse the second substring as a Decimal and assign it to the open property if successful
            if (Decimal.TryParse(subs[1], out temp))
            {
                open = temp; // Assign the parsed value to the open property
            }

            // Try to parse the third substring as a Decimal and assign it to the high property if successful
            if (Decimal.TryParse(subs[2], out temp))
            {
                high = temp; // Assign the parsed value to the high property
            }

            // Try to parse the fourth substring as a Decimal and assign it to the low property if successful
            if (Decimal.TryParse(subs[3], out temp))
            {
                low = temp; // Assign the parsed value to the low property
            }

            // Try to parse the fifth substring as a Decimal and assign it to the close property if successful
            if (Decimal.TryParse(subs[4], out temp))
            {
                close = temp; // Assign the parsed value to the close property
            }

            // Declare a temporary long variable to parse the volume
            long tempVolume;
            // Try to parse the sixth substring as a long and assign it to the volume property if successful
            if (long.TryParse(subs[5], out tempVolume))
            {
                volume = tempVolume; // Assign the parsed value to the volume property
            }
        }
    }
}

