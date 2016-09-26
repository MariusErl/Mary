using System;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using InSimDotNet;
using InSimDotNet.Packets;
using InSimDotNet.Helpers;
using System.Diagnostics;

namespace Mary
{
    class Messages
    {
        static public bool resQonMission = false;
        static public string resQuser = "";
        static public bool afkMode = false;
        static public void Messa(InSim insim, IS_MSO mso)
        {
            try
            {
                string Msg = mso.Msg.Substring(mso.TextStart, (mso.Msg.Length - mso.TextStart));
                string[] StrMsg = Msg.Split(' ');
                var user = Program.GetClient(mso.UCID, true);

                #region Auto stuff
                if (mso.UserType == UserType.MSO_SYSTEM)
                {
                    if (Settings.AUTOLOCATE && Program.MySelf.C.isCop)
                    {
                        string myNameRaw = Program.MySelf.PlayerNameRAW, susNameRaw = Tracker.Trackee.PlayerNameRAW;
                        if (Msg.StartsWith(myNameRaw + " chasing:"))
                        {
                            string[] userToLoc = Regex.Split(Msg, "chasing: ");
                            foreach (Clients all in Program.AllConnections)
                            {
                                if (all.PlayerNameRAW == userToLoc[1] && susNameRaw != userToLoc[1]) { Tracker.EditUserToTrack(all.Username.ToLower()); }
                            }
                        }
                        else if (Msg.StartsWith(myNameRaw + " joined on "))
                        {
                            string[] userToLoc = Regex.Split(Msg, "joined on ");
                            foreach (Clients all in Program.AllConnections)
                            {
                                if (all.PlayerNameRAW == userToLoc[1] && susNameRaw != userToLoc[1]) { Tracker.EditUserToTrack(all.Username.ToLower()); }
                            }
                        }
                        else if (Msg.StartsWith(myNameRaw + " received ") || Msg.StartsWith(myNameRaw + " lost contact with suspect.") || Msg.StartsWith(myNameRaw + " left the chase.") || Msg.StartsWith(susNameRaw + " has been busted") || Msg.StartsWith(susNameRaw + " has been auto-busted") || Msg.StartsWith(susNameRaw + " paid a") || (Msg.StartsWith(susNameRaw + " was fined") && !Msg.EndsWith("speeding")) || Msg.StartsWith(susNameRaw + " escaped!"))
                        {
                            Task tt = Task.Factory.StartNew(() => Tracker.EditUserToTrack(Tracker.Trackee.Username.ToLower()));
                        }
                    }
                    if (Settings.AUTOLOCATE && Program.MySelf.C.isResQ)
                    {
                        if (!resQonMission)
                        {
                            if (Msg.EndsWith(" is calling a tow."))
                            {
                                string[] userToLoc = Regex.Split(Msg, " is calling ");
                                foreach (Clients all in Program.AllConnections)
                                {
                                    if (all.PlayerNameRAW == userToLoc[0] && Tracker.Trackee.PlayerNameRAW != userToLoc[0]) { Tracker.EditUserToTrack(all.Username.ToLower()); resQonMission = true; }
                                }
                            }
                            if (Msg.EndsWith(" is calling a tow(with danger)."))
                            {
                                string[] userToLoc = Regex.Split(Msg, " is calling ");
                                foreach (Clients all in Program.AllConnections)
                                {
                                    if (all.PlayerNameRAW == userToLoc[0] && Tracker.Trackee.PlayerNameRAW != userToLoc[0]) { Tracker.EditUserToTrack(all.Username.ToLower()); resQonMission = true; }
                                }
                            }
                            if (Msg.EndsWith(" is reporting a danger on the road."))
                            {
                                string[] userToLoc = Regex.Split(Msg, " is reporting ");
                                foreach (Clients all in Program.AllConnections)
                                {
                                    if (all.PlayerNameRAW == userToLoc[0] && Tracker.Trackee.PlayerNameRAW != userToLoc[0]) { Tracker.EditUserToTrack(all.Username.ToLower()); resQonMission = true; }
                                }
                            }
                        }
                    }
                    if (Settings.AFKWARNING && Program.FULLVERSION)
                    {
                        if (Msg.StartsWith("Idle warning. You will be kicked soon."))
                        {
                            Program.FlashWindow(Process.GetCurrentProcess().MainWindowHandle, true);
                            ButtonFactory.OpenButton(ButtonFactory.btnAFKwarning);
                        }
                    }
                }
                #endregion

                if (mso.UserType == UserType.MSO_O)
                    switch (StrMsg[0])
                    {
                        case "trip":
                            if (Settings.TRIPCOUNTER)
                            {
                                Settings.TRIPCOUNTER = false;
                                ButtonFactory.RemoveButton(ButtonFactory.tripmMain);
                                SendLocalMsg("Tripcounter switched off.");
                            }
                            else
                            {
                                Settings.TRIPCOUNTER = true;
                                ButtonFactory.OpenButton(ButtonFactory.tripmMain);
                                SendLocalMsg("Tripcounter switched on.");
                            }
                            break;

                        case "script":
                            string[] script = Msg.Remove(0, 7).Split('%').ToArray();
                            Task taskkk = Task.Factory.StartNew(() => Messages.SendMultiMsg(script));
                            break;

                        case "viewsus":
                            if (Program.FULLVERSION)
                            {
                                if (Tracker.Trackee.Username != null) Program.SpectateTrackee();
                                else SendLocalMsg("^1You are not tracking anyone...");
                            }
                            else goto default;
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
                            ButtonFactory.RemoveButton(Statistics.StatsBtnsUsersOnly);
                            Statistics.LoadTable(true);
                            break;

                        case "buttons":
                            Messages.SendLocalMsg("^3Buttons in use:");
                            foreach (ButtonFactory.Buttons btns in ButtonFactory.ButtonsUsed)
                            {
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
                                SendLocalMsg("^1No user specified.");
                            }
                            break;

                        case "help":
                            SendLocalMsg("^2Commandlist:");
                            SendLocalMsg("^6/o help ^3- Show this list");
                            SendLocalMsg("^6/o track ^3- Get track name");
                            SendLocalMsg("^6/o carcontact ^3- Show/hide carcontact made");
                            SendLocalMsg("^6/o checkusers ^3- Get number of cops, tows, meds and civilians");
                            SendLocalMsg("^6/o locate <username> ^3- Locate/unlocate user");
                            SendLocalMsg("^6/o stats ^3- Show stats for online users");
                            SendLocalMsg("^6/o ch <txt> ^3- Encrypt your !ch message so TC can't sniff it through logs.");
                            SendLocalMsg("^6/o admins ^3- Check for online admins");
                            SendLocalMsg("^6/o lock ^3- Lock/unlock the scanner on the tracked user");
                            SendLocalMsg("^6/o siren ^3- Turn siren on/off");
                            SendLocalMsg("^6/o sirenaudio ^3- Turn audio siren on/off");
                            SendLocalMsg("^6/o sirenvisual ^3- Turn visual siren on/off");
                            SendLocalMsg("^6/o afkmode ^3- Toggle if Mary will record who crashes your car");
                            SendLocalMsg("^6/o hits ^3- Check who hit your car");
                            SendLocalMsg("^6/o trip ^3- Toggle tripcounter on/off");
                            if (Program.FULLVERSION)
                            {
                                SendLocalMsg("^6/o viewsus ^3- Switch screen to suspect");
                            }
                            SendLocalMsg("^6/o script <text> ^3- Run text through Mary's script tool");
                            SendLocalMsg("^6/o buttons ^3- Show list of buttons in use");
                            break;

                        case "afkmode":
                            afkMode = afkMode ? false : true;
                            if (afkMode)
                            {
                                SendLocalMsg("AFK mode on. Mary will now record your crashes.");
                            }
                            else
                            {
                                SendLocalMsg("AFK mode off. Mary will no longer record your crashes.");
                                Program.Crashers.Clear();
                            }
                            break;

                        case "hits":
                            if (Program.Crashers.Count() > 0)
                            {
                                SendLocalMsg("Crashes:");
                                foreach (string crash in Program.Crashers)
                                {
                                    SendLocalMsg(crash);
                                }
                            }
                            else
                            {
                                SendLocalMsg("There is no recorded crashes.");
                            }
                            break;

                        case "users":
                            Console.WriteLine("Users:");
                            foreach (Clients users in Program.AllConnections)
                            {
                                Console.WriteLine("" + users.PlayerName + " (" + users.Username + ")");
                            }
                            break;

                        case "track":
                            SendLocalMsg("^6Current track: ^3" + TrackHelper.GetFullTrackName(Program.CurrentTrack) + " (" + Program.CurrentTrack + ")");
                            break;

                        case "carcontact":
                            if (Settings.CARCONTACT)
                            {
                                Settings.CARCONTACT = false;
                                SendLocalMsg("Car contact no longer showing");
                            }
                            else
                            {
                                Settings.CARCONTACT = true;
                                SendLocalMsg("Car contact is now showing");
                            }
                            break;

                        case "checkusers":
                            int cops = 0,tows = 0, meds = 0, sivilians = 0;
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
                                    sivilians++;
                                }
                            }
                            SendLocalMsg("^3COPs: " + cops + " - TOWs: " + tows + " - MEDs: " + meds + " - CIVs: " + (sivilians - 1) + " - Players: " + (Program.AllConnections.Count()));
                            break;

                        default:
                            SendLocalMsg("Unknown command. Try /o help");
                            break;
                    }
                if (Settings.CHATLOGGING)
                {
                    LogMsg(mso.Msg);
                }
            }
            catch (Exception e)
            {
                Program.ConsError(e.ToString());
            }
        }

        static public string RandomCrypt()
        {
            var chars = "ABCDEFGHIJKLNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, random.Next(5, 20)).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        static public void LogMsg(string msg)
        {
            StreamWriter Srrrr = new StreamWriter(@"Chatlogs\\" + CHATLOGDIR + ".txt", true);
            Srrrr.WriteLine(DateTime.Now + " : " + msg);
            Srrrr.Flush();
            Srrrr.Close();
        }

        static public void SendLocalMsg(string msg)
        {
            IS_MSL bajss = new IS_MSL { Msg = "^3Mary - ^8" + msg };
            Program.insim.Send(bajss);
        }

        static public void SendPublicMsg(string msg)
        {
            Program.insim.Send(msg);
        }

        static public void SendMultiMsg(string[] msgs)
        {
            Clients carBehind = new Clients();
            for (int x = 0; x < msgs.Count(); x++)
            {
                if (msgs[x].Contains("{SUSXP}")) { Statistics.DownloadStats(Tracker.Trackee); }
                if (msgs[x].Contains("{CARBEHIND}")) { carBehind = Tracker.ScanCarsBehind(); }
                if (Tracker.Trackee != null)
                {
                    string temp = msgs[x].Replace("{SUSNAME}", Tracker.Trackee.PlayerName).Replace("{SUSCAR}", Tracker.Trackee.P.CarName)
                        .Replace("{SUSSPEEDKPH}", "" + Program.CarSpeed(Tracker.Trackee.P.speed, true)).Replace("{SUSSPEEDMPH}", "" + Program.CarSpeed(Tracker.Trackee.P.speed, false))
                        .Replace("{SUSDIR}", Tracker.Trackee.P.dirName).Replace("{SUSXP}", "" + Tracker.Trackee.C.XPRobber).Replace("{MYCAR}", Program.MySelf.P.CarName)
                        .Replace("{CARBEHIND}", carBehind.PlayerName);

                    if (temp.Length > 1) SendPublicMsg(temp);
                }
                else
                {
                    string temp = msgs[x].Replace("{SUSNAME}", "").Replace("{SUSCAR}", "").Replace("{SUSSPEEDKPH}", "")
                        .Replace("{SUSSPEEDMPH}", "").Replace("{SUSDIR}", "").Replace("{SUSXP}", "").Replace("{MYCAR}", Program.MySelf.P.CarName)
                        .Replace("{CARBEHIND}", carBehind.PlayerName);
                    if (temp.Length > 1) SendPublicMsg(temp);
                }
                System.Threading.Thread.Sleep(550);
            }
        }

        static public string CHATLOGDIR = "";
        static public void CreateChatlogFile()
        {
            CHATLOGDIR = DateTime.Now.ToString("yyyy-MM-dd--HH-mm-ss");
            TextWriter tw = new StreamWriter(@"Chatlogs\\" + CHATLOGDIR + ".txt", true);
            tw.WriteLine("New chatlog started: " + DateTime.Now);
            tw.Close();
        }
    }
}