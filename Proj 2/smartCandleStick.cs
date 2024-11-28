/*

*/

// Importing necessary namespaces
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;


// Defining a namespace for the project
namespace Project_2
{
    // Declaring a class named smartCandleStick that extends the functionality of CandleStick
    internal class smartCandleStick : CandleStick
    {
        // Public properties for additional candlestick characteristics
        public Decimal range { get; set; } // Difference between high and low prices.
        public Decimal bodyRange { get; set; } // Difference between open and close prices.
        public Decimal topPrice { get; set; } // Higher of open or close prices.
        public Decimal bottomPrice { get; set; } // Lower of open or close prices.
        public Decimal topTail { get; set; } // Difference between high and topPrice.
        public Decimal bottomTail { get; set; } // Difference between bottomPrice and low.
        public Boolean isBullish { get; set; } // Indicates if the candlestick is bullish.
        public Boolean isBearish { get; set; } // Indicates if the candlestick is bearish.
        public Boolean isNeutral { get; set; } // Indicates if the candlestick is neutral.
        public Boolean isMarubozu { get; set; } // Indicates if the candlestick is a Marubozu.
        public Boolean isDoji { get; set; } // Indicates if the candlestick is a Doji.
        public Boolean isDragonFlyDoji { get; set; } // Indicates if the candlestick is a Dragonfly Doji.
        public Boolean isGraveStoneDoji { get; set; } // Indicates if the candlestick is a Gravestone Doji.
        public Boolean isHammer { get; set; } // Indicates if the candlestick is a Hammer.
       

        // Static variable shared across all instances for defining Doji buffer threshold
        static Decimal dojiBuffer = 0.05M; // Specifies the allowable range to identify Doji patterns.

        // Constructor that takes an existing candlestick and calculates additional properties
        public smartCandleStick(CandleStick cs)
        {
            // Copy basic properties from the provided candlestick
            this.volume = cs.volume;
            this.open = cs.open;
            this.close = cs.close;
            this.high = cs.high;
            this.low = cs.low;
            this.date = cs.date;

            // Compute additional properties and patterns
            computeHigherProperties();
            computePatterns();
        }

        // Private method to calculate higher-level properties of the candlestick
        void computeHigherProperties()
        {
            range = high - low; // Total price range (high - low).
            bodyRange = Math.Abs(open - close); // Absolute value of (open - close) to handle negative differences.
            topPrice = Math.Max(open, close); // Maximum of open and close prices.
            bottomPrice = Math.Min(open, close); // Minimum of open and close prices.
            topTail = Math.Max(high - topPrice, 0); // Tail above the body, ensuring non-negative value.
            bottomTail = Math.Max(bottomPrice - low, 0); // Tail below the body, ensuring non-negative value.
        }

        // Private method to calculate candlestick patterns
        void computePatterns()
        {
            isBullish = isBullishcs(); // Determine if the candlestick is bullish.
            isBearish = isBearishcs(); // Determine if the candlestick is bearish.
            isNeutral = isNeutralcs(); // Determine if the candlestick is neutral.
            isMarubozu = isMarubozucs(); // Determine if the candlestick is a Marubozu.
            isDoji = isDojics(); // Determine if the candlestick is a Doji.
            isDragonFlyDoji = isDragonFlyDojics(); // Determine if the candlestick is a Dragonfly Doji.
            isGraveStoneDoji = isGraveStoneDojics(); // Determine if the candlestick is a Gravestone Doji.
            isHammer = isHammercs(); // Determine if the candlestick is a Hammer.

        }

        // Method to check if the candlestick is bullish (open < close)
        Boolean isBullishcs()
        {
            return open < close; // A bullish candlestick has a closing price higher than the opening price.
        }

        // Method to check if the candlestick is neutral
        Boolean isNeutralcs()
        {
            // A neutral candlestick has a small body and short tails.
            return (bodyRange <= 0.5M * range) && (topTail <= 0.1M * range) && (bottomTail <= 0.1M * range);
        }

        // Method to check if the candlestick is bearish (open > close)
        Boolean isBearishcs()
        {
            return open > close; // A bearish candlestick has a closing price lower than the opening price.
        }

        // Method to check if the candlestick is a Marubozu
        Boolean isMarubozucs()
        {
            return bodyRange == range; // A Marubozu candlestick has no tails, meaning body equals range.
        }

        // Method to check if the candlestick is a Doji
        Boolean isDojics()
        {
            // A Doji candlestick has a very small body, close to zero.
            return bodyRange < dojiBuffer * open;
        }

        // Method to check if the candlestick is a Hammer
        Boolean isHammercs()
        {
            // A Hammer candlestick has a small top tail, with a medium body within a specified range.
            return topTail < 0.03M * range && bodyRange >= 0.2M * range && bodyRange <= 0.3M * range;
        }


        // Method to check if the candlestick is a Dragonfly Doji
        Boolean isDragonFlyDojics()
        {
            // A Dragonfly Doji has a small body, short bottom tail, and a large top tail.
            return (bodyRange < 0.1M * range) && (bottomTail <= 0.1M * range) && (topTail >= 2 * bottomTail);
        }

        // Method to check if the candlestick is a Gravestone Doji
        Boolean isGraveStoneDojics()
        {
            // A Gravestone Doji has a small body, short top tail, and a large bottom tail.
            return (bodyRange < 0.1M * range) && (topTail <= 0.1M * range) && (bottomTail >= 2 * topTail);
        }
    }
}