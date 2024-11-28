/*

*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

// Namespace for the project, grouping related classes and providing scope for identifiers
namespace Project_2
{
    // A static class containing the entry point for the application
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread] // Attribute indicating that the COM threading model for the application is single-threaded apartment (STA).
        static void Main() // Main method, the entry point where the program execution starts.
        {
            // Enable visual styles for the application.
            // This improves the look and feel of controls (e.g., buttons, text boxes) to match the current Windows theme.
            Application.EnableVisualStyles();

            // Set the default text rendering mode for the application.
            // Ensures that text rendering is compatible with visual styles, making it smoother and more visually appealing.
            Application.SetCompatibleTextRenderingDefault(false);

            // Start the application by running an instance of Form1.
            // The `Application.Run` method starts a standard application message loop and displays the specified form.
            Application.Run(new Form1());
        }
    }
}