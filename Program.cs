using InSimDotNet;
using InSimDotNet.Packets;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mary
{
    static class Program
    {
        public static List<Clients> AllConnections = new List<Clients>();
        public static InSim insim = new InSim();
        static public Form MinimapFormWindow;

        [STAThread]
        static void Main()
        {
            Console.Title = "Mary - made by MariusMM";
            Cons("Application starting up...");
            try
            {
                Cons("Using version " + Identification.VERSIONNUMBER);
                Cons("Checking for new versions...");
                Identification.CheckVersion();
                Cons("Loading settings...");
                Settings.LoadSettings();
                if (Settings.PERFORMANCE) { Performance.InitiatePerformanceGadgets(); }
                insim = new InSim();
                Cons("Mary InSim trying to connect...");
                insim.Initialize(new InSimSettings
                {
                    Host = "127.0.0.1",
                    Port = Settings.PORT,
                    Admin = "",
                    // UdpPort = 30001,
                    Interval = 40,
                    IName = "^3Mary^8",
                    Flags = InSimFlags.ISF_MCI | InSimFlags.ISF_CON | InSimFlags.ISF_LOCAL
                });
            }
            catch (Exception e) { ConsError(e.ToString()); }
            if (insim.IsConnected)
            {
                Cons("Mary InSim connected!");
                Messages.SendLocalMsg("Use '/o help' for a list of commands.");
                if (!Identification.latestVersion)
                {
                    Messages.SendLocalMsg("^1NOTE: You do not have the latest version of Mary!");
                }
            }
            if (Settings.CHATLOGGING) { Messages.CreateChatlogFile(); }
            if (Settings.CHECKADMIN) { AdminCheck.Check(true); }

            insim.Bind<IS_NCN>(NewConnection);
            insim.Bind<IS_CNL>(ConnLeave);
            insim.Bind<IS_MSO>(Messages.ServerChat);
            insim.Bind<IS_NPL>(UpdatePlayer);
            insim.Bind<IS_CPR>(PlayerNameChanged);
            insim.Bind<IS_MCI>(MCIupdates);
            insim.Bind<IS_STA>(StateChange);
            insim.Bind<IS_CON>(CarContactMade);
            insim.Bind<IS_BTC>(ButtonFactory.BTNClicked);
            insim.Bind<IS_BFN>(ShiftB);
            insim.Bind<IS_PLP>(ConnPit);
            insim.Bind<IS_PLL>(ConnSpec);
            insim.Bind<IS_ISM>(HostJoined);
            insim.Bind<IS_VER>(VersionInfo);
            insim.Bind<IS_CIM>(ConnectionInterface);
            insim.InSimError += Insim_InSimError;

            insim.Send(new IS_TINY { SubT = TinyType.TINY_ISM, ReqI = 255 });
            insim.Send(new IS_TINY { SubT = TinyType.TINY_VER, ReqI = 255 });
            System.Threading.Thread.Sleep(500);
            insim.Send(new IS_TINY { SubT = TinyType.TINY_SST, ReqI = 255 });

            System.Threading.Thread.Sleep(1000);

            if (Settings.CRUISECONTROL)
            {
                CruiseControl.CruiseController.Enabled = true;
                CruiseControl.CruiseController.Elapsed += new System.Timers.ElapsedEventHandler(CruiseControl.CruiseControllChecker);
            }

            DUM.RegularRutineTime = new System.Timers.Timer(1000) { Enabled = true };
            DUM.RegularRutineTime.Elapsed += new System.Timers.ElapsedEventHandler(DUM.RegularRutine);

            DUM.FastRutineTime = new System.Timers.Timer(500) { Enabled = true };
            DUM.FastRutineTime.Elapsed += new System.Timers.ElapsedEventHandler(DUM.FastRutine);

            DUM.SuperFastRutineTime = new System.Timers.Timer(100) { Enabled = true };
            DUM.SuperFastRutineTime.Elapsed += new System.Timers.ElapsedEventHandler(DUM.SuperFastRutine);

            DUM.SlowRutineTime = new System.Timers.Timer(5000) { Enabled = true };
            DUM.SlowRutineTime.Elapsed += new System.Timers.ElapsedEventHandler(DUM.SlowRutine);

            if (Settings.OUTGAUGE)
            {
                Outgauge.ConnectOutgaugeToLFS();
            }

            CruiseControl.LoadKeypresses();

            if (Settings.MINIMAP > 1)
            {
                MinimapFormWindow = new MaryMinimap();
                MaryMinimap.LoadBackgroundMap();
                MinimapFormWindow.Show();
            }
            Application.Run();
        }

        static public void Cons(string msg)
        {
            Console.WriteLine(DateTime.Now + " : " + msg);

        }
        static public void ConsError(string msg)
        {
            if (Settings.DEBUG)
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
        }

        static void Insim_InSimError(object sender, InSimErrorEventArgs e)
        {
            if (Settings.DEBUG)
            {
                Cons("---------------------------------------------");
                Cons("----------InSim-Lib Error Message start----------");
                ConsError(e.Exception.ToString());
                Cons("----------InSim-Lib Error Message end------------");
                Cons("---------------------------------------------");
            }
        }


        static public void MCIupdates(InSim insim, IS_MCI mci)
        {
            for (int i = 0; i < mci.NumC; i++)
            {
                var user = GetClient(mci.Info[i].PLID, false);
                user.P.x = mci.Info[i].X / 65536f;
                user.P.y = mci.Info[i].Y / 65536f;
                user.P.direction = mci.Info[i].Direction;
                user.P.heading = mci.Info[i].Heading;
                user.P.speed = CarSpeed(mci.Info[i].Speed, Settings.UNITKPH);
                user.onTrack = true;
                if (user == Identification.MySelf)
                {
                    if (Trip.tripWatch.IsRunning)
                    {
                        user.P.tripDst += ((mci.Info[i].Speed / 32768f) * 100f) / 10;
                        if (Settings.TRIPCOUNTER) Tracker.TopSpeedChecker();
                    }
                }
                if (user.P.speed < 1 && !user.P.afkStart.IsRunning)
                {
                    user.P.afkStart.Start();
                }
                if (user.P.speed > 1)
                {
                    user.P.afkStart.Reset();
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

            bool LaggingInvolved = false;
            if (con.A.Info.HasFlag(ContactFlags.CCI_LAG) || con.B.Info.HasFlag(ContactFlags.CCI_LAG)) { LaggingInvolved = true; }

            if ((Settings.CARCONTACT || Messages.afkMode) && (A == Identification.MySelf || B == Identification.MySelf))
            {
                string speed = "";
                Clients Crasher = (A == Identification.MySelf ? B : A);
                if (Crasher != Tracker.Trackee)
                {
                    speed = Crasher.P.speed + (Settings.UNITKPH ? "kph)" : "mph)");
                    Messages.SendLocalMsg("^3Contact with ^8" + Crasher.PlayerName + " ^2(" + speed + (LaggingInvolved ? " (lagg)" : ""));
                }
                Crashers.Add(Crasher.PlayerName + " ^8(" + Crasher.Username + ") " + " - (" + speed + (LaggingInvolved ? " (lagg)" : ""));
            }
        }

        static public void SpectateUser(Clients user)
        {
            if (ViewedUser != user && user.P.x != 0 && user.P.y != 0)
            {
                SendKeys.SendWait("{TAB}");
                System.Threading.Thread.Sleep(75);
                Task taskkk = Task.Factory.StartNew(() => SpectateUser(user));
            }
            else if (ViewedUser == user)
            {
                Messages.SendLocalMsg("^2Speccing: ^8" + user.PlayerName + " ^8(" + user.Username + ")");
                return;
            }
            if (!AllConnections.Contains(user))
            {
                return;
            }
        }

        static public Clients ViewedUser = new Clients();
        static public string CurrentTrack = "QQ";
        static public bool OnlineWarning = false;
        static void StateChange(InSim insim, IS_STA sta)
        {
            try
            {
                if (CurrentTrack != sta.Track)
                {
                    CurrentTrack = sta.Track;
                    if (Settings.MINIMAP > 1)
                        MaryMinimap.LoadBackgroundMap();
                }
                ViewedUser = GetClient(sta.ViewPLID, false);
                if (!sta.Flags.HasFlag(StateFlags.ISS_MULTI) && IsOnline)
                {
                    Messages.LogMsg("Left server: " + ServerName);
                    Cons("Left server: " + ServerName);
                    ServerName = "Offline";
                    IsOnline = false;
                    sendWlcMsg = false;
                    ButtonFactory.RemoveButton(ButtonFactory.btnAFKwarning);
                    FlashWindow(Process.GetCurrentProcess().MainWindowHandle, false);
                }
                if (sta.Flags.HasFlag(StateFlags.ISS_MULTI))
                {
                    IsOnline = true;
                }
                else if (!OnlineWarning)
                {
                    Messages.SendLocalMsg("^1Note: ^3Application is designed for mulitplayer.");
                    OnlineWarning = true;
                }
                if (!sta.Flags.HasFlag(StateFlags.ISS_VISIBLE) && Settings.MINIMAP > 1)
                {
                    MinimapFormWindow.Invoke((MethodInvoker)delegate { Form temp = MinimapFormWindow; temp.Visible = false; });
                }
            }
            catch { }
        }

        static public bool ShowMinimap = true;
        static void ConnectionInterface(InSim insim, IS_CIM cim)
        {
            if (cim.UCID == 0)
            {
                if (Settings.MINIMAP > 1)
                {
                    if (cim.Mode.HasFlag(ModeIdentifier.CIM_CAR_SELECT) || cim.Mode.HasFlag(ModeIdentifier.CIM_GARAGE) || cim.Mode.HasFlag(ModeIdentifier.CIM_HOST_OPTIONS) || cim.Mode.HasFlag(ModeIdentifier.CIM_OPTIONS) || cim.Mode.HasFlag(ModeIdentifier.CIM_SHIFTU) || cim.Mode.HasFlag(ModeIdentifier.CIM_TRACK_SELECT))
                    {
                        ShowMinimap = false;
                        MinimapFormWindow.Invoke((MethodInvoker)delegate { Form temp = MinimapFormWindow; temp.Visible = false; });
                    }
                    else
                    {
                        ShowMinimap = true;
                    }
                }
            }
        }

        static public string ServerName = "";
        static public bool IsOnline = false, sendWlcMsg = false, hideADTimer = false;
        static public List<string> BuddysNotice = new List<string>();
        static public void HostJoined(InSim insim, IS_ISM ism)
        {
            if (ism.HName.Length > 1)
            {
                buttonsState = 4;
                AllConnections.Clear();
                insim.Send(new IS_TINY { SubT = TinyType.TINY_NCN, ReqI = 255 });
                insim.Send(new IS_TINY { SubT = TinyType.TINY_NPL, ReqI = 255 });

                System.Threading.Thread.Sleep(1000);

                BuddysNotice = new List<string>(Settings.BUDDYS);
                Messages.LogMsg("Joined server: " + ism.HName);
                Cons("Joined server: " + ism.HName);

                ServerName = ism.HName;
                if (ServerName.StartsWith("^1[TC] CityDriving") && Settings.HIDEAD)
                {
                    hideADTimer = true;
                }
            }
        }

        static public string LFSversion = "";
        static public void VersionInfo(InSim insim, IS_VER ver)
        {
            LFSversion = ver.Version;
            //Below is pointless, because this is an open source version
            //if (!LibData.LFSVersionSupported())
            //{
            //    Cons("You are using an unsupported version of LFS. Shutting down application.");
            //    System.Threading.Thread.Sleep(5000);
            //    Environment.Exit(0);
            //}
            //Cons("Checking ID...");
            Identification.CheckKey();
        }

        static public int buttonsState = 4;
        #region Shift + B
        static public void ShiftB(InSim insim, IS_BFN bfn)
        {
            if (ButtonFactory.ViewedButton != -1) ButtonFactory.ClickableUsed[ButtonFactory.ViewedButton].Style = ButtonStyles.ISB_DARK | ButtonStyles.ISB_CLICK;
            Identification.MySelf.ButtonIDs.Clear();
            ButtonFactory.ButtonsUsed.Clear();
            ButtonFactory.ClickableUsed.Clear();
            ButtonFactory.ViewedButton = -1;
            buttonsState = (buttonsState == 4) ? 1 : buttonsState += 1;

            if (buttonsState == 1)
            {
                Messages.SendLocalMsg("InSim hud hidden.");
                if (Settings.MINIMAP > 1)
                {
                    ShowMinimap = false;
                    MinimapFormWindow.Invoke((MethodInvoker)delegate { Form temp = MinimapFormWindow; temp.Visible = false; });
                }
            }
            else if (buttonsState == 2)
            {
                if (Settings.PERFORMANCE || Settings.OUTGAUGE)
                {
                    ButtonFactory.GadgetsButtons();
                }
                if (Settings.TRIPCOUNTER)
                {
                    ButtonFactory.tripm1.Style = ButtonFactory.tripm2.Style = ButtonFactory.tripm3.Style =
                    ButtonFactory.tripm4.Style = ButtonFactory.tripmReset.Style = ButtonFactory.tripmPause.Style = ButtonStyles.ISB_DARK | ButtonStyles.ISB_C1 | ButtonStyles.ISB_CLICK;
                    ButtonFactory.OpenButton(ButtonFactory.tripmMain);
                }
                Messages.SendLocalMsg("InSim hud partly visible.");
            }
            else
            {
                if (Settings.PERFORMANCE || Settings.OUTGAUGE)
                {
                    ButtonFactory.GadgetsButtons();
                }
                if (buttonsState == 3)
                {
                    ButtonFactory.tripm1.Style = ButtonFactory.tripm2.Style = ButtonFactory.tripm3.Style =
                    ButtonFactory.tripm4.Style = ButtonFactory.tripmReset.Style = ButtonFactory.tripmPause.Style = ButtonStyles.ISB_DARK | ButtonStyles.ISB_C1;
                    ButtonFactory.btnTracker1.Style = ButtonStyles.ISB_DARK;
                    Messages.SendLocalMsg("InSim hud un-clickable.");
                }
                else if (buttonsState == 4)
                {
                    if (Identification.MySelf.C.isCop && Settings.COPHUDON) { Roleplay.PoliceButtons(true); if (Settings.EXTRAHUDON) Roleplay.ExtraButtons(true); }
                    if (Identification.MySelf.C.isResQ && Settings.RESQHUDON) { Roleplay.ResQButtons(true); if (Settings.EXTRAHUDON) Roleplay.ExtraButtons(true); }
                    if (Settings.EXTRAHUDEROLEPENDANTON) { ButtonFactory.ReloadButtonText(); Roleplay.ExtraButtonsRolependant(true); }
                    ButtonFactory.tripm1.Style = ButtonFactory.tripm2.Style = ButtonFactory.tripm3.Style =
                    ButtonFactory.tripm4.Style = ButtonFactory.tripmReset.Style = ButtonFactory.tripmPause.Style = ButtonStyles.ISB_DARK | ButtonStyles.ISB_CLICK | ButtonStyles.ISB_C1;
                    ButtonFactory.btnTracker1.Style = ButtonStyles.ISB_DARK | ButtonStyles.ISB_CLICK;
                    Messages.SendLocalMsg("InSim hud visible.");
                }
                if (Settings.TRIPCOUNTER)
                {
                    ButtonFactory.OpenButton(ButtonFactory.tripmMain);
                }
                if (Settings.MINIMAP > 1)
                {
                    ShowMinimap = true;
                    MinimapFormWindow.Invoke((MethodInvoker)delegate { Form temp = MinimapFormWindow; temp.Visible = true; });
                }
            }
            ButtonFactory.RemoveButton(ButtonFactory.btnSpeedOut);
        }
        #endregion

        static public bool IsCop(string name)
        {
            return name.Contains("[cop]") || name.Contains("[vcu]") || name.Contains("[hwa]") || name.Contains("[ca]") || name.Contains("[as]") || name.Contains("[t]");
        }
        static public bool IsMed(string name)
        {
            return name.Contains("[res]") || name.Contains("[med]") || name.Contains("[tow]") || name.Contains("[hwa]");
        }

        static public string StripColours(string str)
        {
            return new Regex(@"\^[0-9]").Replace(str, string.Empty);
        }

        #region New connection
        static void NewConnection(InSim insim, IS_NCN ncn)
        {
            if (ncn.UCID != 0)
            {
                Clients NewConn = new Clients() { UniqueID = ncn.UCID, Username = ncn.UName, PlayerName = ncn.PName, PlayerNameRAW = StripColours(ncn.PName) };
                AllConnections.Add(NewConn);
                NewConn.P.afkStart.Start();

                NewConn.C.isCop = IsCop(NewConn.PlayerName.ToLower());
                NewConn.C.isResQ = IsMed(NewConn.PlayerName.ToLower());
                if (!NewConn.C.isCop && !NewConn.C.isResQ) NewConn.C.isCiv = true;

                if (NewConn.Username.ToLower() == Identification.MyUsername.ToLower())
                {
                    Identification.MySelf = NewConn;
                    ButtonFactory.GadgetsButtons();

                    if (Identification.MySelf.C.isCop && Settings.COPHUDON) { Roleplay.PoliceButtons(true); if (Settings.EXTRAHUDON) Roleplay.ExtraButtons(true); }
                    if (Identification.MySelf.C.isResQ && Settings.RESQHUDON) { Roleplay.ResQButtons(true); if (Settings.EXTRAHUDON) Roleplay.ExtraButtons(true); }
                    if (Settings.EXTRAHUDEROLEPENDANTON) { ButtonFactory.ReloadButtonText(); Roleplay.ExtraButtonsRolependant(true); }
                    if (Settings.TRIPCOUNTER) ButtonFactory.OpenButton(ButtonFactory.tripmMain);
                    Trip.tripWatch.Start();
                }

                if (Settings.CHECKADMIN && Identification.FULLVERSION)
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
            var User = GetClient(npl.UCID, true);

            User.PlayerID = npl.PLID;
            User.P.CarName = npl.CName;
            User.P.Flags = npl.Flags;
            User.PlayerName = npl.PName;
            User.P.SkinName = npl.SName;
            User.P.AddedMassKG = npl.H_Mass;
            User.P.IntakeRes = npl.H_TRes;
            User.P.TiresFront = npl.Tyres.FrontRight.ToString();
            User.P.TiresRear = npl.Tyres.RearRight.ToString();
            User.P.x = 0;
            User.P.y = 0;

            if (!User.Equals(Identification.MySelf) && Settings.SKINCHECK && !Settings.BUDDYS.ToArray().Contains(User.Username.ToLower()))
            {
                SkinCheck.CheckSkin(User);
            }
        }

        static public void PlayerNameChanged(InSim insim, IS_CPR cpr)
        {
            try
            {
                var User = GetClient(cpr.UCID, true);
                User.PlayerName = cpr.PName;
                User.PlayerNameRAW = StripColours(cpr.PName);

                User.C.isCop = IsCop(User.PlayerName.ToLower());
                User.C.isResQ = IsMed(User.PlayerName.ToLower());
                if (!User.C.isCop && !User.C.isResQ) User.C.isCiv = true;

                if (User == Identification.MySelf)
                {

                    if (ButtonFactory.ViewedButton != -1)
                    {
                        ButtonFactory.ClickableUsed[ButtonFactory.ViewedButton].Style = ButtonFactory.ClickableUsed[ButtonFactory.ViewedButton].OldStyle;
                        ButtonFactory.OpenButton(ButtonFactory.ClickableUsed[ButtonFactory.ViewedButton]);
                        ButtonFactory.ViewedButton = -1;
                    }

                    if (Settings.COPHUDON)
                    {
                        if (Identification.MySelf.C.isCop)
                        {
                            Roleplay.PoliceButtons(true);
                            if (Settings.EXTRAHUDON)
                                Roleplay.ExtraButtons(true);
                        }
                        else
                        {
                            Roleplay.PoliceButtons(false);
                            if (Settings.EXTRAHUDON)
                                Roleplay.ExtraButtons(false);
                        }
                    }
                    if (Settings.RESQHUDON)
                    {
                        if (Identification.MySelf.C.isResQ)
                        {
                            Roleplay.ResQButtons(true);

                            if (Settings.EXTRAHUDON)
                                Roleplay.ExtraButtons(true);
                        }
                        else
                        {
                            Roleplay.ResQButtons(false);

                            if (Settings.EXTRAHUDON && !Identification.MySelf.C.isCop)
                                Roleplay.ExtraButtons(false);
                        }
                    }
                    if (Settings.EXTRAHUDEROLEPENDANTON)
                    {
                        ButtonFactory.ReloadButtonText();
                        Roleplay.ExtraButtonsRolependant(false);
                        Roleplay.ExtraButtonsRolependant(true);
                    }

                }
            }
            catch (Exception e) { ConsError(e.ToString()); }
        }

        static public void ConnLeave(InSim insim, IS_CNL cnl)
        {
            var user = GetClient(cnl.UCID, true);
            try
            {
                if (Tracker.Trackee.Equals(user))
                {
                    Tracker.EditUserToTrack(user.Username.ToLower());
                    Identification.MySelf.P.Chasing = false;
                }
                if (Identification.MySelf.P.Chase.Contains(user))
                {
                    DefaultMinimap.UpdateCopsInChase(user.PlayerNameRAW);
                }
                if (Identification.MySelf.Equals(user))
                {
                    Identification.MySelf.P.Chase.Clear();
                    Identification.MySelf.P.Chasing = false;
                    if (ButtonFactory.btnTracker2.Children.Contains(ButtonFactory.btnTracker6)) ButtonFactory.btnTracker2.Children.Remove(ButtonFactory.btnTracker6); ButtonFactory.RemoveButton(ButtonFactory.btnTracker6);
                }
            }
            catch (Exception e) { ConsError(e.ToString()); }
            AllConnections.Remove(user);
        }

        static public void ConnSpec(InSim insim, IS_PLL spec)
        {
            var user = GetClient(spec.PLID, false);
            user.PlayerID = 0;
            user.onTrack = false;
            user.P.x = user.P.y = 0;
            user.P.speed = 0;
            user.P.afkStart.Restart();
            if (Tracker.Trackee.Equals(user))
            {
                Identification.MySelf.P.Chasing = false;
            }
            if (Identification.MySelf.P.Chase.Contains(user))
            {
                DefaultMinimap.UpdateCopsInChase(user.PlayerNameRAW);
            }
            if (Identification.MySelf.Equals(user))
            {
                Identification.MySelf.P.Chase.Clear();
                Identification.MySelf.P.Chasing = false;
                if (ButtonFactory.btnTracker2.Children.Contains(ButtonFactory.btnTracker6)) ButtonFactory.btnTracker2.Children.Remove(ButtonFactory.btnTracker6); ButtonFactory.RemoveButton(ButtonFactory.btnTracker6);
            }
        }

        static public void ConnPit(InSim insim, IS_PLP pit)
        {
            var user = GetClient(pit.PLID, false);
            user.onTrack = false;
            user.P.x = user.P.y = 0;
            user.P.speed = 0;
            user.PlayerID = 0;
            user.P.afkStart.Restart();

            if (Tracker.Trackee.Equals(user))
            {
                Identification.MySelf.P.Chasing = false;
            }
            if (Identification.MySelf.P.Chase.Contains(user))
            {
                DefaultMinimap.UpdateCopsInChase(user.PlayerNameRAW);
            }
            if (Identification.MySelf.Equals(user))
            {
                Identification.MySelf.P.Chase.Clear();
                Identification.MySelf.P.Chasing = false;
                if (ButtonFactory.btnTracker2.Children.Contains(ButtonFactory.btnTracker6)) ButtonFactory.btnTracker2.Children.Remove(ButtonFactory.btnTracker6); ButtonFactory.RemoveButton(ButtonFactory.btnTracker6);
            }
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