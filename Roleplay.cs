using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace Mary
{
    class Roleplay
    {
        static public void PoliceButtons(bool ShowButtons)
        {
            Clients user = Program.MySelf;
            if (ShowButtons)
            {
                if (Settings.SIREN)
                {
                    ButtonFactory.OpenButton(ButtonFactory.btnSiren);
                }
                ButtonFactory.OpenButton(ButtonFactory.btnCophud1);
            }
            else
            {
                if (!user.C.isResQ)
                {
                    ButtonFactory.RemoveButton(ButtonFactory.btnSiren);
                }
                ButtonFactory.RemoveButton(ButtonFactory.btnCophud1);
                if (Tracker.Trackee.Username == null)
                {
                    ButtonFactory.RemoveButton(ButtonFactory.btnTracker1);
                }
            }
        }

        static public void ResQButtons(bool showButtons)
        {
            Clients user = Program.MySelf;
            if (showButtons)
            {
                if (Settings.SIREN)
                {
                    ButtonFactory.OpenButton(ButtonFactory.btnSiren);
                }
                ButtonFactory.OpenButton(ButtonFactory.btnResqhud1);
            }
            else
            {
                if (!user.C.isCop)
                {
                    ButtonFactory.RemoveButton(ButtonFactory.btnSiren);
                }
                ButtonFactory.RemoveButton(ButtonFactory.btnResqhud1);
            }
        }

        static public void ExtraButtons(bool showButtons)
        {
            if (showButtons)
                ButtonFactory.OpenButton(ButtonFactory.btnExtrahud1);
            else
                ButtonFactory.RemoveButton(ButtonFactory.btnExtrahud1);
        }

        static public SirenMode clsSiren;
        public enum SirenMode
        {
            Audio = 1,
            Visual = 2,
            Both = 3
        }

        static int srnFlash = 1;
        static public bool SirenActive = false;
        static public bool PAUSE = false;
        static public int flashbtn = 1;
        static public void UpdateSiren(bool turnOff)
        {
            Process currentProcess = GetActiveProcess();
            if (currentProcess.MainWindowTitle == "Live for Speed")
            {
                sirenBtn = "";
                if (turnOff)
                {
                    SirenActive = false;
                    AudioSiren(true);
                    VisibleSiren(true);
                }
                else
                {
                    SirenActive = true;
                    if (clsSiren == SirenMode.Audio)
                    {
                        AudioSiren(false);
                    }
                    else if (clsSiren == SirenMode.Visual)
                    {
                        VisibleSiren(false);
                    }
                    else
                    {
                        AudioSiren(false);
                        VisibleSiren(false);
                    }
                }
                srnFlash = (srnFlash == 1) ? 2 : 1;
                ButtonFactory.btnSiren.Text = SirenActive ? "^" + srnFlash + "^J" + sirenBtn : "^1SIREN OFF";
                ButtonFactory.OpenButton(ButtonFactory.btnSiren);
            }
        }
        static public string sirenBtn = "";
        static public void AudioSiren(bool turnOff)
        {
            if (turnOff || PAUSE)
            {
                keybd_event(B, 0, KeyUp, 0);
            }
            else if (!PAUSE)
            {
                keybd_event(B, 0, KeyDown, 0);
                SendKeys.SendWait("{BACKSPACE}");
                sirenBtn = "ôôôôô";
            }
        }
        static public void VisibleSiren (bool turnOff)
        {
            if (turnOff || PAUSE)
            {
                SendKeys.SendWait("0");
            }
            else if (!PAUSE)
            {
                SendKeys.SendWait("3");
                SendKeys.SendWait("9");
                SendKeys.SendWait("{BACKSPACE}");
                SendKeys.SendWait("{BACKSPACE}");
                sirenBtn = sirenBtn += "œœœœœ";
            }
        }

        static public void CheckLFS()
        {
            bool exist = false;
            foreach (Process clsProcess in Process.GetProcesses())
            {
                if (clsProcess.MainWindowTitle.Contains("Live for Speed"))
                {
                    exist = true;
                }
            }
            if (!exist)
            {
                Program.Cons("You have shut down LFS and Mary will shut down in 10 seconds...");
                DUM.RegularRutineTime.Dispose();
                System.Threading.Thread.Sleep(10000);
                Environment.Exit(0);
            }
        }

        public static Process GetActiveProcess()
        {
            IntPtr hwnd = GetForegroundWindow();
            return hwnd != null ? GetProcessByHandle(hwnd) : null;
        }

        private static Process GetProcessByHandle(IntPtr hwnd)
        {
            try
            {
                uint processID;
                GetWindowThreadProcessId(hwnd, out processID);
                return Process.GetProcessById((int)processID);
            }
            catch { return null; }
        }
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")]
        private static extern Int32 GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);
        [DllImport("user32.dll", SetLastError = true)]
        static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);
        public const int KeyDown = 0x0001; //Key down flag
        public const int KeyUp = 0x0002; //Key up flag
        public const int B = 0x42; //B
    }
}