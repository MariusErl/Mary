using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mary
{
    class CruiseControl
    {
        static public System.Timers.Timer CruiseController = new System.Timers.Timer(200);
        static public KeyboardHook gkh = new KeyboardHook();

        #region InitializePresses
        static public void LoadKeypresses()
        {
            InitialiseController();
            gkh.HookedKeys.Add(Keys.PageUp);
            gkh.HookedKeys.Add(Keys.PageDown);
            gkh.HookedKeys.Add(Keys.Insert);
            gkh.HookedKeys.Add(Keys.Delete);
            gkh.HookedKeys.Add(Keys.End);
            gkh.HookedKeys.Add(Keys.Home);
            gkh.HookedKeys.Add(Keys.Space);
            gkh.HookedKeys.Add(Keys.LControlKey);
            gkh.HookedKeys.Add(Keys.RControlKey);
            gkh.HookedKeys.Add(Keys.LShiftKey);
            gkh.HookedKeys.Add(Keys.RShiftKey);
            gkh.HookedKeys.Add(Keys.Alt);
            gkh.HookedKeys.Add(Keys.A);
            gkh.HookedKeys.Add(Keys.B);
            gkh.HookedKeys.Add(Keys.C);
            gkh.HookedKeys.Add(Keys.D);
            gkh.HookedKeys.Add(Keys.E);
            gkh.HookedKeys.Add(Keys.F);
            gkh.HookedKeys.Add(Keys.G);
            gkh.HookedKeys.Add(Keys.H);
            gkh.HookedKeys.Add(Keys.I);
            gkh.HookedKeys.Add(Keys.J);
            gkh.HookedKeys.Add(Keys.K);
            gkh.HookedKeys.Add(Keys.L);
            gkh.HookedKeys.Add(Keys.M);
            gkh.HookedKeys.Add(Keys.N);
            gkh.HookedKeys.Add(Keys.O);
            gkh.HookedKeys.Add(Keys.P);
            gkh.HookedKeys.Add(Keys.Q);
            gkh.HookedKeys.Add(Keys.R);
            gkh.HookedKeys.Add(Keys.S);
            gkh.HookedKeys.Add(Keys.T);
            gkh.HookedKeys.Add(Keys.U);
            gkh.HookedKeys.Add(Keys.V);
            gkh.HookedKeys.Add(Keys.W);
            gkh.HookedKeys.Add(Keys.X);
            gkh.HookedKeys.Add(Keys.Y);
            gkh.HookedKeys.Add(Keys.Z);

            gkh.HookedKeys.Add(Keys.T);
            gkh.HookedKeys.Add(Keys.Enter);
            gkh.HookedKeys.Add(Keys.Escape);
            gkh.HookedKeys.Add(Keys.RShiftKey);
            gkh.HookedKeys.Add(Keys.RMenu);

            if (Settings.NUMPAD)
            {
                if (Settings.NUM0.Length > 1)
                { gkh.HookedKeys.Add(Keys.NumPad0); }
                if (Settings.NUM1.Length > 1)
                { gkh.HookedKeys.Add(Keys.NumPad1); }
                if (Settings.NUM2.Length > 1)
                { gkh.HookedKeys.Add(Keys.NumPad2); }
                if (Settings.NUM3.Length > 1)
                { gkh.HookedKeys.Add(Keys.NumPad3); }
                if (Settings.NUM4.Length > 1)
                { gkh.HookedKeys.Add(Keys.NumPad4); }
                if (Settings.NUM5.Length > 1)
                { gkh.HookedKeys.Add(Keys.NumPad5); }
                if (Settings.NUM6.Length > 1)
                { gkh.HookedKeys.Add(Keys.NumPad6); }
                if (Settings.NUM7.Length > 1)
                { gkh.HookedKeys.Add(Keys.NumPad7); }
                if (Settings.NUM8.Length > 1)
                { gkh.HookedKeys.Add(Keys.NumPad8); }
                if (Settings.NUM9.Length > 1)
                { gkh.HookedKeys.Add(Keys.NumPad9); }
                if (Settings.NUMDelt.Length > 1)
                { gkh.HookedKeys.Add(Keys.Divide); }
                if (Settings.NUMGanger.Length > 1)
                { gkh.HookedKeys.Add(Keys.Multiply); }
                if (Settings.NUMPluss.Length > 1)
                { gkh.HookedKeys.Add(Keys.Add); }
                if (Settings.NUMPluss.Length > 1)
                { gkh.HookedKeys.Add(Keys.Add); }
                if (Settings.NUMComma.Length > 1)
                { gkh.HookedKeys.Add(Keys.Decimal); }
            }

            gkh.KeyDown += new KeyEventHandler(Gkh_KeyDown);
            gkh.KeyUp += new KeyEventHandler(Gkh_KeyUp);
        }
        #endregion

        static public bool accelerating = false;
        static public void UpdateCruiseBtn(Clients user)
        {
            string prefix1 = CruiseControlActive ? "^2" : "^1";
            string prefix2 = CruiseControlActive ? "" : "^1";
            if (!Settings.CRUISECONTROLSPEEDLIMIT) { ButtonFactory.btnCruiseControl.Text = prefix1 + "CC - Set: " + CruiseSpeed + (Settings.UNITKPH ? "kph" : "mph") + prefix2 + " Curr: " + user.P.speed + (Settings.UNITKPH ? "kph" : "mph"); }
            else { ButtonFactory.btnCruiseControl.Text = Settings.UNITKPH ? prefix1 + "CC - Speedlimit: " + user.P.SpeedlimitKph + "kph" + prefix2 + " Curr: " + user.P.speed + "kph" : prefix1 + "Cruise control - Speedlimit: " + user.P.SpeedlimitMph + "mph" + prefix2 + " Curr: " + user.P.speed + "mph"; }
            ButtonFactory.OpenButton(ButtonFactory.btnCruiseControl);
        }

        static public void InitialiseController()
        {
            _hookID = SetHook(_proc);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);
        private const int MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const int MOUSEEVENTF_LEFTUP = 0x0004;

        static public int CruiseSpeed = 50;
        static public bool CruiseControlActive = false;
        static public bool StopAccAfterActive = false;
        static public List<int> SpeedLimitsKPH = new List<int>(new int[] { 70, 80, 90, 100, 120, 135, 160, 200, 250 });
        static public List<int> SpeedLimitsMPH = new List<int>(new int[] { 45, 50, 60, 75, 85, 100, 120, 140 });
        static public bool DelayForIdleAvoider = false;

        static public Keys KeyCruiseControlSet;
        static public Keys KeyCruiseControlUp;
        static public Keys KeyCruiseControlDown;
        static public Keys KeyPushButton;

        #region FindKey
        static public Keys DecideKeys(string ønske)
        {
            Keys aa = Keys.Execute;
            switch (ønske.ToLower())
            {
                case "a": aa = Keys.A; break;
                case "b": aa = Keys.B; break;
                case "c": aa = Keys.C; break;
                case "d": aa = Keys.D; break;
                case "e": aa = Keys.E; break;
                case "f": aa = Keys.F; break;
                case "g": aa = Keys.G; break;
                case "h": aa = Keys.H; break;
                case "i": aa = Keys.I; break;
                case "j": aa = Keys.J; break;
                case "k": aa = Keys.K; break;
                case "l": aa = Keys.L; break;
                case "m": aa = Keys.M; break;
                case "n": aa = Keys.N; break;
                case "o": aa = Keys.O; break;
                case "p": aa = Keys.P; break;
                case "q": aa = Keys.Q; break;
                case "r": aa = Keys.R; break;
                case "s": aa = Keys.S; break;
                case "t": aa = Keys.T; break;
                case "u": aa = Keys.U; break;
                case "v": aa = Keys.V; break;
                case "w": aa = Keys.W; break;
                case "x": aa = Keys.X; break;
                case "y": aa = Keys.Y; break;
                case "z": aa = Keys.Z; break;
                case "home": aa = Keys.Home; break;
                case "insert": aa = Keys.Insert; break;
                case "delete": aa = Keys.Delete; break;
                case "end": aa = Keys.End; break;
                case "pgup": aa = Keys.PageUp; break;
                case "pgdn": aa = Keys.PageDown; break;
                case "scroll": aa = Keys.None; break;
            }
            return aa;
        }
        #endregion

        static public void PushInSimButton()
        {
            if (ButtonFactory.ViewedButton != -1)
            {
                Task tas = Task.Factory.StartNew(() => ButtonFactory.ButtonEvents(ButtonFactory.ClickableUsed[ButtonFactory.ViewedButton].Name));
            }
        }

        static public void CruiseSet()
        {
            if (!CruiseControlActive)
            {
                CruiseControlActive = true;
                CruiseSpeed = Identification.MySelf.P.speed;
                Messages.SendLocalMsg("Cruise Control active. Speed: " + Identification.MySelf.P.speed + (Settings.UNITKPH ? "kph" : "mph") + (Settings.CRUISECONTROLSPEEDLIMIT ? " (speedlimit atm)" : ""));
            }
            else
            {
                CruiseControlActive = false;
                StopAccAfterActive = true;
            }
        }

        static public void CruiseUp()
        {
            if (!CruiseControlActive)
            {
                Messages.SendLocalMsg("Cruise Control active. Manual speed: " + CruiseSpeed + (Settings.UNITKPH ? "kph" : "mph") + (Settings.CRUISECONTROLSPEEDLIMIT ? " (speedlimit atm)" : ""));
                CruiseControlActive = true;
            }
            else
            {
                int theNewSpeed = GetNextValidSize(CruiseSpeed, Settings.UNITKPH ? SpeedLimitsKPH.ToArray() : SpeedLimitsMPH.ToArray(), true);
                CruiseSpeed = theNewSpeed;
                Messages.SendLocalMsg("New manual cruise speed: " + theNewSpeed + (Settings.UNITKPH ? "kph" : "mph") + (Settings.CRUISECONTROLSPEEDLIMIT ? " (speedlimit atm)" : ""));
            }
        }

        static public void CruiseDown()
        {
            if (!CruiseControlActive)
            {
                Messages.SendLocalMsg("Cruise Control active. Manual speed: " + CruiseSpeed + (Settings.UNITKPH ? "kph" : "mph") + (Settings.CRUISECONTROLSPEEDLIMIT ? " (speedlimit atm)" : ""));
                CruiseControlActive = true;
            }
            else
            {
                int theNewSpeed = GetNextValidSize(CruiseSpeed, Settings.UNITKPH ? SpeedLimitsKPH.ToArray() : SpeedLimitsMPH.ToArray(), false);
                CruiseSpeed = theNewSpeed;
                Messages.SendLocalMsg("New manual cruise speed: " + theNewSpeed + (Settings.UNITKPH ? "kph" : "mph") + (Settings.CRUISECONTROLSPEEDLIMIT ? " (speedlimit atm)" : ""));
            }
        }

        static public bool ProcessIsLFS()
        {
            bool yep = false;
            Process currentProcess = Roleplay.GetActiveProcess();
            if (currentProcess.MainWindowTitle == "Live for Speed")
            { yep = true; }

            return yep;
        }

        static public void SimulateKeypress()
        {
            SendKeys.SendWait("{" + KeyPushButton.ToString() + "}");
        }

        static public void Gkh_KeyDown(object sender, KeyEventArgs e)
        {
            if (ProcessIsLFS())
            {
                if (e.KeyCode == KeyCruiseControlSet)
                {
                    CruiseSet();
                }
                if (e.KeyCode == KeyCruiseControlUp)
                {
                    CruiseUp();
                }
                if (e.KeyCode == KeyCruiseControlDown)
                {
                    CruiseDown();
                }
                if (e.KeyCode == KeyPushButton)
                {
                    PushInSimButton();
                }

                switch (e.KeyCode)
                {

                    case Keys.NumPad0: { Task tas = Task.Factory.StartNew(() => Messages.SendMsgRunScript(Settings.NUM0)); } e.Handled = true; break;
                    case Keys.NumPad1: { Task tas = Task.Factory.StartNew(() => Messages.SendMsgRunScript(Settings.NUM1)); } e.Handled = true; break;
                    case Keys.NumPad2: { Task tas = Task.Factory.StartNew(() => Messages.SendMsgRunScript(Settings.NUM2)); } e.Handled = true; break;
                    case Keys.NumPad3: { Task tas = Task.Factory.StartNew(() => Messages.SendMsgRunScript(Settings.NUM3)); } e.Handled = true; break;
                    case Keys.NumPad4: { Task tas = Task.Factory.StartNew(() => Messages.SendMsgRunScript(Settings.NUM4)); } e.Handled = true; break;
                    case Keys.NumPad5: { Task tas = Task.Factory.StartNew(() => Messages.SendMsgRunScript(Settings.NUM5)); } e.Handled = true; break;
                    case Keys.NumPad6: { Task tas = Task.Factory.StartNew(() => Messages.SendMsgRunScript(Settings.NUM6)); } e.Handled = true; break;
                    case Keys.NumPad7: { Task tas = Task.Factory.StartNew(() => Messages.SendMsgRunScript(Settings.NUM7)); } e.Handled = true; break;
                    case Keys.NumPad8: { Task tas = Task.Factory.StartNew(() => Messages.SendMsgRunScript(Settings.NUM8)); } e.Handled = true; break;
                    case Keys.NumPad9: { Task tas = Task.Factory.StartNew(() => Messages.SendMsgRunScript(Settings.NUM9)); } e.Handled = true; break;
                    case Keys.Divide: { Task tas = Task.Factory.StartNew(() => Messages.SendMsgRunScript(Settings.NUMDelt)); } e.Handled = true; break;
                    case Keys.Multiply: { Task tas = Task.Factory.StartNew(() => Messages.SendMsgRunScript(Settings.NUMGanger)); } e.Handled = true; break;
                    case Keys.Add: { Task tas = Task.Factory.StartNew(() => Messages.SendMsgRunScript(Settings.NUMPluss)); } e.Handled = true; break;
                    case Keys.Decimal: { Task tas = Task.Factory.StartNew(() => Messages.SendMsgRunScript(Settings.NUMComma)); } e.Handled = true; break;
                }
            }
        }

        static public void Gkh_KeyUp(object sender, KeyEventArgs e)
        {
        }

        public static void CruiseControllChecker(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (CruiseControlActive)
            {
                if (!Settings.CRUISECONTROLSPEEDLIMIT ? Identification.MySelf.P.speed < CruiseSpeed * 0.95 : Settings.UNITKPH ? Identification.MySelf.P.speed < Identification.MySelf.P.SpeedlimitKph * 0.95 : Identification.MySelf.P.speed < Identification.MySelf.P.SpeedlimitMph * 0.95)
                {
                    mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                    accelerating = true;
                    Outgauge.heavyAcc = true;
                }
                else
                {
                    mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                    accelerating = false;
                    Outgauge.heavyAcc = false;
                }
            }
            else if (StopAccAfterActive)
            {
                Messages.SendLocalMsg("Cruise Control deactivated.");
                mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                StopAccAfterActive = false;
                Outgauge.heavyAcc = false;
            }
        }

        static public int GetNextValidSize(int currSpeed, int[] validSizes, bool up)
        {
            int returnValue = currSpeed;

            switch (up)
            {
                case true:
                    for (int i = 0; i < validSizes.Length; i++)
                    {
                        if (validSizes[i] > currSpeed)
                        {
                            returnValue = validSizes[i]; break;
                        }
                        else if (validSizes.Length == i)
                        {
                            returnValue = currSpeed; break;
                        }
                    }
                    break;
                case false:
                    for (int i = 0; i < validSizes.Length; i++)
                    {
                        if (validSizes[i] >= currSpeed)
                        {
                            if (i >= 2)
                            {
                                returnValue = validSizes[i - 1]; break;
                            }
                            else
                            {
                                returnValue = validSizes[0]; break;
                            }
                        }
                        else
                        {
                            returnValue = currSpeed;
                        }
                    }
                    break;
            }
            return returnValue;
        }

        static private IntPtr SetHook(LowLevelMouseProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_MOUSE_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);
        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            MSLLHOOKSTRUCT hookStruct = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));
            if (nCode >= 0 && MouseMessages.WM_RBUTTONDOWN == (MouseMessages)wParam)
            {
                if (CruiseControlActive)
                {
                    CruiseControlActive = false;
                    StopAccAfterActive = true;
                }
            }

            if (ProcessIsLFS())
            {
                {
                    if (MouseMessages.WM_MOUSEWHEEL == (MouseMessages)wParam && Settings.SCRLWHEEL)
                    {
                        if (hookStruct.mouseData == 7864320)
                        {
                            ButtonFactory.MouseScroll(false);
                        }
                        else
                        {
                            ButtonFactory.MouseScroll(true);
                        }
                    }
                }
                if (Settings.KEYPUSHBUTTON == "scroll" && wParam.ToInt32() == 519)
                {
                    PushInSimButton();
                }
            }
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        private const int WH_MOUSE_LL = 14;
        private enum MouseMessages
        {
            WM_LBUTTONDOWN = 0x0201,
            WM_LBUTTONUP = 0x0202,
            WM_MOUSEMOVE = 0x0200,
            WM_MOUSEWHEEL = 0x020A,
            WM_RBUTTONDOWN = 0x0204,
            WM_RBUTTONUP = 0x0205,
            WM_MOUSEWHEELPRESSED = 0x020B
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct POINT
        {
            public int x;
            public int y;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct MSLLHOOKSTRUCT
        {
            public POINT pt;
            public uint mouseData;
            public uint flags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelMouseProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        static readonly private LowLevelMouseProc _proc = HookCallback;
        static public IntPtr _hookID = IntPtr.Zero;
    }
}