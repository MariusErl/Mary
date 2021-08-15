using InSimDotNet;
using InSimDotNet.Packets;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Mary
{
    class ButtonFactory
    {
        static public List<Buttons> ButtonsUsed = new List<Buttons>();
        static public List<Buttons> ClickableUsed = new List<Buttons>();

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
                    taskkk = Task.Factory.StartNew(() => Messages.SendMsgRunScript(Settings.COPHUD1));
                    break;
                case "btnCophud2":
                    taskkk = Task.Factory.StartNew(() => Messages.SendMsgRunScript(Settings.COPHUD2));
                    break;
                case "btnCophud3":
                    taskkk = Task.Factory.StartNew(() => Messages.SendMsgRunScript(Settings.COPHUD3));
                    break;
                case "btnCophud4":
                    taskkk = Task.Factory.StartNew(() => Messages.SendMsgRunScript(Settings.COPHUD4));
                    break;
                case "btnCophud5":
                    taskkk = Task.Factory.StartNew(() => Messages.SendMsgRunScript(Settings.COPHUD5));
                    break;
                case "btnCophud6":
                    taskkk = Task.Factory.StartNew(() => Messages.SendMsgRunScript(Settings.COPHUD6));
                    break;
                case "btnCophud7":
                    taskkk = Task.Factory.StartNew(() => Messages.SendMsgRunScript(Settings.COPHUD7));
                    break;
                case "btnCophud8":
                    taskkk = Task.Factory.StartNew(() => Messages.SendMsgRunScript(Settings.COPHUD8));
                    break;
                case "btnCophud9":
                    taskkk = Task.Factory.StartNew(() => Messages.SendMsgRunScript(Settings.COPHUD9));
                    break;
                case "btnCophud10":
                    taskkk = Task.Factory.StartNew(() => Messages.SendMsgRunScript(Settings.COPHUD10));
                    break;
                case "btnCophud11":
                    taskkk = Task.Factory.StartNew(() => Messages.SendMsgRunScript(Settings.COPHUD11));
                    break;
                case "btnCophud12":
                    taskkk = Task.Factory.StartNew(() => Messages.SendMsgRunScript(Settings.COPHUD12));
                    break;
                case "btnResqhud1":
                    taskkk = Task.Factory.StartNew(() => Messages.SendMsgRunScript(Settings.RESQHUD1));
                    break;
                case "btnResqhud2":
                    taskkk = Task.Factory.StartNew(() => Messages.SendMsgRunScript(Settings.RESQHUD2));
                    break;
                case "btnResqhud3":
                    taskkk = Task.Factory.StartNew(() => Messages.SendMsgRunScript(Settings.RESQHUD3));
                    break;
                case "btnResqhud4":
                    taskkk = Task.Factory.StartNew(() => Messages.SendMsgRunScript(Settings.RESQHUD4));
                    break;
                case "btnResqhud5":
                    taskkk = Task.Factory.StartNew(() => Messages.SendMsgRunScript(Settings.RESQHUD5));
                    break;
                case "btnResqhud6":
                    taskkk = Task.Factory.StartNew(() => Messages.SendMsgRunScript(Settings.RESQHUD6));
                    break;
                case "btnResqhud7":
                    taskkk = Task.Factory.StartNew(() => Messages.SendMsgRunScript(Settings.RESQHUD7));
                    break;
                case "btnExtrahud1":
                    taskkk = Task.Factory.StartNew(() => Messages.SendMsgRunScript(Settings.EXTRAHUD1));
                    break;
                case "btnExtrahud2":
                    taskkk = Task.Factory.StartNew(() => Messages.SendMsgRunScript(Settings.EXTRAHUD2));
                    break;
                case "btnExtrahud3":
                    taskkk = Task.Factory.StartNew(() => Messages.SendMsgRunScript(Settings.EXTRAHUD3));
                    break;
                case "btnExtrahud4":
                    taskkk = Task.Factory.StartNew(() => Messages.SendMsgRunScript(Settings.EXTRAHUD4));
                    break;
                case "btnExtrahud5":
                    taskkk = Task.Factory.StartNew(() => Messages.SendMsgRunScript(Settings.EXTRAHUD5));
                    break;
                case "btnExtraHudRolePendant1":
                    if (Identification.MySelf.C.isCop)
                        taskkk = Task.Factory.StartNew(() => Messages.SendMsgRunScript(Settings.EXTRAHUDROLEPENDANTCOP1));
                    else if (Identification.MySelf.C.isResQ)
                        taskkk = Task.Factory.StartNew(() => Messages.SendMsgRunScript(Settings.EXTRAHUDROLEPENDANTTOW1));
                    else if (Identification.MySelf.C.isCiv)
                        taskkk = Task.Factory.StartNew(() => Messages.SendMsgRunScript(Settings.EXTRAHUDROLEPENDANTCIV1));
                    break;
                case "btnExtraHudRolePendant2":
                    if (Identification.MySelf.C.isCop)
                        taskkk = Task.Factory.StartNew(() => Messages.SendMsgRunScript(Settings.EXTRAHUDROLEPENDANTCOP2));
                    else if (Identification.MySelf.C.isResQ)
                        taskkk = Task.Factory.StartNew(() => Messages.SendMsgRunScript(Settings.EXTRAHUDROLEPENDANTTOW2));
                    else if (Identification.MySelf.C.isCiv)
                        taskkk = Task.Factory.StartNew(() => Messages.SendMsgRunScript(Settings.EXTRAHUDROLEPENDANTCIV2));
                    break;
                case "btnExtraHudRolePendant3":
                    if (Identification.MySelf.C.isCop)
                        taskkk = Task.Factory.StartNew(() => Messages.SendMsgRunScript(Settings.EXTRAHUDROLEPENDANTCOP3));
                    else if (Identification.MySelf.C.isResQ)
                        taskkk = Task.Factory.StartNew(() => Messages.SendMsgRunScript(Settings.EXTRAHUDROLEPENDANTTOW3));
                    else if (Identification.MySelf.C.isCiv)
                        taskkk = Task.Factory.StartNew(() => Messages.SendMsgRunScript(Settings.EXTRAHUDROLEPENDANTCIV3));
                    break;
                case "btnExtraHudRolePendant4":
                    if (Identification.MySelf.C.isCop)
                        taskkk = Task.Factory.StartNew(() => Messages.SendMsgRunScript(Settings.EXTRAHUDROLEPENDANTCOP4));
                    else if (Identification.MySelf.C.isResQ)
                        taskkk = Task.Factory.StartNew(() => Messages.SendMsgRunScript(Settings.EXTRAHUDROLEPENDANTTOW4));
                    else if (Identification.MySelf.C.isCiv)
                        taskkk = Task.Factory.StartNew(() => Messages.SendMsgRunScript(Settings.EXTRAHUDROLEPENDANTCIV4));
                    break;
                case "btnExtraHudRolePendant5":
                    if (Identification.MySelf.C.isCop)
                        taskkk = Task.Factory.StartNew(() => Messages.SendMsgRunScript(Settings.EXTRAHUDROLEPENDANTCOP5));
                    else if (Identification.MySelf.C.isResQ)
                        taskkk = Task.Factory.StartNew(() => Messages.SendMsgRunScript(Settings.EXTRAHUDROLEPENDANTTOW5));
                    else if (Identification.MySelf.C.isCiv)
                        taskkk = Task.Factory.StartNew(() => Messages.SendMsgRunScript(Settings.EXTRAHUDROLEPENDANTCIV5));
                    break;
                case "btnExtraHudRolePendant6":
                    if (Identification.MySelf.C.isCop)
                        taskkk = Task.Factory.StartNew(() => Messages.SendMsgRunScript(Settings.EXTRAHUDROLEPENDANTCOP6));
                    else if (Identification.MySelf.C.isResQ)
                        taskkk = Task.Factory.StartNew(() => Messages.SendMsgRunScript(Settings.EXTRAHUDROLEPENDANTTOW6));
                    else if (Identification.MySelf.C.isCiv)
                        taskkk = Task.Factory.StartNew(() => Messages.SendMsgRunScript(Settings.EXTRAHUDROLEPENDANTCIV6));
                    break;
                case "btnExtraHudRolePendant7":
                    if (Identification.MySelf.C.isCop)
                        taskkk = Task.Factory.StartNew(() => Messages.SendMsgRunScript(Settings.EXTRAHUDROLEPENDANTCOP7));
                    else if (Identification.MySelf.C.isResQ)
                        taskkk = Task.Factory.StartNew(() => Messages.SendMsgRunScript(Settings.EXTRAHUDROLEPENDANTTOW7));
                    else if (Identification.MySelf.C.isCiv)
                        taskkk = Task.Factory.StartNew(() => Messages.SendMsgRunScript(Settings.EXTRAHUDROLEPENDANTCIV7));
                    break;
                case "btnExtraHudRolePendant8":
                    if (Identification.MySelf.C.isCop)
                        taskkk = Task.Factory.StartNew(() => Messages.SendMsgRunScript(Settings.EXTRAHUDROLEPENDANTCOP8));
                    else if (Identification.MySelf.C.isResQ)
                        taskkk = Task.Factory.StartNew(() => Messages.SendMsgRunScript(Settings.EXTRAHUDROLEPENDANTTOW8));
                    else if (Identification.MySelf.C.isCiv)
                        taskkk = Task.Factory.StartNew(() => Messages.SendMsgRunScript(Settings.EXTRAHUDROLEPENDANTCIV8));
                    break;
                case "btnExtraHudRolePendant9":
                    if (Identification.MySelf.C.isCop)
                        taskkk = Task.Factory.StartNew(() => Messages.SendMsgRunScript(Settings.EXTRAHUDROLEPENDANTCOP9));
                    else if (Identification.MySelf.C.isResQ)
                        taskkk = Task.Factory.StartNew(() => Messages.SendMsgRunScript(Settings.EXTRAHUDROLEPENDANTTOW9));
                    else if (Identification.MySelf.C.isCiv)
                        taskkk = Task.Factory.StartNew(() => Messages.SendMsgRunScript(Settings.EXTRAHUDROLEPENDANTCIV9));
                    break;
                case "btnExtraHudRolePendant10":
                    if (Identification.MySelf.C.isCop)
                        taskkk = Task.Factory.StartNew(() => Messages.SendMsgRunScript(Settings.EXTRAHUDROLEPENDANTCOP10));
                    else if (Identification.MySelf.C.isResQ)
                        taskkk = Task.Factory.StartNew(() => Messages.SendMsgRunScript(Settings.EXTRAHUDROLEPENDANTTOW10));
                    else if (Identification.MySelf.C.isCiv)
                        taskkk = Task.Factory.StartNew(() => Messages.SendMsgRunScript(Settings.EXTRAHUDROLEPENDANTCIV10));
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
                    List<Buttons> toDelete = new List<Buttons>();
                    foreach (Buttons butt in Statistics.StatButtons.Values)
                    {
                        toDelete.Add(butt);
                    }
                    RemoveButton(panelstatbtnProgressbar);
                    RemoveButton(panelstatbtn10);
                    foreach (Buttons but in ButtonsUsed)
                    {
                        if (but.Name.Contains("panelstatbtn"))
                        {
                            toDelete.Add(but);
                        }
                    }
                    foreach (Buttons delete in toDelete)
                    { RemoveButton(delete); }
                    break;
                case "panelstatbtn10":
                    List<Buttons> toooDelete = new List<Buttons> { panelstatbtnProgressbar };
                    foreach (Buttons butt in Statistics.StatButtons.Values)
                    {
                        toooDelete.Add(butt);
                    }
                    foreach (Buttons delete in toooDelete)
                    { RemoveButton(delete); }
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
                    Identification.MySelf.P.tripDst = 0;
                    Identification.MySelf.P.topSpeed = 0;
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
        public static void BTNClicked(InSim insim, IS_BTC btc)
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
            catch (Exception e) { Program.ConsError(e.ToString()); }
        }
        #endregion

        #region PreviousBtn
        static public Buttons GetNextButton(bool down)
        {
            Buttons denne = new Buttons();

            int btnsOpen = ButtonsUsed.Count;

            if (down)
            {
                for (int x = ViewedButton + 1; x < btnsOpen; x++)
                {
                    if (ButtonsUsed[x].Clickable)
                    {
                        return ClickableUsed[x];
                    }
                }
            }

            return denne;
        }
        #endregion

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
            catch (Exception e) { Program.ConsError(e.ToString()); }
        }
        #endregion

        #region Values
        public class Buttons
        {
            public string Name, Text, PublicText;
            public byte Width, Height, LeftRight, TopDown, ID;
            public bool Clickable, InFocus;
            public ButtonStyles Style, OldStyle;
            public List<Buttons> Children = new List<Buttons>() { };
        }
        #endregion

        #region Create new id
        static public byte NewID()
        {

            byte x;
            for (x = 1; x <= 140; x++)
            {
                lock (Identification.MySelf.ButtonIDs)
                {
                    if (!Identification.MySelf.ButtonIDs.Contains(x))
                    {
                        Identification.MySelf.ButtonIDs.Add(x);
                        return x;
                    }
                }
            }
            return x;
        }
        #endregion

        #region Open button
        static public void OpenButton(Buttons motherButton)
        {
            try
            {
                var buttonPacket = new List<ISendable>();
                lock (ButtonsUsed)
                {
                    //Create list of buttons you want to open, in case the motherbutton got children.
                    List<Buttons> family = new List<Buttons> { motherButton };
                    foreach (Buttons kid in motherButton.Children)
                    {
                        family.Add(kid);
                    }

                    foreach (Buttons fullFamily in family)
                    {
                        if (fullFamily.ID == 0)
                        {
                            fullFamily.ID = NewID();
                        }

                        buttonPacket.Add(
                        new IS_BTN
                        {
                            BStyle = fullFamily.Style,
                            Text = fullFamily.Text,
                            H = fullFamily.Height,
                            W = fullFamily.Width,
                            L = fullFamily.LeftRight,
                            T = fullFamily.TopDown,
                            UCID = 0,
                            ClickID = fullFamily.ID,
                            ReqI = fullFamily.ID
                        });

                        if (!ButtonsUsed.Contains(fullFamily))
                        {
                            ButtonsUsed.Add(fullFamily);
                            if (fullFamily.Clickable && Program.buttonsState > 2)
                            {
                                ClickableUsed.Add(fullFamily);
                            }
                        }
                    }
                    Program.insim.Send(buttonPacket.ToArray());
                }
            }
            catch (Exception e) { Program.ConsError(e.ToString()); }
        }
        #endregion

        #region Remove button
        static public void RemoveButton(Buttons motherButton)
        {
            try
            {
                lock (ButtonsUsed)
                {
                    List<Buttons> family = new List<Buttons> { motherButton };
                    foreach (Buttons kid in motherButton.Children)
                    {
                        family.Add(kid);
                    }

                    foreach (Buttons fullFamily in family)
                    {
                        //if (ViewedButton != -1 && ClickableUsed[ViewedButton].Name == fullFamily.Name)
                        //{
                        //    ClickableUsed[ViewedButton].Style = ClickableUsed[ViewedButton].OldStyle;
                        //    ViewedButton = -1;
                        //}

                        Program.insim.Send(new IS_BFN
                        {
                            SubT = ButtonFunction.BFN_DEL_BTN,
                            UCID = 0,
                            ClickID = fullFamily.ID
                        });

                        ButtonsUsed.Remove(fullFamily);
                        Identification.MySelf.ButtonIDs.Remove(fullFamily.ID);
                        if (fullFamily.Clickable && Program.buttonsState != 3 && ViewedButton != -1)
                        {
                            if (ClickableUsed[ViewedButton] == fullFamily)
                            {
                                ClickableUsed[ViewedButton].Style = ButtonStyles.ISB_DARK | ButtonStyles.ISB_CLICK;
                                ViewedButton = -1;
                            }
                            ClickableUsed.Remove(fullFamily);
                        }
                    }
                }
            }
            catch (Exception e) { Program.ConsError(e.ToString()); }
        }
        #endregion

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
            ID = 196
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
            ID = 195
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
            ID = 194
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
            ID = 193
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
            ID = 192
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
            ID = 191
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
            ID = 190
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
            ID = 189,
            Children = new List<Buttons>() { tripInfo, tripm1, tripm2, tripm3, tripm4, tripmReset, tripmPause }
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
            ID = 167
        };
        static public Buttons panelstatbtn2 = new Buttons()
        {
            Name = "panelstatbtn2",
            Text = "",
            Style = ButtonStyles.ISB_DARK,
            Width = 150,
            LeftRight = 25,
            TopDown = 40,
            ID = 166
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
            ID = 165
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
            ID = 164
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
            ID = 163
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
            ID = 162
        };
        static public Buttons panelstatbtn7 = new Buttons()
        {
            Name = "panelstatbtn7",
            Text = "[Km] - [Money]",
            Style = ButtonStyles.ISB_C2 | ButtonStyles.ISB_C4 | ButtonStyles.ISB_LEFT,
            Height = 5,
            Width = 40,
            LeftRight = 76,
            TopDown = 50,
            ID = 161
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
            ID = 160
        };
        static public Buttons panelstatbtn9 = new Buttons()
        {
            Name = "panelstatbtn9",
            Text = "Standstill time",
            Style = ButtonStyles.ISB_C2 | ButtonStyles.ISB_C4 | ButtonStyles.ISB_RIGHT,
            Height = 5,
            Width = 24,
            LeftRight = 150,
            TopDown = 50,
            ID = 159,
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
            ID = 158
        };
        static public Buttons panelstatbtnProgressbar = new Buttons()
        {
            Name = "panelstatbtnProgressbar",
            Style = ButtonStyles.ISB_LIGHT,
            Height = 5,
            Width = 15,
            LeftRight = 50,
            TopDown = 42,
            ID = 157
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
            ID = 205
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
            ID = 203
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
            ID = 202
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
            ID = 201
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
            ID = 200
        };
        static public Buttons btnTracker6 = new Buttons()
        {
            Name = "btnTracker6",
            Text = "",
            Style = ButtonStyles.ISB_DARK | ButtonStyles.ISB_C1 | ButtonStyles.ISB_C4,
            Height = 5,
            Width = 20,
            LeftRight = (byte)(Settings.COPTRACKERMAINleftRight - 3),
            TopDown = (byte)(Settings.COPTRACKERMAINupDown + 15),
            InFocus = false,
            Clickable = false,
            ID = 199
        };
        static public Buttons MinimapTrackerSusp = new Buttons()
        {
            Name = "MinimapTrackerSusp",
            Text = "",
            Style = ButtonStyles.ISB_C1,
            Height = 5,
            Width = 5,
            LeftRight = (byte)DefaultMinimap.ReturnPresetMinimapValues()[0],
            TopDown = (byte)DefaultMinimap.ReturnPresetMinimapValues()[1],
            ID = 198
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
            ID = 197,
            Children = new List<Buttons>() { btnTracker1, btnTracker3, btnTracker4, btnTracker5,/* btnTracker6,*/ MinimapTrackerSusp }
        };
        #endregion

        #region Minimaptracking suspect
        static public Buttons MinimapTrackerCop2 = new Buttons()
        {
            Name = "MinimapTrackerCop2",
            Text = "",
            Style = ButtonStyles.ISB_C1,
            Height = 5,
            Width = 5,
            LeftRight = (byte)DefaultMinimap.ReturnPresetMinimapValues()[0],
            TopDown = (byte)DefaultMinimap.ReturnPresetMinimapValues()[1],
            ID = 181
        };
        static public Buttons MinimapTrackerCop3 = new Buttons()
        {
            Name = "MinimapTrackerCop3",
            Text = "",
            Style = ButtonStyles.ISB_C1,
            Height = 5,
            Width = 5,
            LeftRight = (byte)DefaultMinimap.ReturnPresetMinimapValues()[0],
            TopDown = (byte)DefaultMinimap.ReturnPresetMinimapValues()[1],
            ID = 180
        };
        static public Buttons MinimapTrackerCop4 = new Buttons()
        {
            Name = "MinimapTrackerCop4",
            Text = "",
            Style = ButtonStyles.ISB_C1,
            Height = 5,
            Width = 5,
            LeftRight = (byte)DefaultMinimap.ReturnPresetMinimapValues()[0],
            TopDown = (byte)DefaultMinimap.ReturnPresetMinimapValues()[1],
            ID = 179
        };
        static public Buttons MinimapTrackerCop1 = new Buttons()
        {
            Name = "MinimapTrackerCop1",
            Text = "",
            Style = ButtonStyles.ISB_C1,
            Height = 5,
            Width = 5,
            LeftRight = (byte)DefaultMinimap.ReturnPresetMinimapValues()[0],
            TopDown = (byte)DefaultMinimap.ReturnPresetMinimapValues()[1],
            InFocus = false,
            Clickable = false,
            ID = 178,
            Children = new List<Buttons>() { MinimapTrackerCop2, MinimapTrackerCop3, MinimapTrackerCop4 }
        };
        static public Buttons MinimapTrackerCop1Info = new Buttons()
        {
            Name = "MinimapTrackerCop1Info",
            Text = "--------------------------",
            Style = ButtonStyles.ISB_DARK,
            Height = 4,
            Width = 20,
            LeftRight = Settings.COPBARS[0],
            TopDown = Settings.COPBARS[1],
            ID = 177
        };
        static public Buttons MinimapTrackerCop1PBar = new Buttons()
        {
            Name = "MinimapTrackerCop1PBar",
            Text = "--------------------------",
            Style = ButtonStyles.ISB_DARK | ButtonStyles.ISB_C1 | ButtonStyles.ISB_C4,
            Height = 4,
            Width = 20,
            LeftRight = (byte)(Settings.COPBARS[0] + 20),
            TopDown = Settings.COPBARS[1],
            ID = 173,
            Children = new List<Buttons>() { MinimapTrackerCop1Info }
        };
        static public Buttons MinimapTrackerCop2Info = new Buttons()
        {
            Name = "MinimapTrackerCop2Info",
            Text = "--------------------------",
            Style = ButtonStyles.ISB_DARK,
            Height = 4,
            Width = 20,
            LeftRight = Settings.COPBARS[0],
            TopDown = (byte)(Settings.COPBARS[1] + 4),
            ID = 176
        };
        static public Buttons MinimapTrackerCop2PBar = new Buttons()
        {
            Name = "MinimapTrackerCop2PBar",
            Text = "--------------------------",
            Style = ButtonStyles.ISB_DARK | ButtonStyles.ISB_C1 | ButtonStyles.ISB_C4,
            Height = 4,
            Width = 20,
            LeftRight = (byte)(Settings.COPBARS[0] + 20),
            TopDown = (byte)(Settings.COPBARS[1] + 4),
            ID = 172,
            Children = new List<Buttons>() { MinimapTrackerCop2Info }
        };
        static public Buttons MinimapTrackerCop3Info = new Buttons()
        {
            Name = "MinimapTrackerCop3Info",
            Text = "--------------------------",
            Style = ButtonStyles.ISB_DARK,
            Height = 4,
            Width = 20,
            LeftRight = Settings.COPBARS[0],
            TopDown = (byte)(Settings.COPBARS[1] + 8),
            ID = 175
        };
        static public Buttons MinimapTrackerCop3PBar = new Buttons()
        {
            Name = "MinimapTrackerCop3PBar",
            Text = "--------------------------",
            Style = ButtonStyles.ISB_DARK | ButtonStyles.ISB_C1 | ButtonStyles.ISB_C4,
            Height = 4,
            Width = 20,
            LeftRight = (byte)(Settings.COPBARS[0] + 20),
            TopDown = (byte)(Settings.COPBARS[1] + 8),
            ID = 171,
            Children = new List<Buttons>() { MinimapTrackerCop3Info }
        };
        static public Buttons MinimapTrackerCop4Info = new Buttons()
        {
            Name = "MinimapTrackerCop4Info",
            Text = "--------------------------",
            Style = ButtonStyles.ISB_DARK,
            Height = 4,
            Width = 20,
            LeftRight = Settings.COPBARS[0],
            TopDown = (byte)(Settings.COPBARS[1] + 12),
            ID = 174
        };
        static public Buttons MinimapTrackerCop4PBar = new Buttons()
        {
            Name = "MinimapTrackerCop4PBar",
            Text = "--------------------------",
            Style = ButtonStyles.ISB_DARK | ButtonStyles.ISB_C1 | ButtonStyles.ISB_C4,
            Height = 4,
            Width = 20,
            LeftRight = (byte)(Settings.COPBARS[0] + 20),
            TopDown = (byte)(Settings.COPBARS[1] + 12),
            ID = 170,
            Children = new List<Buttons>() { MinimapTrackerCop4Info }
        };
        #endregion

        #region Gadgets
        static public Buttons btnRAMfree1 = new Buttons()
        {
            Name = "btnRAMfree1",
            Text = "^" + Settings.BUTTONCOLOURPREF + "Free RAM:",
            Style = ButtonStyles.ISB_DARK,
            Height = 3,
            Width = Settings.RAMset[3],
            LeftRight = Settings.RAMset[0],
            TopDown = Settings.RAMset[1],
            ID = 238
        };
        static public Buttons btnCPUusage1 = new Buttons()
        {
            Name = "btnCPUusage1",
            Text = "^" + Settings.BUTTONCOLOURPREF + "CPU usage:",
            Style = ButtonStyles.ISB_DARK,
            Height = 3,
            Width = Settings.CPUset[3],
            LeftRight = Settings.CPUset[0],
            TopDown = Settings.CPUset[1],
            ID = 237,
            Children = new List<Buttons>() { btnRAMfree1 }
        };
        static public Buttons btnRAMfree2 = new Buttons()
        {
            Name = "btnRAMfree2",
            Text = "^" + Settings.BUTTONCOLOURPREF + "--",
            Style = ButtonStyles.ISB_LIGHT,
            Height = Settings.RAMset[2],
            Width = Settings.RAMset[3],
            LeftRight = Settings.RAMset[0],
            TopDown = (byte)(Settings.RAMset[1] + 3),
            ID = 236
        };
        static public Buttons btnCPUusage2 = new Buttons()
        {
            Name = "btnCPUusage2",
            Text = "^" + Settings.BUTTONCOLOURPREF + "--",
            Style = ButtonStyles.ISB_LIGHT,
            Height = Settings.CPUset[2],
            Width = Settings.CPUset[3],
            LeftRight = Settings.CPUset[0],
            ID = 235,
            TopDown = (byte)(Settings.CPUset[1] + 3),
            Children = new List<Buttons>() { btnRAMfree2 }
        };
        static public Buttons btnGearOut = new Buttons()
        {
            Name = "btnGearOut",
            Text = "^" + Settings.BUTTONCOLOURPREF + "Gear",
            Style = ButtonStyles.ISB_DARK,
            Height = 3,
            Width = Settings.DIGITALGEARset[3],
            LeftRight = Settings.DIGITALGEARset[0],
            TopDown = Settings.DIGITALGEARset[1],
            ID = 234
        };
        static public Buttons btnUnitOut = new Buttons()
        {
            Name = "btnUnitOut",
            Text = "^" + Settings.BUTTONCOLOURPREF + ((Settings.UNITKPH) ? "km/h" : "mph"),
            Style = ButtonStyles.ISB_RIGHT,
            Height = 5,
            Width = 8,
            LeftRight = (byte)(Settings.DIGITALSPEEDOleftRight + 12),
            TopDown = (byte)(Settings.DIGITALSPEEDOupDown + 7),
            ID = 233
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
            ID = 232
        };
        static public Buttons btnFuelOut2 = new Buttons()
        {
            Name = "btnFuelOut2",
            Text = "^" + Settings.BUTTONCOLOURPREF + "--",
            Style = ButtonStyles.ISB_LIGHT,
            Height = Settings.DIGITALFUELset[2],
            Width = Settings.DIGITALFUELset[3],
            LeftRight = Settings.DIGITALFUELset[0],
            TopDown = (byte)(Settings.DIGITALFUELset[1] + 3),
            ID = 231
        };
        static public Buttons btnFuelOut = new Buttons()
        {
            Name = "btnFuelOut",
            Text = "^" + Settings.BUTTONCOLOURPREF + "Fuel",
            Style = ButtonStyles.ISB_DARK,
            Height = 3,
            Width = Settings.DIGITALFUELset[3],
            LeftRight = Settings.DIGITALFUELset[0],
            TopDown = Settings.DIGITALFUELset[1],
            ID = 230
        };
        static public Buttons btnEngineOut = new Buttons()
        {
            Name = "btnEngineOut",
            Text = "^" + Settings.BUTTONCOLOURPREF + "Engine damage",
            Style = ButtonStyles.ISB_DARK,
            Height = 3,
            Width = Settings.DIGITALENGINEset[3],
            LeftRight = Settings.DIGITALENGINEset[0],
            TopDown = Settings.DIGITALENGINEset[1],
            ID = 229
        };
        static public Buttons btnRPMOut = new Buttons()
        {
            Name = "btnRPMOut",
            Text = "^" + Settings.BUTTONCOLOURPREF + "RPM",
            Style = ButtonStyles.ISB_DARK,
            Height = 3,
            Width = Settings.DIGITALRPMset[3],
            LeftRight = Settings.DIGITALRPMset[0],
            TopDown = Settings.DIGITALRPMset[1],
            ID = 168
        };
        static public Buttons btnClutchOut = new Buttons()
        {
            Name = "btnClutchOut",
            Text = "^" + Settings.BUTTONCOLOURPREF + "Clutch damage",
            Style = ButtonStyles.ISB_DARK,
            Height = 3,
            Width = Settings.DIGITALCLUTCHset[3],
            LeftRight = Settings.DIGITALCLUTCHset[0],
            TopDown = Settings.DIGITALCLUTCHset[1],
            ID = 228,
            Children = new List<Buttons>() { btnGearOut, btnUnitOut, btnFuelOut, btnSpeedBgOut, btnEngineOut, btnRPMOut }
        };
        static public Buttons btnClutchOut2 = new Buttons()
        {
            Name = "btnClutchOut2",
            Text = "^" + Settings.BUTTONCOLOURPREF + "--",
            Style = ButtonStyles.ISB_LIGHT,
            Height = Settings.DIGITALCLUTCHset[2],
            Width = Settings.DIGITALCLUTCHset[3],
            LeftRight = Settings.DIGITALCLUTCHset[0],
            TopDown = (byte)(Settings.DIGITALCLUTCHset[1] + 3),
            ID = 227
        };
        static public Buttons btnEngineOut2 = new Buttons()
        {
            Name = "btnEngineOut2",
            Text = "^" + Settings.BUTTONCOLOURPREF + "--",
            Style = ButtonStyles.ISB_LIGHT,
            Height = Settings.DIGITALENGINEset[2],
            Width = Settings.DIGITALENGINEset[3],
            LeftRight = Settings.DIGITALENGINEset[0],
            TopDown = (byte)(Settings.DIGITALENGINEset[1] + 3),
            ID = 226
        };
        static public Buttons btnGearOut2 = new Buttons()
        {
            Name = "btnGearOut2",
            Text = "^" + Settings.BUTTONCOLOURPREF + "--",
            Style = ButtonStyles.ISB_LIGHT,
            Height = Settings.DIGITALGEARset[2],
            Width = Settings.DIGITALGEARset[3],
            LeftRight = Settings.DIGITALGEARset[0],
            TopDown = (byte)(Settings.DIGITALGEARset[1] + 3),
            ID = 225
        };
        static public Buttons btnRPMOut2 = new Buttons()
        {
            Name = "btnRPMOut2",
            Text = "^" + Settings.BUTTONCOLOURPREF + "--",
            Style = ButtonStyles.ISB_LIGHT,
            Height = Settings.DIGITALRPMset[2],
            Width = Settings.DIGITALRPMset[3],
            LeftRight = Settings.DIGITALRPMset[0],
            TopDown = (byte)(Settings.DIGITALRPMset[1] + 3),
            ID = 169
        };
        static public Buttons btnGearUpDownOut = new Buttons()
        {
            Name = "btnGearUpDownOut",
            Text = "^" + Settings.BUTTONCOLOURPREF + "--",
            Style = ButtonStyles.ISB_C1,
            Height = 4,
            Width = 4,
            LeftRight = (byte)(Settings.DIGITALSPEEDOleftRight + 14),
            TopDown = (byte)(Settings.DIGITALSPEEDOupDown + 3),
            ID = 224,
            Children = new List<Buttons>() { btnClutchOut2, btnGearOut2, btnFuelOut2, btnEngineOut2 }
        };
        static public Buttons btnSpeedOut = new Buttons()
        {
            Name = "btnSpeedOut",
            Text = "^" + Settings.BUTTONCOLOURPREF + "--",
            Style = ButtonStyles.ISB_RIGHT,
            Height = 15,
            Width = 15,
            LeftRight = Settings.DIGITALSPEEDOleftRight,
            TopDown = Settings.DIGITALSPEEDOupDown,
            ID = 239,
            Children = new List<Buttons>() { btnRPMOut2 }
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
            ID = 204
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
            ID = 223
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
            ID = 222
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
            ID = 221
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
            ID = 220
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
            ID = 219
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
            ID = 218
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
            ID = 217
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
            ID = 216
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
            ID = 215
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
            ID = 214
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
            ID = 213
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
            ID = 212,
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
            ID = 188
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
            ID = 187
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
            ID = 186
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
            ID = 185
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
            ID = 184
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
            ID = 183
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
            ID = 182,
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
            ID = 211
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
            ID = 210
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
            ID = 209
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
            ID = 208
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
            ID = 207,
            Children = new List<Buttons>()
        };
        #endregion

        #region Extra hud Rolependant
        static public Buttons btnExtraHudRolePendant2 = new Buttons()
        {
            Name = "btnExtraHudRolePendant2",
            Text = Settings.EXTRAHUDROLEPENDANTCOP2NAME,
            Style = ButtonStyles.ISB_DARK | ButtonStyles.ISB_CLICK,
            Height = Settings.EXTRAHUDROLEPENDANTset[2],
            Width = Settings.EXTRAHUDROLEPENDANTset[3],
            LeftRight = (byte)(Settings.EXTRAHUDROLEPENDANTset[0] + Settings.EXTRAHUDROLEPENDANTset[3] * 1),
            TopDown = Settings.EXTRAHUDROLEPENDANTset[1],
            InFocus = false,
            Clickable = true,
            ID = 156
        };
        static public Buttons btnExtraHudRolePendant3 = new Buttons()
        {
            Name = "btnExtraHudRolePendant3",
            Text = Settings.EXTRAHUDROLEPENDANTCOP3NAME,
            Style = ButtonStyles.ISB_DARK | ButtonStyles.ISB_CLICK,
            Height = Settings.EXTRAHUDROLEPENDANTset[2],
            Width = Settings.EXTRAHUDROLEPENDANTset[3],
            LeftRight = (byte)(Settings.EXTRAHUDROLEPENDANTset[0] + Settings.EXTRAHUDROLEPENDANTset[3] * 2),
            TopDown = Settings.EXTRAHUDROLEPENDANTset[1],
            InFocus = false,
            Clickable = true,
            ID = 155
        };
        static public Buttons btnExtraHudRolePendant4 = new Buttons()
        {
            Name = "btnExtraHudRolePendant4",
            Text = Settings.EXTRAHUDROLEPENDANTCOP4NAME,
            Style = ButtonStyles.ISB_DARK | ButtonStyles.ISB_CLICK,
            Height = Settings.EXTRAHUDROLEPENDANTset[2],
            Width = Settings.EXTRAHUDROLEPENDANTset[3],
            LeftRight = (byte)(Settings.EXTRAHUDROLEPENDANTset[0] + Settings.EXTRAHUDROLEPENDANTset[3] * 3),
            TopDown = Settings.EXTRAHUDROLEPENDANTset[1],
            InFocus = false,
            Clickable = true,
            ID = 154
        };
        static public Buttons btnExtraHudRolePendant5 = new Buttons()
        {
            Name = "btnExtraHudRolePendant5",
            Text = Settings.EXTRAHUDROLEPENDANTCOP5NAME,
            Style = ButtonStyles.ISB_DARK | ButtonStyles.ISB_CLICK,
            Height = Settings.EXTRAHUDROLEPENDANTset[2],
            Width = Settings.EXTRAHUDROLEPENDANTset[3],
            LeftRight = (byte)(Settings.EXTRAHUDROLEPENDANTset[0] + Settings.EXTRAHUDROLEPENDANTset[3] * 4),
            TopDown = Settings.EXTRAHUDROLEPENDANTset[1],
            InFocus = false,
            Clickable = true,
            ID = 153
        };
        static public Buttons btnExtraHudRolePendant6 = new Buttons()
        {
            Name = "btnExtraHudRolePendant6",
            Text = Settings.EXTRAHUDROLEPENDANTCOP6NAME,
            Style = ButtonStyles.ISB_DARK | ButtonStyles.ISB_CLICK,
            Height = Settings.EXTRAHUDROLEPENDANTset[2],
            Width = Settings.EXTRAHUDROLEPENDANTset[3],
            LeftRight = (byte)(Settings.EXTRAHUDROLEPENDANTset[0]),
            TopDown = (byte)(Settings.EXTRAHUDROLEPENDANTset[1] + Settings.EXTRAHUDROLEPENDANTset[2]),
            InFocus = false,
            Clickable = true,
            ID = 152
        };
        static public Buttons btnExtraHudRolePendant7 = new Buttons()
        {
            Name = "btnExtraHudRolePendant7",
            Text = Settings.EXTRAHUDROLEPENDANTCOP7NAME,
            Style = ButtonStyles.ISB_DARK | ButtonStyles.ISB_CLICK,
            Height = Settings.EXTRAHUDROLEPENDANTset[2],
            Width = Settings.EXTRAHUDROLEPENDANTset[3],
            LeftRight = (byte)(Settings.EXTRAHUDROLEPENDANTset[0] + Settings.EXTRAHUDROLEPENDANTset[3] * 1),
            TopDown = (byte)(Settings.EXTRAHUDROLEPENDANTset[1] + Settings.EXTRAHUDROLEPENDANTset[2]),
            InFocus = false,
            Clickable = true,
            ID = 151
        };
        static public Buttons btnExtraHudRolePendant8 = new Buttons()
        {
            Name = "btnExtraHudRolePendant8",
            Text = Settings.EXTRAHUDROLEPENDANTCOP8NAME,
            Style = ButtonStyles.ISB_DARK | ButtonStyles.ISB_CLICK,
            Height = Settings.EXTRAHUDROLEPENDANTset[2],
            Width = Settings.EXTRAHUDROLEPENDANTset[3],
            LeftRight = (byte)(Settings.EXTRAHUDROLEPENDANTset[0] + Settings.EXTRAHUDROLEPENDANTset[3] * 2),
            TopDown = (byte)(Settings.EXTRAHUDROLEPENDANTset[1] + Settings.EXTRAHUDROLEPENDANTset[2]),
            InFocus = false,
            Clickable = true,
            ID = 150
        };
        static public Buttons btnExtraHudRolePendant9 = new Buttons()
        {
            Name = "btnExtraHudRolePendant9",
            Text = Settings.EXTRAHUDROLEPENDANTCOP9NAME,
            Style = ButtonStyles.ISB_DARK | ButtonStyles.ISB_CLICK,
            Height = Settings.EXTRAHUDROLEPENDANTset[2],
            Width = Settings.EXTRAHUDROLEPENDANTset[3],
            LeftRight = (byte)(Settings.EXTRAHUDROLEPENDANTset[0] + Settings.EXTRAHUDROLEPENDANTset[3] * 3),
            TopDown = (byte)(Settings.EXTRAHUDROLEPENDANTset[1] + Settings.EXTRAHUDROLEPENDANTset[2]),
            InFocus = false,
            Clickable = true,
            ID = 149
        };
        static public Buttons btnExtraHudRolePendant10 = new Buttons()
        {
            Name = "btnExtraHudRolePendant10",
            Text = Settings.EXTRAHUDROLEPENDANTCOP10NAME,
            Style = ButtonStyles.ISB_DARK | ButtonStyles.ISB_CLICK,
            Height = Settings.EXTRAHUDROLEPENDANTset[2],
            Width = Settings.EXTRAHUDROLEPENDANTset[3],
            LeftRight = (byte)(Settings.EXTRAHUDROLEPENDANTset[0] + Settings.EXTRAHUDROLEPENDANTset[3] * 4),
            TopDown = (byte)(Settings.EXTRAHUDROLEPENDANTset[1] + Settings.EXTRAHUDROLEPENDANTset[2]),
            InFocus = false,
            Clickable = true,
            ID = 148
        };
        static public Buttons btnExtraHudRolePendant1 = new Buttons()
        {
            Name = "btnExtraHudRolePendant1",
            Text = Settings.EXTRAHUDROLEPENDANTCOP1NAME,
            Style = ButtonStyles.ISB_DARK | ButtonStyles.ISB_CLICK,
            Height = Settings.EXTRAHUDROLEPENDANTset[2],
            Width = Settings.EXTRAHUDROLEPENDANTset[3],
            LeftRight = (byte)(Settings.EXTRAHUDROLEPENDANTset[0] + Settings.EXTRAHUDROLEPENDANTset[3] * 0),
            TopDown = Settings.EXTRAHUDROLEPENDANTset[1],
            InFocus = false,
            Clickable = true,
            ID = 157,
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
            ID = 206
        };
        #endregion

        #region LoadButtonHudNames
        static public void LoadButtonHudNames()
        {
            if (btnExtrahud2.Text.Length > 0) btnExtrahud1.Children.Add(btnExtrahud2);
            if (btnExtrahud3.Text.Length > 0) btnExtrahud1.Children.Add(btnExtrahud3);
            if (btnExtrahud4.Text.Length > 0) btnExtrahud1.Children.Add(btnExtrahud4);
            if (btnExtrahud5.Text.Length > 0) btnExtrahud1.Children.Add(btnExtrahud5);
            btnExtraHudRolePendant1.Children.Clear();
            if (Identification.MySelf.C.isCop)
            {
                btnExtraHudRolePendant1.Text = Settings.EXTRAHUDROLEPENDANTCOP1NAME;
                btnExtraHudRolePendant2.Text = Settings.EXTRAHUDROLEPENDANTCOP2NAME;
                btnExtraHudRolePendant3.Text = Settings.EXTRAHUDROLEPENDANTCOP3NAME;
                btnExtraHudRolePendant4.Text = Settings.EXTRAHUDROLEPENDANTCOP4NAME;
                btnExtraHudRolePendant5.Text = Settings.EXTRAHUDROLEPENDANTCOP5NAME;
                btnExtraHudRolePendant6.Text = Settings.EXTRAHUDROLEPENDANTCOP6NAME;
                btnExtraHudRolePendant7.Text = Settings.EXTRAHUDROLEPENDANTCOP7NAME;
                btnExtraHudRolePendant8.Text = Settings.EXTRAHUDROLEPENDANTCOP8NAME;
                btnExtraHudRolePendant9.Text = Settings.EXTRAHUDROLEPENDANTCOP9NAME;
                btnExtraHudRolePendant10.Text = Settings.EXTRAHUDROLEPENDANTCOP10NAME;
                if (Settings.EXTRAHUDROLEPENDANTCOP2NAME.Length > 0) btnExtraHudRolePendant1.Children.Add(btnExtraHudRolePendant2);
                if (Settings.EXTRAHUDROLEPENDANTCOP3NAME.Length > 0) btnExtraHudRolePendant1.Children.Add(btnExtraHudRolePendant3);
                if (Settings.EXTRAHUDROLEPENDANTCOP4NAME.Length > 0) btnExtraHudRolePendant1.Children.Add(btnExtraHudRolePendant4);
                if (Settings.EXTRAHUDROLEPENDANTCOP5NAME.Length > 0) btnExtraHudRolePendant1.Children.Add(btnExtraHudRolePendant5);
                if (Settings.EXTRAHUDROLEPENDANTCOP6NAME.Length > 0) btnExtraHudRolePendant1.Children.Add(btnExtraHudRolePendant6);
                if (Settings.EXTRAHUDROLEPENDANTCOP7NAME.Length > 0) btnExtraHudRolePendant1.Children.Add(btnExtraHudRolePendant7);
                if (Settings.EXTRAHUDROLEPENDANTCOP8NAME.Length > 0) btnExtraHudRolePendant1.Children.Add(btnExtraHudRolePendant8);
                if (Settings.EXTRAHUDROLEPENDANTCOP9NAME.Length > 0) btnExtraHudRolePendant1.Children.Add(btnExtraHudRolePendant9);
                if (Settings.EXTRAHUDROLEPENDANTCOP10NAME.Length > 0) btnExtraHudRolePendant1.Children.Add(btnExtraHudRolePendant10);
            }
            else if (Identification.MySelf.C.isResQ)
            {
                btnExtraHudRolePendant1.Text = Settings.EXTRAHUDROLEPENDANTTOW1NAME;
                btnExtraHudRolePendant2.Text = Settings.EXTRAHUDROLEPENDANTTOW2NAME;
                btnExtraHudRolePendant3.Text = Settings.EXTRAHUDROLEPENDANTTOW3NAME;
                btnExtraHudRolePendant4.Text = Settings.EXTRAHUDROLEPENDANTTOW4NAME;
                btnExtraHudRolePendant5.Text = Settings.EXTRAHUDROLEPENDANTTOW5NAME;
                btnExtraHudRolePendant6.Text = Settings.EXTRAHUDROLEPENDANTTOW6NAME;
                btnExtraHudRolePendant7.Text = Settings.EXTRAHUDROLEPENDANTTOW7NAME;
                btnExtraHudRolePendant8.Text = Settings.EXTRAHUDROLEPENDANTTOW8NAME;
                btnExtraHudRolePendant9.Text = Settings.EXTRAHUDROLEPENDANTTOW9NAME;
                btnExtraHudRolePendant10.Text = Settings.EXTRAHUDROLEPENDANTTOW10NAME;
                if (Settings.EXTRAHUDROLEPENDANTTOW2NAME.Length > 0) btnExtraHudRolePendant1.Children.Add(btnExtraHudRolePendant2);
                if (Settings.EXTRAHUDROLEPENDANTTOW3NAME.Length > 0) btnExtraHudRolePendant1.Children.Add(btnExtraHudRolePendant3);
                if (Settings.EXTRAHUDROLEPENDANTTOW4NAME.Length > 0) btnExtraHudRolePendant1.Children.Add(btnExtraHudRolePendant4);
                if (Settings.EXTRAHUDROLEPENDANTTOW5NAME.Length > 0) btnExtraHudRolePendant1.Children.Add(btnExtraHudRolePendant5);
                if (Settings.EXTRAHUDROLEPENDANTTOW6NAME.Length > 0) btnExtraHudRolePendant1.Children.Add(btnExtraHudRolePendant6);
                if (Settings.EXTRAHUDROLEPENDANTTOW7NAME.Length > 0) btnExtraHudRolePendant1.Children.Add(btnExtraHudRolePendant7);
                if (Settings.EXTRAHUDROLEPENDANTTOW8NAME.Length > 0) btnExtraHudRolePendant1.Children.Add(btnExtraHudRolePendant8);
                if (Settings.EXTRAHUDROLEPENDANTTOW9NAME.Length > 0) btnExtraHudRolePendant1.Children.Add(btnExtraHudRolePendant9);
                if (Settings.EXTRAHUDROLEPENDANTTOW10NAME.Length > 0) btnExtraHudRolePendant1.Children.Add(btnExtraHudRolePendant10);
            }
            else if (Identification.MySelf.C.isCiv)
            {
                btnExtraHudRolePendant1.Text = Settings.EXTRAHUDROLEPENDANTCIV1NAME;
                btnExtraHudRolePendant2.Text = Settings.EXTRAHUDROLEPENDANTCIV2NAME;
                btnExtraHudRolePendant3.Text = Settings.EXTRAHUDROLEPENDANTCIV3NAME;
                btnExtraHudRolePendant4.Text = Settings.EXTRAHUDROLEPENDANTCIV4NAME;
                btnExtraHudRolePendant5.Text = Settings.EXTRAHUDROLEPENDANTCIV5NAME;
                btnExtraHudRolePendant6.Text = Settings.EXTRAHUDROLEPENDANTCIV6NAME;
                btnExtraHudRolePendant7.Text = Settings.EXTRAHUDROLEPENDANTCIV7NAME;
                btnExtraHudRolePendant8.Text = Settings.EXTRAHUDROLEPENDANTCIV8NAME;
                btnExtraHudRolePendant9.Text = Settings.EXTRAHUDROLEPENDANTCIV9NAME;
                btnExtraHudRolePendant10.Text = Settings.EXTRAHUDROLEPENDANTCIV10NAME;
                if (Settings.EXTRAHUDROLEPENDANTCIV2NAME.Length > 0) btnExtraHudRolePendant1.Children.Add(btnExtraHudRolePendant2);
                if (Settings.EXTRAHUDROLEPENDANTCIV3NAME.Length > 0) btnExtraHudRolePendant1.Children.Add(btnExtraHudRolePendant3);
                if (Settings.EXTRAHUDROLEPENDANTCIV4NAME.Length > 0) btnExtraHudRolePendant1.Children.Add(btnExtraHudRolePendant4);
                if (Settings.EXTRAHUDROLEPENDANTCIV5NAME.Length > 0) btnExtraHudRolePendant1.Children.Add(btnExtraHudRolePendant5);
                if (Settings.EXTRAHUDROLEPENDANTCIV6NAME.Length > 0) btnExtraHudRolePendant1.Children.Add(btnExtraHudRolePendant6);
                if (Settings.EXTRAHUDROLEPENDANTCIV7NAME.Length > 0) btnExtraHudRolePendant1.Children.Add(btnExtraHudRolePendant7);
                if (Settings.EXTRAHUDROLEPENDANTCIV8NAME.Length > 0) btnExtraHudRolePendant1.Children.Add(btnExtraHudRolePendant8);
                if (Settings.EXTRAHUDROLEPENDANTCIV9NAME.Length > 0) btnExtraHudRolePendant1.Children.Add(btnExtraHudRolePendant9);
                if (Settings.EXTRAHUDROLEPENDANTCIV10NAME.Length > 0) btnExtraHudRolePendant1.Children.Add(btnExtraHudRolePendant10);
            }

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

        #region Reload buttonnames (dependant buttons)
        static public void ReloadButtonText()
        {
            if (Identification.MySelf.C.isCop)
            {
                btnExtraHudRolePendant1.Text = Settings.EXTRAHUDROLEPENDANTCOP1NAME;
                btnExtraHudRolePendant2.Text = Settings.EXTRAHUDROLEPENDANTCOP2NAME;
                btnExtraHudRolePendant3.Text = Settings.EXTRAHUDROLEPENDANTCOP3NAME;
                btnExtraHudRolePendant4.Text = Settings.EXTRAHUDROLEPENDANTCOP4NAME;
                btnExtraHudRolePendant5.Text = Settings.EXTRAHUDROLEPENDANTCOP5NAME;
                btnExtraHudRolePendant6.Text = Settings.EXTRAHUDROLEPENDANTCOP6NAME;
                btnExtraHudRolePendant7.Text = Settings.EXTRAHUDROLEPENDANTCOP7NAME;
                btnExtraHudRolePendant8.Text = Settings.EXTRAHUDROLEPENDANTCOP8NAME;
                btnExtraHudRolePendant9.Text = Settings.EXTRAHUDROLEPENDANTCOP9NAME;
                btnExtraHudRolePendant10.Text = Settings.EXTRAHUDROLEPENDANTCOP10NAME;
            }
            else if (Identification.MySelf.C.isResQ)
            {
                btnExtraHudRolePendant1.Text = Settings.EXTRAHUDROLEPENDANTTOW1NAME;
                btnExtraHudRolePendant2.Text = Settings.EXTRAHUDROLEPENDANTTOW2NAME;
                btnExtraHudRolePendant3.Text = Settings.EXTRAHUDROLEPENDANTTOW3NAME;
                btnExtraHudRolePendant4.Text = Settings.EXTRAHUDROLEPENDANTTOW4NAME;
                btnExtraHudRolePendant5.Text = Settings.EXTRAHUDROLEPENDANTTOW5NAME;
                btnExtraHudRolePendant6.Text = Settings.EXTRAHUDROLEPENDANTTOW6NAME;
                btnExtraHudRolePendant7.Text = Settings.EXTRAHUDROLEPENDANTTOW7NAME;
                btnExtraHudRolePendant8.Text = Settings.EXTRAHUDROLEPENDANTTOW8NAME;
                btnExtraHudRolePendant9.Text = Settings.EXTRAHUDROLEPENDANTTOW9NAME;
                btnExtraHudRolePendant10.Text = Settings.EXTRAHUDROLEPENDANTTOW10NAME;
            }
            else if (Identification.MySelf.C.isCiv)
            {
                btnExtraHudRolePendant1.Text = Settings.EXTRAHUDROLEPENDANTCIV1NAME;
                btnExtraHudRolePendant2.Text = Settings.EXTRAHUDROLEPENDANTCIV2NAME;
                btnExtraHudRolePendant3.Text = Settings.EXTRAHUDROLEPENDANTCIV3NAME;
                btnExtraHudRolePendant4.Text = Settings.EXTRAHUDROLEPENDANTCIV4NAME;
                btnExtraHudRolePendant5.Text = Settings.EXTRAHUDROLEPENDANTCIV5NAME;
                btnExtraHudRolePendant6.Text = Settings.EXTRAHUDROLEPENDANTCIV6NAME;
                btnExtraHudRolePendant7.Text = Settings.EXTRAHUDROLEPENDANTCIV7NAME;
                btnExtraHudRolePendant8.Text = Settings.EXTRAHUDROLEPENDANTCIV8NAME;
                btnExtraHudRolePendant9.Text = Settings.EXTRAHUDROLEPENDANTCIV9NAME;
                btnExtraHudRolePendant10.Text = Settings.EXTRAHUDROLEPENDANTCIV10NAME;
            }
        }
        #endregion

        #region Coordinates
        static public Buttons Coordinates = new Buttons()
        {
            Name = "Coordinate",
            Text = "X: -- Y: --",
            Style = ButtonStyles.ISB_DARK,
            Height = 5,
            Width = 30,
            LeftRight = 100,
            TopDown = 100,
            ID = 0
        };
        #endregion

        #region Drift
        static public Buttons DriftPanelBg = new Buttons()
        {
            Name = "DriftPanelBg",
            Text = "",
            Style = ButtonStyles.ISB_DARK,
            Height = 20,
            Width = 33,
            LeftRight = 0,
            TopDown = 70,
            ID = 156
        };
        static public Buttons DriftPanel2 = new Buttons()
        {
            Name = "DriftPanel2",
            Text = "",
            Style = ButtonStyles.ISB_C1,
            Height = 5,
            Width = 15,
            LeftRight = (byte)(DriftPanelBg.LeftRight + 1),
            TopDown = (byte)(DriftPanelBg.TopDown + 6),
            ID = 154
        };
        static public Buttons DriftPanel3 = new Buttons()
        {
            Name = "DriftPanel3",
            Text = "",
            Style = ButtonStyles.ISB_C1,
            Height = 5,
            Width = 15,
            LeftRight = (byte)(DriftPanelBg.LeftRight + 17),
            TopDown = (byte)(DriftPanelBg.TopDown + 1),
            ID = 153
        };
        static public Buttons DriftPanel4 = new Buttons()
        {
            Name = "DriftPanel4",
            Text = "",
            Style = ButtonStyles.ISB_C1,
            Height = 5,
            Width = 15,
            LeftRight = (byte)(DriftPanelBg.LeftRight + 17),
            TopDown = (byte)(DriftPanelBg.TopDown + 6),
            ID = 152
        };
        static public Buttons DriftPanel1 = new Buttons()
        {
            Name = "DriftPanel1",
            Text = "",
            Style = ButtonStyles.ISB_C1,
            Height = 5,
            Width = 15,
            LeftRight = (byte)(DriftPanelBg.LeftRight + 1),
            TopDown = (byte)(DriftPanelBg.TopDown + 1),
            ID = 155,
            Children = new List<Buttons> { DriftPanel2, DriftPanel3, DriftPanel4, DriftPanelBg }
        };
        #endregion

        //157 og opp brukt 152<- drift
        //148 og opp brukt

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
            if (Settings.TRIPCOUNTER)
            {
                OpenButton(tripmMain);
            }
        }
    }
}