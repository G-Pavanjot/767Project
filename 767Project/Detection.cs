using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Interop;
using System.Threading.Tasks;

namespace _767Project
{

    /*
     * Primary class used by the Detector thread in order to check if current window open is on the list of windows needing to be hidden
     */ 
    class Detection
    {
        IntPtr detectedWindow; //The Window handle of the detected window that needs to be hidden

        HidingWindow start; //a new HidingWindow that will be opened once detection occurs

        MainWindow main; //the main program window



        //This class requires the function GetForegroundWindow in order to get the handle of the currently opened window
        //Reference: [7] https://msdn.microsoft.com/en-us/library/windows/desktop/ms633505(v=vs.85).aspx
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();


        //This class requires the function SetForegroundWindow in order to set the foreground window
        //Reference: [5] https://msdn.microsoft.com/en-us/library/windows/desktop/ms633539(v=vs.85).aspx
        [DllImport("user32.dll")]
        static extern void SetForegroundWindow(IntPtr window);


        //This class requires the function GetWindowThreadProcessID in order to obtain the unique identifier of the currently opened window
        //Reference: [8] https://msdn.microsoft.com/en-us/library/windows/desktop/ms633522(v=vs.85).aspx
        // [9] http://stackoverflow.com/questions/6569405/how-to-get-active-process-name-in-c
        [DllImport("user32.dll")]
        static extern double GetWindowThreadProcessId(IntPtr forWind, out int windID);


        List<int> processIDs = new List<int>(); //the list of ID's of processes that need to be detected and hidden
        List<string> addedNames; //the list of program names that need to be detected and hidden that are not included in the processID list
        bool detected = false; //detection boolean, true if detection occurs
       

        public Detection(List<int> p, List<string> names, MainWindow mainWind)
        {
            processIDs = p;
            addedNames = names;
            main = mainWind;
        }

        public void detectionMethod()
        { 
            //While detection has not occured, keep cycling through this loop looking for a detection event until the thread is stopped
            while (!detected)
            {
                
                IntPtr currentOpen = GetForegroundWindow(); //get handle of currently open window
                int currentID;
                GetWindowThreadProcessId(currentOpen, out currentID); //get the process ID of the currently open window
                Process current = Process.GetProcessById(currentID); //Get the process information that goes by that currentID
                
                //iterate through every processID that is in the list that needs to be detected
                for (int x = 0; x < processIDs.Count; x++)
                {
                    
                    if (currentID == processIDs[x]) //if a match occurs between current process and one on the list
                    {
                        detected = true;
                        detectedWindow = currentOpen; //save the handle information of the screen
                        x = processIDs.Count; //exit out of loop
                    }
                }
                if (!detected) //if detection has still not occured through the check above
                {
                    if (addedNames.Count > 0) //if there are values in the addedNames list
                    {
                        //iterate through every added name that is in the list that needs to be detected
                        for (int x = 0; x < addedNames.Count; x++)
                        {

                            if (current.MainWindowTitle.Contains(addedNames[x]) || current.ProcessName.ToUpper().Contains(addedNames[x].ToUpper())) //check if process name contains the name of of the item in the list
                            {
                                detected = true;

                                detectedWindow = currentOpen; //save the handle information
                                x = addedNames.Count; //exit out of loop
                            }
                        }
                    }
                }
                if (detected) //if detection occurs
                {
                    start = new HidingWindow(detectedWindow, main); //create a new hiding window
                    IntPtr hideHandle = new WindowInteropHelper(start).Handle; //find the handle of the newly created hiding window so as to set it to the foreground
                                                                               //Reference: [10] https://msdn.microsoft.com/en-us/library/system.windows.interop.windowinterophelper(v=vs.110).aspx

                    start.ShowDialog(); //open the window
                    SetForegroundWindow(hideHandle); //set the window to be the foreground window
                }
            }

            //While the window is still being detected
            while (detected)
            {
                if (!start.IsVisible && detectedWindow != GetForegroundWindow()) //if the hiding window is closed and the foreground window is not the detected window (checks if the user selected "continue" in the hiding window)
                {
                    detected = false; //detected is back to false
                }
            }
            

            if(!detected)
            detectionMethod(); //repeat while thread is still running
        }
    }
}
