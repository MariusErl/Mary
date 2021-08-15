using InSimDotNet.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Mary
{
    class Commands
    {
        private static readonly Dictionary<string, string> MsgCommands = new Dictionary<string, string>()
        {
            { "help", "Show this list" },
            { "track", "Get track name" },
            { "carcontact", "Show/hide carcontact made" },
            { "checkusers", "Get number of cops, tows, meds and civilians" },
            { "locate <username>", "Locate/unlocate user" },
            { "stats", "Show online statistics for users on the server" },
            { "admins", "Check for online admins" },
            { "lock", "Lock/unlock the scanner on the tracked user" },
            { "siren", "Turn siren on/off" },
            { "sirenaudio", "Turn audio siren on/off" },
            { "sirenvisual", "Turn visual siren on/off" },
            { "afkmode", "Toggle if Mary will record who crashes your car" },
            { "hits", "Check who hit your car" },
            { "trip", "Toggle tripcounter on/off" },
            { "afk", "List cars standing still more then 10 seconds" },
            { "viewsus", "Switch screen to suspect" },
            { "script <text>", "Run text through Mary's script tool" },
            { "buttons", "Show list of buttons in use" },
            { "steal", "Show list of possible stealing victims (afk on track, enough km, enough money, not in SZ, not cop)" },
            { "emtbn", "Emulates a buttonpress for the keypushbutton set in settings, so you can bind Mary-keypress to wheel" },
            { "emscrlup", "Emulates mousescrollwheel up, so you can bind Mary-mousescrolls to wheel" },
            { "emscrldown", "Emulates mousescrollwheel down, so you can bind Mary-mousescrolls to wheel" },
            { "grid", "Display everyone's grid (those on track)" },
            { "togglecruise", "Toggle weather to follow current road speedlimit or set speed by yourself" },
            { "minimap", "See list of commands to adjust Mary's minimap" },
            { "caralarm", "Toggle caralarm on/off" },
            { "minimapgrid", "Toggle grid on/off" },
            { "togglespeedtrap", "Toggle auto-sharing of speedtrap readings in cop chat" },
            { "help2", "See list of commands (full version only)" },
        };


        static public void RunCommand(string Msg)
        {
            switch (Msg.Substring(0).Split(' ')[0])
            {
                case "test":
                    break;


                case "minimapgrid":
                    Settings.MINIMAPGRID = !Settings.MINIMAPGRID;
                    Messages.SendLocalMsg(Settings.MINIMAPGRID ? "Grid is now turned ^2ON." : "Grid is now turned ^1OFF.");
                    MaryMinimap.LoadBackgroundMap();
                    break;

                case "caralarm":
                    Settings.CARALARM = !Settings.CARALARM;
                    Messages.SendLocalMsg(Settings.CARALARM ? "Caralarm is now turned ^2ON." : "Caralarm is now turned ^1OFF.");
                    break;

                case "minimT+":
                    if (Settings.MINIMAP > 1)
                        MaryMinimap.ChangeFormOpazity(true);
                    break;
                case "minimT-":
                    if (Settings.MINIMAP > 1)
                        MaryMinimap.ChangeFormOpazity(false);
                    break;
                case "miniLocY+":
                    if (Settings.MINIMAP > 1)
                        MaryMinimap.ChangeFormLocationDown(true);
                    break;
                case "miniLocY-":
                    if (Settings.MINIMAP > 1)
                        MaryMinimap.ChangeFormLocationDown(false);
                    break;
                case "miniLocX-":
                    if (Settings.MINIMAP > 1)
                        MaryMinimap.ChangeFormLocationRight(false);
                    break;
                case "miniLocX+":
                    if (Settings.MINIMAP > 1)
                        MaryMinimap.ChangeFormLocationRight(true);
                    break;
                case "miniloc":
                    if (Settings.MINIMAP > 1)
                        Messages.SendLocalMsg("Minimap location on screen: " + Program.MinimapFormWindow.Location.X + ", Y: " + Program.MinimapFormWindow.Location.Y + ", Opacity:" + Program.MinimapFormWindow.Opacity);
                    break;

                case "minimap":
                    Messages.SendLocalMsg("^2Commands to adjust Mary's minimap:");
                    Messages.SendLocalMsg("^6/o miniLocX+ ^3- Move minimap right");
                    Messages.SendLocalMsg("^6/o miniLocX- ^3- Move minimap left");
                    Messages.SendLocalMsg("^6/o miniLocY+ ^3- Move minimap down");
                    Messages.SendLocalMsg("^6/o miniLocY- ^3- Move minimap up");
                    Messages.SendLocalMsg("^6/o minimT+ ^3- Increase opacity");
                    Messages.SendLocalMsg("^6/o minimT- ^3- Decrease opacity");
                    Messages.SendLocalMsg("^6/o miniloc ^3- Display current set location on screen (X,Y), so you can save the preferred values to settings-file");
                    break;

                case "help2":
                    if (Identification.FULLVERSION)
                    {
                        Messages.SendLocalMsg("^2List of extra Mary Commands (full version):");
                        Messages.SendLocalMsg("^6/o ajchase ^3- Auto-join first chase that is called for backup");
                    }
                    else goto default;
                    break;

                case "record":
                    if (!Path.RecordMe)
                    {
                        Messages.SendLocalMsg("Recording path.");
                        Path.RecordMe = true;
                    }
                    else
                    {
                        Messages.SendLocalMsg("Recording path stopped.");
                        Path.RecordMe = false;
                    }
                    break;

                case "togglecruise":
                    Settings.CRUISECONTROLSPEEDLIMIT = !Settings.CRUISECONTROLSPEEDLIMIT;
                    Messages.SendLocalMsg(Settings.CRUISECONTROLSPEEDLIMIT ? "Cruisecontrol using road speedlimit." : "Cruisecontrol using manually set speed.");
                    break;

                case "grid":
                    foreach (Clients all in Program.AllConnections)
                    {
                        if (all.onTrack)
                        {
                            Messages.SendLocalMsg(all.PlayerName + "^8's grid: ^7" + Tracks.CalculateGrid((int)all.P.x, (int)all.P.y));
                        }
                    }
                    break;

                case "embtn":
                    CruiseControl.SimulateKeypress();
                    break;
                case "emscrldown":
                    ButtonFactory.MouseScroll(true);
                    break;
                case "emscrlup":
                    ButtonFactory.MouseScroll(false);
                    break;

                case "help":
                    var messagePacket = new List<InSimDotNet.Packets.ISendable>();
                    foreach (KeyValuePair<string, string> cmd in MsgCommands)
                    {
                        messagePacket.Add(new InSimDotNet.Packets.IS_MSL { Msg = "^3Mary - ^6/o " + cmd.Key + " ^3- " + cmd.Value });
                    }
                    Messages.SendLocalMsg("^2List of Mary Commands:");
                    Program.insim.Send(messagePacket.ToArray());
                    break;

                case "trip":
                    if (Settings.TRIPCOUNTER)
                    {
                        Settings.TRIPCOUNTER = false;
                        ButtonFactory.RemoveButton(ButtonFactory.tripmMain);
                        Messages.SendLocalMsg("Tripcounter switched off.");
                    }
                    else
                    {
                        Settings.TRIPCOUNTER = true;
                        ButtonFactory.OpenButton(ButtonFactory.tripmMain);
                        Messages.SendLocalMsg("Tripcounter switched on.");
                    }
                    break;

                case "steal":
                    Task steal = Task.Factory.StartNew(() => CheckStealing());
                    break;

                case "script":
                    Task taskkk = Task.Factory.StartNew(() => Messages.SendMsgRunScript(Msg.Remove(0, 7)));
                    break;

                case "viewsus":
                    if (Tracker.Trackee.Username != null && Tracker.Trackee.onTrack) Program.SpectateUser(Tracker.Trackee);
                    else Messages.SendLocalMsg("^1You are not tracking anyone / they are not on track...");
                    break;

                case "admins":
                    AdminCheck.Check(true);
                    break;

                case "lock":
                    if (Tracker.Trackee.Username == null)
                    {
                        Tracker.Trackee = Tracker.ScanCars();
                        if (Tracker.Trackee.Username != null) Messages.SendLocalMsg("^2Tracking: ^8" + Tracker.Trackee.PlayerName);
                        else Messages.SendLocalMsg("No car in range...");
                    }
                    else
                    {
                        Tracker.EditUserToTrack(Tracker.Trackee.Username.ToLower());
                    }
                    break;

                case "siren":
                    if (Roleplay.SirenActive)
                    {
                        Roleplay.clsSiren = 0;
                        Roleplay.UpdateSiren(true);
                    }
                    else
                    {
                        Roleplay.clsSiren = Roleplay.SirenMode.Both;
                        Roleplay.UpdateSiren(false);
                    }
                    break;

                case "sirenaudio":
                    if (Roleplay.clsSiren.HasFlag(Roleplay.SirenMode.Audio))
                    {
                        Roleplay.clsSiren -= 1;
                        Roleplay.AudioSiren(true);
                        if (Roleplay.clsSiren == 0)
                        {
                            Roleplay.UpdateSiren(true);
                        }
                    }
                    else
                    {
                        Roleplay.clsSiren += 1;
                        Roleplay.SirenActive = true;
                    }
                    break;

                case "sirenvisual":
                    if (Roleplay.clsSiren.HasFlag(Roleplay.SirenMode.Visual))
                    {
                        Roleplay.clsSiren -= 2;
                        Roleplay.VisibleSiren(true);
                        if (Roleplay.clsSiren == 0)
                        {
                            Roleplay.UpdateSiren(true);
                        }
                    }
                    else
                    {
                        Roleplay.clsSiren += 2;
                        Roleplay.SirenActive = true;
                    }
                    break;

                case "stats":
                    Statistics.TempList = new List<Clients>(Program.AllConnections);
                    Statistics.TempList.Sort((x, y) => string.Compare(Program.StripColours(x.PlayerName), Program.StripColours(y.PlayerName)));
                    Messages.SendLocalMsg("Downloading stats...");
                    Statistics.StatButtons.Clear();
                    Statistics.LoadTable(true);
                    break;

                case "buttons":
                    Messages.SendLocalMsg("^3Buttons in use:");
                    foreach (ButtonFactory.Buttons btns in ButtonFactory.ButtonsUsed)
                    {
                        Program.Cons(btns.Name + " (ID: " + btns.ID + ") " + (btns.Children.Count > 0 ? "motherbutton" : ""));
                        Messages.SendLocalMsg(btns.Name + " (ID: " + btns.ID + ")");
                    }
                    break;

                case "locate":
                    if (Msg.Length > 7)
                    {
                        Tracker.EditUserToTrack(Msg.Remove(0, 7).ToLower());
                    }
                    else
                    {
                        Messages.SendLocalMsg("^1No user specified.");
                    }
                    break;

                case "afk":
                    Messages.SendLocalMsg("^2Cars standing still more then 10 seconds:");
                    foreach (Clients all in Program.AllConnections)
                        if (all.P.afkStart.ElapsedMilliseconds > 10000 && all.P.afkStart.IsRunning)
                        {
                            Messages.SendLocalMsg(all.PlayerName + " ^8(" + all.Username + ") ^7: ^3" + all.P.afkStart.Elapsed.ToString(@"hh\:mm\:ss"));
                        }
                    break;
                case "ajchase":
                    if (Identification.FULLVERSION)
                    {
                        if (!Messages.autoJoinChase)
                        {
                            Messages.SendLocalMsg("^2Auto-joining next backup call...");
                            Messages.autoJoinChase = true;
                        }
                        else
                        {
                            Messages.SendLocalMsg("^1No longer auto-joining next backup call...");
                            Messages.autoJoinChase = false;
                        }
                    }
                    else goto default;
                    break;

                case "afkmode":
                    Messages.afkMode = !Messages.afkMode;
                    if (Messages.afkMode)
                    {
                        Messages.SendLocalMsg("AFK mode on. Mary will now record your crashes.");
                    }
                    else
                    {
                        Messages.SendLocalMsg("AFK mode off. Mary will no longer record your crashes.");
                        Program.Crashers.Clear();
                    }
                    break;

                case "hits":
                    if (Program.Crashers.Count() > 0)
                    {
                        Messages.SendLocalMsg("Crashes:");
                        foreach (string crash in Program.Crashers)
                        {
                            Messages.SendLocalMsg(crash);
                        }
                    }
                    else
                    {
                        Messages.SendLocalMsg("There is no recorded crashes.");
                    }
                    break;

                case "users":
                    Console.WriteLine("Users:");

                    Program.AllConnections.Sort((x, y) => string.Compare(Program.StripColours(x.PlayerName), Program.StripColours(y.PlayerName)));

                    foreach (Clients users in Program.AllConnections)
                    {
                        Console.WriteLine("" + Program.StripColours(users.PlayerName) + " (" + users.Username + ")");
                        Messages.SendLocalMsg("" + Program.StripColours(users.PlayerName) + " (" + users.Username + ")");
                    }
                    break;

                case "track":
                    Messages.SendLocalMsg("^6Current track: ^3" + TrackHelper.GetFullTrackName(Program.CurrentTrack) + " (" + Program.CurrentTrack + ")");
                    break;

                case "carcontact":
                    if (Settings.CARCONTACT)
                    {
                        Settings.CARCONTACT = false;
                        Messages.SendLocalMsg("Car contact no longer showing");
                    }
                    else
                    {
                        Settings.CARCONTACT = true;
                        Messages.SendLocalMsg("Car contact is now showing");
                    }
                    break;

                case "togglespeedtrap":
                    Identification.MySelf.C.autoSpeedtrap = !Identification.MySelf.C.autoSpeedtrap;
                    if (Identification.MySelf.C.autoSpeedtrap)
                        Messages.SendLocalMsg("^2Automatically sharing speedtrap readings.");
                    else
                        Messages.SendLocalMsg("^2No longer automatically sharing speedtrap readings.");
                    break;

                case "checkusers":
                    int cops = 0, tows = 0, meds = 0, civs = 0;
                    foreach (Clients users in Program.AllConnections)
                    {
                        string playername = users.PlayerName.ToLower();
                        if (users.C.isCop)
                        {
                            cops++;
                        }
                        else if (playername.Contains("[tow]"))
                        {
                            tows++;
                        }
                        else if (playername.Contains("[med]") || playername.Contains("[res]"))
                        {
                            meds++;
                        }
                        else
                        {
                            civs++;
                        }
                    }
                    Messages.SendLocalMsg("^3COPs: " + cops + " - TOWs: " + tows + " - MEDs: " + meds + " - CIVs: " + (civs - 1) + " - Players: " + (Program.AllConnections.Count()));
                    break;

                default:
                    Messages.SendLocalMsg("Unknown command. Try /o help");
                    break;
            }
        }

        static public void CheckStealing()
        {
            Messages.SendLocalMsg("Searching for possible victims...");
            List<string> allVictims = new List<string>();
            try
            {
                foreach (Clients victim in Program.AllConnections)
                {
                    if (victim.onTrack && victim.P.afkStart.ElapsedMilliseconds > 60000 && victim.P.afkStart.IsRunning && !victim.C.isCop && victim.onTrack && victim.P.x != 0 && victim.P.y != 0)
                    {
                        Statistics.DownloadStatsusername(victim.Username);
                        System.Threading.Thread.Sleep(50);
                        Statistics.LoadStats(victim.Username);
                        Statistics.GetRoadName(victim);
                        if (Convert.ToInt64(victim.C.KM.Replace(",", "")) >= 2500 && /*Convert.ToInt64(victim.C.Wallet.Replace(",", "")) > 2500 && */!victim.P.Location.StartsWith("^2"))
                        {
                            allVictims.Add("Possible victim: " + victim.PlayerName + " ^8(" + victim.Username + "), wallet amount unknown, " + Tracker.GetDirectDistance(Identification.MySelf, victim) + "m away.");
                        }
                    }
                }
            }
            catch (Exception e) { Program.ConsError(e.ToString()); }
            if (allVictims.Count > 0)
            {
                Messages.SendLocalMsg("Possible stealing victims:");
                foreach (string candidate in allVictims)
                {
                    Messages.SendLocalMsg(candidate);
                }
            }
            else
            {
                Messages.SendLocalMsg("No good candidates for stealing right now...");
            }
        }
    }
}
