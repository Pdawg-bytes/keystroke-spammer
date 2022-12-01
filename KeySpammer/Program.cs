using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Authentication;
using System.Threading.Tasks;
using WindowsInput;

namespace Keystroke_Spammer
{
    class Program
    {
        // import user32 for keystrokes
        [DllImport("User32.dll")]
        static extern int SetForegroundWindow(IntPtr point);

        static void Main(String[] args)
        {
            Console.WriteLine("KeystrokeSpammer: A keystroke spammer that sends one key over a set duration to a Microsoft Edge window");
            Console.WriteLine("------------------------------------------------------------------------------------------------------------\n");

            Console.WriteLine("Searching for MSEDGE processes...");
            // Searches for edge process
            Process[] ps = Process.GetProcessesByName("msedge");
            // Counts array and prints count
            Console.WriteLine("Found Processes: " + ps.Length);
            // Create Process object
            Process edgeProcess = ps.FirstOrDefault();
            // Print object
            Console.WriteLine("Process Name: " + edgeProcess + "\n");

            try
            {
                if (edgeProcess != null)
                {

                    // Get keysrokes count
                    Console.WriteLine("How many keystrokes would you like to send? (MUST BE A NUMBER!)");
                    int KSNum = Convert.ToInt32(Console.ReadLine());

                    // Set delay
                    Console.WriteLine("Spam delay (in ms)");
                    int DelayNum = Convert.ToInt32(Console.ReadLine());

                    // Start spammer logic
                    Console.WriteLine("Start spammer? (Y/N)");
                    string DoStart = Console.ReadLine().ToLower();
                    if (DoStart == "y")
                    {
                        Console.WriteLine("");

                        // Start edge process
                        Process.Start("C:\\Program Files (x86)\\Microsoft\\Edge\\Application\\msedge.exe");
                        // Bring edge window into focus
                        Console.WriteLine("Bringing edge window into focus");
                        IntPtr windowHandle = edgeProcess.MainWindowHandle;
                        SetForegroundWindow(windowHandle);

                        Console.WriteLine("Is the textfield selected? Y/N");
                        string TextField = Console.ReadLine().ToLower();
                        if (TextField == "y")
                        {
                            // Init keystoke simulator object
                            InputSimulator isim = new InputSimulator();
                            Console.WriteLine("");
                            Console.WriteLine("Starting in 5 seconds...");
                            Thread.Sleep(5000);

                            int ksadd = 0;
                            do
                            {
                                ksadd++;
                                // Simulates keyboard logic
                                isim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_V);
                                Thread.Sleep(DelayNum);
                                isim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_V);
                                isim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.RETURN);
                                Thread.Sleep(DelayNum);
                                isim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.RETURN);

                            } while (ksadd < KSNum);
                            Console.WriteLine("Spammer running!");
                            Console.WriteLine("");
                            Console.WriteLine("Keystrokes sent: " + ksadd + "\n");
                            Console.WriteLine("Press any key to close");
                            Console.ReadKey();
                        }
                        else if (TextField == "n")
                        {
                            Console.WriteLine("Exiting...");
                            Environment.Exit(1000);
                        }
                    }
                    else if (DoStart == "n")
                    {
                        Console.WriteLine("Exiting...");
                        Environment.Exit(1000);
                    }
                }
                else
                {
                    Console.WriteLine("Edge processs could not be found.");
                }
            }
            catch(Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
            }
        }
    }
}