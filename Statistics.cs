using InSimDotNet.Packets;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;

namespace Mary
{
    class Statistics
    {
        #region Load table
        static public IDictionary<string, ButtonFactory.Buttons> StatButtons = new Dictionary<string, ButtonFactory.Buttons>();
        static public List<Clients> TempList = new List<Clients>();
        static public void LoadTable(bool partOne)
        {
            if (partOne) GetOnlineUsersData();
            int onlineUsers = TempList.Count,
                bgsize = (onlineUsers > 20) ? 99 : 18 + (onlineUsers * 4),
                userNumber = 0;
            int upTo;
            if (partOne)
            {
                upTo = (onlineUsers > 20) ? 20 : onlineUsers;
                ButtonFactory.panelstatbtn1.Height = ButtonFactory.panelstatbtn2.Height = (byte)bgsize;
                ButtonFactory.OpenButton(ButtonFactory.panelstatbtn9);
                if (onlineUsers > 20) ButtonFactory.OpenButton(ButtonFactory.panelstatbtn10);
            }
            else
            {
                upTo = onlineUsers;
                ButtonFactory.RemoveButton(ButtonFactory.panelstatbtn10);
                ButtonFactory.RemoveButton(ButtonFactory.panelstatbtnProgressbar);
            }
            ButtonStyles styleee = ButtonStyles.ISB_DARK;
            int x;
            for (x = (partOne) ? 0 : 20; x < upTo; x++)
            {
                Clients c = TempList[x];

                if (!c.Username.Equals("") && TempList.Contains(c)/*File.Exists(@"Stats\\" + c.Username + ".txt")*/)
                {
                    LoadStats(c.Username);
                    ButtonFactory.panelstatbtnProgressbar.Text = (Progressbar.Progress(partOne ? userNumber + 1 : userNumber + 21, upTo, 150));

                    string[] arr = new string[] { "panelstatbtn0User" + userNumber, "panelstatbtn1User" + userNumber, "panelstatbtn2User" + userNumber, "panelstatbtn3User" + userNumber, "panelstatbtn4User" + userNumber, "panelstatbtn5User" + userNumber };
                    foreach (string name in arr)
                    {
                        StatButtons[name] = new ButtonFactory.Buttons();
                    }
                    c.C.XPRobber = "";//must reset to get correct info on this, because there is 3 defferent XP fields now...

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
                        ID = ButtonFactory.NewID()
                    };

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
                        ID = ButtonFactory.NewID()
                    };

                    StatButtons["panelstatbtn3User" + userNumber] = new ButtonFactory.Buttons
                    {
                        Name = "panelstatbtn3User" + userNumber,
                        Text = "[" + c.C.KM + " km] - [" + c.C.Bank + "€]",
                        PublicText = c.PlayerName + "^8 - Distance: ^3" + c.C.KM + " km^8, Money: ^3" + c.C.Bank + "€",
                        Style = ButtonStyles.ISB_C1 | ButtonStyles.ISB_LEFT | ButtonStyles.ISB_CLICK,
                        Height = 4,
                        Width = 42,
                        LeftRight = 76,
                        TopDown = (byte)(56 + 4 * userNumber),
                        ID = ButtonFactory.NewID()
                    };

                    StatButtons["panelstatbtn4User" + userNumber] = new ButtonFactory.Buttons
                    {
                        Name = "panelstatbtn4User" + userNumber,
                        Text = "[" + c.C.TimeCop + "hours] - [" + c.C.TimeRobber + "hours] - [" + c.C.TimeTotal + "hours]",
                        PublicText = c.PlayerName + "^8 - Activity COP: ^3" + c.C.TimeCop + "hours^8, ROBBER: ^3" + c.C.TimeRobber + "hours^8, TOTAL: ^3" + c.C.TimeTotal + "hours",
                        Style = ButtonStyles.ISB_C1 | ButtonStyles.ISB_RIGHT | ButtonStyles.ISB_CLICK,
                        Height = 4,
                        Width = 33,
                        LeftRight = 118,
                        TopDown = (byte)(56 + 4 * userNumber),
                        ID = ButtonFactory.NewID()
                    };

                    StatButtons["panelstatbtn5User" + userNumber] = new ButtonFactory.Buttons
                    {
                        Name = "panelstatbtn5User" + userNumber,
                        Text = "[" + c.P.Location + "^8] - [" + c.P.afkStart.Elapsed.ToString(@"hh\:mm\:ss") + "]",
                        PublicText = c.PlayerName + " ^8Location: ^3" + c.P.Location + " ^8," + " Standstill time: ^3" + c.P.afkStart.Elapsed.ToString(@"hh\:mm\:ss"),
                        Style = ButtonStyles.ISB_C1 | ButtonStyles.ISB_RIGHT | ButtonStyles.ISB_CLICK,
                        Height = 4,
                        Width = 22,
                        LeftRight = 151,
                        TopDown = (byte)(56 + 4 * userNumber),
                        ID = ButtonFactory.NewID()
                    };

                    StatButtons["panelstatbtn0User" + userNumber] = new ButtonFactory.Buttons
                    {
                        Name = "panelstatbtn0User" + userNumber,
                        Text = "",
                        Style = styleee,
                        Height = 4,
                        Width = 148,
                        LeftRight = 26,
                        TopDown = (byte)(56 + 4 * userNumber),
                        ID = ButtonFactory.NewID(),
                        Children = new List<ButtonFactory.Buttons> { StatButtons["panelstatbtn1User" + userNumber], StatButtons["panelstatbtn2User" + userNumber], StatButtons["panelstatbtn3User" + userNumber], StatButtons["panelstatbtn4User" + userNumber], StatButtons["panelstatbtn5User" + userNumber], }
                    };

                    #endregion

                    ButtonFactory.OpenButton(StatButtons["panelstatbtn0User" + userNumber]);
                    System.Threading.Thread.Sleep(50);
                    ButtonFactory.OpenButton(ButtonFactory.panelstatbtnProgressbar);
                    userNumber++;
                }
                styleee = (styleee == ButtonStyles.ISB_DARK) ? ButtonStyles.ISB_LIGHT : ButtonStyles.ISB_DARK;
            }
        }
        #endregion

        #region CheckUsersOnlineTC
        static public void GetOnlineUsersData()
        {
            try
            {
                foreach (Clients user in Program.AllConnections)
                {
                    DownloadStatsusername(user.Username);
                }
            }
            catch (Exception e) { Program.ConsError(e.ToString()); }
        }
        #endregion

        #region DownloadStatFiles
        static public void DownloadStats(string ID)
        {
            try
            {
                WebRequest request = WebRequest.Create("http://world.city-driving.co.uk/?page=profile&id_user=" + ID);
                WebResponse response = request.GetResponse();
                Stream data = response.GetResponseStream();
                string html = String.Empty;
                string test2 = "";
                using (StreamReader srr = new StreamReader(data))
                {
                    html = srr.ReadToEnd().Remove(0, 11526);
                    var test = html.Split(new string[] { "<td>LFS username</td>" }, StringSplitOptions.None);
                    test2 = test[1].Remove(0, 7).Split('>')[1].Replace("</td", "");
                    StreamWriter Sr = new StreamWriter(@"Stats\\" + test2 + ".txt");
                    Sr.WriteLine(html);
                    Sr.Flush();
                    Sr.Close();
                }
            }
            catch (Exception e) { Program.ConsError(e.ToString()); }
        }

        static public void DownloadStatsusername(string username)
        {
            try
            {
                WebRequest request = WebRequest.Create("http://world.city-driving.co.uk/?page=profile&username=" + username);
                WebResponse response = request.GetResponse();
                Stream data = response.GetResponseStream();
                string html = String.Empty;
                using (StreamReader srr = new StreamReader(data))
                {
                    html = srr.ReadToEnd().Remove(0, 11526);
                    StreamWriter Sr = new StreamWriter(@"Stats\\" + username + ".txt");
                    Sr.WriteLine(html);
                    Sr.Flush();
                    Sr.Close();
                }
            }
            catch (Exception e) { Program.ConsError(e.ToString()); }
        }
        #endregion

        #region Country
        static public string ReturnCountryName(string code)
        {
            RegionInfo info = new RegionInfo(code);
            return info.EnglishName;
        }
        #endregion

        #region Parse stats from files
        static public void LoadStats(string username)
        {
            Clients user = new Clients();
            foreach (Clients a in Program.AllConnections)
                if (a.Username == username)
                {
                    user = a;
                }
            if (File.Exists(@"Stats\\" + username + ".txt"))
            {
                if (user.onTrack && Program.AllConnections.Contains(user))
                {
                    GetRoadName(user);
                }

                using (StreamReader Reader = new StreamReader(@"Stats\\" + username + ".txt"))
                {
                    string linje;
                    bool readNextforCountry = false, readNextforJoindate = false, readNextforKM = false, readNextforBank = false, readNextGamingtimeCop = false, readNextGamingtimeRobber = false, readNextGamingtimeTotal = false, readNextforKM2 = true,
                         readNextXPCop = false, readNextXPRobber = false;
                    user.C.Wallet = "unknown ";
                    while ((linje = Reader.ReadLine()) != null)
                    {
                        linje = linje.Trim();
                        //Get joindate
                        if (readNextforJoindate)
                        {
                            user.C.Joindate = linje.Replace("<td>", "").Replace("</td>", "");
                            readNextforJoindate = false;
                        }
                        if (linje.Contains("<td>Date joined</td>"))
                        {
                            readNextforJoindate = true;
                        }
                        //Get country
                        if (readNextforCountry)
                        {
                            user.C.Country = ReturnCountryName(linje.Replace("<td>", "").Substring(0, 2));
                            readNextforCountry = false;
                        }
                        if (linje.Contains("<td>Country</td>"))
                        {
                            readNextforCountry = true;
                        }
                        //Get KM
                        if (readNextforKM)
                        {
                            user.C.KM = linje.Replace("<td>", "").Replace("</td>", "").Replace(" km", "");
                            readNextforKM = false;
                        }
                        if (linje.Contains("<td>Driven distance</td>") && readNextforKM2)//because 2 fucking lines with this string...
                        {
                            readNextforKM = true;
                            if (readNextforKM2)
                            {
                                readNextforKM2 = false;
                            }
                        }
                        //Get bank
                        if (readNextforBank)
                        {
                            user.C.Bank = linje.Replace("<td>", "").Replace("€</td>", "");
                            readNextforBank = false;
                        }
                        if (linje.Contains("<td>Total money</td>"))
                        {
                            readNextforBank = true;
                        }
                        ////Get wallet
                        //if (readNextforWallet)
                        //{
                        //    user.C.Wallet = linje.Replace("<td>", "").Replace("€</td>", "");
                        //    readNextforWallet = false;
                        //}
                        //if (linje.Contains("<td>Cash</td>"))
                        //{
                        //    readNextforWallet = true;
                        //}
                        //Get gaming time cop
                        if (readNextGamingtimeCop)
                        {
                            string linjee = linje.Replace("<td>", "").Replace("hours</td>", "").Replace(",", "").Replace("</td>", "");
                            if (linjee.Contains("mins"))
                                user.C.TimeCop = "0 ";
                            else user.C.TimeCop = linjee;
                            readNextGamingtimeCop = false;
                        }
                        if (linje.Contains("<td>Playing time cop</td>"))
                        {
                            readNextGamingtimeCop = true;
                        }
                        //Get gaming time robber
                        if (readNextGamingtimeRobber)
                        {
                            string linjee = linje.Replace("<td>", "").Replace("hours</td>", "").Replace(",", "").Replace("</td>", "");
                            if (linjee.Contains("mins"))
                                user.C.TimeRobber = "0 ";
                            else user.C.TimeRobber = linjee;
                            readNextGamingtimeRobber = false;
                        }
                        if (linje.Contains("<td>Playing time civilian</td>"))
                        {
                            readNextGamingtimeRobber = true;
                        }
                        //Get gaming time total
                        if (readNextGamingtimeTotal)
                        {
                            string linjee = linje.Replace("<td>", "").Replace("hours</td>", "").Replace(",", "").Replace("</td>", "");
                            if (linjee.Contains("mins"))
                                user.C.TimeTotal = "0 ";
                            else user.C.TimeTotal = linjee;
                            readNextGamingtimeTotal = false;
                        }
                        if (linje.Contains("<td>Playing time total</td>"))
                        {
                            readNextGamingtimeTotal = true;
                        }
                        ////Get XP RP
                        //if (readNextXPRP)
                        //{
                        //    user.C.XPRP = Convert.ToInt32(linje.Remove(0, 20).Replace("</td>", "").Replace(",", ""));
                        //    readNextXPRP = false;
                        //}
                        //if (linje.Contains(">Roleplaying points</td>"))
                        //{
                        //    readNextXPRP = true;
                        //}
                        //Get XP cop
                        if (readNextXPCop)
                        {
                            user.C.XPCop = linje.Replace("<div>", "").Replace("</div>", "").Replace(" XP", "");
                            readNextXPCop = false;
                        }
                        if (linje.Contains("<div class=\"l\"><sup>"))
                        {
                            readNextXPCop = true;
                        }
                        //Get XP robber
                        if (readNextXPRobber)
                        {
                            user.C.XPRobber = linje.Replace("<td>", "").Replace("</td>", "");
                            readNextXPRobber = false;
                        }
                        if (linje.Contains("<td>XP</td>") && user.C.XPRobber == "")
                        {
                            readNextXPRobber = true;
                        }
                    }
                }
            }
        }
        #endregion

        #region Check TC site for roadname on user
        static public void GetRoadName(Clients user)
        {
            string html = String.Empty;
            try
            {
                if (Program.ServerName.StartsWith("^1[TC] CityDriving ") && Program.AllConnections.Contains(user))
                {
                    WebRequest request = WebRequest.Create("https://api.tc-gaming.co.uk/json/" + user.Username.ToLower() + ".json");
                    WebResponse response = request.GetResponse();
                    Stream data = response.GetResponseStream();

                    using (Stream stream = response.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(stream);
                        html = reader.ReadToEnd();
                    }
                    string[] htmlSplit1 = html.Split(new[] { "," }, StringSplitOptions.None);
                    if (htmlSplit1.Length >= 13)
                    {
                        string loc = System.Text.RegularExpressions.Regex.Unescape(htmlSplit1[10].Remove(0, 11).Replace('"', ' ').Remove(0, 1));
                        user.P.Location = loc.Trim();
                        user.P.SpeedlimitKph = Convert.ToInt16(htmlSplit1[12].Remove(0, 16).Replace('"', ' '));
                        user.P.SpeedlimitMph = Convert.ToInt16(htmlSplit1[13].Remove(0, 16).Replace('"', ' '));
                    }
                }
            }
            catch (Exception e) { Program.ConsError(e.ToString() + "\nWhen checking user: " + user.Username + "\n" + html); }
        }
        #endregion

        #region Personal stats
        #endregion

        #region Buddysearch
        static public void SearchBuddy(string username)
        {
            try
            {
                WebRequest request = WebRequest.Create("http://www.lfsworld.net/pubstat/get_stat2.php?version=1.5&idk=" + Settings.PUBSTATKEY + "&action=pst&racer=" + username);
                WebResponse response = request.GetResponse();
                Stream data = response.GetResponseStream();
                string html = String.Empty;
                using (StreamReader srr = new StreamReader(data))
                {
                    html = srr.ReadToEnd();
                }
                if (html.StartsWith("Invalid Ident-Key") || html.StartsWith("not authed"))
                {
                    Program.ConsError("You have an invalid pubstat key / bad IP.");
                    DUM.SlowRutineTime.Dispose();
                    return;
                }
                if (html.StartsWith("pst: no valid username"))
                {
                    bool online = false;
                    foreach (Clients all in Program.AllConnections)
                    {
                        if (all.Username.ToLower() == username)
                        {
                            online = true;
                            Messages.SendLocalMsg("Your buddy, ^3" + username + "^8, is online @ " + Program.ServerName);
                            DUM.toDelete.Add(username);
                            return;
                        }
                    }
                    if (!online)
                    {
                        //Messages.SendLocalMsg("Your buddy, ^3" + username + "^8's LFSW stats is hidden.");
                        DUM.toDelete.Add(username);
                        return;
                    }
                }
                string[] info = html.Split('\n');
                for (int x = 0; x < info.Length; x++)
                {
                    if (info[13] != "0" && x.Equals(14))
                    {
                        Messages.SendLocalMsg("Your buddy, ^3" + username + ",^8 is online @ " + info[x]);
                        DUM.toDelete.Add(username);
                        return;
                    }
                }
            }
            catch (Exception e) { Program.ConsError(e.ToString()); }
        }
        #endregion
    }
}