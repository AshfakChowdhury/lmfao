/*

*/

/// <summary>
/// Importing necessary namespaces for application functionality.
/// </summary>
using System; 
using System.Collections.Generic; 
using System.ComponentModel; 
using System.Data; 
using System.Drawing; 
using System.IO; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks; 
using System.Windows.Forms; 

/// <summary>
/// Defines the main namespace for the project.
/// </summary>
namespace Project_2
{
    /// <summary>
    /// Partial class for the main form, inheriting from Form to create the application's UI.
    /// </summary>
    public partial class Form1 : Form
    {
        /// <summary>
        /// Static string to represent the required header format for input files.
        /// </summary>
        private static String referenceString = "Date,Open,High,Low,Close,Volume";

        /// <summary>
        /// Default constructor for the FormEntry class.
        /// </summary>
        public Form1()
        {
            // Initializes all components in the form.
            InitializeComponent();
        }

        /// <summary>
        /// Event handler triggered when a file is selected in the file dialog.
        /// </summary>
        /// <param name="sender">Object that triggered the event.</param>
        /// <param name="e">Event arguments containing cancel-related information.</param>
        private void LoadButtonOpenFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            // Load candlestick data from the selected files.
            List<List<CandleStick>> allCandlesticks = loadCandlesticks(LoadButtonOpenFileDialog.FileNames);

            // Open chart display forms for the loaded data.
            openDisplayChartForms(LoadButtonOpenFileDialog.FileNames, allCandlesticks);
        }

        /// <summary>
        /// Converts an array of filenames into a list and loads candlestick data.
        /// </summary>
        /// <param name="arrayofFilenames">Array of filenames to be loaded.</param>
        /// <returns>A list of lists containing candlestick data.</returns>
        private List<List<CandleStick>> loadCandlesticks(string[] arrayofFilenames)
        {
            // Converts the array of filenames into a List of strings.
            List<string> listofFilenames = arrayofFilenames.ToList<string>();

            // Calls the main loading method using the list of filenames.
            return loadCandlesticks(listofFilenames);
        }

        /// <summary>
        /// Loads candlestick data from a list of filenames.
        /// </summary>
        /// <param name="listofFilenames">List of filenames to load data from.</param>
        /// <returns>A list of lists containing candlestick data for each file.</returns>
        private List<List<CandleStick>> loadCandlesticks(List<string> listofFilenames)
        {
            // Initializes a list to store all candlestick data for each file.
            List<List<CandleStick>> resultingList = new List<List<CandleStick>>(listofFilenames.Count());

            // Iterates through each filename in the list.
            foreach (string filename in listofFilenames)
            {
                // Loads candlestick data for the current file.
                List<CandleStick> candlesticks = loadStockFromFile(filename);

                // Adds the loaded data to the result list.
                resultingList.Add(candlesticks);
            }

            // Returns the list containing all loaded candlestick data.
            return resultingList;
        }

        /// <summary>
        /// Reads and parses candlestick data from a file.
        /// </summary>
        /// <param name="filename">Name of the file to be read.</param>
        /// <returns>A list of candlestick objects read from the file.</returns>
        private List<CandleStick> loadStockFromFile(string filename)
        {
            // Initializes a list to hold candlestick data.
            List<CandleStick> templist = new List<CandleStick>(1024);

            // Ensures the StreamReader is disposed of properly after use.
            using (StreamReader sr = new StreamReader(filename))
            {
                // Variable to hold each line read from the file.
                string line;

                // Reads the first line of the file, which is expected to be the header.
                string header = sr.ReadLine();

                // Checks if the header matches the expected format.
                if (header == referenceString)
                {
                    // Reads the remaining lines of the file.
                    while ((line = sr.ReadLine()) != null)
                    {
                        // Creates a new candlestick object from the current line.
                        CandleStick cs = new CandleStick(line);

                        // Adds the candlestick object to the list.
                        templist.Add(cs);
                    }
                }
            }

            // Returns the list of candlestick objects.
            return templist;
        }

        /// <summary>
        /// Opens a new chart display form for each loaded file and its candlestick data.
        /// </summary>
        /// <param name="arrayofFilenames">Array of filenames for the loaded files.</param>
        /// <param name="allCandlesticks">List of candlestick data for each file.</param>
        private void openDisplayChartForms(string[] arrayofFilenames, List<List<CandleStick>> allCandlesticks)
        {
            // Loops through each filename and its associated candlestick data.
            for (int i = 0; i < arrayofFilenames.Length; i++)
            {
                // Creates a new chart display form for the current file.
                ChartDisplayForm newForm = new ChartDisplayForm(arrayofFilenames[i], allCandlesticks[i], DateTimePicker_StartDate, DateTimePicker_EndDate);

                // Displays the new form to the user.
                newForm.Show();
            }
        }

        /// <summary>
        /// Event handler triggered when the Load button is clicked.
        /// </summary>
        /// <param name="sender">Object that triggered the event.</param>
        /// <param name="e">Event arguments.</param>
        private void button_Load_Click(object sender, EventArgs e)
        {
            // Opens the file dialog to allow the user to select files.
            LoadButtonOpenFileDialog.ShowDialog();
        }

        /// <summary>
        /// Placeholder for StartDateLabel click event handler.
        /// </summary>
        private void StartDateLabel_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Placeholder for form load event handler.
        /// </summary>
        private void FormEntry_Load(object sender, EventArgs e)
        {
        }
    }
}
