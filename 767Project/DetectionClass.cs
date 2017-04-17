using System;
namespace _767Project
{
    public class DetectionClass
    {

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();



        [DllImport("user32.dll")]
        static extern void SetForegroundWindow(IntPtr window);

        [DllImport("user32.dll")]
        static extern double GetWindowThreadProcessId(IntPtr forWind, out int windID);


        List<int> processID = new List<int>();
        bool detected = false;

        public DetectionClass(List<int> p)
        {
            processID = p;
        }

        public void detection()
        {
            while (!detected)
            {
                Debug.Write("RUNNING");
                IntPtr currentOpen = GetForegroundWindow();
                int currentID;
                GetWindowThreadProcessId(currentOpen, out currentID);

               
                for (int x = 0; x < processIDs.Count; x++)
                {
                    if (currentID == processIDs[x])
                    {
                        detected = true;
                        Debug.Write("TRUE\n");
                    }
                }
            }
        }
    }
}
