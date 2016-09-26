using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using InSimDotNet;
using InSimDotNet.Packets;

namespace Mary
{
    class ButtonFactory
    {
        #region Button events
        static public void ButtonEvents(string btnName)
        {
            Task taskkk;
            switch (btnName)
            {
                case "btnSiren":
                    if (Roleplay.SirenActive)
                    {
                        Roleplay.UpdateSiren(true);
                        Roleplay.clsSiren = 0;
                    }
                    else
                    {
                        Roleplay.UpdateSiren(false);
                        Roleplay.clsSiren = Roleplay.SirenMode.Both;
                    }
                    break;
                case "btnCophud1":
                    taskkk = Task.Factory.StartNew(() => Messages.SendMultiMsg(Settings.COPHUD1));
                    break;
                case "btnCophud2":
                    taskkk = Task.Factory.StartNew(() => Messages.SendMultiMsg(Settings.COPHUD2));
                    break;
                case "btnCophud3":
                    taskkk = Task.Factory.StartNew(() => Messages.SendMultiMsg(Settings.COPHUD3));
                    break;
                case "btnCophud4":
                    taskkk = Task.Factory.StartNew(() => Messages.SendMultiMsg(Settings.COPHUD4));
                    break;
                case "btnCophud5":
                    taskkk = Task.Factory.StartNew(() => Messages.SendMultiMsg(Settings.COPHUD5));
                    break;
                case "btnCophud6":
                    taskkk = Task.Factory.StartNew(() => Messages.SendMultiMsg(Settings.COPHUD6));
                    break;
                case "btnCophud7":
                    taskkk = Task.Factory.StartNew(() => Messages.SendMultiMsg(Settings.COPHUD7));
                    break;
                case "btnCophud8":
                    taskkk = Task.Factory.StartNew(() => Messages.SendMultiMsg(Settings.COPHUD8));
                    break;
                case "btnCophud9":
                    taskkk = Task.Factory.StartNew(() => Messages.SendMultiMsg(Settings.COPHUD9));
                    break;
                case "btnCophud10":
                    taskkk = Task.Factory.StartNew(() => Messages.SendMultiMsg(Settings.COPHUD10));
                    break;
                case "btnCophud11":
                    taskkk = Task.Factory.StartNew(() => Messages.SendMultiMsg(Settings.COPHUD11));
                    break;
                case "btnCophud12":
                    taskkk = Task.Factory.StartNew(() => Messages.SendMultiMsg(Settings.COPHUD12));
                    break;
                case "btnResqhud1":
                    taskkk = Task.Factory.StartNew(() => Messages.SendMultiMsg(Settings.RESQHUD1));
                    break;
                case "btnResqhud2":
                    taskkk = Task.Factory.StartNew(() => Messages.SendMultiMsg(Settings.RESQHUD2));
                    break;
                case "btnResqhud3":
                    taskkk = Task.Factory.StartNew(() => Messages.SendMultiMsg(Settings.RESQHUD3));
                    break;
                case "btnResqhud4":
                    taskkk = Task.Factory.StartNew(() => Messages.SendMultiMsg(Settings.RESQHUD4));
                    break;
                case "btnResqhud5":
                    taskkk = Task.Factory.StartNew(() => Messages.SendMultiMsg(Settings.RESQHUD5));
                    break;
                case "btnResqhud6":
                    taskkk = Task.Factory.StartNew(() => Messages.SendMultiMsg(Settings.RESQHUD6));
                    break;
                case "btnResqhud7":
                    taskkk = Task.Factory.StartNew(() => Messages.SendMultiMsg(Settings.RESQHUD7));
                    break;
                case "btnExtrahud1":
                    taskkk = Task.Factory.StartNew(() => Messages.SendMultiMsg(Settings.EXTRAHUD1));
                    break;
                case "btnExtrahud2":
                    taskkk = Task.Factory.StartNew(() => Messages.SendMultiMsg(Settings.EXTRAHUD2));
                    break;
                case "btnExtrahud3":
                    taskkk = Task.Factory.StartNew(() => Messages.SendMultiMsg(Settings.EXTRAHUD3));
                    break;
                case "btnExtrahud4":
                    taskkk = Task.Factory.StartNew(() => Messages.SendMultiMsg(Settings.EXTRAHUD4));
                    break;
                case "btnExtrahud5":
                    taskkk = Task.Factory.StartNew(() => Messages.SendMultiMsg(Settings.EXTRAHUD5));
                    break;
                case "btnTracker1":
                    if (Tracker.Trackee.Username == null)
                    {
                        Tracker.Trackee = Tracker.ScanCars();
                        if (Tracker.Trackee.Username != null) Messages.SendLocalMsg("^2Tracking: ^8" + Tracker.Trackee.PlayerName);
                        else Messages.SendLocalMsg("No car in range...");
                    }
                    else
                    {
                        taskkk = Task.Factory.StartNew(() => Tracker.EditUserToTrack(Tracker.Trackee.Username.ToLower()));
                    }
                    break;
                case "panelstatbtn3":
                    RemoveButton(Statistics.StatsBtnsAll);
                    break;
                case "panelstatbtn10":
                    Statistics.LoadTable(false);
                    break;
                case "btnAFKwarning":
                    RemoveButton(btnAFKwarning);
                    Program.FlashWindow(Process.GetCurrentProcess().MainWindowHandle, false);
                    break;
                case "tripm1":
                    Trip.TripbtnShow = 1;
                    break;
                case "tripm2":
                    Trip.TripbtnShow = 2;
                    break;
                case "tripm3":
                    Trip.TripbtnShow = 3;
                    break;
                case "tripm4":
                    Trip.TripbtnShow = 4;
                    break;
                case "tripmReset":
                    Program.MySelf.P.tripDst = 0;
                    Program.MySelf.P.topSpeed = 0;
                    Messages.SendLocalMsg("Tripcounter reset.");
                    Trip.tripWatch.Restart();
                    break;
                case "tripmPause":
                    if (Trip.tripWatch.IsRunning)
                    {
                        Messages.SendLocalMsg("Tripcounter paused.");
                        Trip.tripWatch.Stop();
                    }
                    else
                    {
                        Messages.SendLocalMsg("Tripcounter continued.");
                        Trip.tripWatch.Start();
                    }
                    break;
            }
            foreach (Buttons btns in ButtonsUsed)
            if (btns.Name == btnName && btns.PublicText != null)
            {
                Messages.SendLocalMsg(btns.PublicText);
                break;
            }
        }
        #endregion

        #region Button clicked
        static public void BTNClicked(InSim insim, IS_BTC btc)
        {
            try
            {
                foreach (Buttons btns in ButtonsUsed)
                {                  
                    if (btc.ClickID == btns.ID)
                    {
                        ButtonEvents(btns.Name);
                        break;
                    }
                }
            }
            catch (Exception)
            {
               // Program.ConsError(e.ToString());
            }
        }
        #endregion

        static public void GadgetsButtons()
        {
            if (Settings.PERFORMANCE)
            {
                OpenButton(btnCPUusage1);
            }
            if (Settings.OUTGAUGE)
            {
                OpenButton(btnClutchOut);
            }
        }

        #region Change button
        static public int ViewedButton = -1;
        static public void MouseScroll(bool down)
        {
            ButtonStyles clickStyle = ButtonStyles.ISB_LIGHT | ButtonStyles.ISB_CLICK | ButtonStyles.ISB_C4 | ButtonStyles.ISB_C1;
            try
            {
                if (Program.buttonsState == 4)
                {
                    if (ClickableUsed.Count < ViewedButton)
                    {
                        ViewedButton = -1;
                    }

                    if (ViewedButton == -1)
                    {
                        if (down)
                        {
                            ViewedButton = 0;
                            ClickableUsed[ViewedButton].OldStyle = ClickableUsed[ViewedButton].Style;
                            ClickableUsed[ViewedButton].Style = clickStyle;
                            OpenButton(ClickableUsed[ViewedButton]);
                        }
                        else
                        {
                            ViewedButton = ClickableUsed.Count - 1;
                            ClickableUsed[ViewedButton].OldStyle = ClickableUsed[ViewedButton].Style;
                            ClickableUsed[ViewedButton].Style = clickStyle;
                            OpenButton(ClickableUsed[ViewedButton]);
                        }
                    }
                    else if (ViewedButton == 0 && !down)
                    {
                        ClickableUsed[ViewedButton].Style = ClickableUsed[ViewedButton].OldStyle;
                        OpenButton(ClickableUsed[ViewedButton]);
                        ViewedButton = -1;
                    }
                    else if (ViewedButton == ClickableUsed.Count() - 1)
                    {
                        if (down)
                        {
                            ClickableUsed[ViewedButton].Style = ClickableUsed[ViewedButton].OldStyle;
                            OpenButton(ClickableUsed[ViewedButton]);
                            ViewedButton = -1;
                        }
                        else
                        {
                            ClickableUsed[ViewedButton].Style = ClickableUsed[ViewedButton].OldStyle;
                            OpenButton(ClickableUsed[ViewedButton]);
                            ViewedButton--;
                            ClickableUsed[ViewedButton].OldStyle = ClickableUsed[ViewedButton].Style;
                            ClickableUsed[ViewedButton].Style = clickStyle;
                            OpenButton(ClickableUsed[ViewedButton]);
                        }
                    }
                    else if (ViewedButton > -1 && ViewedButton < ClickableUsed.Count - 1)
                    {
                        ClickableUsed[ViewedButton].Style = ClickableUsed[ViewedButton].OldStyle;
                        OpenButton(ClickableUsed[ViewedButton]);

                        if (down) ViewedButton++;
                        else ViewedButton--;
                        ClickableUsed[ViewedButton].OldStyle = ClickableUsed[ViewedButton].Style;
                        ClickableUsed[ViewedButton].Style = clickStyle;
                        OpenButton(ClickableUsed[ViewedButton]);
                    }
                }
            }
            catch { }
        }
        #endregion

        static public List<Buttons> ButtonsUsed = new List<Buttons>();
        static public List<Buttons> ClickableUsed = new List<Buttons>();

        public class Buttons
        {
            public string Name, Text, PublicText;
            public byte Width, Height, LeftRight, TopDown, ID;
            public bool Clickable, InFocus;
            public ButtonStyles Style, OldStyle;
            public List<Buttons> Children = new List<Buttons>() { };
        }

        static public void OpenButton(Buttons motherButton)
        {
            Program.insim.Send(new IS_BTN { BStyle = motherButton.Style, Text = motherButton.Text, H = motherButton.Height, W = motherButton.Width, L = motherButton.LeftRight, T = motherButton.TopDown, UCID = Program.MySelf.UniqueID, ClickID = motherButton.ID, ReqI = motherButton.ID });
            if (!ButtonsUsed.Contains(motherButton)) { ButtonsUsed.Add(motherButton); if (motherButton.Clickable) { ClickableUsed.Add(motherButton); } }
            for (int x = 0; x < motherButton.Children.Count; x++)
            {
                Buttons btnToSend = motherButton.Children[x];
                Program.insim.Send(new IS_BTN { BStyle = btnToSend.Style, Text = btnToSend.Text, H = btnToSend.Height, W = btnToSend.Width, L = btnToSend.LeftRight, T = btnToSend.TopDown, UCID = Program.MySelf.UniqueID, ClickID = btnToSend.ID, ReqI = btnToSend.ID });
                if (!ButtonsUsed.Contains(btnToSend)) { ButtonsUsed.Add(btnToSend); if (btnToSend.Clickable) { ClickableUsed.Add(btnToSend); } }
            }
        }
        static public void RemoveButton(Buttons motherButton)
        {
            Program.insim.Send(new IS_BFN { SubT = ButtonFunction.BFN_DEL_BTN, UCID = Program.MySelf.UniqueID, ClickID = motherButton.ID });
            ButtonsUsed.Remove(motherButton); if (motherButton.Clickable) { ClickableUsed.Remove(motherButton); }
            for (int x = 0; x < motherButton.Children.Count; x++)
            {
                Buttons btnToSend = motherButton.Children[x];
                Program.insim.Send(new IS_BFN { SubT = ButtonFunction.BFN_DEL_BTN, UCID = Program.MySelf.UniqueID, ClickID = btnToSend.ID });
                ButtonsUsed.Remove(btnToSend); if (btnToSend.Clickable) { ClickableUsed.Remove(btnToSend); }
            }
        }

        static public void RemoveButton(List<byte> ListName)
        {
            List<ButtonFactory.Buttons> tempList = new List<Buttons>();
            foreach (byte id in ListName)
            {  
                foreach (Buttons btn in ButtonsUsed)
                if (id == btn.ID)
                {
                    tempList.Add(btn);
                }
            }
            foreach (var bb in tempList)
            {
                Task t = Task.Factory.StartNew(() => RemoveButton(bb));
            }
        }
        //52 used (eng dmg)
        #region Tripmeter
        static public Buttons tripInfo = new Buttons()
        {
            Name = "tripInfo",
            Text = "--",
            Style = ButtonStyles.ISB_LEFT,
            Width = 14,
            Height = 4,
            LeftRight = (byte)(Settings.TRIPCOUNTERleftRight + 1),
            TopDown = (byte)(Settings.TRIPCOUNTERupDown + 5),
            ID = 61
        };
        static public Buttons tripm1 = new Buttons()
        {
            Name = "tripm1",
            Text = "Dst",
            Style = ButtonStyles.ISB_DARK | ButtonStyles.ISB_CLICK | ButtonStyles.ISB_C1,
            Width = 5,
            Height = 3,
            LeftRight = (byte)(Settings.TRIPCOUNTERleftRight + 1),
            TopDown = (byte)(Settings.TRIPCOUNTERupDown + 1),
            Clickable = true,
            ID = 60
        };
        static public Buttons tripm2 = new Buttons()
        {
            Name = "tripm2",
            Text = "Time",
            Style = ButtonStyles.ISB_DARK | ButtonStyles.ISB_CLICK | ButtonStyles.ISB_C1,
            Width = 5,
            Height = 3,
            LeftRight = (byte)(Settings.TRIPCOUNTERleftRight + 6),
            TopDown = (byte)(Settings.TRIPCOUNTERupDown + 1),
            Clickable = true,
            ID = 59
        };
        static public Buttons tripm3 = new Buttons()
        {
            Name = "tripm3",
            Text = "Avg",
            Style = ButtonStyles.ISB_DARK | ButtonStyles.ISB_CLICK | ButtonStyles.ISB_C1,
            Width = 5,
            Height = 3,
            LeftRight = (byte)(Settings.TRIPCOUNTERleftRight + 11),
            TopDown = (byte)(Settings.TRIPCOUNTERupDown + 1),
            Clickable = true,
            ID = 58
        };
        static public Buttons tripm4 = new Buttons()
        {
            Name = "tripm4",
            Text = "Top",
            Style = ButtonStyles.ISB_DARK | ButtonStyles.ISB_CLICK | ButtonStyles.ISB_C1,
            Width = 5,
            Height = 3,
            LeftRight = (byte)(Settings.TRIPCOUNTERleftRight + 16),
            TopDown = (byte)(Settings.TRIPCOUNTERupDown + 1),
            Clickable = true,
            ID = 57
        };
        static public Buttons tripmReset = new Buttons()
        {
            Name = "tripmReset",
            Text = "^6Reset",
            Style = ButtonStyles.ISB_DARK | ButtonStyles.ISB_CLICK,
            Width = 5,
            Height = 3,
            LeftRight = (byte)(Settings.TRIPCOUNTERleftRight + 16),
            TopDown = (byte)(Settings.TRIPCOUNTERupDown + 4),
            Clickable = true,
            ID = 56
        };
        static public Buttons tripmPause = new Buttons()
        {
            Name = "tripmPause",
            Text = "^2Pause",
            Style = ButtonStyles.ISB_DARK | ButtonStyles.ISB_CLICK,
            Width = 5,
            Height = 3,
            LeftRight = (byte)(Settings.TRIPCOUNTERleftRight + 16),
            TopDown = (byte)(Settings.TRIPCOUNTERupDown + 6),
            Clickable = true,
            ID = 55
        };
        static public Buttons tripmMain = new Buttons()
        {
            Name = "tripmMain",
            Text = "",
            Style = ButtonStyles.ISB_DARK,
            Width = 22,
            Height = 10,
            LeftRight = Settings.TRIPCOUNTERleftRight,
            TopDown = Settings.TRIPCOUNTERupDown,
            ID = 54,
            Children = new List<Buttons>() { tripInfo, tripm1, tripm2, tripm3, tripm4, tripmReset, tripmPause}
        };
        #endregion

        #region Stats
        static public Buttons panelstatbtn1 = new Buttons()
        {
            Name = "panelstatbtn1",
            Text = "",
            Style = ButtonStyles.ISB_DARK,
            Width = 150,
            LeftRight = 25,
            TopDown = 40,
            ID = 193
        };
        static public Buttons panelstatbtn2 = new Buttons()
        {
            Name = "panelstatbtn2",
            Text = "",
            Style = ButtonStyles.ISB_DARK,
            Width = 150,
            LeftRight = 25,
            TopDown = 40,
            ID = 192
        };
        static public Buttons panelstatbtn3 = new Buttons()
        {
            Name = "panelstatbtn3",
            Text = "^1×",
            Style = ButtonStyles.ISB_CLICK | ButtonStyles.ISB_LIGHT,
            Height = 3,
            Width = 3,
            LeftRight = 171,
            TopDown = 41,
            ID = 191
        };
        static public Buttons panelstatbtn4 = new Buttons()
        {
            Name = "panelstatbtn4",
            Text = "--Statistics--",
            Style = ButtonStyles.ISB_C4,
            Height = 8,
            Width = 30,
            LeftRight = 85,
            TopDown = 41,
            ID = 190
        };
        static public Buttons panelstatbtn5 = new Buttons()
        {
            Name = "panelstatbtn5",
            Text = "Name (username) [Country]",
            Style = ButtonStyles.ISB_C2 | ButtonStyles.ISB_C4 | ButtonStyles.ISB_LEFT,
            Height = 5,
            Width = 35,
            LeftRight = 26,
            TopDown = 50,
            ID = 189
        };
        static public Buttons panelstatbtn6 = new Buttons()
        {
            Name = "panelstatbtn6",
            Text = "[Join date]",
            Style = ButtonStyles.ISB_C2 | ButtonStyles.ISB_C4 | ButtonStyles.ISB_LEFT,
            Height = 5,
            Width = 16,
            LeftRight = 61,
            TopDown = 50,
            ID = 188
        };
        static public Buttons panelstatbtn7 = new Buttons()
        {
            Name = "panelstatbtn7",
            Text = "[Km] - [Money] - [Wealth]",
            Style = ButtonStyles.ISB_C2 | ButtonStyles.ISB_C4 | ButtonStyles.ISB_LEFT,
            Height = 5,
            Width = 40,
            LeftRight = 76,
            TopDown = 50,
            ID = 187
        };
        static public Buttons panelstatbtn8 = new Buttons()
        {
            Name = "panelstatbtn8",
            Text = "[Time Cop] - [Robber] - [Total]",
            Style = ButtonStyles.ISB_C2 | ButtonStyles.ISB_C4 | ButtonStyles.ISB_RIGHT,
            Height = 5,
            Width = 30,
            LeftRight = 118,
            TopDown = 50,
            ID = 186
        };
        static public Buttons panelstatbtn9 = new Buttons()
        {
            Name = "panelstatbtn9",
            Text = "[XP Cop] - [XP Robber] - [XP RP]",
            Style = ButtonStyles.ISB_C2 | ButtonStyles.ISB_C4 | ButtonStyles.ISB_RIGHT,
            Height = 5,
            Width = 24,
            LeftRight = 151,
            TopDown = 50,
            ID = 185,
            Children = new List<Buttons>() { panelstatbtn1, panelstatbtn2, panelstatbtn3, panelstatbtn4, panelstatbtn5, panelstatbtn6, panelstatbtn7, panelstatbtn8 }
        };
        static public Buttons panelstatbtn10 = new Buttons()
        {
            Name = "panelstatbtn10",
            Text = "^2Next page",
            Style = ButtonStyles.ISB_LIGHT | ButtonStyles.ISB_CLICK,
            Height = 5,
            Width = 15,
            LeftRight = 130,
            TopDown = 42,
            ID = 184
        };
        static public Buttons panelstatbtnProgressbar = new Buttons()
        {
            Name = "panelstatbtnProgressbar",
            Style = ButtonStyles.ISB_LIGHT,
            Height = 5,
            Width = 15,
            LeftRight = 50,
            TopDown = 42,
            ID = 183
        };

        ///////
        static public Buttons statbtn1User = new Buttons();
        static public Buttons statbtn2User = new Buttons();
        static public Buttons statbtn3User = new Buttons();
        static public Buttons statbtn4User = new Buttons();
        static public Buttons statbtn5User = new Buttons();
        static public Buttons statbtn0User = new Buttons() { Children = new List<Buttons>() { statbtn1User, statbtn2User, statbtn3User, statbtn4User, statbtn5User } };
        #endregion

        #region Cruise control
        static public Buttons btnCruiseControl = new Buttons()
            {
                Name = "btnCruiseControl",
                Text = "-Cruise Control-",
                Style = ButtonStyles.ISB_DARK,
                Height = Settings.CRUISECONTROLset[2],
                Width = Settings.CRUISECONTROLset[3],
                LeftRight = Settings.CRUISECONTROLset[0],
                TopDown = Settings.CRUISECONTROLset[1],
                InFocus = false,
                Clickable = false,
                ID = 239
            };
        #endregion

        #region Scanner og tracker
        static public Buttons btnTracker1 = new Buttons()
        {
            Name = "btnTracker1",
            Text = "",
            Style = ButtonStyles.ISB_DARK | ButtonStyles.ISB_CLICK,
            Height = Settings.COPSCANNERset[2],
            Width = Settings.COPSCANNERset[3],
            LeftRight = Settings.COPSCANNERset[0],
            TopDown = Settings.COPSCANNERset[1],
            InFocus = false,
            Clickable = true,
            ID = 238
        };
        static public Buttons btnTracker3 = new Buttons()
        {
            Name = "btnTracker3",
            Text = "",
            Style = ButtonStyles.ISB_RIGHT,
            Height = 4,
            Width = 6,
            LeftRight = (byte)(Settings.COPTRACKERMAINleftRight + 9),
            TopDown = (byte)(Settings.COPTRACKERMAINupDown + 11),
            InFocus = false,
            Clickable = false,
            ID = 237
        };
        static public Buttons btnTracker4 = new Buttons()
        {
            Name = "btnTracker4",
            Text = "",
            Style = ButtonStyles.ISB_LEFT,
            Height = 4,
            Width = 4,
            LeftRight = Settings.COPTRACKERMAINleftRight,
            TopDown = (byte)(Settings.COPTRACKERMAINupDown + 11),
            InFocus = false,
            Clickable = false,
            ID = 236
        };
        static public Buttons btnTracker5 = new Buttons()
        {
            Name = "btnTracker5",
            Text = "",
            Style = ButtonStyles.ISB_LEFT,
            Height = 4,
            Width = 6,
            LeftRight = (byte)(Settings.COPTRACKERMAINleftRight + 3),
            TopDown = (byte)(Settings.COPTRACKERMAINupDown + 11),
            InFocus = false,
            Clickable = false,
            ID = 235
        };
        static public Buttons btnTracker2 = new Buttons()
        {
            Name = "btnTracker2",
            Text = "",
            Style = ButtonStyles.ISB_DARK,
            Height = 15,
            Width = 15,
            LeftRight = Settings.COPTRACKERMAINleftRight,
            TopDown = Settings.COPTRACKERMAINupDown,
            InFocus = false,
            Clickable = false,
            ID = 234,
            Children = new List<Buttons>() { btnTracker1, btnTracker3, btnTracker4, btnTracker5 }
        };
        #endregion

        #region Gadgets
        static public Buttons btnRAMfree1 = new Buttons()
        {
            Name = "btnRAMfree1",
            Text = "Free RAM:",
            Style = ButtonStyles.ISB_DARK,
            Height = 3,
            Width = Settings.RAMset[3],
            LeftRight = Settings.RAMset[0],
            TopDown = Settings.RAMset[1],
            ID = 233,
        };
        static public Buttons btnCPUusage1 = new Buttons()
        {
            Name = "btnCPUusage1",
            Text = "CPU usage:",
            Style = ButtonStyles.ISB_DARK,
            Height = 3,
            Width = Settings.CPUset[3],
            LeftRight = Settings.CPUset[0],
            TopDown = Settings.CPUset[1],
            ID = 232,
            Children = new List<Buttons>() { btnRAMfree1 }
        };
        static public Buttons btnRAMfree2 = new Buttons()
        {
            Name = "btnRAMfree2",
            Text = "^7--",
            Style = ButtonStyles.ISB_LIGHT,
            Height = Settings.RAMset[2],
            Width = Settings.RAMset[3],
            LeftRight = Settings.RAMset[0],
            TopDown = (byte)(Settings.RAMset[1] + 3),
            ID = 231
        };
        static public Buttons btnCPUusage2 = new Buttons()
        {
            Name = "btnCPUusage2",
            Text = "^7--",
            Style = ButtonStyles.ISB_LIGHT,
            Height = Settings.CPUset[2],
            Width = Settings.CPUset[3],
            LeftRight = Settings.CPUset[0],
            TopDown = (byte)(Settings.CPUset[1] + 3),
            ID = 230,
            Children = new List<Buttons>() { btnRAMfree2 }
        };
        static public Buttons btnGearOut = new Buttons()
        {
            Name = "btnGearOut",
            Text = "Gear",
            Style = ButtonStyles.ISB_DARK | ButtonStyles.ISB_C1,
            Height = 3,
            Width = Settings.DIGITALGEARset[3],
            LeftRight = Settings.DIGITALGEARset[0],
            TopDown = Settings.DIGITALGEARset[1],
            ID = 228
        };
        static public Buttons btnUnitOut = new Buttons()
        {
            Name = "btnUnitOut",
            Text = (Settings.UNITKPH) ? "km/h" : "mph",
            Style = ButtonStyles.ISB_C1,
            Height = 5,
            Width = 8,
            LeftRight = (byte)(Settings.DIGITALSPEEDOleftRight + 12),
            TopDown = (byte)(Settings.DIGITALSPEEDOupDown + 7),
            ID = 227
        };
        static public Buttons btnSpeedBgOut = new Buttons()
        {
            Name = "btnSpeedBgOut",
            Text = "",
            Style = ButtonStyles.ISB_LIGHT,
            Height = 11,
            Width = 18,
            LeftRight = (byte)(Settings.DIGITALSPEEDOleftRight + 2),
            TopDown = (byte)(Settings.DIGITALSPEEDOupDown + 2),
            ID = 225
        };
        static public Buttons btnFuelOut2 = new Buttons()
        {
            Name = "btnFuelOut2",
            Text = "--",
            Style = ButtonStyles.ISB_LIGHT | ButtonStyles.ISB_C1,
            Height = Settings.DIGITALFUELset[2],
            Width = Settings.DIGITALFUELset[3],
            LeftRight = Settings.DIGITALFUELset[0],
            TopDown = (byte)(Settings.DIGITALFUELset[1] + 3),
            ID = 229
        };
        static public Buttons btnFuelOut = new Buttons()
        {
            Name = "btnFuelOut",
            Text = "Fuel",
            Style = ButtonStyles.ISB_DARK | ButtonStyles.ISB_C1,
            Height = 3,
            Width = Settings.DIGITALFUELset[3],
            LeftRight = Settings.DIGITALFUELset[0],
            TopDown = Settings.DIGITALFUELset[1],
            ID = 226
        };
        static public Buttons btnEngineOut = new Buttons()
        {
            Name = "btnEngineOut",
            Text = "",
            Style = ButtonStyles.ISB_C1,
            Height = 3,
            Width = Settings.DIGITALENGINEset[3],
            LeftRight = Settings.DIGITALENGINEset[0],
            TopDown = Settings.DIGITALENGINEset[1],
            ID = 53
        };
        static public Buttons btnClutchOut = new Buttons()
        {
            Name = "btnClutchOut",
            Text = "",
            Style = ButtonStyles.ISB_C1,
            Height = 3,
            Width = Settings.DIGITALCLUTCHset[3],
            LeftRight = Settings.DIGITALCLUTCHset[0],
            TopDown = Settings.DIGITALCLUTCHset[1],
            ID = 224,
            Children = new List<Buttons>() { btnGearOut, btnUnitOut, btnFuelOut, btnSpeedBgOut, btnEngineOut}
        };
        static public Buttons btnClutchOut2 = new Buttons()
        {
            Name = "btnClutchOut2",
            Text = "",
            Style = ButtonStyles.ISB_C1,
            Height = Settings.DIGITALCLUTCHset[2],
            Width = Settings.DIGITALCLUTCHset[3],
            LeftRight = Settings.DIGITALCLUTCHset[0],
            TopDown = (byte)(Settings.DIGITALCLUTCHset[1] + 3),
            ID = 223,
        };
        static public Buttons btnEngineOut2 = new Buttons()
        {
            Name = "btnEngineOut2",
            Text = "",
            Style = ButtonStyles.ISB_C1,
            Height = Settings.DIGITALENGINEset[2],
            Width = Settings.DIGITALENGINEset[3],
            LeftRight = Settings.DIGITALENGINEset[0],
            TopDown = (byte)(Settings.DIGITALENGINEset[1] + 3),
            ID = 52,
        };
        static public Buttons btnGearOut2 = new Buttons()
        {
            Name = "btnGearOut2",
            Text = "--",
            Style = ButtonStyles.ISB_LIGHT | ButtonStyles.ISB_C1,
            Height = Settings.DIGITALGEARset[2],
            Width = Settings.DIGITALGEARset[3],
            LeftRight = Settings.DIGITALGEARset[0],
            TopDown = (byte)(Settings.DIGITALGEARset[1] + 3),
            ID = 222,
        };
        static public Buttons btnGearUpDownOut = new Buttons()
        {
            Name = "btnGearUpDownOut",
            Text = "--",
            Style = ButtonStyles.ISB_C1,
            Height = 4,
            Width = 4,
            LeftRight = (byte)(Settings.DIGITALSPEEDOleftRight + 14),
            TopDown = (byte)(Settings.DIGITALSPEEDOupDown + 3),
            ID = 221,
            Children = new List<Buttons>() { btnClutchOut2, btnGearOut2, btnFuelOut2, btnEngineOut2 }
        };
        static public Buttons btnSpeedOut = new Buttons()
        {
            Name = "btnSpeedOut",
            Text = "--",
            Style = ButtonStyles.ISB_C1 | ButtonStyles.ISB_RIGHT,
            Height = 15,
            Width = 15,
            LeftRight = Settings.DIGITALSPEEDOleftRight,
            TopDown = Settings.DIGITALSPEEDOupDown,
            ID = 220
        };
        #endregion

        #region  Siren
        static public Buttons btnSiren = new Buttons()
        {
            Name = "btnSiren",
            Text = "^1SIREN OFF",
            Style = ButtonStyles.ISB_DARK | ButtonStyles.ISB_CLICK,
            Height = Settings.SIRENset[2],
            Width = Settings.SIRENset[3],
            LeftRight = Settings.SIRENset[0],
            TopDown = Settings.SIRENset[1],
            InFocus = false,
            Clickable = true,
            ID = 219
        };
        #endregion

        #region Cop hud
        static public Buttons btnCophud2 = new Buttons()
        {
            Name = "btnCophud2",
            Text = Settings.COPHUD2NAME,
            Style = ButtonStyles.ISB_DARK | ButtonStyles.ISB_CLICK,
            Height = Settings.COPHUDset[2],
            Width = Settings.COPHUDset[3],
            LeftRight = (byte)(Settings.COPHUDset[0] + Settings.COPHUDset[3] * 1),
            TopDown = Settings.COPHUDset[1],
            InFocus = false,
            Clickable = true,
            ID = 218,
        };
        static public Buttons btnCophud3 = new Buttons()
        {
            Name = "btnCophud3",
            Text = Settings.COPHUD3NAME,
            Style = ButtonStyles.ISB_DARK | ButtonStyles.ISB_CLICK,
            Height = Settings.COPHUDset[2],
            Width = Settings.COPHUDset[3],
            LeftRight = (byte)(Settings.COPHUDset[0] + Settings.COPHUDset[3] * 2),
            TopDown = Settings.COPHUDset[1],
            InFocus = false,
            Clickable = true,
            ID = 217
        };
        static public Buttons btnCophud4 = new Buttons()
        {
            Name = "btnCophud4",
            Text = Settings.COPHUD4NAME,
            Style = ButtonStyles.ISB_DARK | ButtonStyles.ISB_CLICK,
            Height = Settings.COPHUDset[2],
            Width = Settings.COPHUDset[3],
            LeftRight = (byte)(Settings.COPHUDset[0] + Settings.COPHUDset[3] * 3),
            TopDown = Settings.COPHUDset[1],
            InFocus = false,
            Clickable = true,
            ID = 216
        };
        static public Buttons btnCophud5 = new Buttons()
        {
            Name = "btnCophud5",
            Text = Settings.COPHUD5NAME,
            Style = ButtonStyles.ISB_DARK | ButtonStyles.ISB_CLICK,
            Height = Settings.COPHUDset[2],
            Width = Settings.COPHUDset[3],
            LeftRight = (byte)(Settings.COPHUDset[0] + Settings.COPHUDset[3] * 4),
            TopDown = Settings.COPHUDset[1],
            InFocus = false,
            Clickable = true,
            ID = 215
        };
        static public Buttons btnCophud6 = new Buttons()
        {
            Name = "btnCophud6",
            Text = Settings.COPHUD6NAME,
            Style = ButtonStyles.ISB_DARK | ButtonStyles.ISB_CLICK,
            Height = Settings.COPHUDset[2],
            Width = Settings.COPHUDset[3],
            LeftRight = (byte)(Settings.COPHUDset[0] + Settings.COPHUDset[3] * 5),
            TopDown = Settings.COPHUDset[1],
            InFocus = false,
            Clickable = true,
            ID = 214
        };
        static public Buttons btnCophud7 = new Buttons()
        {
            Name = "btnCophud7",
            Text = Settings.COPHUD7NAME,
            Style = ButtonStyles.ISB_DARK | ButtonStyles.ISB_CLICK,
            Height = Settings.COPHUDset[2],
            Width = Settings.COPHUDset[3],
            LeftRight = (Settings.COPHUDset[0]),
            TopDown = (byte)(Settings.COPHUDset[1] + Settings.COPHUDset[2] * 1),
            InFocus = false,
            Clickable = true,
            ID = 213
        };
        static public Buttons btnCophud8 = new Buttons()
        {
            Name = "btnCophud8",
            Text = Settings.COPHUD8NAME,
            Style = ButtonStyles.ISB_DARK | ButtonStyles.ISB_CLICK,
            Height = Settings.COPHUDset[2],
            Width = Settings.COPHUDset[3],
            LeftRight = (byte)(Settings.COPHUDset[0] + Settings.COPHUDset[3] * 1),
            TopDown = (byte)(Settings.COPHUDset[1] + Settings.COPHUDset[2] * 1),
            InFocus = false,
            Clickable = true,
            ID = 212
        };
        static public Buttons btnCophud9 = new Buttons()
        {
            Name = "btnCophud9",
            Text = Settings.COPHUD9NAME,
            Style = ButtonStyles.ISB_DARK | ButtonStyles.ISB_CLICK,
            Height = Settings.COPHUDset[2],
            Width = Settings.COPHUDset[3],
            LeftRight = (byte)(Settings.COPHUDset[0] + Settings.COPHUDset[3] * 2),
            TopDown = (byte)(Settings.COPHUDset[1] + Settings.COPHUDset[2] * 1),
            InFocus = false,
            Clickable = true,
            ID = 211
        };
        static public Buttons btnCophud10 = new Buttons()
        {
            Name = "btnCophud10",
            Text = Settings.COPHUD10NAME,
            Style = ButtonStyles.ISB_DARK | ButtonStyles.ISB_CLICK,
            Height = Settings.COPHUDset[2],
            Width = Settings.COPHUDset[3],
            LeftRight = (byte)(Settings.COPHUDset[0] + Settings.COPHUDset[3] * 3),
            TopDown = (byte)(Settings.COPHUDset[1] + Settings.COPHUDset[2] * 1),
            InFocus = false,
            Clickable = true,
            ID = 209
        };
        static public Buttons btnCophud11 = new Buttons()
        {
            Name = "btnCophud11",
            Text = Settings.COPHUD11NAME,
            Style = ButtonStyles.ISB_DARK | ButtonStyles.ISB_CLICK,
            Height = Settings.COPHUDset[2],
            Width = Settings.COPHUDset[3],
            LeftRight = (byte)(Settings.COPHUDset[0] + Settings.COPHUDset[3] * 4),
            TopDown = (byte)(Settings.COPHUDset[1] + Settings.COPHUDset[2] * 1),
            InFocus = false,
            Clickable = true,
            ID = 208
        };
        static public Buttons btnCophud12 = new Buttons()
        {
            Name = "btnCophud12",
            Text = Settings.COPHUD12NAME,
            Style = ButtonStyles.ISB_DARK | ButtonStyles.ISB_CLICK,
            Height = Settings.COPHUDset[2],
            Width = Settings.COPHUDset[3],
            LeftRight = (byte)(Settings.COPHUDset[0] + Settings.COPHUDset[3] * 5),
            TopDown = (byte)(Settings.COPHUDset[1] + Settings.COPHUDset[2] * 1),
            InFocus = false,
            Clickable = true,
            ID = 207
        };
        static public Buttons btnCophud1 = new Buttons()
        {
            Name = "btnCophud1",
            Text = Settings.COPHUD1NAME,
            Style = ButtonStyles.ISB_DARK | ButtonStyles.ISB_CLICK,
            Height = Settings.COPHUDset[2],
            Width = Settings.COPHUDset[3],
            LeftRight = Settings.COPHUDset[0],
            TopDown = Settings.COPHUDset[1],
            InFocus = false,
            Clickable = true,
            ID = 206,
            Children = new List<Buttons>()
        };
        #endregion

        #region ResQ hud
        static public Buttons btnResqhud2 = new Buttons()
        {
            Name = "btnResqhud2",
            Text = Settings.RESQ2NAME,
            Style = ButtonStyles.ISB_DARK | ButtonStyles.ISB_CLICK,
            Height = Settings.RESQUEHUDset[2],
            Width = Settings.RESQUEHUDset[3],
            LeftRight = (byte)(Settings.RESQUEHUDset[0] + Settings.RESQUEHUDset[3] * 1),
            TopDown = Settings.RESQUEHUDset[1],
            InFocus = false,
            Clickable = true,
            ID = 205,
        };
        static public Buttons btnResqhud3 = new Buttons()
        {
            Name = "btnResqhud3",
            Text = Settings.RESQ3NAME,
            Style = ButtonStyles.ISB_DARK | ButtonStyles.ISB_CLICK,
            Height = Settings.RESQUEHUDset[2],
            Width = Settings.RESQUEHUDset[3],
            LeftRight = (byte)(Settings.RESQUEHUDset[0] + Settings.RESQUEHUDset[3] * 2),
            TopDown = Settings.RESQUEHUDset[1],
            InFocus = false,
            Clickable = true,
            ID = 204,
        };
        static public Buttons btnResqhud4 = new Buttons()
        {
            Name = "btnResqhud4",
            Text = Settings.RESQ4NAME,
            Style = ButtonStyles.ISB_DARK | ButtonStyles.ISB_CLICK,
            Height = Settings.RESQUEHUDset[2],
            Width = Settings.RESQUEHUDset[3],
            LeftRight = (byte)(Settings.RESQUEHUDset[0] + Settings.RESQUEHUDset[3] * 3),
            TopDown = Settings.RESQUEHUDset[1],
            InFocus = false,
            Clickable = true,
            ID = 203,
        };
        static public Buttons btnResqhud5 = new Buttons()
        {
            Name = "btnResqhud5",
            Text = Settings.RESQ5NAME,
            Style = ButtonStyles.ISB_DARK | ButtonStyles.ISB_CLICK,
            Height = Settings.RESQUEHUDset[2],
            Width = Settings.RESQUEHUDset[3],
            LeftRight = (byte)(Settings.RESQUEHUDset[0] + Settings.RESQUEHUDset[3] * 4),
            TopDown = Settings.RESQUEHUDset[1],
            InFocus = false,
            Clickable = true,
            ID = 202,
        };
        static public Buttons btnResqhud6 = new Buttons()
        {
            Name = "btnResqhud6",
            Text = Settings.RESQ6NAME,
            Style = ButtonStyles.ISB_DARK | ButtonStyles.ISB_CLICK,
            Height = Settings.RESQUEHUDset[2],
            Width = Settings.RESQUEHUDset[3],
            LeftRight = (byte)(Settings.RESQUEHUDset[0] + Settings.RESQUEHUDset[3] * 5),
            TopDown = Settings.RESQUEHUDset[1],
            InFocus = false,
            Clickable = true,
            ID = 201,
        };
        static public Buttons btnResqhud7 = new Buttons()
        {
            Name = "btnResqhud7",
            Text = Settings.RESQ7NAME,
            Style = ButtonStyles.ISB_DARK | ButtonStyles.ISB_CLICK,
            Height = Settings.RESQUEHUDset[2],
            Width = Settings.RESQUEHUDset[3],
            LeftRight = (byte)(Settings.RESQUEHUDset[0] + Settings.RESQUEHUDset[3] * 6),
            TopDown = Settings.RESQUEHUDset[1],
            InFocus = false,
            Clickable = true,
            ID = 200,
        };
        static public Buttons btnResqhud1 = new Buttons()
        {
            Name = "btnResqhud1",
            Text = Settings.RESQ1NAME,
            Style = ButtonStyles.ISB_DARK | ButtonStyles.ISB_CLICK,
            Height = Settings.RESQUEHUDset[2],
            Width = Settings.RESQUEHUDset[3],
            LeftRight = Settings.RESQUEHUDset[0],
            TopDown = Settings.RESQUEHUDset[1],
            InFocus = false,
            Clickable = true,
            ID = 199,
            Children = new List<Buttons>()
        };
        #endregion

        #region Extra hud
        static public Buttons btnExtrahud2 = new Buttons()
        {
            Name = "btnExtrahud2",
            Text = Settings.EXTRAHUD2NAME,
            Style = ButtonStyles.ISB_DARK | ButtonStyles.ISB_CLICK,
            Height = Settings.EXTRAHUDset[2],
            Width = Settings.EXTRAHUDset[3],
            LeftRight = (byte)(Settings.EXTRAHUDset[0] + Settings.EXTRAHUDset[3] * 1),
            TopDown = Settings.EXTRAHUDset[1],
            InFocus = false,
            Clickable = true,
            ID = 198,
        };
        static public Buttons btnExtrahud3 = new Buttons()
        {
            Name = "btnExtrahud3",
            Text = Settings.EXTRAHUD3NAME,
            Style = ButtonStyles.ISB_DARK | ButtonStyles.ISB_CLICK,
            Height = Settings.EXTRAHUDset[2],
            Width = Settings.EXTRAHUDset[3],
            LeftRight = (byte)(Settings.EXTRAHUDset[0] + Settings.EXTRAHUDset[3] * 2),
            TopDown = Settings.EXTRAHUDset[1],
            InFocus = false,
            Clickable = true,
            ID = 197,
        };
        static public Buttons btnExtrahud4 = new Buttons()
        {
            Name = "btnExtrahud4",
            Text = Settings.EXTRAHUD4NAME,
            Style = ButtonStyles.ISB_DARK | ButtonStyles.ISB_CLICK,
            Height = Settings.EXTRAHUDset[2],
            Width = Settings.EXTRAHUDset[3],
            LeftRight = (byte)(Settings.EXTRAHUDset[0] + Settings.EXTRAHUDset[3] * 3),
            TopDown = Settings.EXTRAHUDset[1],
            InFocus = false,
            Clickable = true,
            ID = 196,
        };
        static public Buttons btnExtrahud5 = new Buttons()
        {
            Name = "btnExtrahud5",
            Text = Settings.EXTRAHUD5NAME,
            Style = ButtonStyles.ISB_DARK | ButtonStyles.ISB_CLICK,
            Height = Settings.EXTRAHUDset[2],
            Width = Settings.EXTRAHUDset[3],
            LeftRight = (byte)(Settings.EXTRAHUDset[0] + Settings.EXTRAHUDset[3] * 4),
            TopDown = Settings.EXTRAHUDset[1],
            InFocus = false,
            Clickable = true,
            ID = 195,
        };
        static public Buttons btnExtrahud1 = new Buttons()
        {
            Name = "btnExtrahud1",
            Text = Settings.EXTRAHUD1NAME,
            Style = ButtonStyles.ISB_DARK | ButtonStyles.ISB_CLICK,
            Height = Settings.EXTRAHUDset[2],
            Width = Settings.EXTRAHUDset[3],
            LeftRight = Settings.EXTRAHUDset[0],
            TopDown = Settings.EXTRAHUDset[1],
            InFocus = false,
            Clickable = true,
            ID = 194,
            Children = new List<Buttons>()
        };
        #endregion

        #region AFK Warning
        static public Buttons btnAFKwarning = new Buttons()
        {
            Name = "btnAFKwarning",
            Text = "^1AFK WARNING",
            Style = ButtonStyles.ISB_DARK | ButtonStyles.ISB_CLICK,
            Height = 60,
            Width = 100,
            LeftRight = 50,
            TopDown = 60,
            ID = 62
        };
        #endregion

        #region LoadButtonHudNames
        static public void LoadButtonHudNames()
        {
            if (btnExtrahud2.Text.Length > 0) btnExtrahud1.Children.Add(btnExtrahud2);
            if (btnExtrahud3.Text.Length > 0) btnExtrahud1.Children.Add(btnExtrahud3);
            if (btnExtrahud4.Text.Length > 0) btnExtrahud1.Children.Add(btnExtrahud4);
            if (btnExtrahud5.Text.Length > 0) btnExtrahud1.Children.Add(btnExtrahud5);

            if (btnCophud2.Text.Length > 0) btnCophud1.Children.Add(btnCophud2);
            if (btnCophud3.Text.Length > 0) btnCophud1.Children.Add(btnCophud3);
            if (btnCophud4.Text.Length > 0) btnCophud1.Children.Add(btnCophud4);
            if (btnCophud5.Text.Length > 0) btnCophud1.Children.Add(btnCophud5);
            if (btnCophud6.Text.Length > 0) btnCophud1.Children.Add(btnCophud6);
            if (btnCophud7.Text.Length > 0) btnCophud1.Children.Add(btnCophud7);
            if (btnCophud8.Text.Length > 0) btnCophud1.Children.Add(btnCophud8);
            if (btnCophud9.Text.Length > 0) btnCophud1.Children.Add(btnCophud9);
            if (btnCophud10.Text.Length > 0) btnCophud1.Children.Add(btnCophud10);
            if (btnCophud11.Text.Length > 0) btnCophud1.Children.Add(btnCophud11);
            if (btnCophud12.Text.Length > 0) btnCophud1.Children.Add(btnCophud12);

            if (btnResqhud2.Text.Length > 0) btnResqhud1.Children.Add(btnResqhud2);
            if (btnResqhud3.Text.Length > 0) btnResqhud1.Children.Add(btnResqhud3);
            if (btnResqhud4.Text.Length > 0) btnResqhud1.Children.Add(btnResqhud4);
            if (btnResqhud5.Text.Length > 0) btnResqhud1.Children.Add(btnResqhud5);
            if (btnResqhud6.Text.Length > 0) btnResqhud1.Children.Add(btnResqhud6);
            if (btnResqhud7.Text.Length > 0) btnResqhud1.Children.Add(btnResqhud7);
        }
        #endregion
    }
}