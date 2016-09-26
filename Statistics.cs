using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using InSimDotNet.Packets;

namespace Mary
{
    class Statistics
    {
        static public byte copsonline = 0;
        static public byte towsonline = 0;
        static public byte robbersonline = 0;
        static public byte part = 0;

        static public List<byte> StatsBtnsAll = new List<byte>();
        static public List<byte> StatsBtnsUsersOnly = new List<byte>();

        static public void LoadList()
        {
            for (byte x = 193; x >= 63; x--)
            {
                StatsBtnsAll.Add(x);
                if (x < 183) StatsBtnsUsersOnly.Add(x);
            }
        }

        #region User stats
        static public IDictionary<string, ButtonFactory.Buttons> StatButtons = new Dictionary<string, ButtonFactory.Buttons>();
        static public void LoadTable(bool partOne)
        {
            int x = 0, upTo = 0, onlineUsers = Program.AllConnections.Count, bgsize = (onlineUsers > 20) ? 99 : 18 + (onlineUsers * 4);
            byte userNumber = 0;
            if (partOne)
            {
                upTo = (onlineUsers > 20) ? 19 : onlineUsers;
                ButtonFactory.panelstatbtn1.Height = ButtonFactory.panelstatbtn2.Height = (byte)bgsize;
                ButtonFactory.OpenButton(ButtonFactory.panelstatbtn9);
                if (onlineUsers > 20) ButtonFactory.OpenButton(ButtonFactory.panelstatbtn10);
            }
            else
            {
                upTo = onlineUsers;
                ButtonFactory.RemoveButton(ButtonFactory.panelstatbtn10);
                ButtonFactory.RemoveButton(ButtonFactory.panelstatbtnProgressbar);
                ButtonFactory.RemoveButton(StatsBtnsUsersOnly);
            }
            byte btnID = 182;
            ButtonStyles styleee = ButtonStyles.ISB_DARK;
            for (x = (partOne) ? 0 : 20; x <= upTo; x++)
            {
                Clients c = Program.AllConnections[x];
                if (!c.Username.Equals(""))
                {
                    DownloadStats(c);
                    ButtonFactory.panelstatbtnProgressbar.Text = (Progressbar.Progress(partOne ? userNumber + 1 : userNumber + 21, upTo, 150));

                    string[] arr = new string[] { "panelstatbtn0User" + userNumber, "panelstatbtn1User" + userNumber, "panelstatbtn2User" + userNumber, "panelstatbtn3User" + userNumber, "panelstatbtn4User" + userNumber, "panelstatbtn5User" + userNumber };
                    foreach (string name in arr)
                    {
                        StatButtons[name] = new ButtonFactory.Buttons();
                    }

                    #region Buttoninfo
                    StatButtons["panelstatbtn1User" + userNumber] = new ButtonFactory.Buttons
                    {
                        Name = "panelstatbtn1User" + userNumber,
                        Text = c.PlayerName + " ^8(" + c.Username + ") [" + c.C.Country + "]",
                        PublicText = c.PlayerName + "^8 (" + c.Username + ") - From: ^3" + c.C.Country,
                        Style = ButtonStyles.ISB_C1 | ButtonStyles.ISB_LEFT | ButtonStyles.ISB_CLICK,
                        Height = 4,
                        Width = 35,
                        LeftRight = 26,
                        TopDown = (byte)(56 + 4 * userNumber),
                        ID = btnID,
                        Clickable = true
                    }; btnID--;

                    StatButtons["panelstatbtn2User" + userNumber] = new ButtonFactory.Buttons
                    {
                        Name = "panelstatbtn2User" + userNumber,
                        Text = c.C.Joindate,
                        PublicText = c.PlayerName + "^8 - Join Date: ^3" + c.C.Joindate,
                        Style = ButtonStyles.ISB_C1 | ButtonStyles.ISB_LEFT | ButtonStyles.ISB_CLICK,
                        Height = 4,
                        Width = 15,
                        LeftRight = 61,
                        TopDown = (byte)(56 + 4 * userNumber),
                        ID = btnID,
                        Clickable = true
                    }; btnID--;

                    StatButtons["panelstatbtn3User" + userNumber] = new ButtonFactory.Buttons
                    {
                        Name = "panelstatbtn3User" + userNumber,
                        Text = "[" + c.C.KM.ToString("0,0") + " km] - [" + c.C.Money.ToString("0,0") + "€] - [" + c.C.Wealth.ToString("0,0") + "€]",
                        PublicText = c.PlayerName + "^8 - Distance: ^3" + c.C.KM.ToString("0,0") + " km^8, Money: ^3" + c.C.Money.ToString("0,0") + "€",
                        Style = ButtonStyles.ISB_C1 | ButtonStyles.ISB_LEFT | ButtonStyles.ISB_CLICK,
                        Height = 4,
                        Width = 42,
                        LeftRight = 76,
                        TopDown = (byte)(56 + 4 * userNumber),
                        ID = btnID,
                        Clickable = true
                    }; btnID--;

                    StatButtons["panelstatbtn4User" + userNumber] = new ButtonFactory.Buttons
                    {
                        Name = "panelstatbtn4User" + userNumber,
                        Text = "[" + c.C.TimeCop + "] - [" + c.C.TimeRobber + "] - [" + c.C.TimeTotal + "]",
                        PublicText = c.PlayerName + "^8 - Time (min) COP: ^3" + c.C.TimeCop + "^8, ROBBER: ^3" + c.C.TimeRobber + "^8, TOTAL: ^3" + c.C.TimeTotal,
                        Style = ButtonStyles.ISB_C1 | ButtonStyles.ISB_RIGHT | ButtonStyles.ISB_CLICK,
                        Height = 4,
                        Width = 33,
                        LeftRight = 118,
                        TopDown = (byte)(56 + 4 * userNumber),
                        ID = btnID,
                        Clickable = true
                    }; btnID--;

                    StatButtons["panelstatbtn5User" + userNumber] = new ButtonFactory.Buttons
                    {
                        Name = "panelstatbtn5User" + userNumber,
                        Text = "[" + c.C.XPCop + "] - [" + c.C.XPRobber + "] - [" + c.C.XPRP + "]",
                        PublicText = c.PlayerName + "^8 - XP COP: ^3" + c.C.XPCop + "^8, ROBBER: ^3" + c.C.XPRobber + "^8, RP: ^3" + c.C.XPRP,
                        Style = ButtonStyles.ISB_C1 | ButtonStyles.ISB_RIGHT | ButtonStyles.ISB_CLICK,
                        Height = 4,
                        Width = 23,
                        LeftRight = 151,
                        TopDown = (byte)(56 + 4 * userNumber),
                        ID = btnID,
                        Clickable = true
                    }; btnID--;

                    StatButtons["panelstatbtn0User" + userNumber] = new ButtonFactory.Buttons
                    {
                        Name = "panelstatbtn0User" + userNumber,
                        Text = "",
                        Style = styleee,
                        Height = 4,
                        Width = 146,
                        LeftRight = 26,
                        TopDown = (byte)(56 + 4 * userNumber),
                        ID = btnID,
                        Children = new List<ButtonFactory.Buttons> { StatButtons["panelstatbtn1User" + userNumber], StatButtons["panelstatbtn2User" + userNumber], StatButtons["panelstatbtn3User" + userNumber], StatButtons["panelstatbtn4User" + userNumber], StatButtons["panelstatbtn5User" + userNumber], }
                    }; btnID--;

                    #endregion

                    ButtonFactory.OpenButton(StatButtons["panelstatbtn0User" + userNumber]);
                    ButtonFactory.OpenButton(ButtonFactory.panelstatbtnProgressbar);
                    userNumber++;
                }
                styleee = (styleee == ButtonStyles.ISB_DARK) ? ButtonStyles.ISB_LIGHT : ButtonStyles.ISB_DARK;
            }
        }

        static public void DownloadStats(Clients user)
        {
            try
            {
                string username = user.Username;
                if (user.Username.Contains("æ".ToLower())) { username = user.Username.Replace("æ", "%E6"); }
                WebRequest request = WebRequest.Create("http://insim.city-driving.co.uk/license.php?username=" + username);
                WebResponse response = request.GetResponse();
                Stream data = response.GetResponseStream();
                string html = String.Empty;
                using (StreamReader srr = new StreamReader(data))
                {
                    html = srr.ReadToEnd();
                    System.IO.StreamWriter Sr = new System.IO.StreamWriter(@"Stats\\" + username + ".txt");
                    Sr.WriteLine(html);
                    Sr.Flush();
                    Sr.Close();
                }
                LoadStats(user);
            }
            catch (Exception e)
            {
                Program.ConsError(e.ToString());
            }
        }

        static public void LoadStats(Clients user)
        {
            using (StreamReader Reader = new StreamReader(@"Stats\\" + user.Username.Replace("æ", "%E6") + ".txt"))
            {
                String linje;
                bool readNextforJoindate = false;
                bool readNextforKM = false;
                bool readNextforMoney = false;
                bool readNextforWealth = false;
                bool readNextGamingtimeCop = false;
                bool readNextGamingtimeRobber = false;
                bool readNextGamingtimeTotal = false;
                bool readNextXPCop = false;
                bool readNextXPRobber = false;
                bool readNextXPRP = false;
                while ((linje = Reader.ReadLine()) != null)
                {
                    //Get joindate
                    if (readNextforJoindate)
                    {
                        user.C.Joindate = linje.Replace("<td>", "").Replace("</td>", "");
                        readNextforJoindate = false;
                    }
                    if (linje.Contains("<td>Joined:</td>"))
                    {
                        readNextforJoindate = true;
                    }
                    //Get country
                    if (linje.Contains("Flag of"))
                    {
                        string[] newres = linje.Remove(0, 82).Replace("</td>", "").Split(';', ' ');
                        if (newres.Length > 1)
                        {
                            user.C.Country = newres[0] + " " + newres[1];
                        }
                        else
                        {
                            user.C.Country = newres[0];
                        }
                    }
                    //Get KM
                    if (readNextforKM)
                    {
                        user.C.KM = Convert.ToInt32(linje.Replace("<td>", "").Replace("</td>", "").Replace(",", "").Replace("Km", ""));
                        readNextforKM = false;
                    }
                    if (linje.Contains("<td>Driven Km:</td>"))
                    {
                        readNextforKM = true;
                    }
                    //Get money
                    if (readNextforMoney)
                    {
                        user.C.Money = Convert.ToInt32(linje.Replace("<td>", "").Replace("&euro;&nbsp;</td>", "").Replace(",", ""));
                        readNextforMoney = false;
                    }
                    if (linje.Contains("<td>Money:</td>"))
                    {
                        readNextforMoney = true;
                    }
                    //Get wealth
                    if (readNextforWealth)
                    {
                        user.C.Wealth = Convert.ToInt32(linje.Remove(0, 41).Replace("&euro;</td>", "").Replace(",", ""));
                        readNextforWealth = false;
                    }
                    if (linje.Contains(">Wealth</td>"))
                    {
                        readNextforWealth = true;
                    }
                    //Get gaming time cop
                    if (readNextGamingtimeCop)
                    {
                        string linjee = linje.Remove(0, 20).Replace("</td>", "");
                        user.C.TimeCop = linjee;
                        readNextGamingtimeCop = false;
                    }
                    if (linje.Contains(">Gaming time cop</td>"))
                    {
                        readNextGamingtimeCop = true;
                    }
                    //Get gaming time robber
                    if (readNextGamingtimeRobber)
                    {
                        string linjee = linje.Remove(0, 20).Replace("</td>", "");
                        user.C.TimeRobber = linjee;
                        readNextGamingtimeRobber = false;
                    }
                    if (linje.Contains(">Gaming time robber</td>"))
                    {
                        readNextGamingtimeRobber = true;
                    }
                    //Get gaming time total
                    if (readNextGamingtimeTotal)
                    {
                        string linjee = linje.Remove(0, 20).Replace("</td>", "");
                        user.C.TimeTotal = linjee;
                        readNextGamingtimeTotal = false;
                    }
                    if (linje.Contains(">Gaming time total</td>"))
                    {
                        readNextGamingtimeTotal = true;
                    }
                    //Get XP RP
                    if (readNextXPRP)
                    {
                        user.C.XPRP = Convert.ToInt32(linje.Remove(0, 20).Replace("</td>", "").Replace(",", ""));
                        readNextXPRP = false;
                    }
                    if (linje.Contains(">Roleplaying points</td>"))
                    {
                        readNextXPRP = true;
                    }
                    //Get XP cop
                    if (readNextXPCop)
                    {
                        user.C.XPCop = Convert.ToInt32(linje.Remove(0, 20).Replace("</td>", "").Replace(",", ""));
                        readNextXPCop = false;
                    }
                    if (linje.Contains(">Cop XP</td>"))
                    {
                        readNextXPCop = true;
                    }
                    //Get XP robber
                    if (readNextXPRobber)
                    {
                        user.C.XPRobber = Convert.ToInt32(linje.Remove(0, 20).Replace("</td>", "").Replace(",", ""));
                        readNextXPRobber = false;
                    }
                    if (linje.Contains(">Robber XP</td>"))
                    {
                        readNextXPRobber = true;
                    }
                }
            }
        }
        #endregion

        #region Personal stats
        static public void PersonalStats(Clients user)
        {
            try
            {
                WebRequest request = WebRequest.Create("http://www.lfsworld.net/pubstat/get_stat2.php?version=1.5&idk=" + Settings.PUBSTATKEY + "&action=pst&racer=" + user.Username.Replace("æ", "%e6"));
                WebResponse response = request.GetResponse();
                Stream data = response.GetResponseStream();
                string html = String.Empty;
                using (StreamReader srr = new StreamReader(data))
                {
                    html = srr.ReadToEnd();
                }

                string[] info = html.Split('\n');
                for (int x = 0; x < info.Length; x++)
                {
                    Console.WriteLine(info[x].ToString());
                    if (x.Equals(0))
                    {
                        user.C.LFSdst = Convert.ToInt64(info[x]);
                    }
                    else if (x.Equals(1))
                    {
                        user.C.LFSfuel = Convert.ToInt64(info[x]);
                    }
                    else if (x.Equals(2))
                    {
                        user.C.LFSlaps = Convert.ToInt64(info[x]);
                    }
                    else if (x.Equals(3))
                    {
                        user.C.LFShosts = Convert.ToInt64(info[x]);
                    }
                    else if (x.Equals(4))
                    {
                        user.C.LFSwins = Convert.ToInt64(info[x]);
                    }
                    else if (x.Equals(7))
                    {
                        user.C.LFSfinished = Convert.ToInt64(info[x]);
                    }
                }
                LoadStats(user);

                Clients me = Program.MySelf;
                //Buttons.OpenButton(me, "persInfoBg1", "", ButtonStyles.ISB_DARK, 120, 80, 60, 40, 99);
                //Buttons.OpenButton(me, "persInfoBg2", "", ButtonStyles.ISB_DARK, 120, 80, 60, 40, 99);
                //Buttons.OpenButton(me, "persInfoHead1", "^J‚r‚”‚‚”‚‰‚“‚”‚‰‚ƒ‚“ ‚†‚‚’ ^L'" + user.Username + "'", ButtonStyles.ISB_C4, 7, 80, 60, 40, 0);
                //Buttons.OpenButton(me, "persInfoStrek1", "^H¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë", ButtonStyles.ISB_C4, 2, 80, 60, 47, 0);
                //Buttons.OpenButton(me, "persInfoStrek2", "^H¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë", ButtonStyles.ISB_C4 | ButtonStyles.ISB_LEFT, 2, 80, 61, 56, 0);
                //Buttons.OpenButton(me, "persInfoStrek3", "^H¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë¡Ë", ButtonStyles.ISB_C4 | ButtonStyles.ISB_LEFT, 2, 80, 61, 126, 0);
                //Buttons.OpenButton(me, "persInfoClose", "^1×", ButtonStyles.ISB_LIGHT | ButtonStyles.ISB_CLICK, 3, 3, 136, 41, 0);
                //Buttons.OpenButton(me, "persInfoHead2", "^J[‚s‚b] ‚b‚‰‚”‚™ ‚c‚’‚‰‚–‚‰‚Ž‚‡", ButtonStyles.ISB_CLICK | ButtonStyles.ISB_LEFT | ButtonStyles.ISB_C4, 6, 40, 61, 50, 0);
                //Buttons.OpenButton(me, "persInfoHead3", "^J‚k‚‰‚–‚… ‚e‚‚’ ‚r‚‚…‚…‚„", ButtonStyles.ISB_CLICK | ButtonStyles.ISB_LEFT | ButtonStyles.ISB_C4, 6, 40, 61, 120, 0);

                //Buttons.OpenButton(me, "persInfoJoined", "Joined date: " + user.C.Joindate, ButtonStyles.ISB_C1 | ButtonStyles.ISB_LEFT, 5, 45, 61, 57, 0);
                //Buttons.OpenButton(me, "persInfoFrom", "Comes from: " + user.C.Country, ButtonStyles.ISB_C1 | ButtonStyles.ISB_LEFT, 5, 30, 61, 62, 0);
                //Buttons.OpenButton(me, "persInfoTimeonline", "Time online: " + user.C.TimeTotal, ButtonStyles.ISB_C1 | ButtonStyles.ISB_LEFT, 5, 30, 61, 67, 0);
                //Buttons.OpenButton(me, "persInfoTimecop", "Time cop: " + user.C.TimeCop, ButtonStyles.ISB_C1 | ButtonStyles.ISB_LEFT, 5, 30, 61, 72, 0);
                //Buttons.OpenButton(me, "persInfoTimerobber", "Time robber: " + user.C.TimeRobber, ButtonStyles.ISB_C1 | ButtonStyles.ISB_LEFT, 5, 30, 61, 77, 0);
                //Buttons.OpenButton(me, "persInfoDistance", "Driven distance: " + user.C.KM + "km", ButtonStyles.ISB_C1 | ButtonStyles.ISB_LEFT, 5, 30, 61, 82, 0);

                //Buttons.OpenButton(me, "persInfoMoney", "Money: " + user.C.Money + "€", ButtonStyles.ISB_C1 | ButtonStyles.ISB_LEFT, 5, 30, 61, 87, 0);
                //Buttons.OpenButton(me, "persInfoWelath", "Wealth: " + user.C.Wealth + "€", ButtonStyles.ISB_C1 | ButtonStyles.ISB_LEFT, 5, 30, 61, 92, 0);
                //Buttons.OpenButton(me, "persInfoXPcop", "XP cop: " + user.C.XPCop, ButtonStyles.ISB_C1 | ButtonStyles.ISB_LEFT, 5, 30, 61, 97, 0);
                //Buttons.OpenButton(me, "persInfoXProbber", "XP robber: " + user.C.XPCop, ButtonStyles.ISB_C1 | ButtonStyles.ISB_LEFT, 5, 30, 61, 102, 0);
                //Buttons.OpenButton(me, "persInfoXPrp", "XP role play: " + user.C.XPCop, ButtonStyles.ISB_C1 | ButtonStyles.ISB_LEFT, 5, 30, 61, 107, 0);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        #endregion

        #region Buddysearch
        static public string SearchBuddy(string username)
        {
            string sendBack = "";

            WebRequest request = WebRequest.Create("http://www.lfsworld.net/pubstat/get_stat2.php?version=1.5&idk=" + Settings.PUBSTATKEY + "&action=pst&racer=" + username);
            WebResponse response = request.GetResponse();
            Stream data = response.GetResponseStream();
            string html = String.Empty;
            using (StreamReader srr = new StreamReader(data))
            {
                html = srr.ReadToEnd();
            }
            if (html.StartsWith("Invalid Ident-Key"))
            {
                return "Invalid Ident-Key";
            }

            string[] info = html.Split('\n');
            for (int x = 0; x < info.Length; x++)
            {
                if (x.Equals(13))
                {
                    if (info[x] != "0")
                    {
                        sendBack += "Your buddy ^3," + username + ", ^8is online @ ";
                    }
                }
                else if (x.Equals(14) && sendBack.Length > 1)
                {
                    sendBack += info[x];
                }
            }
            return sendBack;
        }
        #endregion
    }
}