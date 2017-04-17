/**
 * Author: Pavanjot Gill
 * CAS 767 Project: SecureStream
 * */


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace _767Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {


        Process[] runningProgs; //process array which will contain every running process
        List<int> processIDs; //List that will contain every process ID that detection class needs to search for
        bool progStart; //Boolean whether or not Detection thread is running
        Thread detector; //Detection Thread
        List<string> addedNames = new List<string>(); //List that will contain any custom programs that user has added into the datagrid


        /*
         * Start the main program, create processID list, make sure progStart is false, and call the method which fills in the datagrid
         */
        public MainWindow()
        {
            InitializeComponent();
            processIDs = new List<int>();
            progStart = false;
            fillData();
            
        }

        /*
         * fillData fills the datagrid with every running process so the user may select which ones will need to be hidden during stream 
         */
        private void fillData()
        {
            
            string processName;
            runningProgs = Process.GetProcesses(); //get all processes currently active
            //Source: [1] https://msdn.microsoft.com/en-us/library/1f3ys1f9.aspx

            for (int x = 0; x < runningProgs.Length; x++)
                        {
                            processName = runningProgs[x].MainWindowTitle; //get the name of the running process
                            if (processName != "")
                            {
                                lstProgs.Items.Add(new { SelectProg = false, ProgName = processName, procID = runningProgs[x].Id }); //add item to datagrid, consisting of the checkbox (false), window name, and process ID (this is hidden from user)
                            }

                        }

            lstProgs.Items.SortDescriptions.Add(new SortDescription("ProgName", ListSortDirection.Ascending)); //Sort the items in the datagrid based on their name alphabetically
   
        }

        /*
         * Function to call when the user selects or deselects an item in the datagrid
         */ 
        private void ProgSelected(object obj, SelectionChangedEventArgs sce)
        {
            if (lstProgs.SelectedItem != null)
            {
                string selItem = lstProgs.SelectedItem.ToString(); //obtain the selected item
                int selectedIndexVal = lstProgs.SelectedIndex;
                string[] ProgCols = selItem.Split('='); //trim the selected item's text so as to get each individual value (selected, name, ID)
                ProgCols[2] = ProgCols[2].Trim(new char[] { '}', ' ' });
                ProgCols[2] = ProgCols[2].Remove(ProgCols[2].Length - 8);
                if (ProgCols[1].Contains("True")) //if the item was true initially, it should be added as false now
                {
                    lstProgs.Items.Add(new { SelectProg = false, ProgName = ProgCols[2], procID = ProgCols[3].Trim(new char[] { '}', ' ' }) });
                }
                else //else, it should be added as true now
                {
                lstProgs.Items.Add(new { SelectProg = true, ProgName = ProgCols[2], procID = ProgCols[3].Trim(new char[] { '}', ' ' }) });
                }
                lstProgs.Items.Refresh(); //refresh the datagrid
                lstProgs.Items.RemoveAt(selectedIndexVal); //remove old item as new one with the correct checkbox state was added

            }

        }

        /*
         * Function to call when the Start/Stop button has been pressed
         */ 
        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            progStart = !progStart; //if the detection thread was running, the user has clicked stop so this should be come false, else it should be true
            lstProgs.IsEnabled = !lstProgs.IsEnabled; //disable or enable the datagrid depending on if the thread is running
            if (progStart) //if the user has clicked start
            {
                btnStart.Content = "Stop"; //change text of button
                string itemIter;

                /*
                 * Iterate through all of the items in the datagrid and add the processID's of each item that is checked into processID list
                 */ 
                for (int z = 0; z < lstProgs.Items.Count; z++)
                {
                    itemIter = lstProgs.Items.GetItemAt(z).ToString();
                    string[] itemValues = itemIter.Split('=');
                    if (itemValues[1].Contains("True"))
                    {
                        if (int.Parse(itemValues[3].Trim(new char[] { '}', ' ' })) != -10) //exclude any processID's that are equal to -10 as that is just a value given to those processes that are custom added by a user and do not have a processID
                        {
                            processIDs.Add(int.Parse(itemValues[3].Trim(new char[] { '}', ' ' }))); //add processID's
                        }
                        else
                        {
                          addedNames.Add(itemValues[2].Split(',')[0].Trim()); //if the processID is -10, then add the name of the program to use for the detection thread
                        }
                    }

                }


                //Thread reference: [2] https://msdn.microsoft.com/en-us/library/aa645740(v=vs.71).aspx#vcwlkthreadingtutorialexample1creating
                Detection detect = new Detection(processIDs, addedNames, this); //create a new instance of the detection class, providing in parameters the lists and this main program window

                detector = new Thread(new ThreadStart(detect.detectionMethod)); //start the detector thread
                detector.SetApartmentState(ApartmentState.STA);
                /*must set the apartment state to STA in order to allow the thread to open the hiding window, else error occurs:  
                 * 'The calling thread must be STA, because many UI components require this.'
                 * References: [3] http://stackoverflow.com/questions/2329978/the-calling-thread-must-be-sta-because-many-ui-components-require-this
                 * [4] https://msdn.microsoft.com/en-us/library/system.threading.thread.apartmentstate(v=vs.110).aspx
                 */

                detector.Start();
            }
            else //if the user has selected to stop the detection thread
            {
                if (detector.IsAlive) //if the thread is still alive, abort its operations
                {
                    detector.Abort();
                }
                btnStart.Content = "Start"; //change button text back to start
            }
 
        }

        /*
         * Function called when the refresh button is pressed, erases the data in the datagrid and refills it using the latest current processes information
         */ 
        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            lstProgs.Items.Clear();
            processIDs.Clear();
            fillData();
        }

        /*
         * Function called when the user selects the add button to add their own program name to hide which is not in the datagrid already
         */ 
        private void btnPlus_Click(object sender, RoutedEventArgs e)
        {
            AddingWindow Add = new AddingWindow(lstProgs); //open the addingwindow form
            Add.Show();
            
        }

        /*
         * Function called when the user selects the settings button to save their broadcasting software info
         */
        private void mnuSettings_Click(object sender, RoutedEventArgs e)
        {
            SettingsWind settings = new SettingsWind();
            settings.Show();
        }

        /*
         * Function called when the user is closing the program
         */
        private void MainProg_Closing(object sender, CancelEventArgs e)
        {
            ClosingWind();
        }

        /*
         * Function called when the user selects the exit button
         */
        private void MnuExit_Click(object sender, RoutedEventArgs e)
        {
            ClosingWind();
        }


        /*
         * Function to shut down the application, first it should check if the thread is still running, if it is, then that should be aborted before closing the application
         */ 
        private void ClosingWind()
        {
            if (detector != null)
            {
                if (detector.IsAlive)
                {
                    detector.Abort();
                }
            }
            Application.Current.Shutdown();
        }
    }

}
