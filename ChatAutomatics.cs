using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Mary
{
    class ChatAutomatics
    {
        static public void CheckAuto(string Msg)
        {
            if (Messages.autoJoinChase && Msg.EndsWith("is calling for backup."))
            {
                Messages.SendPublicMsg("!join");
                Messages.autoJoinChase = false;
            }

            if (Identification.MySelf.C.isCop)
            {
                string myNameRaw = Identification.MySelf.PlayerNameRAW, susNameRaw = Tracker.Trackee.PlayerNameRAW;
                if (Msg.StartsWith(myNameRaw + " chasing:"))
                {
                    Identification.MySelf.P.Chasing = true;
                    if (!ButtonFactory.btnTracker2.Children.Contains(ButtonFactory.btnTracker6)) ButtonFactory.btnTracker2.Children.Add(ButtonFactory.btnTracker6);
                    string[] userToLoc = Regex.Split(Msg, "chasing: ");
                    if (Settings.AUTOLOCATE)
                    {
                        foreach (Clients all in Program.AllConnections)
                        {
                            if (all.PlayerNameRAW == userToLoc[1] && susNameRaw != userToLoc[1]) { Tracker.EditUserToTrack(all.Username.ToLower()); }
                        }
                    }
                }
                else if (Msg.StartsWith(myNameRaw + " joined on "))
                {
                    Identification.MySelf.P.Chasing = true;
                    if (!ButtonFactory.btnTracker2.Children.Contains(ButtonFactory.btnTracker6)) ButtonFactory.btnTracker2.Children.Add(ButtonFactory.btnTracker6);
                    string[] userToLoc = Regex.Split(Msg, "joined on ");
                    if (Settings.AUTOLOCATE)
                    {
                        foreach (Clients all in Program.AllConnections)
                        {
                            if (all.PlayerNameRAW == userToLoc[1] && susNameRaw != userToLoc[1]) { Tracker.EditUserToTrack(all.Username.ToLower()); }
                        }
                    }
                    Identification.MySelf.P.joinMode = true;
                }
                else if (
                       Msg.StartsWith(myNameRaw + " lost contact with suspect.")
                    || Msg.StartsWith(myNameRaw + " left the chase.")
                    || Msg.StartsWith(susNameRaw + " has been busted")
                    || Msg.StartsWith(susNameRaw + " has been auto-busted")
                    || Msg.StartsWith(susNameRaw + " paid a")
                    || Msg.StartsWith(susNameRaw + " escaped!")
                    || Msg.StartsWith(susNameRaw + " was fined") && !Msg.EndsWith(" for spamming.") && !Msg.EndsWith(" for using automatic greetings."))
                {
                    Task tt = Task.Factory.StartNew(() => Tracker.EditUserToTrack(Tracker.Trackee.Username.ToLower()));
                    Identification.MySelf.P.joinMode = false;
                    Identification.MySelf.P.Chasing = false;
                    if (ButtonFactory.btnTracker2.Children.Contains(ButtonFactory.btnTracker6)) ButtonFactory.btnTracker2.Children.Remove(ButtonFactory.btnTracker6); ButtonFactory.RemoveButton(ButtonFactory.btnTracker6);
                }

                if (Identification.MySelf.C.autoSpeedtrap && !Msg.Contains("[COP]") && Msg.Contains(" clocked @ ") && Msg.Contains(" - Fine: "))
                {
                    string car = "";
                    foreach (Clients FindUser in Program.AllConnections)
                        if (Msg.Contains(FindUser.PlayerNameRAW))
                        {
                            car = FindUser.P.CarName;
                            break;
                        }
                    Messages.SendPublicMsg("!c " + Msg + " [" + car + "]");
                }
            }

            if (Msg.StartsWith(Identification.MySelf.PlayerNameRAW + " caught up with the chase.") || Msg.StartsWith(Identification.MySelf.PlayerNameRAW + " is taking over the chase"))
            {
                Identification.MySelf.P.joinMode = false;
            }

            if (Msg.Contains("chasing: " + Identification.MySelf.PlayerNameRAW))
            {
                string tempMsg = Msg, tempMsg2 = Msg;
                string[] newCop = Regex.Split(tempMsg, " chasing:");

                foreach (Clients all in Program.AllConnections)
                {
                    if (all.PlayerNameRAW == newCop[0])
                    {
                        Identification.MySelf.P.Chase.Add(all);
                    }
                }
            }
            if (Msg.EndsWith(" joined on " + Identification.MySelf.PlayerNameRAW))
            {
                string tempMsg = Msg, tempMsg2 = Msg;
                string[] newCop2 = Regex.Split(tempMsg2, " joined on");
                foreach (Clients all in Program.AllConnections)
                {
                    if (all.PlayerNameRAW == newCop2[0])
                    {
                        Identification.MySelf.P.Chase.Add(all);
                        all.P.joinMode = true;
                    }
                }
            }
            if (Msg.Contains(" caught up with the chase."))
            {
                foreach (Clients cops in Identification.MySelf.P.Chase)
                {
                    if (Msg.StartsWith(cops.PlayerNameRAW + " caught up with the chase."))
                    {
                        cops.P.joinMode = false;
                        break;
                    }
                }
            }

            if (Identification.MySelf.P.Chase.Count() > 0)
            {
                DefaultMinimap.UpdateCopsInChase(Msg + " Mary remove");
            }

            if (Settings.AFKWARNING && Identification.FULLVERSION)
            {
                if (Msg.StartsWith("Idle warning. You will be kicked soon."))
                {
                    Program.FlashWindow(Process.GetCurrentProcess().MainWindowHandle, true);
                    ButtonFactory.OpenButton(ButtonFactory.btnAFKwarning);
                }
            }
        }

        static public void SpamChat()
        {
            for (int x = 0; x < 20; x++)
            {
                Messages.SendPublicMsg("/echo  ");
            }
        }
    }
}
