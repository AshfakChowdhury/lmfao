/*

*/


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Project_2 // Define the namespace for the project
{
    public partial class ChartDisplayForm : Form // Partial class for the chart display form
    {
        private Chart chart1; // Reference to a Chart control (not used in the visible code)
        private BindingList<smartCandleStick> BindingCandleSticks { get; set; } // List of candlestick data for binding to the chart
        private List<CandleStick> templist; // Temporary storage for the list of candlestick data
        private string filename; // Stores the name of the file being processed

        public ChartDisplayForm() // Default constructor
        {
            InitializeComponent(); // Initialize form components
            DateTimePicker_EndDate.Value = DateTime.Now; // Set the end date picker to today's date
        }

        internal ChartDisplayForm(string filename, List<CandleStick> candlesticks, DateTimePicker start, DateTimePicker end)
        {
            InitializeComponent(); // Initialize form components

            templist = new List<CandleStick>(); // Initialize the temporary candlestick list
            templist = candlesticks; // Assign the provided candlestick data to the temporary list
            this.filename = filename; // Store the file name

            DateTimePicker_StartDate.Value = start.Value; // Set the start date picker to the provided start date
            DateTimePicker_EndDate.Value = DateTime.Now; // Set the end date picker to today's date

            getCandlesticksInDateRange(start.Value, end.Value); // Filter candlestick data based on the date range

            // Configure the chart title
            Chart_StockData.Titles.Clear(); // Clear any existing chart titles
            Chart_StockData.Titles.Add(Path.GetFileNameWithoutExtension(filename)); // Add a title using the file name (without extension)
            Chart_StockData.Titles[0].Font = new Font("Microsoft Sans Serif", 14.2F, FontStyle.Regular); // Set the font for the title

            // Bind candlestick data to the chart
            Chart_StockData.DataSource = BindingCandleSticks;
            Chart_StockData.DataBind();

            ConfigureChartForDailyData(); // Configure chart settings for daily data
            normalizeChart(); // Normalize the chart axis for better visualization
        }

        private void ConfigureChartForDailyData()
        {
            // Ensure the X-axis values are indexed for volume series
            Chart_StockData.Series["series_Volume"].IsXValueIndexed = true;
        }

        private void normalizeChart()
        {
            if (BindingCandleSticks.Count > 0) // Check if there is data to normalize
            {
                DateTime startDate = DateTimePicker_StartDate.Value; // Get the start date from the picker
                DateTime endDate = DateTimePicker_EndDate.Value; // Get the end date from the picker

                // Find the minimum price within the date range
                decimal minPrice = BindingCandleSticks
                    .Where(c => c.date >= startDate && c.date <= endDate)
                    .Min(c => c.low);

                // Find the maximum price within the date range
                decimal maxPrice = BindingCandleSticks
                    .Where(c => c.date >= startDate && c.date <= endDate)
                    .Max(c => c.high);

                // Add a 2% buffer to the min and max prices for better visualization
                double adjustedMin = (double)(minPrice * 0.98m); // Decrease min by 2%
                double adjustedMax = (double)(maxPrice * 1.02m); // Increase max by 2%

                // Set the Y-axis range for the candlestick chart
                Chart_StockData.ChartAreas["area_OHLC"].AxisY.Minimum = adjustedMin;
                Chart_StockData.ChartAreas["area_OHLC"].AxisY.Maximum = adjustedMax;

                // Set the Y-axis interval for clarity
                double range = adjustedMax - adjustedMin; // Calculate the range
                Chart_StockData.ChartAreas["area_OHLC"].AxisY.Interval = Math.Ceiling(range / 10); // Divide range into 10 parts

                // Format Y-axis labels to show two decimal places
                Chart_StockData.ChartAreas["area_OHLC"].AxisY.LabelStyle.Format = "F2";
            }
        }


        public void DetectPeaks()
        {
            // Clear all existing annotations to reset the chart for peak detection
            Chart_StockData.Annotations.Clear();

            // Determine the number of segments to divide the data for peak detection
            int segmentCount = Math.Min(8, Math.Max(2, BindingCandleSticks.Count / 50));

            // Calculate the size of each segment based on the total number of candlesticks
            int segmentSize = BindingCandleSticks.Count / segmentCount;

            // Loop through each segment to identify peaks
            for (int segment = 0; segment < segmentCount; segment++)
            {
                // Calculate the start and end indices for the current segment
                int startIndex = segment * segmentSize;
                int endIndex = (segment == segmentCount - 1) ? BindingCandleSticks.Count - 1 : startIndex + segmentSize;

                // Initialize variables to track the peak's index and its high value
                int peakIndex = -1;
                decimal peakHigh = decimal.MinValue;

                // Iterate through the current segment to find the highest value (peak)
                for (int i = startIndex; i <= endIndex; i++)
                {
                    var current = BindingCandleSticks[i]; // Get the current candlestick
                    if (current.high > peakHigh) // Check if the current high is greater than the tracked peak
                    {
                        peakHigh = current.high; // Update the highest value
                        peakIndex = i; // Update the index of the peak
                    }
                }

                // If a peak is found, annotate it and draw a horizontal line at the peak's price
                if (peakIndex != -1)
                {
                    AddArrowAnnotation(peakIndex, (double)peakHigh, Color.Green, true); // Add a green upward arrow at the peak
                    DrawHorizontalLine((double)peakHigh, Color.Green); // Draw a green horizontal line at the peak
                }
            }
        }

        public void DetectValleys()
        {
            // Clear all existing annotations to reset the chart for valley detection
            Chart_StockData.Annotations.Clear();

            // Determine the number of segments to divide the data for valley detection
            int segmentCount = Math.Min(8, Math.Max(2, BindingCandleSticks.Count / 50));

            // Calculate the size of each segment based on the total number of candlesticks
            int segmentSize = BindingCandleSticks.Count / segmentCount;

            // Loop through each segment to identify valleys
            for (int segment = 0; segment < segmentCount; segment++)
            {
                // Calculate the start and end indices for the current segment
                int startIndex = segment * segmentSize;
                int endIndex = (segment == segmentCount - 1) ? BindingCandleSticks.Count - 1 : startIndex + segmentSize;

                // Initialize variables to track the valley's index and its low value
                int valleyIndex = -1;
                decimal valleyLow = decimal.MaxValue;

                // Iterate through the current segment to find the lowest value (valley)
                for (int i = startIndex; i <= endIndex; i++)
                {
                    var current = BindingCandleSticks[i]; // Get the current candlestick
                    if (current.low < valleyLow) // Check if the current low is smaller than the tracked valley
                    {
                        valleyLow = current.low; // Update the lowest value
                        valleyIndex = i; // Update the index of the valley
                    }
                }

                // If a valley is found, annotate it and draw a horizontal line at the valley's price
                if (valleyIndex != -1)
                {
                    AddArrowAnnotation(valleyIndex, (double)valleyLow, Color.Red, false); // Add a red downward arrow at the valley
                    DrawHorizontalLine((double)valleyLow, Color.Red); // Draw a red horizontal line at the valley
                }
            }
        }

        private void AddArrowAnnotation(int index, double price, Color color, bool isUpward)
        {
            // Create a new arrow annotation
            ArrowAnnotation arrow = new ArrowAnnotation
            {
                AxisX = Chart_StockData.ChartAreas["area_OHLC"].AxisX, // Set the X-axis for the arrow
                AxisY = Chart_StockData.ChartAreas["area_OHLC"].AxisY, // Set the Y-axis for the arrow
                AnchorDataPoint = Chart_StockData.Series["series_OHLC"].Points[index], // Anchor the arrow to a specific data point
                LineColor = color, // Set the color of the arrow
                Height = isUpward ? -5 : 5, // Set the arrow height: negative for upward, positive for downward
                Width = 1, // Set the arrow width
                LineWidth = 1, // Set the arrow's line thickness
                ForeColor = color // Set the foreground color of the arrow
            };

            // Add the arrow annotation to the chart
            Chart_StockData.Annotations.Add(arrow);
        }

        private void DrawHorizontalLine(double price, Color color)
        {
            // Check if a horizontal line at the given price already exists to prevent duplicates
            bool lineExists = Chart_StockData.Annotations.OfType<HorizontalLineAnnotation>()
                                .Any(line => line.AnchorY == price && line.LineColor == color);

            if (!lineExists) // Proceed only if no line exists
            {
                // Create and configure a new horizontal line annotation
                HorizontalLineAnnotation line = new HorizontalLineAnnotation
                {
                    AxisX = Chart_StockData.ChartAreas["area_OHLC"].AxisX, // Set the X-axis for the line
                    AxisY = Chart_StockData.ChartAreas["area_OHLC"].AxisY, // Set the Y-axis for the line
                    IsSizeAlwaysRelative = false, // Use absolute positioning for the line
                    AnchorY = price, // Set the Y-coordinate (price level) for the line
                    ClipToChartArea = "area_OHLC", // Restrict the line to the defined chart area
                    LineColor = color, // Set the line's color
                    LineWidth = 1, // Set the line thickness
                    LineDashStyle = ChartDashStyle.Solid, // Use a solid line style
                    IsInfinitive = true // Extend the line across the entire chart
                };

                // Add the horizontal line annotation to the chart
                Chart_StockData.Annotations.Add(line);
            }
        }



        // Method to get candlesticks within a specific date range
        private void getCandlesticksInDateRange(DateTime start, DateTime end)
        {
            // Initialize or reset the BindingCandleSticks list
            BindingCandleSticks = new BindingList<smartCandleStick>();

            // Clear the list if it's not null
            if (BindingCandleSticks != null)
            {
                BindingCandleSticks.Clear();
            }

            // Iterate through the temporary candlestick list
            for (int i = 0; i < templist.Count; i++)
            {
                // Get the current candlestick from the temporary list
                CandleStick cs = templist[i];

                // Convert the regular candlestick to a smartCandlestick
                smartCandleStick scs = new smartCandleStick(cs);

                // If the current candlestick's date exceeds the end date, stop processing
                if (cs.date > end)
                    break;

                // If the candlestick's date is within the range, add it to the BindingCandleSticks list
                if (cs.date >= start)
                {
                    BindingCandleSticks.Add(scs);
                }
            }
        }

        // Event handler for the Update button click
        private void Button_Update_Click(object sender, EventArgs e)
        {
            // Clear the BindingCandleSticks list if it exists
            if (BindingCandleSticks != null)
            {
                BindingCandleSticks.Clear();
            }

            // Populate the BindingCandleSticks list with data in the specified date range
            getCandlesticksInDateRange(DateTimePicker_StartDate.Value, DateTimePicker_EndDate.Value);

            // Set the data source for the chart to the updated BindingCandleSticks list
            Chart_StockData.DataSource = BindingCandleSticks;

            // Bind the data source to the chart to refresh its display
            Chart_StockData.DataBind();

            // Configure the chart for daily data visualization
            ConfigureChartForDailyData();

            // Normalize the chart's appearance or data
            normalizeChart();

            // Clear existing annotations from the chart
            Chart_StockData.Annotations.Clear();

            // Get the currently selected pattern from the dropdown menu
            string selectedPattern = DropDownMenu_SelectPattern.SelectedItem?.ToString();

            // Perform actions based on the selected pattern
            if (selectedPattern == "Peak")
            {
                // Detect and annotate peaks on the chart
                DetectPeaks();
            }
            else if (selectedPattern == "Valley")
            {
                // Detect and annotate valleys on the chart
                DetectValleys();
            }
        }

        // Event handler for the Pattern button click
        private void Button_Pattern_Click(object sender, EventArgs e)
        {
            // Get the selected pattern from the dropdown menu
            String selectedPattern = DropDownMenu_SelectPattern.SelectedItem.ToString();

            // Clear existing annotations on the chart
            Chart_StockData.Annotations.Clear();

            // Iterate through each smartCandlestick in the BindingCandleSticks collection
            foreach (smartCandleStick scs in BindingCandleSticks)
            {
                // Perform actions based on the selected pattern
                switch (selectedPattern)
                {
                    case "Bullish":
                        if (scs.isBullish)
                        {
                            // Annotate a bullish pattern
                            ArrowAnnotationFunc(BindingCandleSticks.IndexOf(scs), "Bullish");
                        }
                        break;

                    case "Bearish":
                        if (scs.isBearish)
                        {
                            // Annotate a bearish pattern
                            ArrowAnnotationFunc(BindingCandleSticks.IndexOf(scs), "Bearish");
                        }
                        break;

                    case "Neutral":
                        if (scs.isNeutral)
                        {
                            // Annotate a neutral pattern
                            ArrowAnnotationFunc(BindingCandleSticks.IndexOf(scs), "Neutral");
                        }
                        break;

                    case "Marubozu":
                        if (scs.isMarubozu)
                        {
                            // Annotate a Marubozu pattern
                            ArrowAnnotationFunc(BindingCandleSticks.IndexOf(scs), "Marubozu");
                        }
                        break;

                    case "Doji":
                        if (scs.isDoji)
                        {
                            // Annotate a Doji pattern
                            ArrowAnnotationFunc(BindingCandleSticks.IndexOf(scs), "Doji");
                        }
                        break;

                    case "DragonFlyDoji":
                        if (scs.isDragonFlyDoji)
                        {
                            // Annotate a DragonFlyDoji pattern
                            ArrowAnnotationFunc(BindingCandleSticks.IndexOf(scs), "DragonFlyDoji");
                        }
                        break;

                    case "GravestoneDoji":
                        if (scs.isGraveStoneDoji)
                        {
                            // Annotate a GravestoneDoji pattern
                            ArrowAnnotationFunc(BindingCandleSticks.IndexOf(scs), "GravestoneDoji");
                        }
                        break;

                    case "Hammer":
                        if (scs.isHammer)
                        {
                            // Annotate a Hammer pattern
                            ArrowAnnotationFunc(BindingCandleSticks.IndexOf(scs), "Hammer");
                        }
                        break;



                    case "Peak":
                        // Detect and annotate peaks
                        DetectPeaks();
                        break;

                    case "Valley":
                        // Detect and annotate valleys
                        DetectValleys();
                        break;

                    default:
                        break;
                }
            }
        }

        // Method to add an arrow annotation for a specified data point and pattern
        private void ArrowAnnotationFunc(int ind, String str)
        {
            // Create a new ArrowAnnotation object
            System.Windows.Forms.DataVisualization.Charting.ArrowAnnotation arrow = new System.Windows.Forms.DataVisualization.Charting.ArrowAnnotation();

            // Get the data point at the specified index
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint = Chart_StockData.Series["series_OHLC"].Points[ind];

            // Configure the properties of the arrow annotation
            arrow.ClipToChartArea = "area_OHLC";
            arrow.AxisX = Chart_StockData.ChartAreas["area_OHLC"].AxisX;
            arrow.AxisY = Chart_StockData.ChartAreas["area_OHLC"].AxisY;
            arrow.AnchorDataPoint = dataPoint;
            arrow.LineColor = Color.Black;
            arrow.ForeColor = Color.Black;
            arrow.BackColor = Color.Black;
            arrow.BackSecondaryColor = Color.Black;
            arrow.ShadowColor = Color.Transparent;
            arrow.Height = -7;
            arrow.Width = 0.2;
            arrow.LineWidth = 1;
            arrow.Alignment = ContentAlignment.TopLeft;

            // Prevent the Y-axis from starting at zero and ensure size is relative
            arrow.AxisY.IsStartedFromZero = false;
            arrow.IsSizeAlwaysRelative = true;

            // Add the arrow annotation to the chart's annotations
            Chart_StockData.Annotations.Add(arrow);
        }

        // Placeholder event handler for chart clicks
        private void Chart_StockData_Click(object sender, EventArgs e)
        {
            // Currently does nothing
        }
    }
}
