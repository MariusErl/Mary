using InSimDotNet.Packets;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Mary
{
    class Roleplay
    {
        static public void PoliceButtons(bool ShowButtons)
        {
            Clients user = Identification.MySelf;
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
            Clients user = Identification.MySelf;
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

        static public void ExtraButtonsRolependant(bool showButtons)
        {
            if (showButtons && ButtonFactory.btnExtraHudRolePendant1.Text.Length > 0)
            {
                //Reload text (in case of rolechange)
                ButtonFactory.LoadButtonHudNames();
                ButtonFactory.OpenButton(ButtonFactory.btnExtraHudRolePendant1);
            }
            else
                ButtonFactory.RemoveButton(ButtonFactory.btnExtraHudRolePendant1);
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
                ButtonFactory.btnSiren.Text = SirenActive ? "^" + srnFlash + sirenBtn : "^1SIREN OFF";
                ButtonFactory.OpenButton(ButtonFactory.btnSiren);
            }
        }
        static public string sirenBtn = "";
        static public bool AudioSirenIsOn = false;
        static public void AudioSiren(bool turnOff)
        {
            if (turnOff)
            {
                Messages.SendPublicMsg("/siren off");
                AudioSirenIsOn = false;
            }
            else
            {
                sirenBtn = UnicodeEncoding.Music + UnicodeEncoding.Music + UnicodeEncoding.Music + UnicodeEncoding.Music + UnicodeEncoding.Music;
                if (!AudioSirenIsOn)
                {
                    Messages.SendPublicMsg("/siren slow");
                    AudioSirenIsOn = true;
                }
            }
        }
        static public void VisibleSiren(bool turnOff)
        {
            if (turnOff)
            {
                Program.insim.Send(new IS_SMALL { SubT = SmallType.SMALL_LCS, UVal = LocalCarSwitches.LCS_SIGNALS_OFF | LocalCarSwitches.LCS_HEADLIGHTS_OFF });
            }
            else
            {
                Program.insim.Send(new IS_SMALL { SubT = SmallType.SMALL_LCS, UVal = LocalCarSwitches.LCS_SIGNALS_HAZARD | (srnFlash == 1 ? LocalCarSwitches.LCS_HEADLIGHTS_ON : LocalCarSwitches.LCS_HEADLIGHTS_OFF) });
                sirenBtn = sirenBtn += UnicodeEncoding.SirenLight + UnicodeEncoding.SirenLight + UnicodeEncoding.SirenLight + UnicodeEncoding.SirenLight + UnicodeEncoding.SirenLight;
            }
        }
        static public bool carAlarmOffAfterUsage = false;
        static public void CarAlarm()
        {
            if (Settings.CARALARM && Identification.MySelf.onTrack && Identification.MySelf.P.speed < 1 && !Identification.MySelf.C.isCop && Identification.MySelf.P.afkStart.ElapsedMilliseconds >= 60000)
            {
                foreach (Clients all in Program.AllConnections)
                    if (all.onTrack && Tracker.GetDirectDistance(Identification.MySelf, all) <= 5 && all.P.speed < 1 && all != Identification.MySelf && !all.C.isCop)
                    {
                        Program.insim.Send(new IS_SMALL { SubT = SmallType.SMALL_LCS, UVal = (DateTime.Now.Second % 2 == 0 ? LocalCarSwitches.LCS_HORN_2 : LocalCarSwitches.LCS_HORN_OFF) | LocalCarSwitches.LCS_SIGNALS_HAZARD | LocalCarSwitches.LCS_HEADLIGHTS_ON });
                        carAlarmOffAfterUsage = true;
                        return;
                    }
            }
            if (carAlarmOffAfterUsage)
            {
                Program.insim.Send(new IS_SMALL { SubT = SmallType.SMALL_LCS, UVal = LocalCarSwitches.LCS_HORN_OFF | LocalCarSwitches.LCS_SIGNALS_OFF | LocalCarSwitches.LCS_HEADLIGHTS_OFF });
                carAlarmOffAfterUsage = false;
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
                GetWindowThreadProcessId(hwnd, out uint processID);
                return Process.GetProcessById((int)processID);
            }
            catch { return null; }
        }
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")]
        private static extern Int32 GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);
        [DllImport("user32.dll", SetLastError = true)]
        static extern void Keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);
        public const int KeyDown = 0x0001; //Key down flag
        public const int KeyUp = 0x0002; //Key up flag
    }
}