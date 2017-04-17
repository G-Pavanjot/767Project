# 767Project
SecureStream software to prevent accidental windows from showing to an audience


README:
SecureStream README
By: Pavanjot Gill

To Run Program:
Method 1:
Open the .SLN (Visual Studio Project) in visual studio and build the project
The executable will be located in the bin > Debug folder

Method 2:
Executable is also provided within the 767Project > bin > Debug folder, where it can be run


To Use Program:
Step 1: Run the program (see above)

Step 2: Go to File > Settings and enter your broadcasting software name if you have one, else, this is not important to see if detection works accurately.

Step 3: Check whichever of the current programs showing in the datagrid that you do not want your audience to see

Step 4 (optional): If one of the programs you wish to hide is not currently running, you can press the "+" button to add the name of this program

Step 5: When all programs you wish to hide have been checked, press the start button to begin the detection thread which works in the background, always checking if the current window opened is one which needs to be hidden

If the detection thread has detected such a window, it will open up the hiding window screen which will become the new foreground window and maximized so as to hide the confidential window from everyone

Step 6: If the hiding window is open, user has the options to either continue to the confidential window (if they so wish), go to the main SecureStream program window, or to go to the user's broadcasting software (if the user has entered it in settings and it is open)



Code References:

[1] “Process.GetProcessesMethod(),”Process.GetProcessesMethod(System.Diagnostics).[Online]. Available: https://msdn.microsoft.com/en-us/library/1f3ys1f9(v=vs.110).aspx. [Accessed: 05- Mar-2017]. 

[2] “Threading Tutorial,” Threading Tutorial (C#). [Online]. Available: https://msdn.microsoft.com/en-us/library/aa645740(v=vs.71).aspx#vcwlkthreadingtutorialexample1creating. [Accessed: 08-Mar-2017].

[3] Timores, “The calling thread must be STA, because many UI components require this,” wpf - The calling thread must be STA, because many UI components require this - Stack Overflow, 24-Feb-2010. [Online]. Available: http://stackoverflow.com/questions/2329978/the-calling-thread-must-be-sta-because-many-ui-components-require-this. [Accessed: 12-Mar-2017].

[4] “Thread.ApartmentState Property,” Thread.ApartmentState Property (System.Threading). [Online]. Available: https://msdn.microsoft.com/en-us/library/system.threading.thread.apartmentstate(v=vs.110).aspx. [Accessed: 12-Mar-2017].

[5] “SetForegroundWindow function,” SetForegroundWindow function (Windows). [Online]. Available: https://msdn.microsoft.com/en-us/library/windows/desktop/ms633539(v=vs.85).aspx. [Accessed: 06-Mar-2017].

[6] Candide, “The calling thread cannot access this object because a different thread owns it,” c# - The calling thread cannot access this object because a different thread owns it - Stack Overflow, 16-Mar-2012. [Online]. Available: http://stackoverflow.com/questions/9732709/the-calling-thread-cannot-access-this-object-because-a-different-thread-owns-it. [Accessed: 05-Apr-2017].

[7] “GetForegroundWindowfunction,”GetForegroundWindowfunction(Windows).[Online]. Available: https://msdn.microsoft.com/en-us/library/windows/desktop/ms633505(v=vs.85).aspx. [Accessed: 05-Mar-2017]. 

[8] “GetWindowThreadProcessId function,” GetWindowThreadProcessId function (Windows). [Online]. Available: https://msdn.microsoft.com/en-us/library/windows/desktop/ms633522(v=vs.85).aspx. [Accessed: 05-Mar-2017].

[9] Svick, “How to Get Active Process Name in C#?,” Stack Overflow, 04-Jul-2011. [Online]. Available: http://stackoverflow.com/questions/6569405/how-to-get-active-process-name-in-c. [Accessed: 05-Mar-2017].

[10] “WindowInteropHelper Class,” WindowInteropHelper Class (System.Windows.Interop). [Online]. Available: https://msdn.microsoft.com/en-us/library/system.windows.interop.windowinterophelper(v=vs.110).aspx. [Accessed: 20-Mar-2017].
