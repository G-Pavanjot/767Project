using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace _767Project
{
    /// <summary>
    /// Interaction logic for HidingWindow.xaml
    /// </summary>
    public partial class HidingWindow : Window
    {

        IntPtr Continue; //the handle of the window that is being hidden
        String streamSoftware; //the name of the broadcasting software
        IntPtr streamID; //the handle of the broadcasting software if it is open

        MainWindow mainWnd; //the main window of SecureStream

        //This class requires the function SetForegroundWindow in order to set the foreground window
        //Reference: [5] https://msdn.microsoft.com/en-us/library/windows/desktop/ms633539(v=vs.85).aspx
        [DllImport("user32.dll")]
        static extern void SetForegroundWindow(IntPtr window); 

        /*
         * Open the hiding window when the detection class tries to hide a specific window from coming into the foreground
         */ 
        public HidingWindow(IntPtr Cont, MainWindow main)
        {
            InitializeComponent();
            mainWnd = main;
            if (File.Exists(@"./Settings.txt")) //check if user has stated what broadcasting software they use
            {
                StreamReader settingsReader = File.OpenText(@"./Settings.txt");
                streamSoftware = settingsReader.ReadLine();
                settingsReader.Close();
                btnOBS.Content = "To " + streamSoftware;
                Process[] activeSoftware = Process.GetProcesses(); //get all running processes to check if the broadcasting software is running
                for (int x = 0; x < activeSoftware.Length; x++)
                {
                    //if broadcasting software is running, then the To Streaming Software button should be enabled
                    if(activeSoftware[x].ProcessName.ToUpper().Contains(streamSoftware.ToUpper()) || activeSoftware[x].MainWindowTitle.ToUpper().Contains(streamSoftware.ToUpper()))
                    {
                        btnOBS.IsEnabled = true;
                        streamID = activeSoftware[x].MainWindowHandle;
                        x = activeSoftware.Length; //exit out of loop
                    }
                }

            }
            else //if user has not provided a settings file, then this button should be disabled
            {
                btnOBS.Content = "To Streaming Software";
                btnOBS.IsEnabled = false;
            }
            Continue = Cont;
        }

        /*
         * If the user wants to continue to the hidden window, this window will close and that window will be set to the foreground
         */ 
        private void btnContinue_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            SetForegroundWindow(Continue);
        }

        /*
         * If the user wants to go back to the main program window of SecureStream, in case they wish to stop the detection thread
         */
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {

            //Allow this window to have access to activating the main window
            //Reference: [6] http://stackoverflow.com/questions/9732709/the-calling-thread-cannot-access-this-object-because-a-different-thread-owns-it
            mainWnd.Dispatcher.Invoke(() =>
            { mainWnd.Activate(); 
            });
            this.Close(); //close this window
        
        }

        /*
        * If the user wants to go to their broadcasting software
        * Unfortunately, this function does not seem to work as of yet
        */
        private void btnOBS_Click(object sender, RoutedEventArgs e)
        {
            SetForegroundWindow(streamID); //set the foreground window to be the broadcasting software
            this.Close(); //close this window
        }
    }
}
