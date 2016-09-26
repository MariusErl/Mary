using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.Windows.Forms;
using System.Threading.Tasks;
using InSimDotNet;
using InSimDotNet.Packets;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace Mary
{
    static class Program
    {
        public static List<Clients> AllConnections = new List<Clients>();
        public static InSim insim = new InSim();
        public static DateTime AppStarted;

        static public InSim newInSimObject()
        {
            if (insim.IsConnected)
            insim.Dispose();
            return insim = new InSim();
        }
        [STAThread]
        static void Main()
        {
            Console.Title = "Mary - made by MariusMM";
            Cons("Application starting up...");
            try
            {
                Cons("Using version " + Identification.VERSIONNUMBER);
                //Cons("Checking for new versions...");
                //Identification.CheckVersion();
                Cons("Loading settings...");
                Settings.LoadSettings();
                if (Settings.PERFORMANCE) { Performance.a(); }
                //Cons("Checking key...");
                //Identification.CheckKey();
                insim = newInSimObject();
                Cons("Mary Insim trying to connect...");
                insim.Initialize(new InSimSettings
                {
                    Host = "127.0.0.1",
                    Port = Settings.PORT,
                    Admin = Settings.PASSWORD,
                    Interval = 250,
                    IName = "^3Mary",
                    Flags = InSimFlags.ISF_MCI | InSimFlags.ISF_CON | InSimFlags.ISF_LOCAL
                });
            }
            catch (Exception e) { ConsError(e.ToString()); }
            if (insim.IsConnected)
            {
                Cons("Mary Insim connected!");
                Messages.SendLocalMsg("Use '/o help' for a list of commands.");
                if (!Identification.latestVersion)
                {
                    Messages.SendLocalMsg("^1NOTE: You do not have the latest version of Mary!");
                }
            }
            if (Settings.CHATLOGGING) { Messages.CreateChatlogFile(); }
            if (Settings.CHECKADMIN) { AdminCheck.Check(false); }

            AppStarted = DateTime.Now;
            insim.Bind<IS_NCN>(newConnection);
            insim.Bind<IS_CNL>(connLeave);
            insim.Bind<IS_MSO>(Messages.Messa);
            insim.Bind<IS_NPL>(UpdatePlayer);
            insim.Bind<IS_CPR>(PlayerNameChanged);
            insim.Bind<IS_MCI>(MCIupdates);
            insim.Bind<IS_STA>(StateChange);
            insim.Bind<IS_CON>(CarContactMade);
            insim.Bind<IS_BTC>(ButtonFactory.BTNClicked);
            insim.Bind<IS_BFN>(ShiftB);
            insim.Bind<IS_PLP>(connPit);
            insim.Bind<IS_PLL>(connSpec);
            insim.Bind<IS_ISM>(HostJoined);
            insim.Bind<IS_VER>(VersionInfo);
            insim.InSimError += insim_InSimError;
            
            insim.Send(new IS_TINY { SubT = TinyType.TINY_SST, ReqI = 255 });
            insim.Send(new IS_TINY { SubT = TinyType.TINY_ISM, ReqI = 255 });
            insim.Send(new IS_TINY { SubT = TinyType.TINY_VER, ReqI = 255 });

            System.Threading.Thread.Sleep(1000);

            Statistics.LoadList();

            if (Settings.CRUISECONTROL)
            {
                CruiseControl.CruiseController.Enabled = true;
                CruiseControl.CruiseController.Elapsed += new System.Timers.ElapsedEventHandler(CruiseControl.CruiseControllChecker);
            }

            DUM.RegularRutineTime = new System.Timers.Timer(1000);
            DUM.RegularRutineTime.Enabled = true;
            DUM.RegularRutineTime.Elapsed += new System.Timers.ElapsedEventHandler(DUM.RegularRutine);

            DUM.FastRutineTime = new System.Timers.Timer(500);
            DUM.FastRutineTime.Enabled = true;
            DUM.FastRutineTime.Elapsed += new System.Timers.ElapsedEventHandler(DUM.FastRutine);

            DUM.SlowRutineTime = new System.Timers.Timer(5000);
            DUM.SlowRutineTime.Enabled = true;
            DUM.SlowRutineTime.Elapsed += new System.Timers.ElapsedEventHandler(DUM.SlowRutine);
            
            if (Settings.OUTGAUGE)
            {
                Outgauge.ConnectOutgaugeToLFS();
            }
            CruiseControl.LoadKeypresses();
            Application.Run();
        }

        static public void Cons(string msg)
        {
            Console.WriteLine(DateTime.Now + " : " + msg);
        }
        static public void ConsError(string msg)
        {
            Cons("---------------------------------------------");
            Cons("----------InSim Error Message start----------");
            if (msg.StartsWith("System.Resources.MissingManifestResourceException"))
            {
                Cons("You have most likely not setup Mary/LFS correct.");
                Cons("-Make sure you have LFS started.");
                Cons("-Make sure you have typed in the correct port in LFS (default is /insim=29999)");
                Cons("-Make sure your 'password=' setting is matching your ingame admin password (LFS->Multiplayer->Start new host->Admin password)");
                System.Threading.Thread.Sleep(30000);
                Environment.Exit(0);
            }
            else if (msg.StartsWith("System.TypeLoadException: Could not load type"))
            {
                Cons("You have most likely not Microsoft .net framework 4.5 installed on your computer.");
                System.Threading.Thread.Sleep(30000);
                Environment.Exit(0);
            }
            else
                Cons(msg);
            Cons("----------InSim Error Message end------------");
            Cons("---------------------------------------------");
        }

        static void insim_InSimError(object sender, InSimErrorEventArgs e)
        {
            Cons("---------------------------------------------");
            Cons("----------InSim-Lib Error Message start----------");
            ConsError(e.Exception.ToString());
            Cons("----------InSim-Lib Error Message end------------");
            Cons("---------------------------------------------");
        }

        static public string MyUsername = "";
        static public Clients MySelf = new Clients();
        static public bool FULLVERSION = true;
        static public void MCIupdates(InSim insim, IS_MCI mci)
        {
            for (int i = 0; i < mci.NumC; i++)
            {
                var user = Program.GetClient(mci.Info[i].PLID, false);
                user.P.x = mci.Info[i].X;
                user.P.y = mci.Info[i].Y;
                user.P.direction = mci.Info[i].Direction;
                user.P.heading = mci.Info[i].Heading;
                user.P.speed = mci.Info[i].Speed;
                if (user == MySelf)
                {
                    if (Trip.tripWatch.IsRunning)
                    {
                        user.P.tripDst += ((mci.Info[i].Speed / 32768f) * 100f) / 4;
                        if (Settings.TRIPCOUNTER) Tracker.TopSpeedChecker();
                    }
                }
            }
        }

        static public int CarSpeed(int SpeedRaw, bool kph)
        {
            return (kph) ? (int)(SpeedRaw / 91.02) : (int)(SpeedRaw / 146.486067);
        }

        static public List<string> Crashers = new List<string>();
        private static void CarContactMade(InSim insim, IS_CON con)
        {
           Clients A = GetClient(con.A.PLID, false);
           Clients B = GetClient(con.B.PLID, false);
           if ((Settings.CARCONTACT || Messages.afkMode) && (A == MySelf || B == MySelf))
           {
                Clients Crasher = B;
                string speed = "";
                if (A == MySelf) { Crasher = B; } else if (B == MySelf) { Crasher = A; } if (Crasher != Tracker.Trackee)
                {
                    speed = (Settings.UNITKPH ? CarSpeed(Crasher.P.speed, true) + "kph)" : CarSpeed(Crasher.P.speed, false) + "mph)");
                    Messages.SendLocalMsg("^3Contact with ^8" + Crasher.PlayerName + " ^2(" + speed);
                }
                Crashers.Add(Crasher.PlayerName + " ^8(" + Crasher.Username + ") " + " - (" + speed);
            }
        }

        static public void SpectateTrackee()
        {
            if (ViewedUser != Tracker.Trackee)
            {
                SendKeys.SendWait("{TAB}");
                System.Threading.Thread.Sleep(50);
                Task taskkk = Task.Factory.StartNew(() => SpectateTrackee());               
            }
            else
            {
                Messages.SendLocalMsg("Speccing: " + Tracker.Trackee.PlayerName);
            }
        }

        static public Clients ViewedUser = new Clients();
        static public string CurrentTrack = "";
        static void StateChange(InSim insim, IS_STA sta)
        {
            CurrentTrack = sta.Track;
            ViewedUser = GetClient(sta.ViewPLID, false);

            if (!sta.Flags.HasFlag(StateFlags.ISS_MULTI) && IsOnline)
            {
                Messages.LogMsg("Left server: " + ServerName);
                Cons("Left server: " + ServerName);
                IsOnline = false;
                sendWlcMsg = false;
                ButtonFactory.RemoveButton(ButtonFactory.btnAFKwarning);
                FlashWindow(Process.GetCurrentProcess().MainWindowHandle, false);
            }
            if (sta.Flags.HasFlag(StateFlags.ISS_MULTI))
            {
                IsOnline = true;
            }
        }

        static public string ServerName = "";
        static public bool IsOnline = false;
        static public bool sendWlcMsg = false;
        static public List<string> BuddysNotice = new List<string>();
        static public void HostJoined(InSim insim, IS_ISM ism)
        {
            if (ism.HName.Length > 1)
            {
                ButtonFactory.ButtonsUsed.Clear();
                buttonsState = 4;
                AllConnections.Clear();
                insim.Send(new IS_TINY { SubT = TinyType.TINY_NCN, ReqI = 255 });
                insim.Send(new IS_TINY { SubT = TinyType.TINY_NPL, ReqI = 255 });

                BuddysNotice = new List<string> (Settings.BUDDYS);
                Messages.LogMsg("Joined server: " + ism.HName);
                Cons("Joined server: " + ism.HName);
                ServerName = ism.HName;
            }
        }

        static public string LFSversion = "";
        static public void VersionInfo(InSim insim, IS_VER ver)
        {
            LFSversion = ver.Version;
        }

        #region Shift + B
        static public int buttonsState = 4;
        static public void ShiftB(InSim insim, IS_BFN bfn)
        {
            if (ButtonFactory.ViewedButton != -1) ButtonFactory.ClickableUsed[ButtonFactory.ViewedButton].Style = ButtonStyles.ISB_DARK | ButtonStyles.ISB_CLICK;
            ButtonFactory.ButtonsUsed.Clear();
            ButtonFactory.ClickableUsed.Clear();
            ButtonFactory.ViewedButton = -1;
            buttonsState = (buttonsState == 4) ? 1 : buttonsState+=1;
            if (buttonsState == 1)
            { 
                Messages.SendLocalMsg("InSim hud hidden.");
            }
            else if (buttonsState == 2)
            {
                ButtonFactory.GadgetsButtons();
                ButtonFactory.tripm1.Style = ButtonFactory.tripm2.Style = ButtonFactory.tripm3.Style =
                ButtonFactory.tripm4.Style = ButtonFactory.tripmReset.Style = ButtonFactory.tripmPause.Style = ButtonStyles.ISB_DARK | ButtonStyles.ISB_C1 | ButtonStyles.ISB_CLICK;
                ButtonFactory.OpenButton(ButtonFactory.tripmMain);
                Messages.SendLocalMsg("InSim hud partly visible.");
            }
            else
            {
                ButtonFactory.GadgetsButtons();
                if (buttonsState == 3)
                {
                    ButtonFactory.tripm1.Style = ButtonFactory.tripm2.Style = ButtonFactory.tripm3.Style = 
                    ButtonFactory.tripm4.Style = ButtonFactory.tripmReset.Style = ButtonFactory.tripmPause.Style = ButtonStyles.ISB_DARK | ButtonStyles.ISB_C1;
                    ButtonFactory.btnTracker1.Style = ButtonStyles.ISB_DARK;
                    Messages.SendLocalMsg("InSim hud un-clickable.");
                }
                else if (buttonsState == 4)
                {
                    if (MySelf.C.isCop && Settings.COPHUDON) { Roleplay.PoliceButtons(true); if (Settings.EXTRAHUDON) Roleplay.ExtraButtons(true); }
                    if (MySelf.C.isResQ && Settings.RESQHUDON) { Roleplay.ResQButtons(true); if (Settings.EXTRAHUDON) Roleplay.ExtraButtons(true); }
                    ButtonFactory.tripm1.Style = ButtonFactory.tripm2.Style = ButtonFactory.tripm3.Style =
                    ButtonFactory.tripm4.Style = ButtonFactory.tripmReset.Style = ButtonFactory.tripmPause.Style = ButtonStyles.ISB_DARK | ButtonStyles.ISB_CLICK | ButtonStyles.ISB_C1;
                    ButtonFactory.btnTracker1.Style = ButtonStyles.ISB_DARK | ButtonStyles.ISB_CLICK;
                    Messages.SendLocalMsg("InSim hud visible.");
                }
                ButtonFactory.OpenButton(ButtonFactory.tripmMain);
            }
            ButtonFactory.RemoveButton(ButtonFactory.btnSpeedOut);
        }
        #endregion

        static public bool IsCop(string name)
        {
            bool cop = false;
            if (name.Contains("[cop]") || name.Contains("[vcu]") || name.Contains("[hwa]") || name.Contains("[ca]") || name.Contains("[as]") || name.Contains("[t]"))
                cop = true;

            return cop;
        }
        static public bool isMed(string name)
        {
            bool med = false;
            if (name.Contains("[res]") || name.Contains("[med]") || name.Contains("[tow]"))
                med = true;
            return med;
        }

        static string StripColours(string str)
        {
            return new Regex(@"\^[0-9]").Replace(str, string.Empty);
        }

        #region New connection
        static void newConnection(InSim insim, IS_NCN ncn)
        {
            if (ncn.UCID != 0)
            {
                Clients NewConn = new Clients();
                NewConn.UniqueID = ncn.UCID;
                NewConn.Username = ncn.UName;
                NewConn.PlayerName = ncn.PName;
                NewConn.PlayerNameRAW = StripColours(ncn.PName);
                AllConnections.Add(NewConn);

                if (IsCop(NewConn.PlayerName.ToLower()))
                { NewConn.C.isCop = true; }
                else { NewConn.C.isCop = false; }

                if (isMed(NewConn.PlayerName.ToLower()))
                { NewConn.C.isResQ = true; }
                else { NewConn.C.isResQ = false; }

                if (NewConn.Username.ToLower() == MyUsername.ToLower())
                {
                    MySelf = NewConn;
                    ButtonFactory.GadgetsButtons();
                    if (MySelf.C.isCop && Settings.COPHUDON) { Roleplay.PoliceButtons(true); if (Settings.EXTRAHUDON) Roleplay.ExtraButtons(true); }
                    if (MySelf.C.isResQ && Settings.RESQHUDON) { Roleplay.ResQButtons(true); if (Settings.EXTRAHUDON) Roleplay.ExtraButtons(true); }
                    if (Settings.TRIPCOUNTER) ButtonFactory.OpenButton(ButtonFactory.tripmMain);
                    Trip.tripWatch.Start();
                }

                if (Settings.CHECKADMIN && FULLVERSION)
                {
                    if (ncn.Admin) { Messages.SendLocalMsg(NewConn.PlayerName + " ^8is server admin."); }
                    if (AdminCheck.AdminList.ToArray().Contains(NewConn.Username.ToLower())) Messages.SendLocalMsg(NewConn.PlayerName + " ^8is insim admin.");
                }

                if (Settings.WELCOMEBUDDY && sendWlcMsg)
                {
                    for (int i = 0; i < Settings.BUDDYS.Count; i++)
                    {
                        if (Settings.BUDDYS[i].ToLower() == NewConn.Username.ToLower())
                        {
                            Messages.SendPublicMsg(Settings.WELCOMEMSG.Replace("{BUDDY}", NewConn.PlayerName));
                            break;
                        }
                    }
                }
            }
        }
        #endregion

        static public void UpdatePlayer(InSim insim, IS_NPL npl)
        {
            var User = Program.GetClient(npl.UCID, true);
            User.PlayerID = npl.PLID;
            User.P.CarName = npl.CName;
            User.P.Flags = npl.Flags;
            User.PlayerName = npl.PName;
            User.P.SkinName = npl.SName;
            User.P.AddedMassKG = npl.H_Mass;
            User.P.IntakeRes = npl.H_TRes;
            User.P.TiresFront = npl.Tyres.FrontRight.ToString();
            User.P.TiresRear = npl.Tyres.RearRight.ToString();
            User.onTrack = true;

            if (!User.Equals(MySelf) && Settings.SKINCHECK && !Settings.BUDDYS.ToArray().Contains(User.Username.ToLower()))
            {
                SkinCheck.CheckSkin(User);
            }
        }

        static public void PlayerNameChanged(InSim insim, IS_CPR cpr)
        {
            var User = GetClient(cpr.UCID, true);
            User.PlayerName = cpr.PName;
            User.PlayerNameRAW = StripColours(cpr.PName);

            if (IsCop(User.PlayerName.ToLower()))
                User.C.isCop = true;
            else
                User.C.isCop = false;

            if (isMed(User.PlayerName.ToLower()))
                User.C.isResQ = true;
            else
                User.C.isResQ = false;

            if (User == MySelf)
            {
                if (Settings.COPHUDON)
                {
                    if (MySelf.C.isCop) { Roleplay.PoliceButtons(true); if (Settings.EXTRAHUDON) Roleplay.ExtraButtons(true); }
                    else { Roleplay.PoliceButtons(false); if (Settings.EXTRAHUDON) Roleplay.ExtraButtons(false); }
                }
                if (Settings.RESQHUDON)
                {
                    if (MySelf.C.isResQ) { Roleplay.ResQButtons(true); if (Settings.EXTRAHUDON) Roleplay.ExtraButtons(true); }
                    else { Roleplay.ResQButtons(false); if (Settings.EXTRAHUDON) if (!MySelf.C.isCop) Roleplay.ExtraButtons(false); }
                }
            }
        }

        static public void connLeave(InSim insim, IS_CNL cnl)
        {
            var user = GetClient(cnl.UCID, true);
            try
            {
                if (Tracker.Trackee.Equals(user))
                {
                    Tracker.EditUserToTrack(user.Username.ToLower());
                }
            }
            catch { }
            AllConnections.Remove(user);
        }

        static public void connSpec(InSim insim, IS_PLL spec)
        {
            var user = Program.GetClient(spec.PLID, false);
            user.onTrack = false;
            user.P.speed = 0;
        }

        static public void connPit(InSim insim, IS_PLP pit)
        {
            var user = Program.GetClient(pit.PLID, false);
            user.onTrack = false;
            user.P.speed = 0;
        }

        static public Clients GetClient(byte ID, bool thatWasUniqueID)
        {

            for (int i = 0; i < AllConnections.Count; i++)
            {
                if (thatWasUniqueID)
                {
                    if (AllConnections[i].UniqueID == ID) { return AllConnections[i]; }
                }
                else
                {
                    if (AllConnections[i].PlayerID == ID) { return AllConnections[i]; }
                }
            }
            return new Clients();
        }

        #region Flashing afk warning
        // To support flashing.
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool FlashWindowEx(ref FLASHWINFO pwfi);

        //Flash both the window caption and taskbar button.
        //This is equivalent to setting the FLASHW_CAPTION | FLASHW_TRAY flags. 
        public const UInt32 FLASHW_ALL = 3;
        public const UInt32 FLASH_NON = 0;

        // Flash continuously until the window comes to the foreground. 
        public const UInt32 FLASHW_TIMERNOFG = 12;

        [StructLayout(LayoutKind.Sequential)]
        public struct FLASHWINFO
        {
            public UInt32 cbSize;
            public IntPtr hwnd;
            public UInt32 dwFlags;
            public UInt32 uCount;
            public UInt32 dwTimeout;
        }

        static public void FlashWindow(IntPtr hWnd, bool flash)
        {
            FLASHWINFO fInfo = new FLASHWINFO();

            fInfo.cbSize = Convert.ToUInt32(Marshal.SizeOf(fInfo));
            fInfo.hwnd = hWnd;
            fInfo.dwFlags = flash ? FLASHW_ALL : FLASH_NON;
            fInfo.uCount = UInt32.MaxValue;
            fInfo.dwTimeout = 0;

            FlashWindowEx(ref fInfo);
        }
        #endregion
    }
}